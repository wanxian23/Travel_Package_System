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
    public partial class AccommodationBookingPage: Form
    {
        public AccommodationBookingPage()
        {
            InitializeComponent();
        }

        private void AccommodationBookingPage_Load(object sender, EventArgs e)
        {

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
