using DBproject.DAL.Models;

namespace DBproject.ViewModels
{
    public class TaskViewModel
    {
        public TaskModel? CurrTask { get; set; }
        public TaskListModel? CurrTaskListModel { get; set; }
    }
}
