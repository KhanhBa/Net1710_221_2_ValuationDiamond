﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ValuationDiamond.Data.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public string Cccd { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public bool? Status { get; set; }

    public DateTime? DoB { get; set; }

    public string Address { get; set; }

    public string Avatar { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}