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
    public partial class TicketCheckout : Form
    {
        TemporaryDataStore previousDateStore;
        public TicketCheckout(TemporaryDataStore previousDateStore)
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void backBt_Click(object sender, EventArgs e)
        {
            TicketSeat ticketSeat = new TicketSeat(previousDateStore);
            this.Hide();
            ticketSeat.ShowDialog();
        }
    }
}
