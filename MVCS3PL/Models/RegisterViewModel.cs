using System.ComponentModel.DataAnnotations;

namespace MVCS3PL.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "FName is Required ")]
        public string FName { get; set; }

        [Required(ErrorMessage = "LName is Required  ")]
        public string LName { get; set; }

        [Required(ErrorMessage = "E-mail is required "), EmailAddress(ErrorMessage = "Invalid E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required ")
            ,DataType(DataType.Password)]

        public string Password { get; set; }
        [Required(ErrorMessage = "Password is Required ")
            , DataType(DataType.Password) 
            ,Compare("Password" ,ErrorMessage = "Confirm Password Dose Not Match Password")]
        public string ConfirmPassword { get; set; }

        public bool IsActive { get; set; }


    }
}
