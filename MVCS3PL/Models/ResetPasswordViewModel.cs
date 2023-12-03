using System.ComponentModel.DataAnnotations;

namespace MVCS3PL.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required ") , DataType(DataType.Password)]

        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Password is Required ")
            , DataType(DataType.Password)
            , Compare("NewPassword", ErrorMessage = "Confirm Password Dose Not Match Password")]
        public string ConfirmPassword { get; set; }
    }
}
