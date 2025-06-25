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

namespace TravelXpress_Package_System
{
    public partial class AccommodationBookingPage: Form
    {
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataSet dataset;

        public AccommodationBookingPage()
        {
            InitializeComponent();

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Travel_Package_System\\TravelXpress_Package_System\\TravelXpress_Package_System\\TravelXpressDBMS.mdf;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        private void AccommodationBookingPage_Load(object sender, EventArgs e)
        {
            LoadRooms();
        }

        private void LoadRooms()
        {
            flowLayoutPanel1.Controls.Clear();

            string query = "SELECT * FROM RoomType";
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Travel_Package_System\\TravelXpress_Package_System\\TravelXpress_Package_System\\TravelXpressDBMS.mdf;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RoomOutput card = new RoomOutput();
                    card.SetRoomData(
                        reader[""].ToString(),
                        reader[""].ToString()
                        
                    );                    
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccommodationDetailsPage accommDetailsPage = new AccommodationDetailsPage();
            accommDetailsPage.ShowDialog();
        }
    }
}
