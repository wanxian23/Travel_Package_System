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
    public partial class TicketSeat : Form
    {
        public TicketSeat()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void backBt_Click(object sender, EventArgs e)
        {
            TicketForm ticketForm = new TicketForm();
            this.Hide();
            ticketForm.ShowDialog();
        }

        private void nextBt_Click(object sender, EventArgs e)
        {
            TicketCheckout ticketCheckout = new TicketCheckout();
            this.Hide();
            ticketCheckout.ShowDialog();
        }
    }
}
