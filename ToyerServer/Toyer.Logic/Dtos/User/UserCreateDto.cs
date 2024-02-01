using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Toyer.Logic.Dtos.User;

public record UserCreateDto
{
    [Required(ErrorMessage = "Login is required")]
    [StringLength(24, ErrorMessage = "Login cannot exceed 20 characters")]
    [DisplayName("Login")]
    [RegularExpression("^[A-z][A-z0-9-_]{3,23}$", ErrorMessage = "Letters, numbers, underscores, hyphens allowed")]
    public string UserName {  get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Surname name is required")]
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(20, ErrorMessage = "Surname cannot exceed 20 characters")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Email address is required")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Birth date is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Birth date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateOnly BirthDate { get; set; }

    [Required(ErrorMessage = "Street is required")]
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(30, ErrorMessage = "Street cannot exceed 30 characters")]
    public string Street { get; set; }

    [Required(ErrorMessage = "Street number is required")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Unit can consit of digits only.")]
    [Display(Name = "Street number")]
    public int StreetNumber { get; set; }

    [RegularExpression(@"^\d+$", ErrorMessage = "Unit can consit of digits only.")]
    [Display(Name = "Unit number")]
    public int? UnitNumber { get; set; }

    [Required(ErrorMessage = "City is required")]
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(30, ErrorMessage = "Street cannot exceed 30 characters")]
    public string City { get; set; }

    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(30, ErrorMessage = "State cannot exceed 30 characters")]
    public string? State { get; set; }

    [Required(ErrorMessage = "Postal code is required")]
    [Display(Name = "Postal code")]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(30, ErrorMessage = "Country cannot exceed 30 characters")]
    public string Country { get; set; }

    [RegularExpression(@"^\(\+\d{2}\) \d{3}-\d{3}-\d{3}$", ErrorMessage = "Invalid phone number format")]
    [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
    [Display(Name = "Phone number")]
    public string? PhoneNumber { get; set; }
}

