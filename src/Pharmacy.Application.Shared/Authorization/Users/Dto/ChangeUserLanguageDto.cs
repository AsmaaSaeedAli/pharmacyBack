using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
