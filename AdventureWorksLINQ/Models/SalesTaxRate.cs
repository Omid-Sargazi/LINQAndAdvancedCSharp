﻿using System;
using System.Collections.Generic;

namespace AdventureWorksLINQ.Models;

/// <summary>
/// Tax rate lookup table.
/// </summary>
public partial class SalesTaxRate
{
    /// <summary>
    /// Primary key for SalesTaxRate records.
    /// </summary>
    public int SalesTaxRateId { get; set; }

    /// <summary>
    /// State, province, or country/region the sales tax applies to.
    /// </summary>
    public int StateProvinceId { get; set; }

    /// <summary>
    /// 1 = Tax applied to retail transactions, 2 = Tax applied to wholesale transactions, 3 = Tax applied to all sales (retail and wholesale) transactions.
    /// </summary>
    public byte TaxType { get; set; }

    /// <summary>
    /// Tax rate amount.
    /// </summary>
    public decimal TaxRate { get; set; }

    /// <summary>
    /// Tax rate description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual StateProvince StateProvince { get; set; } = null!;
}
