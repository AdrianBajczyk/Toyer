using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User;


public record AddressDto
{
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(30, ErrorMessage = "Street cannot exceed 30 characters")]
    public string? Street { get; set; }

    [StringLength(4, ErrorMessage = "Street number cannot exceed 4 numbers")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Unit can consit of digits only.")]
    [Display(Name = "Street number")]
    public int? StreetNumber { get; set; }

    [StringLength(4, ErrorMessage = "Street number cannot exceed 4 numbers")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Unit can consit of digits only.")]
    [Display(Name = "Unit number")]
    public int? UnitNumber { get; set; }

    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(30, ErrorMessage = "Street cannot exceed 30 characters")]
    public string? City { get; set; }

    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(30, ErrorMessage = "State cannot exceed 30 characters")]
    public string? State { get; set; }

    [DataType(DataType.PostalCode)]
    [Display(Name = "Postal code")]
    public string? PostalCode { get; set; }

    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(30, ErrorMessage = "Country cannot exceed 30 characters")]
    public string? Country { get; set; }
}
