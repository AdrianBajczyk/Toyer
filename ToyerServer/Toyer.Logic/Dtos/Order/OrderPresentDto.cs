﻿using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.Order;

public record OrderPresentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}