using DBproject.DAL.Models;

namespace DBproject.ViewModels
{
    public class TeamViewModel
    {
        public IEnumerable<TaskListModel>? TaskList { get; set; }
        public TeamModel? Team { get; set; }
        public bool IsInTeam { get; set; }
        public string? UserRole { get; set; }
    }
}
