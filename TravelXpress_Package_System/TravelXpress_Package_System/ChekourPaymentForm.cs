using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelXpress_Package_System.Module;

namespace TravelXpress_Package_System
{
    public partial class ChekourPaymentForm : Form
    {
        public PackageCheckout PackageCheckout { get; set; }
        public ConnectionClass connectionClass { get; set; }
        public ChekourPaymentForm(PackageCheckout PackageCheckout)
        {
            InitializeComponent();
            this.PackageCheckout = PackageCheckout;
            connectionClass = new ConnectionClass();

            loadData();

            labelAmountDetails.Text = $"Package Amount: RM {PackageCheckout.packageAmount:F2} \n";
            if (PackageCheckout.roomAmount != 0)
            {
                labelAmountDetails.Text += $"Room extra cost: RM {PackageCheckout.roomAmount:F2}";
            }
            labelTotalAmount.Text = $"Total Amount: RM {PackageCheckout.totalAmount:F2}";


        }

        private void ChekourPaymentForm_Shown(object sender, EventArgs e)
        {
            // Ensure it's at the top
            panel1.VerticalScroll.Value = 0;
            panel1.AutoScrollPosition = Point.Empty;
        }


        void loadData()
        {
            tableLayoutPanelSummary.Controls.Clear();
            tableLayoutPanelSummary.RowCount = 0;

            addRowWithSpan("Customer Details");
            addRow("Name: ", PackageCheckout.CustomerDetails.Name);
            addRow("IC: ", PackageCheckout.CustomerDetails.IC);
            addRow("Contact: ", PackageCheckout.CustomerDetails.Contact);
            addRow("Email: ", PackageCheckout.CustomerDetails.Email);
            addRow("Gender: ", PackageCheckout.CustomerDetails.Gender);

            if (PackageCheckout.CustomerDetails.memberNum != 0)
            {
                int memberCount = 1;
                foreach (var member in PackageCheckout.CustomerMembersDetails)
                {
                    addRowWithSpan($"Customer Member {memberCount} Details");
                    addRow("Name: ", member.Name);
                    addRow("IC: ", member.IC);
                    addRow("Contact: ", member.Contact);
                    addRow("Gender: ", member.Gender);
                    memberCount++;
                }
            }
            else
            {
                addRowWithSpan("No Customer Member Details");
            }

            addRowWithSpan("Package Details");
            getDataFromDB(PackageCheckout.PackageID);
            addRow("From: ", packageDetails.FromLocation);
            addRow("To: ", packageDetails.ToLocation);
            addRow("Transport: ", "Bus");
            addRow("Duration: ", packageDetails.duration + " days");
            addRow("Depart date: ", PackageCheckout.StartDate.ToString("dd/MM/yyyy"));
            addRow("End date: ", PackageCheckout.EndDate.ToString("dd/MM/yyyy"));
            addRow("Price per pax: ", packageDetails.Price);

            addRowWithSpan("Accomodation details");
            getDataFromTableAccomodation(accomID);
            addRow("Hotel name: ", hotelName);
            if (PackageCheckout.SelectedPackageRoom.singleBed != 0)
            {
                addRow("Single bed room: ", PackageCheckout.SelectedPackageRoom.singleBed.ToString());
            }
            if (PackageCheckout.SelectedPackageRoom.singleRKingBed != 0)
            {
                addRow("Single King bed room: ", PackageCheckout.SelectedPackageRoom.singleRKingBed.ToString());
            }
            if (PackageCheckout.SelectedPackageRoom.familyRoom != 0)
            {
                addRow("Family room (King bed x 2): ", PackageCheckout.SelectedPackageRoom.familyRoom.ToString());
            }
        }

