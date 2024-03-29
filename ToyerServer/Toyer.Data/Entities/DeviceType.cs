﻿namespace Toyer.Data.Entities;

public class DeviceType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public List<Device> Devices { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
}
