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
using TravelXpress_Package_System.Module;

namespace TravelXpress_Package_System
{
    public partial class TicketReceipt : Form
    {
        private SqlConnection connection;
        private TemporaryBusDetailsStore previousDateStore = new TemporaryBusDetailsStore();
        private SeatDetail previousSeatDetails = new SeatDetail();
        private CustomerDetails userDetails = new CustomerDetails();
        public TicketReceipt(TemporaryBusDetailsStore previousDateStore, SeatDetail previousSeatDetails, CustomerDetails userDetails)
        {
            InitializeComponent();

            this.previousDateStore = previousDateStore;
            this.previousSeatDetails = previousSeatDetails;
            this.userDetails = userDetails;

            // Initialize the connection object
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Coding\\C#\\Travel_Package_System\\TravelXpress_Package_System\\TravelXpress_Package_System\\TravelXpressDBMS.mdf;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        private void TicketReceipt_Load(object sender, EventArgs e)
        {
            string sqlPullBooking = "SELECT b.*, t.*, r.*, ticket.*, bus.* " +
                                  "FROM Booking b" +
                                  "INNER JOIN Ticket ticket ON b.TicketID = ticket.TicketID" +
                                  "INNER JOIN Trip t ON ticket.TripID = t.TripID " +
                                  "INNER JOIN Bus r ON t.BusID = r.BusID " +
                                  "INNER JOIN BusDetails bus ON r.BusDetailsID = bus.BusDetailsID " +
                                  "WHERE ticket.TicketID = @TicketID";
            using (SqlCommand cmd = new SqlCommand(sqlPullBooking, connection))
            {
                cmd.Parameters.AddWithValue("@ticketID", previousDateStore.ticketID);

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
                        string paymentMethod = reader["PaymentMethod"].ToString();

                        DateTime DepartureDate = Convert.ToDateTime(reader["DepartureDate"]);
                        DateTime DepartureTime = Convert.ToDateTime(reader["DepartureTime"]);
                        DateTime arrivalTime = Convert.ToDateTime(reader["arrivalTime"]);
                        DateTime bookingDate = Convert.ToDateTime(reader["BookingDate"]);

                        TimeSpan duration = arrivalTime - DepartureTime;
                        string durationFormatted = $"{(int)duration.TotalHours}h {duration.Minutes}m";

                        string receiptNumber = GenerateReceiptNumber();


                        Panel panel = CloneReceiptPanel();

                        // Now set values in the cloned panel using control names
                        panel.Controls["label5"].Text = userDetails.Name;
                        panel.Controls["label6"].Text = userDetails.Contact;
                        panel.Controls["label8"].Text = userDetails.Email;
                        panel.Controls["label12"].Text = bookingDate.ToString("dd MMMM yyyy");
                        panel.Controls["label14"].Text = receiptNumber;
                        panel.Controls["label58"].Text = DepartureDate.ToString("dd MMMM yyyy");
                        panel.Controls["label56"].Text = DepartureTime.ToString("hh:mm tt");
                        panel.Controls["label54"].Text = previousSeatDetails.NumberOfSeats.ToString();
                        panel.Controls["label60"].Text = Price;
                        panel.Controls["label52"].Text = Bus;
                        panel.Controls["label50"].Text = Origin;
                        panel.Controls["label48"].Text = Destination;
                        panel.Controls["label16"].Text = Price;
                        panel.Controls["label18"].Text = previousSeatDetails.TotalFees.ToString("C2");
                        panel.Controls["label23"].Text = paymentMethod;
                        panel.Controls["label22"].Text = previousSeatDetails.TotalFees.ToString("C2");
                        panel.Controls["label44"].Text = previousSeatDetails.TotalFees.ToString("C2");



                        panel.Controls["label13"].Text = previousSeatDetails.NumberOfSeats.ToString();
                        panel.Controls["label34"].Text = Price;
                        panel.Controls["label12"].Text = Bus;
                        panel.Controls["label8"].Text = Origin;
                        panel.Controls["label23"].Text = Destination;
                        panel.Controls["label24"].Text = dropoff;
                        panel.Controls["label11"].Text = boarding;
                        panel.Controls["label25"].Text = previousSeatDetails.TotalFees.ToString("N2");

                        receiptFlowPanel.Controls.Add(panel);
                    }
                }
                connection.Close();
            }
        }