        void addRowWithSpan(string label)
        {
            int rowIndex = tableLayoutPanelSummary.RowCount;
            tableLayoutPanelSummary.RowStyles.Add(new RowStyle());
            tableLayoutPanelSummary.RowCount++;

            Label header = new Label()
            {
                Text = label,
                AutoSize = false, 
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            tableLayoutPanelSummary.Controls.Add(header, 0, rowIndex);
            tableLayoutPanelSummary.SetColumnSpan(header, 2);
        }

        void addRow(string label, string value)
        {
            int rowIndex = tableLayoutPanelSummary.RowCount;
            tableLayoutPanelSummary.RowStyles.Add(new RowStyle());
            tableLayoutPanelSummary.RowCount++;

            tableLayoutPanelSummary.Controls.Add(new Label() { Text = label, AutoSize = true }, 0, rowIndex);
            tableLayoutPanelSummary.Controls.Add(new Label() { Text = value, AutoSize = true }, 1, rowIndex);
        }

        public string accomID;
        public string hotelName;

        void getDataFromDB(string packageID)
        {
            string connectionString = connectionClass.connectionString;
            string query = "SELECT * FROM Package WHERE PackageID = @PackageID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PackageID", packageID);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Package");

                var data = dataSet.Tables["Package"];

                if (data.Rows.Count != 0)
                {
                    accomID = data.Rows[0]["AccomID"].ToString();
                    packageDetails.FromLocation = data.Rows[0]["FromLocation"].ToString();
                    packageDetails.ToLocation = data.Rows[0]["ToLocation"].ToString();
                    packageDetails.duration = data.Rows[0]["DurationDays"].ToString();
                    packageDetails.Price = data.Rows[0]["Price"].ToString();
                }
            }
        }

        void getDataFromTableAccomodation(string accomID)
        {
            string connectionString = connectionClass.connectionString;
            string query = "SELECT * FROM Accommodation WHERE AccomID = @AccomID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccomID", accomID);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Accommodation");

                var data = dataSet.Tables["Accommodation"];

                if (data.Rows.Count != 0)
                {
                    hotelName = data.Rows[0]["AccomName"].ToString();
                }
            }
        }

        public PackageDetails packageDetails = new PackageDetails();

        public class PackageDetails
        {
            public string FromLocation { get; set; }
            public string ToLocation { get; set; }
            public string duration { get; set; }
            public string Price { get; set; }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }        

        private void radioButtonTng_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTng.Checked)
            {
                pictureBoxPayment.Image = Image.FromFile(@"Image\Payment\tng.png");
                cardPanel.Visible = false;
            }
        }

        private void radioButtonFPX_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFPX.Checked)
            {
                pictureBoxPayment.Image = Image.FromFile(@"Image\Payment\FPX.png");
                cardPanel.Visible = false;
            }
        }

        private void radioButtonCard_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCard.Checked)
            {
                pictureBoxPayment.Image = Image.FromFile(@"Image\Payment\Card.png");
                //Image\fishboneSabah.jpeg
                //Image\Bus\TransnationalLogo.png
                cardPanel.Visible = true;
            }
        }

        private void buttonBook_Click(object sender, EventArgs e)
        {
            if (radioButtonCard.Checked)
            {
                if (string.IsNullOrWhiteSpace(cardHolderNameTb.Text))
                {
                    MessageBox.Show("Card Holder Name Cannot Be Null!", "NULL ERROR!");
                    return;
                }

                string cleanedText = cardNumTb.Text.Replace("_", "").Replace("-", "").Trim();
                if (string.IsNullOrWhiteSpace(cleanedText))
                {
                    MessageBox.Show("Card Number Cannot Be Null", "NULL ERROR!");
                    return;
                }

                if (exDateMMTb.Value == 0 || exDateYYTb.Value == 0 || exDateMMTb == null || exDateYYTb == null)
                {
                    MessageBox.Show("Expiry Date For Both Month and Year Cannot Be Null or Zero!", "NULL/ INPUT ERROR");
                    return;
                }

                if (cvvTb.Value == 0 || cvvTb == null)
                {
                    MessageBox.Show("CVV Number Cannot Be Null or Zero!", "NULL/ INPUT ERROR");
                    return;
                }

                if (!visaRb.Checked && !masterRb.Checked)
                {
                    MessageBox.Show("Visa/ Master Must Be Chosen Either One!", "NULL ERROR");
                    return;
                }

                if (!policyCb.Checked)
                {
                    MessageBox.Show("Term And Policy Must Be Checked Before Proceed!", "NULL ERROR");
                    return;
                }
            }

        }
    }
}
