using DBproject.DAL.Models;

namespace DBproject.ViewModels
{
    public class TaskListViewModel
    {
        public IEnumerable<TaskModel>? Tasks { get; set; }
         public TaskListModel? TaskList { get; set; }
    }
}
