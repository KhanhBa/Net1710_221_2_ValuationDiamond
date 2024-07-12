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
            //try
            //{
            //    var item = await _business.GetById(txtOrderdetailId.Text);

            //    if (item.Data == null)
            //    {
            //        var orderDetail = new OrderDetail();
            //        {
            //            OrderDetailId = txtOrderdetailId.Text;
            //            ServiceId = txtServiceId.Text;
            //            Status = txtStatus.Text;
            //            Price = txtPrice.Text;
            //            OrderId = txtOrderId.Text;
            //            CurrencyCode = txtCurrencyCode.Text,
            //            CurrencyName = txtCurrencyName.Text,
            //            NationCode = txtNationCode.Text,
            //            IsActive = chkIsActive.IsChecked
            //        };

            //        var result = await _business.Save(orderDetail);
            //        MessageBox.Show(result.Message, "Save");
            //    }
            //    else
            //    {
            //        var orderDetail = item.Data as OrderDetail;
            //        //currency.CurrencyCode = txtCurrencyCode.Text;
            //        orderDetail.OrderDetailId = int.Parse(txtOrderdetailId.Text);
            //        orderDetail.ServiceId = int.Parse(txtServiceId.Text);
            //        orderDetail.Status = txtStatus.Text;
            //        orderDetail.Price = double.Parse(txtPrice.Text) ;
            //        orderDetail.OrderId = int.Parse(txtOrderId.Text);

            //        var result = await _business.Update(orderDetail);
            //        MessageBox.Show(result.Message, "Update");
            //    }

            //    txtOrderdetailId.Text = string.Empty;
            //    txtServiceId.Text = string.Empty;
            //    txtPrice.Text = string.Empty;
            //    txtStatus.Text = string.Empty;
            //    txtOrderId.Text = string.Empty;
            //    this.LoadGrdCurrencies();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "Error");
            //}
        }

        private async void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void grdCustomer_MouseDouble_Click(object sender, RoutedEventArgs e)
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
                                txtOrderdetailId.Text = item.OrderDetailId.ToString();
                                txtServiceId.Text = item.ServiceId.ToString();
                                txtStatus.Text = item.Status;
                                txtPrice.Text = item.Price.ToString();
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
                grdCustomer.ItemsSource = result.Data as List<OrderDetail>;
            }
            else
            {
                grdCustomer.ItemsSource = new List<OrderDetail>();
            }
        }

    }
}
