﻿namespace Toyer.Data.Entities;

public class AddressDto
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public Guid PersonalInfoId { get; set; } 
    public PersonalInfo PersonalInfo { get; set; }
}
