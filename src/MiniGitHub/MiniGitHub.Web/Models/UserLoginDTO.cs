using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniGitHub.Web.Models;

public class UserLoginDTO {
    [DisplayName("Username or Email")]
    [DataType(DataType.Text)]
    public string UsernameOrEmail {get;set;} = null!;
    
    [DisplayName("Password")]
    [DataType(DataType.Password)]
    public string Password {get;set;} = null!;
}

