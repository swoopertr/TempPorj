using DataLayer.Db.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Db
{
    [Table("Users")]
    public class User : DbBase
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
