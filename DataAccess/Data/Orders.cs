﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Data;

public partial class Orders
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string OrderNo { get; set; }

    public Guid? PriceListId { get; set; }

    public int Piece { get; set; }

    public bool IsCompleteOrder { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public Guid UpdateBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Users User { get; set; }
}