        private string GenerateReceiptNumber()
        {
            Random random = new Random();
            string digits = "";

            for (int i = 0; i < 10; i++)
            {
                digits += random.Next(0, 10); // generate a digit between 0-9
            }

            return "R" + digits;
        }

        private Panel CloneReceiptPanel()
        {
            Panel clonedPanel = new Panel
            {
                AutoScroll = true,
                Location = new Point(20, 5),
                Margin = new Padding(6, 5, 6, 5),
                Name = "receiptPanelClone",
                Size = new Size(1227, 596),
                TabIndex = 3,
                BackColor = receiptPanel.BackColor // Optional: clone style
            };

            foreach (Control ctrl in receiptPanel.Controls)
            {
                Control clonedControl = null;

                if (ctrl is Panel originalPanel)
                {
                    // Shallow clone of the panel
                    clonedControl = new Panel
                    {
                        Location = originalPanel.Location,
                        Size = originalPanel.Size,
                        BackColor = originalPanel.BackColor,
                        Name = originalPanel.Name + "_clone",
                        BorderStyle = originalPanel.BorderStyle,
                        AutoScroll = originalPanel.AutoScroll
                    };

                    // (Optional) Deep clone child controls inside each panel
                    foreach (Control innerCtrl in originalPanel.Controls)
                    {
                        Control innerClone = CloneControl(innerCtrl);
                        if (innerClone != null)
                            clonedControl.Controls.Add(innerClone);
                    }
                }
                else if (ctrl is GroupBox originalGroupBox)
                {
                    clonedControl = new GroupBox
                    {
                        Text = originalGroupBox.Text,
                        Location = originalGroupBox.Location,
                        Size = originalGroupBox.Size,
                        Font = originalGroupBox.Font,
                        Name = originalGroupBox.Name + "_clone"
                    };

                    foreach (Control innerCtrl in originalGroupBox.Controls)
                    {
                        Control innerClone = CloneControl(innerCtrl);
                        if (innerClone != null)
                            clonedControl.Controls.Add(innerClone);
                    }
                }

                if (clonedControl != null)
                    clonedPanel.Controls.Add(clonedControl);
            }

            return clonedPanel;
        }

        private Control CloneControl(Control original)
        {
            if (original is Label lbl)
            {
                return new Label
                {
                    Text = lbl.Text,
                    Location = lbl.Location,
                    Size = lbl.Size,
                    Font = lbl.Font,
                    Name = lbl.Name + "_clone",
                    AutoSize = lbl.AutoSize,
                    ForeColor = lbl.ForeColor,
                    BackColor = lbl.BackColor
                };
            }
            else if (original is TextBox txt)
            {
                return new TextBox
                {
                    Text = txt.Text,
                    Location = txt.Location,
                    Size = txt.Size,
                    Font = txt.Font,
                    Name = txt.Name + "_clone"
                };
            }
            else if (original is Button btn)
            {
                return new Button
                {
                    Text = btn.Text,
                    Location = btn.Location,
                    Size = btn.Size,
                    Font = btn.Font,
                    Name = btn.Name + "_clone"
                    // Add event handler manually if needed
                };
            }
            // Add more control types here as needed

            return null; // Unsupported control type
        }

        private void confirmBt_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your ticket has been successfully booked! \nThank you for choosing TravelXpress.", "Booking Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CustomerMainPage customerMainPage = new CustomerMainPage();
            this.Hide();
            customerMainPage.ShowDialog();
        }
    }
}
