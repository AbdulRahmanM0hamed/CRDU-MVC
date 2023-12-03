using System.ComponentModel.DataAnnotations;

namespace MVCS3PL.Models
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "E-mail is required "), EmailAddress(ErrorMessage = "Invalid E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required ")
            , DataType(DataType.Password)]

        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}
