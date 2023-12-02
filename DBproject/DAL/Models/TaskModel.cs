namespace DBproject.DAL.Models
{
    public class TaskModel
    {
        public int? TaskId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set;}
        public int? Proirity { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public int? StatusId { get; set;}
        public int? TaskListId { get; set; }
        public int? SourceId { get; set; }

    }
}
