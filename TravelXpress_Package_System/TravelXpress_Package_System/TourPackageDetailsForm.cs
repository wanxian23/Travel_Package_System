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
    public partial class TourPackageDetailsForm : Form
    {
        public int packageType;
        public TourPackageDetailsForm(int packageType)
        {
            InitializeComponent();
            this.packageType = packageType;
        }

    }
}
