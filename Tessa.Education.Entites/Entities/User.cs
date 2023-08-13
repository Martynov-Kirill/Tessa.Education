using System.ComponentModel.DataAnnotations.Schema;

namespace Tessa.Education.Entites.Entities
{
    [Table("User")]
    public class User : Entity
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public UserActivity? Activity { get; set; }
    }

    public class UserActivity : Entity
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? LastSeenDate { get; set; }
    }
}
