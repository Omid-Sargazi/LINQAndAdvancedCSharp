﻿using System;
using System.Collections.Generic;

namespace AdventureWorksLINQ.Models;

/// <summary>
/// Where to send a person email.
/// </summary>
public partial class EmailAddress
{
    /// <summary>
    /// Primary key. Person associated with this email address.  Foreign key to Person.BusinessEntityID
    /// </summary>
    public int BusinessEntityId { get; set; }

    /// <summary>
    /// Primary key. ID of this email address.
    /// </summary>
    public int EmailAddressId { get; set; }

    /// <summary>
    /// E-mail address for the person.
    /// </summary>
    public string? EmailAddress1 { get; set; }

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual Person BusinessEntity { get; set; } = null!;
}
