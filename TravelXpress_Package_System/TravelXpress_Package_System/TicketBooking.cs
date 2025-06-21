using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TravelXpress_Package_System
{
    public partial class TicketBookingForm : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;

        SeatDetail seatDetail = new SeatDetail();
        public TicketBookingForm(SeatDetail seatDetail)
        {
            InitializeComponent();

            string DBConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\My Drive\\School_Stuff\\UTeM_Doc\\Sem\\Year2\\Sem2\\Event-Driven Programming\\Lab\\Project\\Travel_Package_System\\TravelXpress_Package_System\\TravelXpress_Package_System\\TravelXpressDBMS.mdf";
            connection = new SqlConnection(DBConnection);
            this.seatDetail = seatDetail;
        }

        public string busFrom { get { return busFromTb.Text; } set { busFromTb.Text = value; } }
        public string busTo { get { return busToTb.Text; } set { busToTb.Text = value; } }
        public DateTime busDepartDate { get { return departureDate.Value.Date; } set { departureDate.Value = value; } }
        public DateTime busReturnDate { get { return returnDate.Value.Date; } set { returnDate.Value = value; } }

        private void backBt_Click(object sender, EventArgs e)
        {
            CustomerMainPage customerMainPage = new CustomerMainPage();
            this.Hide();
            customerMainPage.ShowDialog();
        }

        private void searchBt_Click(object sender, EventArgs e)
        {
            TemporaryBusDetailsStore dataStore = new TemporaryBusDetailsStore();

            dataStore.departDate = departureDate.Value.Date;
            dataStore.reDate = returnDate.Value.Date;

            dataStore.busFrom = busFromTb.Text;
            dataStore.busTo = busToTb.Text;

            dataStore.departDate = departureDate.Value.Date;
            dataStore.reDate = returnDate.Value.Date;

            dataStore.busFrom = busFromTb.Text;
            dataStore.busTo = busToTb.Text;


            if (string.IsNullOrWhiteSpace(busFromTb.Text))
            {
                MessageBox.Show("'Bus From' Textbox Cannot be Null!", "NULL WARNING");
                return;
            }

            if (dataStore.departDate <= DateTime.Now.Date)
            {
                MessageBox.Show("Departure Date Cannot Smaller Than or Same As Today!", "DATE INPUT ERROR");
                return;
            }

            if (returnDate.Enabled == true)
            {
                if (dataStore.reDate <= dataStore.departDate)
                {
                    MessageBox.Show("Return Date Cannot Smaller Than or Same As Departure Date!", "DATE INPUT ERROR");
                    return;
                }
                else if (dataStore.reDate < DateTime.Now.Date)
                {
                    MessageBox.Show("Return Date Cannot Smaller Than or Same As Today!", "DATE INPUT ERROR");
                    return;
                }
            }

            TicketForm ticketForm = new TicketForm(dataStore, seatDetail);
            this.Hide();
            ticketForm.ShowDialog();
        }

        private void TicketBookingForm_Load(object sender, EventArgs e)
        {
            this.busDetailsTableAdapter.Fill(this.travelXpressDataSet.BusDetails);
        }

        private void clearBt_Click(object sender, EventArgs e)
        {
            busFromTb.Clear();
            busToTb.Clear();
            departureDate.Value = DateTime.Now;
            returnDate.Value = DateTime.Now;
        }

        private void busDetailsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void busToTb_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(busToTb.Text))
            {
                returnDate.Enabled = false;
            } 
            else
            {
                returnDate.Enabled = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

}
