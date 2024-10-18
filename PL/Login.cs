using Autofac;
using Eldokkan.pl;
using ELDOKKAN.Application.Services;

namespace PL
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }



        private void tb_username_Click(object sender, EventArgs e)
        {
            tb_username.Clear();
        }



        private void tb_username_mouseleave(object sender, EventArgs e)
        {
            if (tb_username.Text == string.Empty)
                tb_username.DefaultText = "User Name";
        }

        private void tb_password_MouseLeave(object sender, EventArgs e)
        {
            if (tb_password.Text == string.Empty)
                tb_password.DefaultText = "Password";
        }

        private void tb_password_MouseHover(object sender, EventArgs e)
        {
            tb_password.Clear();

        }

        private void btnSign_Click_1(object sender, EventArgs e)
        {
            var user = tb_username.Text; string pass = tb_password.Text;
            if (string.IsNullOrEmpty(user))
                guna2MessageDialog1.Show("Please Enter user name");
            if (string.IsNullOrEmpty(pass))
                guna2MessageDialog1.Show("Please Enter user password");

            if (tb_password.Text != string.Empty)
                tb_password.Focus();
            // pass = PasswordHasher.HashPassword(pass);
            var autoFac = AutoFac.Inject();
            ICustomerService customer = autoFac.Resolve<ICustomerService>();
            IAdminService admin = autoFac.Resolve<IAdminService>();

            var verfiyadmin = admin.GetAllAdmins().Where(p => p.Name == user && p.Password == pass).FirstOrDefault();
            // verfiyadmin = verfiyadmin.Where(p=> p.Password==PasswordHasher.HashPassword( user)).ToList();
            // var verfycustomer = customer.GetAllCustomers().Where(p => p.Name == user && PasswordHasher.VerifyPassword(p.Password, user)).ToList();
            var verfycustomer = customer.GetAllCustomers().Where(p => p.Name == user && p.Password == pass).FirstOrDefault();

            if (verfiyadmin != null)
            {

                using (Dashborad dashborad = new Dashborad())
                {
                    dashborad.ShowDialog();
                    this.Hide();

                }
            }
            else if (verfycustomer != null)
            {
                //
                using (FrontEnd front = new FrontEnd())
                {
                    front.CustomerID = verfycustomer.CustomerID;
                    front.ShowDialog();
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("invalid username or password");
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void btn_signup_Click(object sender, EventArgs e)
        {
            using (SignUp sign = new SignUp())
            {
                this.Hide();

                sign.ShowDialog();

            }
        }
    }
}
