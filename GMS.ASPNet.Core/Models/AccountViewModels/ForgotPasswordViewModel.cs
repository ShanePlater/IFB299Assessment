using System.ComponentModel.DataAnnotations;

namespace GMS.ASPNet.Core.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
