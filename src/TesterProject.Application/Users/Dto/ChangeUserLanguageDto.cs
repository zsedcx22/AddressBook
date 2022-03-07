using System.ComponentModel.DataAnnotations;

namespace TesterProject.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}