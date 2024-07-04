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

namespace ValuDia.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wValuationDiamond.xaml
    /// </summary>
    public partial class wValuationDiamond : Window
    {
        private readonly ValuationDiamondBusiness _business;
        public wValuationDiamond()
        {
            InitializeComponent();
            this._business = new ValuationDiamondBusiness();
            this.LoadGrdValuates();
        }

        private async void LoadGrdValuates()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdValuate.ItemsSource = result.Data as List<ValuateDiamond>;
            }
            else
            {
                grdValuate.ItemsSource = new List<ValuateDiamond>();
            }
        }

        private async  void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtID.Text == "")
                {
                    txtID.Text = "0";
                }
                var item = await _business.GetById(int.Parse(txtID.Text));

                if (item.Data == null)
                {
                    
                    var valuate = new ValuateDiamond()
                    {
                       
                        Price = int.Parse(txtPrice.Text),
                        Time = DateTime.Parse(txtTime.Text),
                        ValuationStaffName = txtStaffName.Text,
                        OrderDetailId = int.Parse(txtODID.Text),

                    };

                    var result = await _business.Save(valuate);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var valuate = item.Data as ValuateDiamond;
                    //currency.CurrencyCode = txtCurrencyCode.Text;
                   
                    valuate.Price = int.Parse(txtPrice.Text);
                    valuate.Time = DateTime.Parse(txtTime.Text);
                    valuate.ValuationStaffName = txtStaffName.Text;
                    valuate.OrderDetailId = int.Parse(txtODID.Text);

                    var result = await _business.Update(valuate.ValuateDiamondId, valuate);
                    MessageBox.Show(result.Message, "Update");
                }      
                txtPrice.Text = string.Empty;
                txtTime.Text = string.Empty;
                txtStaffName.Text = string.Empty;
                txtODID.Text = string.Empty;
                this.LoadGrdValuates();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtStaffName.Text = string.Empty;
            txtTime.Text = string.Empty;
            txtODID.Text = string.Empty;
        }

        private async void grdValuate_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as ValuateDiamond;
                    if (item != null)
                    {
                        var ValuateResult = await _business.GetById(item.ValuateDiamondId);

                        if (ValuateResult.Status > 0 && ValuateResult.Data != null)
                        {
                            item = ValuateResult.Data as ValuateDiamond;
                            txtID.Text = item.ValuateDiamondId.ToString();
                            txtPrice.Text = item.Price.ToString();
                            txtStaffName.Text = item.ValuationStaffName;
                            txtTime.Text = item.Time.ToString();
                            txtODID.Text = item.OrderDetailId.ToString();
                        }
                    }
                }
            }
        }

        private async void grdValuate_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string currencyCode = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(currencyCode))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteByID(int.Parse(currencyCode));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdValuates();
                }
            }
        }

    }


}
