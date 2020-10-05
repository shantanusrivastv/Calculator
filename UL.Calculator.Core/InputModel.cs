using System.ComponentModel.DataAnnotations;

namespace UL.Calculator.Core.Model
{
    public class InputModel
    {
        [Required]
        public string Expression { get; set; }
    }
}