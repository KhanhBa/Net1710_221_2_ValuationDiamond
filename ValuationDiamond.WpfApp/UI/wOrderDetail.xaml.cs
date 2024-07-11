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
    public partial class wOrderDetail : Window
    {
        private readonly OrderDetailBusiness _business;
        public wOrderDetail()
        {
            InitializeComponent();
            this._business ??= new OrderDetailBusiness();
            this.LoadGrdCurrencies();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await _business.GetById(int.Parse(txtOrderDetailId.Text));

                if (item.Data == null)
                {
                    var orderDetail = new OrderDetail()
                    {
                        DetailCode = txtDetailCode.Text,
                        EstimateLength = int.Parse(txtEstimateLength.Text),
                        Description = txtDescription.Text,
                        OrderId = int.Parse(txtOrderId.Text) ,
                        Price = double.Parse(txtPrice.Text),
                        ServiceId = int.Parse(txtServiceId.Text),
                        Status = txtStatus.Text
                    };

                    var result = await _business.Save(orderDetail);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderDetailId = int.Parse(txtOrderDetailId.Text),
                        Description = txtDescription.Text,
                        DetailCode = txtDetailCode.Text,
                        EstimateLength = double.Parse(txtEstimateLength.Text) ,
                        OrderId = int.Parse(txtOrderId.Text),
                        Status = txtStatus.Text,
                        ServiceId = int.Parse(txtServiceId.Text) ,
                        Price = double.Parse(txtPrice.Text) ,

                    };

                    var result = await _business.Update(orderDetail);
                    MessageBox.Show(result.Message, "Update");
                }

                ClearFields();
                LoadGrdCurrencies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateNewCustomer();
        }

        private void CreateNewCustomer()
        {
            ClearFields();

        }


        private async void grdOrderDetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int customerId = (int)btn.CommandParameter;

            if (customerId > 0)
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(customerId);
                    MessageBox.Show($"{result.Message}", "Delete");
                    LoadGrdCurrencies();
                }
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private async void grdOrderDetail_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as OrderDetail;
                    if (item != null)
                    {
                        txtOrderDetailId.Text = item.OrderDetailId.ToString();
                        txtDescription.Text = item.Description;
                        txtDetailCode.Text = item.DetailCode;
                        txtEstimateLength.Text = item.EstimateLength.ToString();
                        txtOrderId.Text = item.OrderId.ToString();
                        txtStatus.Text = item.Status;
                        txtPrice.Text = item.Price.ToString();
                        txtServiceId.Text =item.ServiceId.ToString();
                    
                    }
                }
            }
        }

        private async void grdOrderDetail_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            {
                //MessageBox.Show("Double Click on Grid");
                DataGrid grd = sender as DataGrid;
                if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
                {
                    var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                    if (row != null)
                    {
                        var item = row.Item as OrderDetail;
                        if (item != null)
                        {
                            var orderDetailResult = await _business.GetById(item.OrderDetailId);

                            if (orderDetailResult.Status > 0 && orderDetailResult.Data != null)
                            {
                                item = orderDetailResult.Data as OrderDetail;
                                txtOrderDetailId.Text = item.OrderDetailId.ToString();
                                txtDescription.Text = item.Description;
                                txtDetailCode.Text = item.DetailCode;
                                txtEstimateLength.Text = item.EstimateLength.ToString();
                                txtPrice.Text = item.Price.ToString();
                                txtServiceId.Text = item.ServiceId.ToString();
                                txtStatus.Text = item.Status;
                                txtOrderId.Text = item.OrderId.ToString();

                            }
                        }
                    }
                }
            }
        }
        private async void LoadGrdCurrencies()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
               grdOrderDetail.ItemsSource = result.Data as List<OrderDetail>;
            }
            else
            {
                grdOrderDetail.ItemsSource = new List<OrderDetail>();
            }
        }

        private void ClearFields()
        {
            txtOrderDetailId.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtDetailCode.Text = string.Empty;
            txtEstimateLength.Text = string.Empty;
            txtOrderId.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtServiceId.Text= string.Empty;
        }

    }
}
