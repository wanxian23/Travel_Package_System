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
    public partial class TicketForm : Form
    {
        TemporaryDataStore previousDateStore;
        public TicketForm(TemporaryDataStore previousDateStore)
        {
            InitializeComponent();

            this.previousDateStore = previousDateStore;
        }

        private void backBt_Click(object sender, EventArgs e)
        {
            TicketBookingForm ticketBookingForm = new TicketBookingForm();
            this.Hide();
            ticketBookingForm.ShowDialog();
        }
        private void chooseBt1_Click(object sender, EventArgs e)
        {
            TicketSeat ticketSeat = new TicketSeat(previousDateStore);
            this.Hide();
            ticketSeat.ShowDialog();
        }

        private void chooseBt2_Click(object sender, EventArgs e)
        {
            TicketSeat ticketSeat = new TicketSeat(previousDateStore);
            this.Hide();
            ticketSeat.ShowDialog();
        }

        private void TicketForm_Load(object sender, EventArgs e)
        {

            busFromTb.Text = previousDateStore.busFrom;
            busToTb.Text = previousDateStore.busTo;
            departTb.Text = previousDateStore.departDate.ToString();
            returnTb.Text = previousDateStore.reDate.ToString();

            if (string.IsNullOrWhiteSpace(busToTb.Text))
            {
                busToTb.Visible = false;
                busToLb.Visible = false;
                returnTb.Visible = false;
                returnDateLb.Visible = false;
                busFromLb.Location = new Point(354, 18);
                busFromTb.Location = new Point(462, 22);
                departDateLb.Location = new Point(419, 104);
                departTb.Location = new Point(598, 108);
            } 

        }

    }
}
