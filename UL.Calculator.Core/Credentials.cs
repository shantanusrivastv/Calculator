using System.ComponentModel.DataAnnotations;

namespace UL.Calculator.Core.Model
{
    public class Credentials
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}