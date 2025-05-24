using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TravelXpress_Package_System
{
    public partial class CheckoutForm : Form
    {
        public CheckoutForm()
        {
            InitializeComponent();

            groupBoxCustomerFamilyDetails.Hide();

            groupBoxCustomerFamilyDetails.Text = "Details for customer 2: ";
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDownNumPax_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownNumPax.Value > 1)
            {
                panel1.AutoScroll = false;
                groupBoxCustomerFamilyDetails.Show();
                panel1.AutoScroll = true;
            }
            else if (numericUpDownNumPax.Value == 1)
            {
                panel1.AutoScroll = false;
                groupBoxCustomerFamilyDetails.Hide();
                panel1.AutoScroll = true;
            }
        }

        private void buttonCheckOut_Click(object sender, EventArgs e)
        {
            ChekourPaymentForm form = new ChekourPaymentForm();
            form.ShowDialog();
        }
    }
}
