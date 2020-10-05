using System.ComponentModel.DataAnnotations.Schema;

namespace UL.Calculator.Entities
{
    public class UserLogin
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public SubscriptionType SubscriptionType { get; set; }

        [NotMapped]
        public string Token { get; set; }

        public virtual User User { get; set; }
    }
}