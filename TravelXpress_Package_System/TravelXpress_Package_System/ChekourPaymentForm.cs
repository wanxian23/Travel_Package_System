using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelXpress_Package_System.Module;

namespace TravelXpress_Package_System
{
    public partial class ChekourPaymentForm : Form
    {
        public PackageCheckout PackageCheckout { get; set; }
        public ChekourPaymentForm(PackageCheckout PackageCheckout)
        {
            InitializeComponent();
            this.PackageCheckout = PackageCheckout;
        }

    }
}
