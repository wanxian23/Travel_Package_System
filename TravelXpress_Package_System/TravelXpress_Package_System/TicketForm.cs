using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace TravelXpress_Package_System
{
    public partial class TicketForm : Form
    {
        private SqlConnection connection;

        TemporaryBusDetailsStore previousDateStore;
        SeatDetail seatDetail = new SeatDetail();
        public TicketForm(TemporaryBusDetailsStore previousDateStore, SeatDetail seatDetail)
        {
            InitializeComponent();

            this.previousDateStore = previousDateStore;
            this.seatDetail = seatDetail;

            // Initialize the connection object
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Coding\\C#\\Travel_Package_System\\TravelXpress_Package_System\\TravelXpress_Package_System\\TravelXpressDBMS.mdf;Integrated Security=True";
            connection = new SqlConnection(connectionString);
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
            Button clickedButton = sender as Button;
            string selectedTicketID = "";
            if (clickedButton != null && clickedButton.Tag != null)
            {
                selectedTicketID = clickedButton.Tag.ToString();
                // Or do something else with selectedTicketID
            }
            previousDateStore.ticketID = selectedTicketID;

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

            string sqlPullTrip = "SELECT t.*, r.*, ticket.*, bus.* " +
                                 "FROM Trip t " +
                                 "INNER JOIN RoutePath r ON t.RouteID = r.RouteID " +
                                 "INNER JOIN Ticket ticket ON t.TripID = ticket.TripID " +
                                 "INNER JOIN BusDetails bus ON t.BusID = bus.BusID " +
                                 "WHERE r.Origin LIKE @origin AND r.Destination LIKE @destination";

            // Handle one-way trips: Hide return UI
            if (previousDateStore.roundTrip == false)
            {
                returnTb.Visible = false;
                returnDateLb.Visible = false;
                departDateLb.Location = new Point(415, 104);
                departTb.Location = new Point(594, 108);
            }

            using (SqlCommand cmd = new SqlCommand(sqlPullTrip, connection))
            {
                cmd.Parameters.AddWithValue("@origin", "%" + busFromTb.Text + "%");
                cmd.Parameters.AddWithValue("@destination", "%" + busToTb.Text + "%");

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string BusID = reader["BusID"].ToString();
                        string Bus = reader["Bus"].ToString();
                        string Origin = reader["Origin"].ToString();
                        string Destination = reader["Destination"].ToString();
                        string Price = reader["Price"].ToString();
                        string busImagePath = reader["BusUpload"].ToString();
                        string totalSeats = reader["TotalSeats"].ToString();
                        string boarding = reader["BoardingPoint"].ToString();
                        string dropoff = reader["DropOffPoint"].ToString();
                        previousDateStore.ticketID = reader["TicketID"].ToString();

                        Image busImage;
                        if (File.Exists(busImagePath))
                        {
                            busImage = Image.FromFile(busImagePath);
                            
                        }
                        else
                        {
                            // Fallback image if not found
                            busImage = Image.FromFile("NoImageUploaded.png");
                        }

                        DateTime DepartureDate = Convert.ToDateTime(reader["DepartureDate"]);
                        DateTime DepartureTime = Convert.ToDateTime(reader["DepartureTime"]);
                        DateTime arrivalTime = Convert.ToDateTime(reader["arrivalTime"]);

                        TimeSpan duration = arrivalTime - DepartureTime;
                        string durationFormatted = $"{(int)duration.TotalHours}h {duration.Minutes}m";


                        Panel panel = CloneTicketPanel();

                        // Now set values in the cloned panel using control names
                        panel.Controls["label5"].Text = DepartureTime.ToString("hh:mm tt");
                        panel.Controls["label6"].Text = Bus;
                        panel.Controls["label7"].Text = totalSeats;
                        panel.Controls["label8"].Text = "Total Seats";
                        panel.Controls["label9"].Text = "RM";
                        panel.Controls["label10"].Text = Price;
                        panel.Controls["label11"].Text = boarding;
                        panel.Controls["label12"].Text = durationFormatted;
                        panel.Controls["label14"].Text = dropoff;
                        panel.Controls["label16"].Text = Destination;
                        panel.Controls["label30"].Text = totalSeats;
                        panel.Controls["label31"].Text = "Total Seats";

                        PictureBox pic = panel.Controls["pictureBox1"] as PictureBox;
                        if (pic != null)
                            pic.Image = busImage;

                        Button chooseButton = panel.Controls["chooseBt1"] as Button;
                        if (chooseButton != null)
                        {
                            chooseButton.Tag = previousDateStore.ticketID;
                            chooseButton.Click += chooseBt1_Click;
                        }

                        flowTicket1.Controls.Add(panel);
                    }
                }
                connection.Close();
            }
        }

        private Panel CloneTicketPanel()
        {
            Panel template = ticket1;
            Panel clone = new Panel
            {
                Size = template.Size,
                BackColor = template.BackColor,
                Font = template.Font,
                Margin = template.Margin,
                Padding = template.Padding
            };

            foreach (Control ctrl in template.Controls)
            {
                Control newCtrl = (Control)Activator.CreateInstance(ctrl.GetType());

                newCtrl.Size = ctrl.Size;
                newCtrl.Location = ctrl.Location;
                newCtrl.Font = ctrl.Font;
                newCtrl.Text = ctrl.Text;
                newCtrl.BackColor = ctrl.BackColor;
                newCtrl.ForeColor = ctrl.ForeColor;
                newCtrl.Name = ctrl.Name;

                if (ctrl is PictureBox pic && newCtrl is PictureBox newPic)
                {
                    newPic.Image = pic.Image;
                    newPic.SizeMode = pic.SizeMode;
                }

                if (ctrl is Button btn && newCtrl is Button newBtn)
                {
                    newBtn.Click += chooseBt1_Click; // attach event
                }

                clone.Controls.Add(newCtrl);
            }

            return clone;
        }

    }
}
