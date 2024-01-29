using System.ComponentModel.DataAnnotations;

namespace Toyer.Data.Entities;

public class Address
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public int? UnitNumber { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public Guid PersonalInfoId { get; set; } 
    public PersonalInfo PersonalInfo { get; set; }
}
