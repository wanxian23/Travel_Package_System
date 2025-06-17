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
        TemporaryBusDetailsStore previousDateStore;
        SeatDetail seatDetail = new SeatDetail();
        public TicketForm(TemporaryBusDetailsStore previousDateStore, SeatDetail seatDetail)
        {
            InitializeComponent();

            this.previousDateStore = previousDateStore;
            this.seatDetail = seatDetail;
        }

        private void backBt_Click(object sender, EventArgs e)
        {
            TicketBookingForm ticketBookingForm = new TicketBookingForm(seatDetail);
            this.Hide();
            ticketBookingForm.busFrom = previousDateStore.busFrom;
            ticketBookingForm.busTo = previousDateStore.busTo;  
            ticketBookingForm.busDepartDate = previousDateStore.departDate;
            ticketBookingForm.busReturnDate = previousDateStore.reDate;
            ticketBookingForm.ShowDialog();
        }
        private void chooseBt1_Click(object sender, EventArgs e)
        {
            TicketSeat ticketSeat = new TicketSeat(previousDateStore, seatDetail);
            this.Hide();
            ticketSeat.ShowDialog();
        }

        private void chooseBt2_Click(object sender, EventArgs e)
        {
            TicketSeat ticketSeat = new TicketSeat(previousDateStore, seatDetail);
            this.Hide();
            ticketSeat.ShowDialog();
        }

        private void TicketForm_Load(object sender, EventArgs e)
        {

            busFromTb.Text = previousDateStore.busFrom;
            busToTb.Text = previousDateStore.busTo;
            departTb.Text = previousDateStore.departDate.ToString("dd MMMM yyyy");
            returnTb.Text = previousDateStore.reDate.ToString("dd MMMM yyyy");

            if (string.IsNullOrWhiteSpace(busToTb.Text))
            {
                busToTb.Visible = false;
                busToLb.Visible = false;
                returnTb.Visible = false;
                returnDateLb.Visible = false;
                busFromLb.Location = new Point(350, 18);
                busFromTb.Location = new Point(458, 22);
                departDateLb.Location = new Point(415, 104);
                departTb.Location = new Point(594, 108);
            } 

        }

        private Panel CreateTicketPanel(
            string busName,
            string departDate,
            string returnDate,
            string departureTime,
            string returnTime,
            string fromLocation,
            string toLocation,
            string price,
            Image busImage,
            EventHandler chooseButtonClickHandler
        )
        {
            Panel ticketPanel = new Panel();
            ticketPanel.BackColor = Color.FromArgb(224, 224, 224);
            ticketPanel.Size = new Size(1138, 193);
            ticketPanel.Font = new Font("Microsoft Sans Serif", 12F);
            ticketPanel.Margin = new Padding(10);

            // Bus Image
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = busImage;
            pictureBox.Size = new Size(140, 140);
            pictureBox.Location = new Point(20, 25);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            ticketPanel.Controls.Add(pictureBox);

            // Labels
            Label lblBusName = new Label();
            lblBusName.Text = $"Bus: {busName}";
            lblBusName.Location = new Point(180, 20);
            lblBusName.AutoSize = true;
            ticketPanel.Controls.Add(lblBusName);

            Label lblFrom = new Label();
            lblFrom.Text = $"From: {fromLocation}";
            lblFrom.Location = new Point(180, 50);
            lblFrom.AutoSize = true;
            ticketPanel.Controls.Add(lblFrom);

            Label lblTo = new Label();
            lblTo.Text = $"To: {toLocation}";
            lblTo.Location = new Point(180, 80);
            lblTo.AutoSize = true;
            ticketPanel.Controls.Add(lblTo);

            Label lblDepartDate = new Label();
            lblDepartDate.Text = $"Depart Date: {departDate}";
            lblDepartDate.Location = new Point(400, 20);
            lblDepartDate.AutoSize = true;
            ticketPanel.Controls.Add(lblDepartDate);

            Label lblReturnDate = new Label();
            lblReturnDate.Text = $"Return Date: {returnDate}";
            lblReturnDate.Location = new Point(400, 50);
            lblReturnDate.AutoSize = true;
            ticketPanel.Controls.Add(lblReturnDate);

            Label lblDepartTime = new Label();
            lblDepartTime.Text = $"Depart Time: {departureTime}";
            lblDepartTime.Location = new Point(400, 80);
            lblDepartTime.AutoSize = true;
            ticketPanel.Controls.Add(lblDepartTime);

            Label lblReturnTime = new Label();
            lblReturnTime.Text = $"Return Time: {returnTime}";
            lblReturnTime.Location = new Point(400, 110);
            lblReturnTime.AutoSize = true;
            ticketPanel.Controls.Add(lblReturnTime);

            Label lblPrice = new Label();
            lblPrice.Text = $"Price: RM {price}";
            lblPrice.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            lblPrice.ForeColor = Color.DarkGreen;
            lblPrice.Location = new Point(900, 30);
            lblPrice.AutoSize = true;
            ticketPanel.Controls.Add(lblPrice);

            // Choose Button
            Button chooseButton = new Button();
            chooseButton.Text = "Choose";
            chooseButton.Font = new Font("Microsoft Sans Serif", 12F);
            chooseButton.Location = new Point(900, 100);
            chooseButton.Size = new Size(150, 40);
            chooseButton.Click += chooseButtonClickHandler;
            ticketPanel.Controls.Add(chooseButton);

            return ticketPanel;
        }


    }
}
