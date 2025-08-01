﻿using System;
using System.Collections.Generic;

namespace AdventureWorksLINQ.Models;

/// <summary>
/// General sales order information.
/// </summary>
public partial class SalesOrderHeader
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public int SalesOrderId { get; set; }

    /// <summary>
    /// Incremental number to track changes to the sales order over time.
    /// </summary>
    public byte RevisionNumber { get; set; }

    /// <summary>
    /// Dates the sales order was created.
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Date the order is due to the customer.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Date the order was shipped to the customer.
    /// </summary>
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled
    /// </summary>
    public byte Status { get; set; }

    /// <summary>
    /// 0 = Order placed by sales person. 1 = Order placed online by customer.
    /// </summary>
    public bool OnlineOrderFlag { get; set; }

    /// <summary>
    /// Unique sales order identification number.
    /// </summary>
    public string SalesOrderNumber { get; set; } = null!;

    /// <summary>
    /// Customer purchase order number reference. 
    /// </summary>
    public string? PurchaseOrderNumber { get; set; }

    /// <summary>
    /// Financial accounting number reference.
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Customer identification number. Foreign key to Customer.BusinessEntityID.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Sales person who created the sales order. Foreign key to SalesPerson.BusinessEntityID.
    /// </summary>
    public int? SalesPersonId { get; set; }

    /// <summary>
    /// Territory in which the sale was made. Foreign key to SalesTerritory.SalesTerritoryID.
    /// </summary>
    public int? TerritoryId { get; set; }

    /// <summary>
    /// Customer billing address. Foreign key to Address.AddressID.
    /// </summary>
    public int BillToAddressId { get; set; }

    /// <summary>
    /// Customer shipping address. Foreign key to Address.AddressID.
    /// </summary>
    public int ShipToAddressId { get; set; }

    /// <summary>
    /// Shipping method. Foreign key to ShipMethod.ShipMethodID.
    /// </summary>
    public int ShipMethodId { get; set; }

    /// <summary>
    /// Credit card identification number. Foreign key to CreditCard.CreditCardID.
    /// </summary>
    public int? CreditCardId { get; set; }

    /// <summary>
    /// Approval code provided by the credit card company.
    /// </summary>
    public string? CreditCardApprovalCode { get; set; }

    /// <summary>
    /// Currency exchange rate used. Foreign key to CurrencyRate.CurrencyRateID.
    /// </summary>
    public int? CurrencyRateId { get; set; }

    /// <summary>
    /// Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.
    /// </summary>
    public decimal SubTotal { get; set; }

    /// <summary>
    /// Tax amount.
    /// </summary>
    public decimal TaxAmt { get; set; }

    /// <summary>
    /// Shipping cost.
    /// </summary>
    public decimal Freight { get; set; }

    /// <summary>
    /// Total due from customer. Computed as Subtotal + TaxAmt + Freight.
    /// </summary>
    public decimal TotalDue { get; set; }

    /// <summary>
    /// Sales representative comments.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual Address BillToAddress { get; set; } = null!;

    public virtual CreditCard? CreditCard { get; set; }

    public virtual CurrencyRate? CurrencyRate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();

    public virtual ICollection<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReasons { get; set; } = new List<SalesOrderHeaderSalesReason>();

    public virtual SalesPerson? SalesPerson { get; set; }

    public virtual ShipMethod ShipMethod { get; set; } = null!;

    public virtual Address ShipToAddress { get; set; } = null!;

    public virtual SalesTerritory? Territory { get; set; }
}
