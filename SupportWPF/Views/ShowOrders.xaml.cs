using SupportWPF.Models;
using SupportWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupportWPF.Views
{
    /// <summary>
    /// Interaction logic for ShowOrders.xaml
    /// </summary>
    public partial class ShowOrders : UserControl
    {
        public ShowOrders()
        {
            InitializeComponent();
            CreateDataGridLayout();

            cb_Status.Items.Add("Not Started");
            cb_Status.Items.Add("Ongoing");
            cb_Status.Items.Add("Closed");

            
        }

        public async void CreateDataGridLayout()
        {
            DataGridTextColumn id = new()
            {
                Header = "Id",
                Binding = new Binding("Id"),
                Width = 25
            };
            DataGridTextColumn created = new()
            {
                Header = "Created",
                Binding = new Binding("Created"),
                Width = 150
            };
            DataGridTextColumn title = new()
            {
                Header = "Subject",
                Binding = new Binding("Subject"),
                Width = 150
            };
            DataGridTextColumn productName = new()
            {
                Header = "Product",
                Binding = new Binding("ProductName"),
                Width = 150
            };
            DataGridTextColumn customerName = new()
            {
                Header = "Customer",
                Binding = new Binding("DisplayName"),
                Width = 150
            };

            DataGridTextColumn priority = new()
            {
                Header = "Priority",
                Binding = new Binding("Priority"),
                Width = 150
            };

            DataGridTextColumn orderStatus = new()
            {
                Header = "Status",
                Binding = new Binding("OrderStatus"),
                Width = 150,
            };
            DataGridTextColumn deadline = new()
            {
                Header = "Deadline",
                Binding = new Binding("Deadline"),
                Width = 150
            };
            dg_Orders.Columns.Add(id);
            dg_Orders.Columns.Add(created);
            dg_Orders.Columns.Add(title);
            dg_Orders.Columns.Add(productName);
            dg_Orders.Columns.Add(customerName);
            dg_Orders.Columns.Add(priority);
            dg_Orders.Columns.Add(orderStatus);
            dg_Orders.Columns.Add(deadline);
            dg_Orders.ItemsSource = await OrderService.GetAllAsync();
        }

        private void Dg_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb_TicketId.Text = "Ticket ID: ";
            tb_CreatedOn.Text = "Created On: ";
            tb_Deadline.Text = "Time Left: ";
            tb_Subject.Text = "Subject: ";
            tb_Product.Text = "Product: ";
            tb_ArticleNumber.Text = "Product Id: ";
            tb_Name.Text = "Name: ";
            tb_Email.Text = "Email: ";
            tb_PhoneNumber.Text = "Phone number: ";
            tb_Status.Text = "Status: ";
            tb_Address.Text = "Address: ";
            tb_Priority.Text = "Priority: ";
            tb_Comment.Text = "Employee comment: ";

            foreach (OrderRow or in dg_Orders.SelectedItems)
            {
                TimeSpan ts = DateTime.Now.Subtract(or.Deadline);
                tb_TicketId.Text += or.Id.ToString();
                tb_CreatedOn.Text += or.Created.ToShortDateString();
                tb_Subject.Text += or.Subject;
                tb_ArticleNumber.Text += or.ArticleNumber;
                tb_Product.Text += or.ProductName;
                tb_Name.Text += or.DisplayName;
                tb_Email.Text += or.Email;
                tb_PhoneNumber.Text += or.PhoneNumber;
                tb_Address.Text += or.Address;
                tBox_Comment.Text = or.Comment;
                tb_Priority.Text += or.Priority;
                cb_Status.SelectedValue = or.OrderStatus;
                tb_Deadline.Text += ts.ToString("d' Days 'h' Hours 'm' Minutes'");
            }
        }

        private async void Cb_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (OrderRow or in dg_Orders.SelectedItems)
            {
                or.OrderStatus = cb_Status.SelectedValue.ToString()!;
                await OrderService.UpdateAsync(or);
            }
        }

        private async void Btn_SaveTicket_Click(object sender, RoutedEventArgs e)
        {
            foreach (OrderRow or in dg_Orders.SelectedItems)
            {
                or.Comment = tBox_Comment.Text;
                await OrderService.UpdateAsync(or);
            }
            dg_Orders.ItemsSource = await OrderService.GetAllAsync();
        }
    }
}
