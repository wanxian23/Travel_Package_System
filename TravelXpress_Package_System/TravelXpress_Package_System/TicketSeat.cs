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
        TemporaryDataStore previousDateStore;
        public TicketSeat(TemporaryDataStore previousDateStore)
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void backBt_Click(object sender, EventArgs e)
        {
            TicketForm ticketForm = new TicketForm(previousDateStore);
            this.Hide();
            ticketForm.ShowDialog();
        }

        private void nextBt_Click(object sender, EventArgs e)
        {
            TicketCheckout ticketCheckout = new TicketCheckout(previousDateStore);
            this.Hide();
            ticketCheckout.ShowDialog();
        }
    }
}
