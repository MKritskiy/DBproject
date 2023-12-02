using System.ComponentModel.DataAnnotations;

namespace DBproject.DAL.Models
{
    public class TeamModel
    {
        public int? TeamId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
