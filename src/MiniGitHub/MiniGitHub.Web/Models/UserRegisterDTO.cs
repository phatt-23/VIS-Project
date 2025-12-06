using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniGitHub.Web.Models;

public class UserRegisterDTO {
    [DataType(DataType.Text)]
    public string Username {get;set;} = null!;
    
    [DataType(DataType.EmailAddress)]
    public string Email {get;set;} = null!;
    
    [DataType(DataType.Password)]
    [DisplayName("Password")]
    public string Password {get;set;} = null!;
    
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [DisplayName("Confirm Password")]
    public string ConfirmPassword {get;set;} = null!;
    
}