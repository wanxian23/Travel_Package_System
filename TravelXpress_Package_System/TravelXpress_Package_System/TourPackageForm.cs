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
    public partial class TourPackageForm : Form
    {
        public TourPackageForm()
        {
            InitializeComponent();
        }

        public int packageID;

        private void button1_Click(object sender, EventArgs e)
        {
            packageID = 1;
            CheckoutForm checkoutform = new CheckoutForm(packageID);
            checkoutform.ShowDialog();
        }

        private void buttonCheckout2_Click(object sender, EventArgs e)
        {
            packageID = 2;
            CheckoutForm checkoutform = new CheckoutForm(packageID);
            checkoutform.ShowDialog();
        }

        private void buttonCheckout3_Click(object sender, EventArgs e)
        {
            packageID = 3;
            CheckoutForm checkoutform = new CheckoutForm(packageID);
            checkoutform.ShowDialog();
        }

        private void buttonCheckout4_Click(object sender, EventArgs e)
        {
            packageID = 4;
            CheckoutForm checkoutform = new CheckoutForm(packageID);
            checkoutform.ShowDialog();
        }

        private void buttonCheckout5_Click(object sender, EventArgs e)
        {
            packageID = 5;
            CheckoutForm checkoutform = new CheckoutForm(packageID);
            checkoutform.ShowDialog();
        }

        private void linkLabelMorDetails1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            packageID = 1;
            TourPackageDetailsForm tourPackageDetailsForm = new TourPackageDetailsForm(packageID);
            tourPackageDetailsForm.ShowDialog();

        }

        private void linkLabelMorDetails2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
