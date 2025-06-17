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
        TemporaryBusDetailsStore previousDateStore;
        SeatDetail seatDetail = new SeatDetail();
        public TicketSeat(TemporaryBusDetailsStore previousDateStore, SeatDetail seatDetails)
        {
            InitializeComponent();

            this.previousDateStore = previousDateStore;

            numSeatTb.Value = seatDetails.NumberOfSeats;
            seatsTb.Text = seatDetails.SeatNumber;
            numSeatLb.Text = seatDetails.NumberOfSeats.ToString();
            totalFeesLb.Text = seatDetails.TotalFees.ToString("N2");

            if (numSeatTb.Value != 0 && seatsTb.Text != string.Empty)
            {
                nextBt.Visible = true;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void backBt_Click(object sender, EventArgs e)
        {
            TicketForm ticketForm = new TicketForm(previousDateStore, seatDetail);
            this.Hide();
            ticketForm.ShowDialog();
        }

        private void nextBt_Click(object sender, EventArgs e)
        {
            TicketCheckout ticketCheckout = new TicketCheckout(previousDateStore, seatDetail);
            this.Hide();
            ticketCheckout.ShowDialog();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void confirmBt_Click(object sender, EventArgs e)
        {
            if (numSeatTb.Value <= 0 || numSeatTb == null)
            {
                MessageBox.Show("Number of Seat Cannot Less Than or Equal To Zero", "NUMBER INPUT ERROR");
                return;
            }

            if (string.IsNullOrWhiteSpace(seatsTb.Text))
            {
                MessageBox.Show("Seat Number Cannot Be Empty", "NULL WARNING");
                return;
            }

            seatDetail.NumberOfSeats = (int)numSeatTb.Value;
            numSeatLb.Text = numSeatTb.Value.ToString();

            seatDetail.SeatNumber = seatsTb.Text;

            double totalFees = ((int) numSeatTb.Value) * 1;
            totalFeesLb.Text = totalFees.ToString("N2");
            nextBt.Visible = true;
        }

        private void clearBt_Click(object sender, EventArgs e)
        {
            numSeatTb.Value = 0;
            seatsTb.Text = string.Empty;
        }

    }

    public class SeatDetail
    {
        public string SeatNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TotalFees { get; set; }
        //public void SeatDetails(string seatNumber, int numberOfSeats, decimal totalFees)
        //{
        //    SeatNumber = seatNumber;
        //    NumberOfSeats = numberOfSeats;
        //    TotalFees = totalFees;
        //}
    }
}
