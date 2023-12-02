using System.ComponentModel.DataAnnotations;

namespace DBproject.DAL.Models
{
    public class TaskListModel
    {
        public int? TaskListId { get; set; }
        [Required]
        public string? Name { get; set; } = null!;
        public int TeamId { get; set; }
    }
}
