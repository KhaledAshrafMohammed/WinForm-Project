using Autofac;
using Eldokkan.pl;
using ELDOKKAN.Application.DTOs.Customer;
using ELDOKKAN.Application.Services;
using ELDOKKAN.Models;
using Guna.Charts.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IContainer = Autofac.IContainer;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PL
{

    public partial class FrontEnd : Form
    {
        IContainer Container = AutoFac.Inject();
        IProductService productservice;
        IOrderService OrderService;
        IOrderDetailsService OrderDetsService;
        private int currentPage;
        int size;
        public int CustomerID { get; set; } = 1;



        List<GetAllProductDTO> CartList;
        BindingNavigator bindingNavigator;
        BindingSource PrdBindingSource;
        public FrontEnd()
        {
            InitializeComponent();
            CartList = new List<GetAllProductDTO>();

            productservice = Container.Resolve<IProductService>();

            OrderService = Container.Resolve<IOrderService>();

            OrderDetsService = Container.Resolve<IOrderDetailsService>();

            size = (productservice.GetAllProducts().Count() / 9) + 1;

            currentPage = 1;

        }
        private void AddToCartButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Tag is GetAllProductDTO item)
                {
                    CartList.Add(item);

                }
            }
        }

        private void FrontEnd_Load(object sender, EventArgs e)
        {
            loadElements(1);
        }

        private void loadElements(int PageNum)
        {
            foreach (var item in productservice.GetPagination(9, PageNum))
            {
                GroupBox g1 = new GroupBox();
                g1.Size = new Size(200, 100);
                g1.Padding = new Padding(20);
                g1.BackColor = Color.White;

                Label label1 = new Label();
                label1.Text = item.UnitPrice.ToString();
                label1.Location = new Point(10, 20); // Set location within the GroupBox

                Label label2 = new Label();
                label2.Text = item.Name;
                label2.Location = new Point(10, 40);

                // Create an "Add to Cart" button
                Button button1 = new Button();
                button1.Width = 178;
                button1.Text = " Add To Cart ";
                button1.Location = new Point(10, 60);
                button1.Tag = item;

                // Add event handler for the button click
                button1.Click += AddToCartButton_Click;

                // Add the controls to the GroupBox
                g1.Controls.Add(label1);
                g1.Controls.Add(label2);
                g1.Controls.Add(button1);

                // Add the GroupBox to the form or flow layout panel
                flowProLayout.Controls.Add(g1);
            }
        }


        private void LoadOneElement(string nama)
        {
            var item = productservice.GetProductByName(nama).FirstOrDefault();

            if (item != null)
            {
                GroupBox g1 = new GroupBox();
                g1.Size = new Size(200, 100);
                g1.Padding = new Padding(20);
                g1.BackColor = Color.White;

                Label label1 = new Label();
                label1.Text = item.UnitPrice.ToString();
                label1.Location = new Point(10, 20); // Set location within the GroupBox

                Label label2 = new Label();
                label2.Text = item.Name;
                label2.Location = new Point(10, 40);

                // Create an "Add to Cart" button
                Button button1 = new Button();
                button1.Width = 178;
                button1.Text = " Add To Cart ";
                button1.Location = new Point(10, 60);
                button1.Tag = item;

                // Add event handler for the button click
                button1.Click += AddToCartButton_Click;

                // Add the controls to the GroupBox
                g1.Controls.Add(label1);
                g1.Controls.Add(label2);
                g1.Controls.Add(button1);

                // Add the GroupBox to the form or flow layout panel
                flowProLayout.Controls.Add(g1);

            }

        }

        private void Front_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Front.SelectedIndex == 1) // Adjust index as needed
            {
                CardGrid.DataSource = null; // Clear the previous binding
                CardGrid.DataSource = CartList; // Set the new data source
                CardGrid.Refresh(); // Refresh to apply the new data
                CardGrid.Columns[0].Visible = false;
                CardGrid.Columns[2].Visible = false;
                CardGrid.Columns[4].Visible = false;


            }
        }


        private void Update_SelectedIndexChanged(object sender, EventArgs e)
        {



        }


        private void moveNext_Click(object sender, EventArgs e)
        {
            if (currentPage >= 1 && currentPage < size)
            {
                flowProLayout.Controls.Clear();
                currentPage++;
                loadElements(currentPage);
            }
        }

        private void movePrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                flowProLayout.Controls.Clear();
                currentPage--;
                loadElements(currentPage);
            }
        }

        private void searhBtn_Click(object sender, EventArgs e)
        {
            if (SearchText.Text != string.Empty)
            {
                string name = SearchText.Text;
                flowProLayout.Controls.Clear();
                LoadOneElement(name);
            }
        }

        private void SearchText_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchText.Text))
            {
                flowProLayout.Controls.Clear();
                loadElements(1);
            }
        }

        private void CartNext_Click(object sender, EventArgs e)
        {
            PrdBindingSource.MoveNext();
        }

        private void CartPrev_Click(object sender, EventArgs e)
        {
            PrdBindingSource.MovePrevious();
        }

        private void DeleteCart_Click(object sender, EventArgs e)
        {
            string name = this.LabelName.Text;
            var p = CartList.FirstOrDefault(x => x.Name == name);
            if (p != null)
            {
                CartList.Remove(p);
                CartDetailes.Controls.Clear();
                var caGrid = new Guna.UI2.WinForms.Guna2DataGridView();
                CartDetailes.Controls.Add(caGrid);
                caGrid.DataSource = CartList;
                caGrid.ReadOnly = true;
                caGrid.Size = CartDetailes.Size;
                caGrid.Columns[0].Visible = false;
                caGrid.Columns[2].Visible = false;
                caGrid.Columns[4].Visible = false;

                PrdBindingSource.DataSource = CartList;
                ProUpdate.Refresh();


                foreach (var c in CartList)
                {
                    Debug.WriteLine(c.Name);
                }
            }


        }

        private void Update_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (PrdBindingSource == null)
            {
                PrdBindingSource = new BindingSource(CartList, "");

                ProUpdate.DataSource = productservice.GetAllProducts().ToList();
                ProUpdate.DisplayMember = "Name";
                ProUpdate.ValueMember = "Name";
                // ProUpdate.DataBindings.Add("SelectedValue", PrdBindingSource, "Name");

                LabelName.DataBindings.Add("Text", PrdBindingSource, "Name");
                ProPrice.DataBindings.Add("Text", PrdBindingSource, "UnitPrice");


                CardGrid.Refresh();
            }

        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            // finish this code to Update

            var oldProduct = CartList.Where(p => p.Name == LabelName.Text).FirstOrDefault();

            var NewProductName = ProUpdate.SelectedValue.ToString();
            LabelName.Text = ProUpdate.SelectedValue.ToString();

            var newProduct = productservice.GetAllProducts().Where(p => p.Name == NewProductName).FirstOrDefault();

            ProPrice.Text = newProduct.UnitPrice.ToString();

            CartList.Remove(oldProduct);
            CartList.Add(newProduct);


            CardGrid.DataSource = null; // Reset DataSource to refresh DataGridView
            CardGrid.DataSource = CartList;
            CardGrid.Columns[0].Visible = false;
            CardGrid.Columns[2].Visible = false;
            CardGrid.Columns[4].Visible = false;
        }

        private void OrderDetailes_Click(object sender, EventArgs e)
        {
            int OrderID = OrderService.GetAllOrders().Where(p => p.CustomerID == CustomerID.ToString()).Select(p => p.OrderID).FirstOrDefault();

            var OrderList = CartList.GroupBy(p => p.ProductID);

            foreach (var order in OrderList)
            {
                string o = order.Select(p => p.Name).FirstOrDefault();
                int productID = productservice.GetAllProducts().Where(p => p.Name == o).Select(p => p.ProductID).FirstOrDefault();
                CreateOrderDetailsDTO DtoDetailes = new CreateOrderDetailsDTO()
                {
                    OrderStatus = OrderStatus.Processing,
                    ProductID = productID,
                    Discount = 0,
                    Quantity = 1,
                    OrderID = OrderID,
                };

                OrderDetsService.AddOrderDetails(DtoDetailes);
                MessageBox.Show("Order added");
            }


        }
        private void BillBtn_Click(object sender, EventArgs e)
        {
            decimal? price = 0;
            foreach (var item in CartList)
            {
                if (price != null)
                    price += item.UnitPrice;
            }
            DialogResult massege = MessageBox.Show($"Your Bill = {price}$ ");

            if (massege == DialogResult.OK)
            {
                CreateOrderDTO dto = new() { CustomerID = CustomerID.ToString(), OrderDate = DateTime.Now };
                OrderService.AddOrder(dto);
            }
        }

        private void FrontEnd_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }
    }
} 
