using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelXpress_Package_System.Module
{
    public class PackageCheckout
    {
        public string PackageID { get; set; }
        public string BookingType { get; set; } = "Package";
        public CustomerDetails CustomerDetails { get; set; }
        public List<CustomerMemberDetails> CustomerMembersDetails { get; set; }
        public DateTime bookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int numPax {  get; set; }
        public double totalAmount { get; set; }
        public string paymentMethod { get; set; }


    }
}
