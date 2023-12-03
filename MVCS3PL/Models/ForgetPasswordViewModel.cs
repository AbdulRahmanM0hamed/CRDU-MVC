using System.ComponentModel.DataAnnotations;

namespace MVCS3PL.Models
{
	public class ForgetPasswordViewModel
	{
        [Required(ErrorMessage = "E-mail is required ")
            , EmailAddress(ErrorMessage = "Invalid E-Mail")]
        public string Email { get; set; }
    }
}
