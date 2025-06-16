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
        public TicketBookingForm()
        {
            InitializeComponent();

            string DBConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\My Drive\\School_Stuff\\UTeM_Doc\\Sem\\Year2\\Sem2\\Event-Driven Programming\\Lab\\Project\\Travel_Package_System\\TravelXpress_Package_System\\TravelXpress_Package_System\\TravelXpressDBMS.mdf";
            connection = new SqlConnection(DBConnection);
        }
        private void backBt_Click(object sender, EventArgs e)
        {
            CustomerMainPage customerMainPage = new CustomerMainPage();
            this.Hide();
            customerMainPage.ShowDialog();
        }

        private void searchBt_Click(object sender, EventArgs e)
        {
            TemporaryDataStore dataStore = new TemporaryDataStore();

            dataStore.departDate = departureDate.Value.Date;
            dataStore.reDate = returnDate.Value.Date;

            dataStore.busFrom = busFromTb.Text;
            dataStore.busTo = busToTb.Text;

            if (string.IsNullOrWhiteSpace(busFromTb.Text))
            {
                MessageBox.Show("'Bus From' Textbox Cannot be Null!", "NULL WARNING");
                return;
            }
            
            if (returnDate.Enabled == true)
            {
                if (dataStore.reDate <= dataStore.departDate)
                {
                    MessageBox.Show("Return Date Cannot Smaller Than Departure Date!", "DATE INPUT ERROR");
                    return;
                }
            }

            TicketForm ticketForm = new TicketForm(dataStore);
            this.Hide();
            ticketForm.ShowDialog();
        }

        private void TicketBookingForm_Load(object sender, EventArgs e)
        {
            this.busDetailsTableAdapter.Fill(this.travelXpressDataSet.BusDetails);

            // Delay hiding until UI is ready
            this.BeginInvoke((MethodInvoker)delegate {
                HideUnwantedColumns();
            });
        }


        private void HideUnwantedColumns()
        {
            // Now it's safe to hide columns
            if (busDetailsDataGridView.Columns.Contains("BusID"))
                busDetailsDataGridView.Columns["BusID"].Visible = false;

            if (busDetailsDataGridView.Columns.Contains("BusPlateNum"))
                busDetailsDataGridView.Columns["BusPlateNum"].Visible = false;

            if (busDetailsDataGridView.Columns.Contains("BusUpload"))
                busDetailsDataGridView.Columns["BusUpload"].Visible = false;

            if (busDetailsDataGridView.Columns.Contains("SeatUpload"))
                busDetailsDataGridView.Columns["SeatUpload"].Visible = false;
        }

        private void busDetailsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.busDetailsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.travelXpressDataSet);

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

    public class TemporaryDataStore : Form
    {
        public string busFrom;
        public string busTo;
        public DateTime departDate;
        public DateTime reDate;
    }
}
