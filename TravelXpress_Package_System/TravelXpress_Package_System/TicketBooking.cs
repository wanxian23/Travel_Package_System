using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelXpress_Package_System
{
    public partial class TicketBookingForm : Form
    {
        public TicketBookingForm()
        {
            InitializeComponent();
        }

        private void backBt_Click(object sender, EventArgs e)
        {
            CustomerMainPage customerMainPage = new CustomerMainPage();
            this.Hide();
            customerMainPage.ShowDialog();
        }

        private void searchBt_Click(object sender, EventArgs e)
        {
            TicketForm ticketForm = new TicketForm();
            this.Hide();
            ticketForm.ShowDialog();
        }
    }
}
