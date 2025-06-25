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
using TravelXpress_Package_System.Module;

namespace TravelXpress_Package_System
{
    public partial class TourPackageDetailsForm : Form
    {
        public int packageType;
        public ConnectionClass connectionClass;
        public TourPackageDetailsForm(int packageType)
        {
            InitializeComponent();
            this.packageType = packageType;
            this.connectionClass = new ConnectionClass();

            getDataFromDB();
        }

        void getDataFromDB()
        {
            string connectionString = connectionClass.connectionString;
            string query = "SELECT * FROM ImagePath WHERE PackageID = @PackageID AND imagePath LIKE '%itinerary%'";
            using (SqlConnection connection1 = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection1);
                command.Parameters.AddWithValue("@PackageID", packageType);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "ImagePath");

                var data = dataSet.Tables["ImagePath"];
                if (data.Rows.Count > 0)
                {
                    pictureBoxIntinerary.Image = Image.FromFile(data.Rows[0]["imagePath"].ToString());
                }
            }
            
            string query2 = "SELECT * FROM Package WHERE PackageID = @PackageID"; ;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

            }

        //Package 1
//        From: Selangor
//        To: Melaka
//        Transport: Bus
//        Accomodation: CATS Hotel
//Duration: 3 Days 2 Night
//Price: RM 1000.00 per pax
//Special Attraction: A'Famosa, The Stadthuys, Jonker Street

        }
    }
}
