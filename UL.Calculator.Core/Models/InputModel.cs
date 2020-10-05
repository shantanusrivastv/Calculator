using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UL.Calculator.WebAPI.Model
{
    public class InputModel
    {
        [Required]
        public string Expression { get; set; }
    }
}