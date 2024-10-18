using Autofac;
using ELDOKKAN.Application.Services;
using PL;
using System.Text.RegularExpressions;


namespace Eldokkan.pl
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private bool IsValidPassword(string password)
        {
            // Adjust the regex pattern to match your password requirements
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            var regex = new Regex(passwordPattern);
            return regex.IsMatch(password);
        }
        private void tb_password_Click(object sender, EventArgs e)
        {
            tb_password.Clear();
        }

        private void tb_password_MouseLeave(object sender, EventArgs e)
        {
            if (tb_password.Text == string.Empty)
                tb_password.UseSystemPasswordChar = true;
            if (!IsValidPassword(tb_password.Text))
            {
                // MessageBox.Show("Password must be at least 8 characters long, include uppercase letters, numbers, and special characters.");
                lb_password.Text = "Password must be at least 8 characters long, include uppercase , numbers, special characters.";
                lb_password.ForeColor = Color.Red;
                tb_password.Focus();
            }
            else
            {
                lb_password.Text = "Password";
                lb_password.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
        {
            tb_re_password.Clear();
        }

        private void tb_re_password_MouseLeave(object sender, EventArgs e)
        {
            if (tb_re_password.Text == string.Empty)
                tb_re_password.UseSystemPasswordChar = true;
            if (tb_password.Text != tb_re_password.Text)
            {
                //MessageBox.Show("Passwords do not match.");
                lb_re_password.Text = "Passwords do not match.";
                lb_re_password.ForeColor = (Color.Red);
                tb_re_password.Focus();
            }
            else
            {
                lb_re_password.Text = "renter Password";
                lb_re_password.ForeColor = (Color.Black);
            }
        }

        private void tb_name_Click(object sender, EventArgs e)
        {
            tb_name.Clear();
        }

        private void tb_name_MouseLeave(object sender, EventArgs e)
        {
            if (tb_name.Text == string.Empty)
                tb_name.DefaultText = "Full Name";
            if (ValidateName(tb_name.Text))
            {
                lb_name.Text = "Full Name";
                lb_name.ForeColor = Color.Black;
            }
            else
            {
                tb_name.BackColor = Color.LightCoral;
                lb_name.Text = "Enter Vaild Name";
                lb_name.ForeColor = Color.Red;
                tb_name.Focus();
            }
        }

        private void tb_address_Click(object sender, EventArgs e)
        {
            tb_address.Clear();
        }

        private void tb_address_MouseLeave(object sender, EventArgs e)
        {
            if (tb_address.Text == string.Empty)
            {
                tb_address.DefaultText = "Address";
                lb_address.Text = "Please Enter Address";
                lb_address.ForeColor = Color.Red;
            }
            else
            {
                lb_address.Text = "Address";
                lb_address.ForeColor = Color.Black;
            }
        }

        private void tb_email_Click(object sender, EventArgs e)
        {
            tb_email.Clear();
        }
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            var regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }
        private void tb_email_MouseLeave(object sender, EventArgs e)
        {
            if (tb_email.Text == string.Empty)
                tb_email.DefaultText = "Email";
            if (!IsValidEmail(tb_email.Text))
            {
                //  MessageBox.Show("Please enter a valid email address.");
                lb_email.Text = "Please enter a valid email address.";
                lb_email.ForeColor = Color.Red;
                tb_email.Focus();
            }
            else
            {
                lb_email.Text = "Email";
                lb_email.ForeColor = Color.Black;
            }
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var phonePattern = @"^(01[0125]\d{8}|02\d{8})$";
            var regex = new Regex(phonePattern);
            return regex.IsMatch(phoneNumber);
        }
        private void tb_phone_Click(object sender, EventArgs e)
        {
            tb_phone.Clear();
        }

        private void tb_phone_MouseLeave(object sender, EventArgs e)
        {
            if (tb_phone.Text == string.Empty)
                tb_phone.DefaultText = "Phone";
            if (!IsValidPhoneNumber(tb_phone.Text))
            {
                // MessageBox.Show("Please enter a valid Egyptian phone number.");
                lb_phone.Text = "Please enter a valid Egyptian phone number.";
                lb_phone.ForeColor = Color.Red;
                tb_phone.Focus();
            }
            else
            {
                lb_phone.Text = "Phone";
                lb_phone.ForeColor = Color.Black;
            }
        }

        private void tb_PostalCode_Click(object sender, EventArgs e)
        {
            tb_PostalCode.Clear();
        }

        private void tb_PostalCode_MouseLeave(object sender, EventArgs e)
        {
            if (tb_PostalCode.Text == string.Empty)
            {
                tb_PostalCode.DefaultText = "Postal Code";
                lb_postal.Text = "Please enter postal code";
                lb_postal.ForeColor = Color.Red;
            }
            else
            {
                lb_postal.Text = "Postal Code";
                lb_postal.ForeColor = Color.Black;
            }

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var autoFac = AutoFac.Inject();
            ICustomerService customer = autoFac.Resolve<ICustomerService>();
            customer.AddCustomer(new CreateCustomerDTO()
            {
                Name = tb_name.Text,
                Email = tb_email.Text,
                Password = tb_password.Text,
                Address = tb_address.Text,
                Phone = tb_phone.Text,
                PostalCode = tb_PostalCode.Text,
            });
            MessageBox.Show("Created Succefuly!");
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            using (Login login = new Login())
            {
                login.ShowDialog();
                this.Hide();
            }

        }
        private bool ValidateName(string name)
        {
            string NamePattern = @"^[A-Za-z\s]{3,50}$";

            Regex regex = new Regex(NamePattern);

            return regex.IsMatch(name);
        }
        private void tb_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
