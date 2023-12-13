using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User;

public class UserAddressDto
{
    [StringLength(30, ErrorMessage = "Street cannot exceed 30 characters")]
    public string? Street { get; set; }

    [StringLength(30, ErrorMessage = "Street cannot exceed 30 characters")]
    public string? City { get; set; }

    [StringLength(30, ErrorMessage = "State cannot exceed 30 characters")]
    public string? State { get; set; }

    [StringLength(6, ErrorMessage = "Postal code cannot exceed 10 characters")]
    public string? PostalCode { get; set; }

    [StringLength(30, ErrorMessage = "Country cannot exceed 30 characters")]
    public string? Country { get; set; }
}
