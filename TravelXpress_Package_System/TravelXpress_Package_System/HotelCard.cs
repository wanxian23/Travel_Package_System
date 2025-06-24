using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelXpress_Package_System
{
    public partial class HotelOutput: UserControl
    {
        public HotelOutput()
        {
            InitializeComponent();
        }

        public void SetHotelData(string name, string location, float rating) //string imagePath
        {
            lblAccommName.Text = name;
            lblAccommLocation.Text = location;
            lblAccommRating.Text = rating.ToString("0.0") + " / 10";

            //if (File.Exists(imagePath))
            //{
            //    pictureBoxImage.Image = Image.FromFile(imagePath);
            //    pictureBoxImage.SizeMode = PictureBoxSizeMode.StretchImage;
            //}
            //else
            //{
            //    pictureBoxImage.Image = Properties.Resources.DefaultImage; // or set default
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccommodationBookingPage accommBookingPage = new AccommodationBookingPage();
            accommBookingPage.ShowDialog();
        }
    }
}
