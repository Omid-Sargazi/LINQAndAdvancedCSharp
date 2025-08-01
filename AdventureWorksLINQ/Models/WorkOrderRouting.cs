﻿using System;
using System.Collections.Generic;

namespace AdventureWorksLINQ.Models;

/// <summary>
/// Work order details.
/// </summary>
public partial class WorkOrderRouting
{
    /// <summary>
    /// Primary key. Foreign key to WorkOrder.WorkOrderID.
    /// </summary>
    public int WorkOrderId { get; set; }

    /// <summary>
    /// Primary key. Foreign key to Product.ProductID.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Primary key. Indicates the manufacturing process sequence.
    /// </summary>
    public short OperationSequence { get; set; }

    /// <summary>
    /// Manufacturing location where the part is processed. Foreign key to Location.LocationID.
    /// </summary>
    public short LocationId { get; set; }

    /// <summary>
    /// Planned manufacturing start date.
    /// </summary>
    public DateTime ScheduledStartDate { get; set; }

    /// <summary>
    /// Planned manufacturing end date.
    /// </summary>
    public DateTime ScheduledEndDate { get; set; }

    /// <summary>
    /// Actual start date.
    /// </summary>
    public DateTime? ActualStartDate { get; set; }

    /// <summary>
    /// Actual end date.
    /// </summary>
    public DateTime? ActualEndDate { get; set; }

    /// <summary>
    /// Number of manufacturing hours used.
    /// </summary>
    public decimal? ActualResourceHrs { get; set; }

    /// <summary>
    /// Estimated manufacturing cost.
    /// </summary>
    public decimal PlannedCost { get; set; }

    /// <summary>
    /// Actual manufacturing cost.
    /// </summary>
    public decimal? ActualCost { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual WorkOrder WorkOrder { get; set; } = null!;
}
