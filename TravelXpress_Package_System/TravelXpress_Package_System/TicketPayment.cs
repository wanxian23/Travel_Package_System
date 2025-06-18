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

        string[] imagePath = {
                "Image/Payment/tng.png",
                "Image/Payment/FPX.png",
                "Image/Payment/Card.png"
            };

        public TicketPayment(UserDetails userDetails)
        {
            InitializeComponent();

            this.userDetails = userDetails;

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

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

        private void bookBt_Click(object sender, EventArgs e)
        {
            if (cardRb.Checked)
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
