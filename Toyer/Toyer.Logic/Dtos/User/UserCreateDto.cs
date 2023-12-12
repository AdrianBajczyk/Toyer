using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User;

public class UserCreateDto
{
    [Required(ErrorMessage = "Login is required")]
    [StringLength(20, ErrorMessage = "Login cannot exceed 20 characters")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Login can only contain letters and numbers")]
    // https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0
    public string Login {  get; set; }

    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{6,20}$", ErrorMessage = "Password must have at least one uppercase letter and one digit")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Surname name is required")]
    [StringLength(20, ErrorMessage = "Surname cannot exceed 20 characters")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Email address is required")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    // https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0
    public string Email { get; set; }

    [Required(ErrorMessage = "Birth date is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateOnly BirthDate { get; set; }

    [Required(ErrorMessage = "Street is required")]
    [StringLength(30, ErrorMessage = "Street cannot exceed 30 characters")]
    public string Street { get; set; }

    [Required(ErrorMessage = "City is required")]
    [StringLength(30, ErrorMessage = "Street cannot exceed 30 characters")]
    public string City { get; set; }

    [StringLength(30, ErrorMessage = "State cannot exceed 30 characters")]
    public string State { get; set; }

    [Required(ErrorMessage = "Postal code is required")]
    [StringLength(6, ErrorMessage = "Postal code cannot exceed 10 characters")]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [StringLength(30, ErrorMessage = "Country cannot exceed 30 characters")]
    public string Country { get; set; }

    [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = "Invalid phone number format")]
    public string? PhoneNumber { get; set; }
}

//partialupdate - asp.net patch mechanics (readabout)
//