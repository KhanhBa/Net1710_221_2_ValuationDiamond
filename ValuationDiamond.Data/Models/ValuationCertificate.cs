﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ValuationDiamond.Data.Models;

public partial class ValuationCertificate
{
    public int ValuationCertificateId { get; set; }

    public double Price { get; set; }

    public string Status { get; set; }

    public DateTime Day { get; set; }

    public string Description { get; set; }

    public string Sign { get; set; }

    public string ManagerName { get; set; }

    public string CustomerName { get; set; }

    public int? ValuateDiamondId { get; set; }

    public string CustomerEmail { get; set; }

    public virtual ValuateDiamond ValuateDiamond { get; set; }
}