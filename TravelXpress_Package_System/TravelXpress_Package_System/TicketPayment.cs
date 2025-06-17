using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TravelXpress_Package_System.TicketCheckout;

namespace TravelXpress_Package_System
{
    public partial class TicketPayment : Form
    {
        UserDetails userDetails = new UserDetails();

        string basePath = AppDomain.CurrentDomain.BaseDirectory;

        string[] imagePath;

        public TicketPayment(UserDetails userDetails)
        {
            InitializeComponent();

            this.userDetails = userDetails;

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] imagePath = {
                Path.Combine(basePath, @"..\..\Image\Payment\FPX.png"),
                Path.Combine(basePath, @"..\..\Image\Payment\tng.png"),
                Path.Combine(basePath, @"..\..\Image\Payment\Card.png")
            };
        }

        private void cardRb_CheckedChanged(object sender, EventArgs e)
        {
            if (cardRb.Checked)
            {
                cardPanel.Visible = true;
                paymentPic.Image = Image.FromFile(imagePath[2]);
            }
        }

        private void ewalletRb_CheckedChanged(object sender, EventArgs e)
        {
            if (ewalletRb.Checked)
            {
                cardPanel.Visible = false;
                paymentPic.Image = Image.FromFile(imagePath[0]);
            }
        }

        private void fpxRb_CheckedChanged(object sender, EventArgs e)
        {
            if (fpxRb.Checked)
            {
                cardPanel.Visible = false;
                paymentPic.Image = Image.FromFile(imagePath[1]);
            }
        }

        private void TicketPayment_Load(object sender, EventArgs e)
        {

        }
    }
}
