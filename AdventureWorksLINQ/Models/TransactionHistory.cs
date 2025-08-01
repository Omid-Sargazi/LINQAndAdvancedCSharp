﻿using System;
using System.Collections.Generic;

namespace AdventureWorksLINQ.Models;

/// <summary>
/// Record of each purchase order, sales order, or work order transaction year to date.
/// </summary>
public partial class TransactionHistory
{
    /// <summary>
    /// Primary key for TransactionHistory records.
    /// </summary>
    public int TransactionId { get; set; }

    /// <summary>
    /// Product identification number. Foreign key to Product.ProductID.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Purchase order, sales order, or work order identification number.
    /// </summary>
    public int ReferenceOrderId { get; set; }

    /// <summary>
    /// Line number associated with the purchase order, sales order, or work order.
    /// </summary>
    public int ReferenceOrderLineId { get; set; }

    /// <summary>
    /// Date and time of the transaction.
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// W = WorkOrder, S = SalesOrder, P = PurchaseOrder
    /// </summary>
    public string TransactionType { get; set; } = null!;

    /// <summary>
    /// Product quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Product cost.
    /// </summary>
    public decimal ActualCost { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual Product Product { get; set; } = null!;
}
