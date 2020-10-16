using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}