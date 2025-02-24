using System;
using System.Collections.Generic;

namespace Task_24_2_2025.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }
}
