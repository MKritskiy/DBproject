namespace DBproject.DAL.Models
{
    public class TaskModel
    {
        public int? TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set;}
        public int? TaskProirity { get; set; }

        public int? StatusId { get; set;}
        public int? TaskListId { get; set; }
        public int? SourceId { get; set; }

    }
}
