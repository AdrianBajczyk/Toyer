﻿using System.ComponentModel.DataAnnotations;

namespace Toyer.Data.Entities;

public class UserDevices
{
    [Key]
    public string UserId { get; set; }
    public List<string?> DevicesIds { get; set; } = new();
}