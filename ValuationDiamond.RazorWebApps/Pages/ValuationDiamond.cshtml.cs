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

        public void OnDelete()
        {
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
