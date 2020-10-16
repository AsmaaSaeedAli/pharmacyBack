using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}