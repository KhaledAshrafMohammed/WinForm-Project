using Autofac;
using Eldokkan.pl;
using ELDOKKAN.Application.Services;
using ELDOKKAN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PL
{
    public partial class Dashborad : Form
    {

        public Dashborad()
        {
            InitializeComponent();
            bindingSource = new BindingSource();
            bindingadmin = new BindingSource();
            bindingcategory = new BindingSource();
            bindingCustomer = new BindingSource();

            Navigator = new BindingNavigator();
            bindingSource1 = new BindingSource();

            //Product TAB Dashboards
            IProService = ProductContainer.Resolve<IProductService>();
            admin = autoFac.Resolve<IAdminService>();
             category = autoFac.Resolve<ICategoryService>();
              order = autoFac.Resolve<IOrderService>();
              customer = autoFac.Resolve<ICustomerService>();
             orderdetails = autoFac.Resolve<IOrderDetailsService>();

            ProductBindSource = new BindingSource();
        }
        BindingSource bindingadmin;
        BindingSource bindingcategory;
        BindingSource bindingCustomer;
        BindingSource bindingSource;
        Autofac.IContainer autoFac = AutoFac.Inject();

        BindingNavigator Navigator;
        BindingSource bindingSource1;
        IAdminService admin;
        ICategoryService category;
        IOrderService order;
            ICustomerService customer;
        IOrderDetailsService orderdetails;


        //Product TAB Dashboards
        Autofac.IContainer ProductContainer = AutoFac.Inject();
        Autofac.IContainer cat = AutoFac.Inject();

        IProductService IProService;
        BindingSource ProductBindSource;
        List<GetAllProductDTO> ProductList;

        private bool IsValidPassword(string password)
        {
            // Adjust the regex pattern to match your password requirements
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            var regex = new Regex(passwordPattern);
            return regex.IsMatch(password);
        }
        private void tb_password_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidPassword(tb_password.Text))
                {
                    // MessageBox.Show("Password must be at least 8 characters long, include uppercase letters, numbers, and special characters.");
                    guna2HtmlLabel3.Text = "Password must be at least 8 characters long, include uppercase , numbers, special characters.";
                    guna2HtmlLabel3.ForeColor = Color.Red;
                    tb_password.Focus();
                }
                else
                {
                    guna2HtmlLabel3.Text = "Password";
                    guna2HtmlLabel3.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tb_repassword_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (tb_password.Text != tb_repassword.Text)
                {
                    //MessageBox.Show("Passwords do not match.");
                    guna2HtmlLabel4.Text = "Passwords do not match.";
                    guna2HtmlLabel4.ForeColor = (Color.Red);
                    guna2HtmlLabel4.Focus();
                }
                else
                {
                    guna2HtmlLabel4.Text = "renter Password";
                    guna2HtmlLabel4.ForeColor = (Color.Black);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            var regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }
        private void tb_email_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidEmail(tb_email.Text))
                {
                    //  MessageBox.Show("Please enter a valid email address.");
                    guna2HtmlLabel2.Text = "Please enter a valid email address.";
                    guna2HtmlLabel2.ForeColor = Color.Red;
                    tb_email.Focus();
                }
                else
                {
                    guna2HtmlLabel2.Text = "Email";
                    guna2HtmlLabel2.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool ValidateName(string name)
        {
            string NamePattern = @"^[A-Za-z\s]{3,50}$";

            Regex regex = new Regex(NamePattern);

            return regex.IsMatch(name);
        }
        private void tb_username_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (ValidateName(tb_username.Text))
                {
                    guna2HtmlLabel1.Text = "Full Name";
                    guna2HtmlLabel1.ForeColor = Color.Black;
                }
                else
                {
                    tb_username.BackColor = Color.LightCoral;
                    guna2HtmlLabel1.Text = "Enter Vaild Name";
                    guna2HtmlLabel1.ForeColor = Color.Red;
                    tb_username.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_saveAdmin_Click(object sender, EventArgs e)
        {
            try
            {

                var result = admin.GetAllAdmins().Where(p => p.Name == tb_username.Text).ToList();
                if (result.Count > 0)
                {
                    MessageBox.Show("user name already exist try another one");
                }
                else
                {
                    var re = admin.AddAdmin(new CreateAdminDTO()
                    {
                        Name = tb_username.Text,
                        Email = tb_email.Text,
                        Password = PasswordHasher.HashPassword(tb_password.Text),
                        //Password = EncryptionHelper.Encrypt(tb_password.Text),
                    });
                    MessageBox.Show("Record Inserted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Dashborad_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        void BindControls()
        {
            // tb.DataBindings.Add("Text", bindingSource, "TitleId");
            if (tb_useradminUP.DataBindings.Count == 0)
            {


                tb_useradminUP.DataBindings.Add("Text", bindingadmin, "Name");
                tb_passwordadmin.DataBindings.Add("Text", bindingadmin, "Password");

            }
        }
        private void tabControl3_Click(object sender, EventArgs e)
        {
            try
            {
                bindingadmin.DataSource = admin.GetAllAdmins().ToList();
                Navigator = new BindingNavigator(bindingadmin);

                BindControls();
                Navigator.Dock = DockStyle.Right;

                this.Controls.Add(Navigator);
                bindingadmin.DataSource = admin.GetAllAdmins();
                bindingadmin.ResetBindings(false);
                //  dtgv_admin.Columns[]
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Dashborad_Load(object sender, EventArgs e)
        {


        }

        private void btn_updateAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                // UpdateAdminDTO update= new UpdateAdminDTO() { AdminID= bindingSource.Current };
                if (bindingadmin.Current is GetAllAdminDTO dTO)
                {
                    admin.UpdateAdmin(dTO.AdminID, dTO);

                    MessageBox.Show("Updeted Successfuly");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_deleteadmin_Click(object sender, EventArgs e)
        {
            try
            {
                if (bindingadmin.Current is GetAllAdminDTO dTO)
                {
                    admin.DeleteAdmin(dTO.AdminID);
                    MessageBox.Show("Deleted Successfuly");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {
            try
            {
                bindingcategory.DataSource = category.GetAllCategories().ToList();
                Navigator = new BindingNavigator(bindingcategory);
                Navigator.Dock = DockStyle.Right;

                this.Controls.Add(Navigator);
                if (tb_category.DataBindings.Count == 0)
                {
                    tb_category.DataBindings.Add("Text", bindingcategory, "CategoryName");
                    tb_description.DataBindings.Add("Text", bindingcategory, "Description");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_saveCategory_Click(object sender, EventArgs e)
        {
            try
            {
 
                category.AddCategory(new CreateCategoryDTO()
                {
                    CategoryName = tb_category.Text,
                    Description = tb_category.Text,
                });
                MessageBox.Show("inserted Successfuly!");
                bindingcategory.DataSource = category.GetAllCategories();
                bindingcategory.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_updatecategory_Click(object sender, EventArgs e)
        {
            try
            {
                 if (bindingcategory.Current is GetAllCategoryDTO dTO)
                {
                    category.UpdateCategory(dTO.CategoryID, dTO);
                    MessageBox.Show("Updated Successfuly!");
                    bindingcategory.DataSource = category.GetAllCategories();
                    bindingcategory.ResetBindings(false);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_deletecategory_Click(object sender, EventArgs e)
        {
             if (bindingcategory.Current is GetAllCategoryDTO dTO)
            {
                category.DeleteCategory(dTO.CategoryID);
                MessageBox.Show("Delted Successfuly!");
                bindingcategory.DataSource = category.GetAllCategories();

                bindingcategory.ResetBindings(false);


            }
        }


        private void btn_CategorySearch_Click(object sender, EventArgs e)
        {
            try
            {
                  bindingcategory.DataSource = category.GetAllCategories()
                                                        .Where(p => p.CategoryName.ToLower().Contains(tb_CategorySearch.Text.ToLower()) ||
                                                        p.Description.ToLower().Contains(tb_CategorySearch.Text.ToLower())).ToList();
                dtgv_categorySearch.DataSource = bindingcategory;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabControl4_Click(object sender, EventArgs e)
        {
            try
            {
  
                bindingcategory.DataSource = category.GetAllCategories();
                var data = category.GetAllCategories();
                if (dtgv_categorySearch.DataSource == null)
                    dtgv_categorySearch.DataSource = bindingcategory;
                // 011 58 99 19 67
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage10_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                 
                bindingSource.DataSource = order.GetAllOrders().ToList();
                Navigator = new BindingNavigator(bindingSource);
                Navigator.Dock = DockStyle.Right;

                this.Controls.Add(Navigator);
                if (tb_orderID.DataBindings.Count == 0)
                {
                    guna2DateTimePicker2.DataBindings.Add("Text", bindingSource, "OrderDate");
                    tb_orderID.DataBindings.Add("Text", bindingSource, "OrderID");
                    cb_customer.DataSource = customer.GetAllCustomers().Select(p => new { Name = p.Name, CustomerID = p.CustomerID }).ToList();
                    cb_customer.DisplayMember = "Name";
                    cb_customer.ValueMember = "CustomerID";
                    cb_customer.DataBindings.Add("SelectedValue", bindingSource, "CustomerID");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_orderDelete_Click(object sender, EventArgs e)
        {
            try
            {
 
                if (bindingSource.Current is GetAllOrderDTO dTO)
                {
                    //var id = orderdetails.GetAllOrderDetails().Where(d => d.OrderID == dTO.OrderID).Select(d=>d.);
                    // orderdetails.DeleteOrderDetails();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            try
            {
                 bindingSource.DataSource = order.GetAllOrders();
                if (dtgv_categorySearch.DataSource == null)
                    dtgv_categorySearch.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }
        //customer=====================================================
        private void btn_saveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
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
                bindingCustomer.DataSource = customer.GetAllCustomers();

                bindingCustomer.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_updateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                 if (bindingCustomer.Current is GetAllCustomerDTO dTO)
                {
                    customer.UpdateCustomer(dTO.CustomerID, dTO);
                    MessageBox.Show("Updated Successfuly");
                    bindingCustomer.DataSource = customer.GetAllCustomers();

                    bindingCustomer.ResetBindings(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                  bindingCustomer.DataSource = customer.GetAllCustomers().ToList();
                Navigator = new BindingNavigator(bindingCustomer);
                Navigator.Dock = DockStyle.Right;

                this.Controls.Add(Navigator);
                if (tb_name.DataBindings.Count == 0)
                {
                    tb_name.DataBindings.Add("Text", bindingCustomer, "Name");
                    guna2TextBox2.DataBindings.Add("Text", bindingCustomer, "Password");
                    tb_PostalCode.DataBindings.Add("Text", bindingCustomer, "PostalCode");
                    tb_phone.DataBindings.Add("Text", bindingCustomer, "Phone");
                    guna2TextBox3.DataBindings.Add("Text", bindingCustomer, "Email");
                    tb_address.DataBindings.Add("Text", bindingCustomer, "Address");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_deleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                 if (bindingCustomer.Current is GetAllCustomerDTO dTO)
                {
                    customer.DeleteCustomer(dTO.CustomerID);
                    MessageBox.Show("Deleted Successfuly!");
                    bindingCustomer.DataSource = customer.GetAllCustomers();

                    bindingCustomer.ResetBindings(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage13_Click(object sender, EventArgs e)
        {
            try
            {   // tb_customerSearch
                 bindingCustomer.DataSource = customer.GetAllCustomers();
                guna2DataGridView2.DataSource = bindingCustomer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                  bindingCustomer.DataSource = customer.GetAllCustomers()
                                                        .Where(p => p.Name.ToLower().Contains(tb_customerSearch.Text.ToLower()) ||
                                                        p.Address.ToLower().Contains(tb_customerSearch.Text.ToLower()) ||
                                                        p.Email.ToLower().Contains(tb_customerSearch.Text.ToLower()) ||
                                                        p.Phone.ToLower().Contains(tb_customerSearch.Text.ToLower()) ||
                                                        p.PostalCode.ToLower().Contains(tb_customerSearch.Text.ToLower())
                                                        ).ToList();
                guna2DataGridView2.DataSource = bindingCustomer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //==============================================
        //Product TAB DAshboard
        private void ProductTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                ICategoryService ICatService = cat.Resolve<ICategoryService>();
                BahgatcategBox.DataSource = ICatService.GetAllCategories().ToList();
                BahgatcategBox.DisplayMember = "CategoryName";
                BahgatcategBox.ValueMember = "CategoryID";

                ProductList = IProService.GetAllProducts().ToList();
                ProductBahgatGridView.DataSource = ProductList;
                ProductBahgatGridView.ReadOnly = true;

                ProductBindSource.DataSource = ProductBahgatGridView.DataSource;

                BahgatProText.DataBindings.Clear();
                BahgatUnitsText.DataBindings.Clear();
                BahgatcategBox.DataBindings.Clear();
                BahgatIDLabel.DataBindings.Clear();
                BahgatPriceText.DataBindings.Clear();

                BahgatProText.DataBindings.Add("Text", ProductBindSource, "Name");
                BahgatUnitsText.DataBindings.Add("Text", ProductBindSource, "UnitsInStock");
                BahgatIDLabel.DataBindings.Add("Text", ProductBindSource, "ProductID");
                BahgatPriceText.DataBindings.Add("Text", ProductBindSource, "UnitPrice");

                BahgatcategBox.DataBindings.Add("SelectedValue", ProductBindSource, "CategoryID");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void moveNext_Click(object sender, EventArgs e)
        {
            ProductBindSource.MoveNext();
        }

        private void movePrev_Click(object sender, EventArgs e)
        {
            ProductBindSource.MovePrevious();
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                bool parsing = int.TryParse(BahgatIDLabel.Text.ToString(), out int proID);

                if (parsing == true)
                {
                    IProService.DeleteProduct(proID);
                    MessageBox.Show("Deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                CreateProductDTO dto = new CreateProductDTO();
                dto.UnitPrice = decimal.Parse(BahgatUnitsText.Text);
                dto.Name = BahgatProText.Text;
                dto.UnitsInStock = int.Parse(BahgatUnitsText.Text);
                dto.CategoryID = int.Parse(BahgatcategBox.SelectedValue.ToString());
                this.Text = BahgatcategBox.SelectedValue.ToString();

                IProService.AddProduct(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {


                bool parsing = int.TryParse(BahgatIDLabel.Text.ToString(), out int proID);

                if (parsing == true)
                {
                    var product = ProductList.Where(p => p.ProductID == proID).FirstOrDefault();
                    IProService.UpdateProduct(proID, product);
                    MessageBox.Show("Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            BahgatProText.Clear();
            BahgatUnitsText.Clear();
            BahgatIDLabel.Text = " ";
            BahgatPriceText.Clear();
            Bahgataddbtn.Visible = true;


            BahgatProText.DataBindings.Clear();
            BahgatUnitsText.DataBindings.Clear();
            BahgatcategBox.DataBindings.Clear();
            BahgatIDLabel.DataBindings.Clear();
            BahgatPriceText.DataBindings.Clear();

        }

        //==============================================
    }
}
