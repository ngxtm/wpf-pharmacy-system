using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
