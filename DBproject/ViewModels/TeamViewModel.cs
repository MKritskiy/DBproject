using DBproject.DAL.Models;

namespace DBproject.ViewModels
{
    public class TeamViewModel
    {
        public IEnumerable<TaskListModel>? taskList { get; set; }
        public TeamModel? team { get; set; }
    }
}
