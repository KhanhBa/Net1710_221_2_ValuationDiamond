using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ValuationDiamond.Business;
using ValuationDiamond.Data.Models;

namespace ValuationDiamond.RazorWebApp.Pages
{
    public class ValuationDiamondModel : PageModel
    {

        private readonly ValuationDiamondBusiness _valuateBusiness = new ValuationDiamondBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public ValuateDiamond ValuateDiamond { get; set; } = default;
        public List<ValuateDiamond> ValuateDiamonds { get; set; } = new List<ValuateDiamond>();

        public void OnGet()
        {
            ValuateDiamonds = this.GetCurrencies();
        }

        public void OnPost()
        {
            this.SaveCurrency();
        }

       

        public void OnDelete(int diamondId) // Delete function
        {
            if (diamondId > 0)
            {
                var deleteResult = _valuateBusiness.DeleteByID(diamondId);

                if (deleteResult != null)
                {
                    this.Message = deleteResult.Result.Message;
                }
                else
                {
                    this.Message = "Error deleting diamond";
                }
            }
            else
            {
                this.Message = "Invalid diamond ID";
            }
        }

        private void AddValuationDiamond(ValuateDiamond diamond)
        {
            var saveResult = _valuateBusiness.Save(diamond);

            if (saveResult != null)
            {
                this.Message = saveResult.Result.Message;
                // Optionally, reload the list of diamonds after successful add
                ValuateDiamonds = this.GetCurrencies();
            }
            else
            {
                this.Message = "Error adding diamond";
            }
        }

        private List<ValuateDiamond> GetCurrencies()
        {
            var currencyResult = _valuateBusiness.GetAll();

            if (currencyResult.Status > 0 && currencyResult.Result.Data != null)
            {
                var currencies = (List<ValuateDiamond>)currencyResult.Result.Data;
                return currencies;
            }
            return new List<ValuateDiamond>();
        }
        private void UpdateValuationDiamond(ValuateDiamond diamond)
        {
            var saveResult = _valuateBusiness.Update(diamond.ValuateDiamondId, diamond);

            if (saveResult != null)
            {
                this.Message = saveResult.Result.Message;
                // Optionally, reload the list of diamonds after successful update
                ValuateDiamonds = this.GetCurrencies();
            }
            else
            {
                this.Message = "Error updating diamond";
            }
        }
        private void SaveCurrency()
        {
            var ValuationResult = _valuateBusiness.Save(this.ValuateDiamond);

            if (ValuationResult != null)
            {
                this.Message = ValuationResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
    }
}
