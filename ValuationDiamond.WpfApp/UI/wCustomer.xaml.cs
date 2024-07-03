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
using System.Windows.Shapes;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wCustomer.xaml
    /// </summary>
    public partial class wCustomer : Window
    {
        private readonly CustomerBusiness _business;
        public wCustomer()
        {
            InitializeComponent();
            this._business ??= new CustomerBusiness();
            this.LoadGrdCustomers();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await _business.GetCustomer(int.Parse(txtCustomerId.Text));

                if (item.Data == null)
                {
                    var customer = new Customer()
                    {
                        CustomerId = int.Parse(txtCustomerId.Text),
                        Name = txtCustomerName.Text,
                        Cccd = txtCccd.Text,
                        Email = txtEmail.Text,
                        Password = txtPassword.Text,
                        Status = chkStatus.IsChecked ?? false,
                        DoB = dpDoB.SelectedDate ?? DateTime.Now,
                        Address = txtAddress.Text
                    };

                    var result = await _business.SaveCustomer(customer);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var customer = new Customer()
                    {
                        CustomerId = int.Parse(txtCustomerId.Text),
                        Name = txtCustomerName.Text,
                        Cccd = txtCccd.Text,
                        Email = txtEmail.Text,
                        Password = txtPassword.Text,
                        Status = chkStatus.IsChecked ?? false,
                        DoB = dpDoB.SelectedDate ?? DateTime.Now,
                        Address = txtAddress.Text
                    };

                    var result = await _business.UpdateCustomer(customer);
                    MessageBox.Show(result.Message, "Update");
                }

                ClearFields();
                LoadGrdCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int customerId = (int)btn.CommandParameter;

            if (customerId > 0)
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteCustomer(customerId);
                    MessageBox.Show($"{result.Message}", "Delete");
                    LoadGrdCustomers();
                }
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private async void grdCustomer_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Customer;
                    if (item != null)
                    {
                        txtCustomerId.Text = item.CustomerId.ToString();
                        txtCustomerName.Text = item.Name;
                        txtCccd.Text = item.Cccd;
                        txtEmail.Text = item.Email;
                        txtPassword.Text = item.Password;
                        chkStatus.IsChecked = item.Status;
                        dpDoB.SelectedDate = item.DoB;
                        txtAddress.Text = item.Address;
                    }
                }
            }
        }

        private async void LoadGrdCustomers()
        {
            var result = await _business.GetAllCustomer();

            if (result.Status > 0 && result.Data != null)
            {
                grdCustomer.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                grdCustomer.ItemsSource = new List<Customer>();
            }
        }

        private void ClearFields()
        {
            txtCustomerId.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtCccd.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            chkStatus.IsChecked = false;
            dpDoB.SelectedDate = null;
            txtAddress.Text = string.Empty;
        }
    }
}






