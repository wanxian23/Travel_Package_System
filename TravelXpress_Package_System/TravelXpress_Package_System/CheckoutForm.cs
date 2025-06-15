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
        public int packagetype;
        public CheckoutForm(int packagetype)
        {
            InitializeComponent();
            this.packagetype = packagetype;

            groupBoxCustomerFamilyDetails.Hide();
            groupBoxCustomerFamilyDetails.Text = "Details for customer member 1: ";

            string connection = "";
        }

        public class CustomerMemberDetails
        {
            public string ID {  get; set; }
            public string Name { get; set; }
            public string IC { get; set; }
            public string PhoneNO { get; set; }
            public string Gender { get; set; }
        }

        public List<CustomerMemberDetails> memberDetails = new List<CustomerMemberDetails>();

        public decimal customerMemberPax = 0;
        public int currentCustomerMemberNo = 0;

        private void radioButtonTrue_CheckedChanged(object sender, EventArgs e)
        {
            panel1.AutoScroll = false;
            if (radioButtonTrue.Checked)
            {
                if (!checkCustomerDetail())
                {
                    MessageBox.Show("Your details is not complete fill in");
                    radioButtonTrue.Checked = false;
                    return;
                }
                labelNumberPax.Visible = true;
                numericUpDownNumPax.Visible = true;
                buttonMemberPaxConfirm.Visible = true;
                buttonMemberPaxEdit.Visible = true;
            }
            else
            {
                labelNumberPax.Visible = false;
                numericUpDownNumPax.Visible = false;
                buttonMemberPaxConfirm.Visible = false;
                buttonMemberPaxEdit.Visible = false;
                groupBoxCustomerFamilyDetails.Hide();
                buttonConfirmCustomerMemberDetails.Visible = false;
                buttonEditCustomerMemberDetails.Visible = false;
            }
            panel1.AutoScroll = true;
        }

        private void buttonMemberPaxConfirm_Click(object sender, EventArgs e)
        {
            panel1.AutoScroll = false;
            groupBoxCustomerFamilyDetails.Show();
            groupBoxCustomerFamilyDetails.Enabled = true;
            buttonConfirmCustomerMemberDetails.Visible = true;
            buttonEditCustomerMemberDetails.Visible = true;
            panel1.AutoScroll = true;
            customerMemberPax = numericUpDownNumPax.Value;
            currentCustomerMemberNo = 1;
            numericUpDownNumPax.Enabled = false;

            if (customerMemberPax == 1)
            {
                buttonNextPerson.Enabled = false;
            }
            else
            {
                buttonNextPerson.Enabled = true;
            }
            buttonPrevious.Enabled = false;
        }

        private void buttonMemberPaxEdit_Click(object sender, EventArgs e)
        {
            numericUpDownNumPax.Enabled = true;
        }

        private void buttonNextPerson_Click(object sender, EventArgs e)
        {
            if (!checkCustomerMemberDetails())
            {
                MessageBox.Show("The member details is not complete fill in");
            }
            else
            {
                if (memberDetails.Count + 1 == currentCustomerMemberNo)
                {
                    addNewMemberDetails();
                    clearMemberDetails();
                }
                else
                {
                    editCurrentMemberDetails();
                }
                currentCustomerMemberNo++;
                groupBoxCustomerFamilyDetails.Text = $"Details for customer member {currentCustomerMemberNo}: ";
                if (memberDetails.Count >= currentCustomerMemberNo)
                {
                    showCurrentMemberDetails();
                }
                buttonPrevious.Enabled = true;

                if (currentCustomerMemberNo == customerMemberPax)
                {
                    buttonNextPerson.Enabled = false;
                }
            }
        }

        void addNewMemberDetails()
        {
            var validNextID = "";
            if (memberDetails.Any())
            {
                var lastMemberindex = memberDetails.Count - 1;
                var lastMemberid = memberDetails[lastMemberindex].ID.Substring(1);
                int nextid = int.Parse(lastMemberid) + 1;
                validNextID = "C" + nextid.ToString("D4");
            }
            else
            {
                validNextID = "C0001";
            }
            memberDetails.Add(new CustomerMemberDetails
            {
                ID = validNextID,
                Name = textBoxCustomerMemberName.Text,
                IC = textBoxCustomerMemberIC.Text,
                PhoneNO = textBoxCustomerMemberPhone.Text,
                Gender = radioButtonMale.Checked ? "Male" : "Female"
            });
            
        }
        
        void clearMemberDetails()
        {
            textBoxCustomerMemberName.Text = "";
            textBoxCustomerMemberIC.Text = "";
            textBoxCustomerMemberPhone.Text = "";
            radioButtonMale.Checked = false;
            radioButtonFemale.Checked = false;
        }

        void showCurrentMemberDetails()
        {
            textBoxCustomerMemberName.Text = memberDetails[currentCustomerMemberNo - 1].Name;
            textBoxCustomerMemberIC.Text = memberDetails[currentCustomerMemberNo - 1].IC;
            textBoxCustomerMemberPhone.Text = memberDetails[currentCustomerMemberNo - 1].PhoneNO;
            if (memberDetails[currentCustomerMemberNo - 1].Gender == "Male")
            {
                radioButtonMale.Checked = true;
            }
            else
            {
                radioButtonFemale.Checked = true;
            }
        }

        void editCurrentMemberDetails()
        {
            memberDetails[currentCustomerMemberNo - 1].Name = textBoxCustomerMemberName.Text;
            memberDetails[currentCustomerMemberNo - 1].IC = textBoxCustomerMemberIC.Text;
            memberDetails[currentCustomerMemberNo - 1].PhoneNO = textBoxCustomerMemberPhone.Text;
            memberDetails[currentCustomerMemberNo - 1].Gender = radioButtonMale.Checked ? "Male" : "Female";
        }


        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (currentCustomerMemberNo > memberDetails.Count)
            {
                addNewMemberDetails();
            }
            else
            {
                editCurrentMemberDetails();
            }
            currentCustomerMemberNo--;
            groupBoxCustomerFamilyDetails.Text = $"Details for customer member {currentCustomerMemberNo}: ";
            buttonNextPerson.Enabled = true;

            if (currentCustomerMemberNo == 1)
            {
                buttonPrevious.Enabled = false;
            }
            showCurrentMemberDetails();
        }

        private void buttonConfirmCustomerMemberDetails_Click(object sender, EventArgs e)
        {
            if (currentCustomerMemberNo != customerMemberPax)
            {
                MessageBox.Show($"Your member {currentCustomerMemberNo + 1} details not yet fill in. Please fill in by click 'Next Person' button");
            }
            else if (!checkCustomerMemberDetails())
            {
                MessageBox.Show($"Your member {currentCustomerMemberNo} details not yet fill in complete.");
            }
            else
            {
                if (currentCustomerMemberNo > memberDetails.Count)
                {
                    addNewMemberDetails();
                }
                else
                {
                    editCurrentMemberDetails();
                }
                groupBoxCustomerFamilyDetails.Enabled = false;
            }
        }

        bool checkCustomerMemberDetails()
        {
            if (string.IsNullOrEmpty(textBoxCustomerMemberName.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(textBoxCustomerMemberIC.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(textBoxCustomerMemberPhone.Text))
            {
                return false;
            }
            else if (!radioButtonMale.Checked && !radioButtonFemale.Checked)
            {
                return false;
            }
            return true;
        }

        private void buttonEditCustomerMemberDetails_Click(object sender, EventArgs e)
        {
            groupBoxCustomerFamilyDetails.Enabled = true;
        }

        bool checkCustomerDetail()
        {
            if (string.IsNullOrEmpty(textBoxCustomerName.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(textBoxCustomerIC.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(textBoxCustomerContact.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(textBoxCustomerEmail.Text))
            {
                return false;
            }
            else if (!radioButtonTrue.Checked && !radioButtonFalse.Checked)
            {
                return false;
            }
            return true;
        }

        private void radioButtonFalse_CheckedChanged(object sender, EventArgs e)
        {            
            if (radioButtonFalse.Checked)
            {
                if (!checkCustomerDetail())
                {
                    MessageBox.Show("Your details is not complete fill in yet");
                    radioButtonFalse.Checked = false;
                    return;
                }
                panel1.ScrollControlIntoView(buttonCheckOut);
            }
        }

        private void checkBoxSingleBed_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSingleBed.Checked)
            {
                numericUpDownSingleBed.Visible = true;
            }
        }

        private void checkBoxSingleRKingBed_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSingleRKingBed.Checked)
            {
                numericUpDownSingleRKingBed.Visible = true;
            }
        }

        private void checkBoxFamilyRoom_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFamilyRoom.Checked)
            {
                numericUpDownFamilyRoom.Visible = true;
            }
        }
        private void buttonCheckOut_Click(object sender, EventArgs e)
        {
            Console.WriteLine(memberDetails.Count);
            int numPeople = checkRoomPax();
            if (numPeople != 0)
            {
                MessageBox.Show($"Not enough room booked! You are short by {numPeople}. \nPlease add more room to accomodate all {customerMemberPax + 1} people");
            }
            else
            {
                ChekourPaymentForm form = new ChekourPaymentForm();
                form.ShowDialog();
            }
        }
        int checkRoomPax()
        {
            int singleBed = (int)numericUpDownSingleBed.Value;
            int singleRKingBed = (int)numericUpDownSingleRKingBed.Value * 2;
            int familyRoom = (int)numericUpDownFamilyRoom.Value * 4;

            int estimatedRoomPax = singleBed + singleRKingBed + familyRoom;

            if (estimatedRoomPax < (customerMemberPax + 1))
            {
                return (((int)customerMemberPax + 1) - estimatedRoomPax);
            }
            return 0;
        }
    }
}
