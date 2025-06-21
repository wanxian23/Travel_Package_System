using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelXpress_Package_System
{
    public partial class TicketCheckout : Form
    {
        TemporaryBusDetailsStore previousDateStore;
        SeatDetail previousSeatDetails;
        UserDetails userDetails = new UserDetails();
        public TicketCheckout(TemporaryBusDetailsStore previousDateStore, SeatDetail previousSeatDetails)
        {
            InitializeComponent();
            this.previousSeatDetails = previousSeatDetails;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void backBt_Click(object sender, EventArgs e)
        {
            TicketSeat ticketSeat = new TicketSeat(previousDateStore, previousSeatDetails);
            this.Hide();
            ticketSeat.ShowDialog();
        }

        private void TicketCheckout_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void proceedBt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTb.Text)) 
            {
                MessageBox.Show("Name Cannot Be Null!", "NULL ERROR!");
                return;
            }

            if (string.IsNullOrWhiteSpace(contactTb.Text))
            {
                MessageBox.Show("Contact Number Cannot Be Null!", "NULL ERROR!");
                return;
            }

            if (string.IsNullOrWhiteSpace(icTb.Text))
            {
                MessageBox.Show("IC Number Cannot Be Null!", "NULL ERROR!");
                return;
            }

            if (!maleRb.Checked && !femaleRb.Checked)
            {
                MessageBox.Show("Gender Cannot Be Null!", "NULL ERROR!");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(emailTb.Text))
            {
                MessageBox.Show("Email Cannot Be Null!", "NULL ERROR!");
                return;
            }

            userDetails.name = nameTb.Text;
            userDetails.contact = contactTb.Text;
            userDetails.ic = icTb.Text;
            userDetails.email = emailTb.Text;

            if (maleRb.Checked)
            {
                userDetails.gender = maleRb.Text;
            } else
            {
                userDetails.gender = femaleRb.Text;
            }

            TicketPayment ticketPayment = new TicketPayment(userDetails);
            this.Hide();
            ticketPayment.ShowDialog();

        }

        public class UserDetails : Form
        {
            public string name {  get; set; }
            public string contact { get; set; }
            public string ic { get; set; }
            public string gender { get; set; }
            public string email { get; set; }
        }
    }
}
