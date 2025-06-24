using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TravelXpress_Package_System
{
    public partial class AccommodationAvailabilityPage: Form
    {
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;
        public AccommodationAvailabilityPage()
        {
            InitializeComponent();

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\A'isyah Insyirah\\Desktop\\UTEM\\YEAR 2 SEM 2\\EVENT-BASED\\PROJECT\\Travel_Package_System\\TravelXpress_Package_System\\TravelXpress_Package_System\\TravelXpressDBMS.mdf\";Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccommodationBookingPage accommBookingPage = new AccommodationBookingPage();
            accommBookingPage.ShowDialog();
        }

        private void AccommodationAvailabilityPage_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'travelXpressDataSet.Accommodation' table. You can move, or remove it, as needed.
            this.accommodationTableAdapter.Fill(this.travelXpressDataSet.Accommodation);

            LoadHotels();
            LoadHotelsbyStates();         // Load unique states from database
            LoadHotelsbyRating();
        }

        private void LoadHotels()
        {
            flowLayoutPanel1.Controls.Clear();

            string selectState = cbBoxStates.SelectedItem?.ToString();
            string selectRating = cbBoxRating.SelectedItem?.ToString();

            string query = "SELECT AccomName, Location, Rating, State FROM Accommodation";

            List<string> conditions = new List<string>();           

            if (!string.IsNullOrEmpty(selectState) && selectState != "All")
            {
                conditions.Add("State = @State");
            }
            else
            {

            }

            if (conditions.Count > 0)
                query += " WHERE " + string.Join(" AND ", conditions);

            if (selectRating == "Rating - Low to High")
                query += " ORDER BY Rating ASC";

            if (selectRating == "Rating - High to Low")
                query += " ORDER BY Rating DESC";

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\A'isyah Insyirah\\Desktop\\UTEM\\YEAR 2 SEM 2\\EVENT-BASED\\PROJECT\\Travel_Package_System\\TravelXpress_Package_System\\TravelXpress_Package_System\\TravelXpressDBMS.mdf\";Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrEmpty(selectState))
                    cmd.Parameters.AddWithValue("@State", selectState);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    HotelOutput card = new HotelOutput();

                    card.SetHotelData(
                        reader["AccomName"].ToString(),
                        reader["Location"].ToString(),
                        Convert.ToSingle(reader["Rating"])
                    );

                    flowLayoutPanel1.Controls.Add(card);
                }

                reader.Close();
            }
        }

        private void LoadHotelsbyStates()
        {
            cbBoxStates.Items.Clear();
            cbBoxStates.Items.Add("All");

            string query = "SELECT DISTINCT State FROM Accommodation";
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\A'isyah Insyirah\\Desktop\\UTEM\\YEAR 2 SEM 2\\EVENT-BASED\\PROJECT\\Travel_Package_System\\TravelXpress_Package_System\\TravelXpress_Package_System\\TravelXpressDBMS.mdf\";Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cbBoxStates.Items.Add(reader["State"].ToString());
                }
                reader.Close();
            }            
        }

        private void LoadHotelsbyRating()
        {
            cbBoxRating.Items.Clear();
            cbBoxRating.Items.Add("Rating - Low to High");
            cbBoxRating.Items.Add("Rating - High to Low");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            CustomerMainPage customerMainPage = new CustomerMainPage();
            customerMainPage.ShowDialog();
        }

        private void SEARCH_Click(object sender, EventArgs e)
        {
            LoadHotels();
        }

        private void cbBoxRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHotels();
        }
    }
}
