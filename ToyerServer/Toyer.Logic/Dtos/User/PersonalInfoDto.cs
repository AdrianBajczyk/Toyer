using System.ComponentModel.DataAnnotations;

namespace Toyer.Logic.Dtos.User;

public record PersonalInfoDto
{
    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters")]
    public string? Name { get; set; }

    [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Invalid format. The first letter must be a capital letter, and only small letters are allowed after that.")]
    [StringLength(20, ErrorMessage = "Surname cannot exceed 20 characters")]
    public string? Surname { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Display(Name = "Birth date")]
    public DateOnly? BirthDate { get; set; }

}
