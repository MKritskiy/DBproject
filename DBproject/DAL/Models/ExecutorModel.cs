using System.ComponentModel.DataAnnotations;

namespace DBproject.DAL.Models
{
    public class ExecutorModel
    {
        public int? ExecutorId;
        [Required]
        public string Name = null!;
        public string? Email;
        [Required]
        public string Password = null!;
        public string? PhoneNumber;
        public int RoleId;
    }
}
