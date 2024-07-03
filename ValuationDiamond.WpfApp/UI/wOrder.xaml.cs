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
    /// Interaction logic for wOrder.xaml
    /// </summary>
    public partial class wOrder : Window
    {
        private readonly OrderBusiness _orderBusiness;

        public wOrder()
        {
            InitializeComponent();
            this._orderBusiness ??= new OrderBusiness();
            this.LoadGrdOrders();
            txtOrderId.IsEnabled = false;

        }

        private async void LoadGrdOrders()
        {
            var result = await _orderBusiness.GetAllOrders();

            if (result.Status > 0 && result.Data != null)
            {
                grdOrder.ItemsSource = result.Data as List<Order>;
            }
            else
            {
                grdOrder.ItemsSource = new List<Order>();
            }
        }

        private async void grdOrder_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Order;
                    if (item != null)
                    {
                        var orderResult = await _orderBusiness.ReadOrder(item.OrderId);

                        if (orderResult.Status > 0 && orderResult.Data != null)
                        {
                            item = orderResult.Data as Order;
                            txtOrderId.Text = item.OrderId.ToString();
                            txtPayStatus.Text = item.PayStatus.ToString();
                            txtStatus.Text = item.Status.ToString();
                            txtPayment.Text = item.Payment.ToString();
                            txtQuantity.Text = item.Quantity.ToString();
                            txtDay.Text = item.Day.ToString();
                            txtTotalAmount.Text = item.TotalAmount.ToString();
                            txtCustomerId.Text = item.CustomerId.ToString();
                        }
                    }
                }
            }
        }

        private async void grdOrder_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string orderId = btn.CommandParameter.ToString();

            //MessageBox.Show(orderId);

            if (!string.IsNullOrEmpty(orderId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _orderBusiness.DeleteOrder(int.Parse(orderId));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdOrders();
                }
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idTmp = -1;
                int.TryParse(txtOrderId.Text, out idTmp);

                if (txtOrderId.Text == string.Empty)
                {
                    txtOrderId.Text = "0";
                }
                var item = await _orderBusiness.ReadOrder(int.Parse(txtOrderId.Text));

                if (item.Data == null)
                {
                    var order = new Order()
                    {
                        //OrderId = idTmp,
                        PayStatus = txtPayStatus.Text=="",
                        Status = txtStatus.Text,
                        Payment = txtPayment.Text,
                        Day = DateTime.Parse(txtDay.Text),
                        Quantity = int.Parse(txtQuantity.Text),
                        TotalAmount = double.Parse(txtTotalAmount.Text),
                        CustomerId = int.Parse(txtCustomerId.Text)
                    };

                    var result = await _orderBusiness.CreateOrder(order);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var order = item.Data as Order;
                    //order.OrderId = int.Parse(txtOrderId.Text);
                    order.PayStatus = txtPayStatus.Text=="";
                    order.Status = txtStatus.Text;
                    order.Payment = txtPayment.Text;
                    order.Day = DateTime.Parse(txtDay.Text);
                    order.Quantity = int.Parse(txtQuantity.Text);
                    order.TotalAmount = double.Parse(txtTotalAmount.Text);
                    order.CustomerId = int.Parse(txtCustomerId.Text);

                    var result = await _orderBusiness.UpdateOrder(order);
                    MessageBox.Show(result.Message, "Update");
                }

                txtOrderId.Text = string.Empty;
                txtPayStatus.Text = string.Empty;
                txtPayment.Text = string.Empty;
                txtStatus.Text = string.Empty;
                txtDay.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtTotalAmount.Text = string.Empty;
                txtCustomerId.Text = string.Empty;

                this.LoadGrdOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtOrderId.Text = string.Empty;
            txtPayStatus.Text = string.Empty;
            txtPayment.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtDay.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            txtCustomerId.Text = string.Empty;
        }

        private void grdOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
