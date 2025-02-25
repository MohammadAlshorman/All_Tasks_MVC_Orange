using System;
using System.Collections.Generic;

namespace Revision.Models;

public partial class CurrentUser
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? RepeatPassword { get; set; }

    public string Role { get; set; } = "User";
}
