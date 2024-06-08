using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using ValuationDiamond.Data.DAO;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wCertiticateValuation.xaml
    /// </summary>
    public partial class wCertiticateValuation : Window
    {
        private readonly ValuationCertificateBusiness _certificateBusiness;
        public wCertiticateValuation()
        {
            InitializeComponent();
            this._certificateBusiness ??= new ValuationCertificateBusiness();
            this.LoadGrdValuationCertificates();
        }
        private async void LoadGrdValuationCertificates()
        {
            var result = await _certificateBusiness.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdValuationCertificate.ItemsSource = result.Data as List<ValuationCertificate>;
            }
            else
            {
                grdValuationCertificate.ItemsSource = new List<ValuationCertificate>();
            }
        }


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtCertificateValuationId.Text = string.Empty;
            txtCertificateValuationPrice.Text = string.Empty;
            txtDay.Text = string.Empty;
            txtvalueid.Text = string.Empty;
            txtdes.Text = string.Empty;
            chkIsActive.Text = string.Empty;
        }

        private async void grdCertificateValuation_ButtonDelete_Click(object sender, RoutedEventArgs e) {
            Button btn = (Button)sender;

            string Id = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(Id))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _certificateBusiness.DeleteByID(int.Parse(Id));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdValuationCertificates();
                }
            }
        }

        private async void grdCertificateValuation_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as ValuationCertificate;
                    if (item != null)
                    {
                        var currencyResult = await _certificateBusiness.GetbyId(item.ValuationId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as ValuationCertificate;
                            txtCertificateValuationId.Text = item.ValuationId.ToString();
                            txtCertificateValuationPrice.Text = item.Price.ToString();
                            txtDay.Text = item.Day.ToString();
                            txtvalueid.Text = item.ValuateDiamondId.ToString();
                            txtdes.Text = item.Description.ToString();
                            chkIsActive.Text = item.Status.ToString();
                        }
                    }
                }
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = -1;
                if (txtCertificateValuationId.Text == "")
                {
                     id = -1;
                }
                else
                {
                    id = int.Parse(txtCertificateValuationId.Text);
                }
                var item = await _certificateBusiness.GetbyId(id);

                if (item.Data == null)
                {
                    var valuationCertificate = new ValuationCertificate()
                    {
                        Price = double.Parse(txtCertificateValuationPrice.Text),
                        Day = DateTime.Parse(txtDay.Text),
                        Status = chkIsActive.Text,
                        Description = txtdes.Text,
                        ValuateDiamondId = int.Parse(txtvalueid.Text),
                    };

                    var result = await _certificateBusiness.Create(valuationCertificate);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var valuationCertificate = item.Data as ValuationCertificate;
                    valuationCertificate.ValuationId = id;
                    valuationCertificate.Price = double.Parse(txtCertificateValuationPrice.Text);
                    valuationCertificate.Day = DateTime.Parse(txtDay.Text);
                    valuationCertificate.Status = chkIsActive.Text;
                    valuationCertificate.Description = txtdes.Text;
                    valuationCertificate.ValuateDiamondId = int.Parse(txtvalueid.Text);

                    var result = await _certificateBusiness.Save(valuationCertificate);
                    MessageBox.Show(result.Message, "Update");
                }

                txtCertificateValuationId.Text = string.Empty;
                txtCertificateValuationPrice.Text = string.Empty;
                txtDay.Text = string.Empty;
                txtvalueid.Text = string.Empty;
                txtdes.Text = string.Empty;
                chkIsActive.Text = string.Empty;

                this.LoadGrdValuationCertificates();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

        }

        private async void grdValuationCertificate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as ValuationCertificate;
                    if (item != null)
                    {
                        var currencyResult = await _certificateBusiness.GetbyId(item.ValuateDiamondId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as ValuationCertificate;
                            txtCertificateValuationId.Text = item.ValuateDiamondId.ToString();
                            txtCertificateValuationPrice.Text = item.Price.ToString();
                            txtDay.Text = item.Day.ToString();
                            txtvalueid.Text = item.ValuationId.ToString();
                            txtdes.Text = item.Description.ToString();
                            chkIsActive.Text = item.Status.ToString();
                        }
                    }
                }
            }
        }
    }
}
