﻿using System;
using System.Collections.Generic;

namespace AdventureWorksLINQ.Models;

/// <summary>
/// Cross-reference table mapping product models and illustrations.
/// </summary>
public partial class ProductModelIllustration
{
    /// <summary>
    /// Primary key. Foreign key to ProductModel.ProductModelID.
    /// </summary>
    public int ProductModelId { get; set; }

    /// <summary>
    /// Primary key. Foreign key to Illustration.IllustrationID.
    /// </summary>
    public int IllustrationId { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual Illustration Illustration { get; set; } = null!;

    public virtual ProductModel ProductModel { get; set; } = null!;
}
