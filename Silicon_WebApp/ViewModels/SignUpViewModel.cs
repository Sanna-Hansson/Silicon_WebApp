using Silicon_WebApp.Filters;
using System.ComponentModel.DataAnnotations;

namespace Silicon_WebApp.ViewModels;

public class SignUpViewModel
{
    [Required]
    [Display(Name = "First name", Prompt = "Enter your first name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last name", Prompt = "Enter your last name")]

    public string LastName { get; set; } = null!;
    [Required]
    [Display(Name = "E-mail address", Prompt = "Enter your e-mail address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password", Prompt = "Enter a password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Display(Name = "Password", Prompt = "Match your password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage ="The Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;

    [CheckboxRequired]
    [Display(Name = "I agree to the Terms & Conditions.", Prompt = "Terms and Conditions")]
    public bool TermsAndConditions { get; set;}

}
