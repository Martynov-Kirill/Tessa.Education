using System.ComponentModel.DataAnnotations;

namespace Tessa.Education.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticateModel
    {
        [Required]
        public string login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
