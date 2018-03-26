using System.ComponentModel.DataAnnotations;

namespace TaxiCloud.Core.Repository.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fio { get; set; }
        public int Type { get; set; }

        public UserType TypeCasted { get { return (UserType)Type; } }
    }
}