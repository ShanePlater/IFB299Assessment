using System.ComponentModel.DataAnnotations;

namespace GMS.ASPNet.Core.Models.SessionViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
