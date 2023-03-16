using SupportWPF.Models;
using SupportWPF.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace SupportWPF.Views
{
    /// <summary>
    /// Interaction logic for CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : UserControl
    {
        public CreateOrder()
        {
            InitializeComponent();
            
        }

        private async void Btn_Create_Click(object sender, RoutedEventArgs e)
        {
            List<string> priorities = new()
            {
                "Low",
                "Medium",
                "High",
                "Critical"
            };
            Random r = new();
            int i = r.Next(priorities.Count);

            await OrderService.SaveAsync(new OrderRow()
            {
                Created = DateTime.Now,
                ProductName = tb_ProductName.Text,
                Subject = tb_Subject.Text,
                Priority = priorities[i],
                FirstName = tb_FirstName.Text,
                LastName = tb_LastName.Text,
                Email = tb_Email.Text,
                PhoneNumber = tb_PhoneNumber.Text,
                StreetName = tb_StreetName.Text,
                StreetNumber = tb_StreetNumber.Text,
                PostalCode = tb_PostalCode.Text,
                City = tb_City.Text,
                Deadline = DateTime.Now.AddDays(7)
            });
            ClearText();
        }

        private void Btn_Auto_Click(object sender, RoutedEventArgs e)
        {
            tb_ProductName.Text = "Monitor";
            tb_Subject.Text = "Cracked";
            tb_FirstName.Text = "Bert";
            tb_LastName.Text = "Bertsson";
            tb_Email.Text = "bert@domain.com";
            tb_PhoneNumber.Text = "070-123 45 67";
            tb_StreetName.Text = "Bertvägen";
            tb_StreetNumber.Text = "68G";
            tb_PostalCode.Text = "123 45";
            tb_City.Text = "Bertil";
        }

        private void Tb_PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(tb_PhoneNumber.Text + e.Text, @"^\d{0,3}-?\d{0,3}\s?\d{0,2}\s?\d{0,2}$"))
            {
                if (tb_PhoneNumber.Text.Length == 3)
                {
                    tb_PhoneNumber.Text += "-";
                    tb_PhoneNumber.SelectionStart = tb_PhoneNumber.Text.Length;
                    tb_PhoneNumber.SelectionLength = 0;

                }
                if (tb_PhoneNumber.Text.Length == 7)
                {
                    tb_PhoneNumber.Text += " ";
                    tb_PhoneNumber.SelectionStart = tb_PhoneNumber.Text.Length;
                    tb_PhoneNumber.SelectionLength = 0;
                }
                if (tb_PhoneNumber.Text.Length == 10)
                {
                    tb_PhoneNumber.Text += " ";
                    tb_PhoneNumber.SelectionStart = tb_PhoneNumber.Text.Length;
                    tb_PhoneNumber.SelectionLength = 0;
                }
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void Tb_PostalCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(tb_PostalCode.Text + e.Text, @"^\d{0,3}\s?\d{0,2}$"))
            {
                if (tb_PostalCode.Text.Length == 3)
                {
                    tb_PostalCode.Text += " ";
                    tb_PostalCode.SelectionStart = tb_PostalCode.Text.Length;
                    tb_PostalCode.SelectionLength = 0;
                }
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void Tb_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(tb_Email.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void ClearText()
        {
            tb_ProductName.Text = string.Empty;
            tb_Subject.Text = string.Empty;
            tb_FirstName.Text = string.Empty;
            tb_LastName.Text = string.Empty;
            tb_Email.Text = string.Empty;
            tb_PhoneNumber.Text = string.Empty;
            tb_StreetName.Text = string.Empty;
            tb_StreetNumber.Text = string.Empty;
            tb_PostalCode.Text = string.Empty;
            tb_City.Text = string.Empty;
        }
    }
}
