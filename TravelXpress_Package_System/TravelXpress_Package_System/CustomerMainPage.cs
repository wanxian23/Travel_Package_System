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
    public partial class CustomerMainPage : Form
    {
        public CustomerMainPage()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TourPackageForm tourPackageForm = new TourPackageForm();
            tourPackageForm.ShowDialog();
        }

        private void buttonTicket_Click(object sender, EventArgs e)
        {
            TicketBookingForm ticketBookingForm = new TicketBookingForm();
            this.Hide();
            ticketBookingForm.ShowDialog();
        }
    }
}
