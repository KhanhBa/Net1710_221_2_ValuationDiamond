using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuationDiamond.Business
{
    public interface IValuationDiamondResult
    {
        int Status { get; set; }
        string? Message { get; set; }
        object? Data { get; set; }
    }

    public class ValuationDiamondResult : IValuationDiamondResult
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public ValuationDiamondResult()
        {
            Status = -1;
            Message = "Action fail";
        }

        public ValuationDiamondResult(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public ValuationDiamondResult(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }

}
