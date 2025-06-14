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
        public TicketForm()
        {
            InitializeComponent();

            chooseBt1.MouseEnter += chooseBt1_MouseEnter;
            chooseBt1.MouseLeave += chooseBt1_MouseLeave;
            chooseBt2.MouseEnter += chooseBt2_MouseEnter;
            chooseBt2.MouseLeave += chooseBt2_MouseLeave;
        }

        private void backBt_Click(object sender, EventArgs e)
        {
            TicketBookingForm ticketBookingForm = new TicketBookingForm();
            this.Hide();
            ticketBookingForm.ShowDialog();
        }
        private void chooseBt1_MouseEnter(object sender, EventArgs e)
        {
            chooseBt1.BackColor = Color.Gray;
        }
        private void chooseBt1_MouseLeave(object sender, EventArgs e)
        {
            chooseBt1.BackColor = Color.Black;
        }
        private void chooseBt2_MouseEnter(object sender, EventArgs e)
        {
            chooseBt2.BackColor = Color.Gray;
        }
        private void chooseBt2_MouseLeave(object sender, EventArgs e)
        {
            chooseBt2.BackColor = Color.Black;
        }

        private void chooseBt1_Click(object sender, EventArgs e)
        {
            TicketSeat ticketSeat = new TicketSeat();
            this.Hide();
            ticketSeat.ShowDialog();
        }

        private void chooseBt2_Click(object sender, EventArgs e)
        {
            TicketSeat ticketSeat = new TicketSeat();
            this.Hide();
            ticketSeat.ShowDialog();
        }
    }
}
