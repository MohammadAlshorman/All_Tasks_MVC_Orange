using System;
using System.Collections.Generic;

namespace Task_23_2_2025.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Location { get; set; }

    public int? EstablishedYear { get; set; }

    public string? Manager { get; set; }
}
