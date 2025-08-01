﻿using System;
using System.Collections.Generic;

namespace AdventureWorksLINQ.Models;

/// <summary>
/// State and province lookup table.
/// </summary>
public partial class StateProvince
{
    /// <summary>
    /// Primary key for StateProvince records.
    /// </summary>
    public int StateProvinceId { get; set; }

    /// <summary>
    /// ISO standard state or province code.
    /// </summary>
    public string StateProvinceCode { get; set; } = null!;

    /// <summary>
    /// ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. 
    /// </summary>
    public string CountryRegionCode { get; set; } = null!;

    /// <summary>
    /// 0 = StateProvinceCode exists. 1 = StateProvinceCode unavailable, using CountryRegionCode.
    /// </summary>
    public bool IsOnlyStateProvinceFlag { get; set; }

    /// <summary>
    /// State or province description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// ID of the territory in which the state or province is located. Foreign key to SalesTerritory.SalesTerritoryID.
    /// </summary>
    public int TerritoryId { get; set; }

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual CountryRegion CountryRegionCodeNavigation { get; set; } = null!;

    public virtual ICollection<SalesTaxRate> SalesTaxRates { get; set; } = new List<SalesTaxRate>();

    public virtual SalesTerritory Territory { get; set; } = null!;
}
