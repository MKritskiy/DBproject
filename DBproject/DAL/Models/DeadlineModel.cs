namespace DBproject.DAL.Models
{
    public class DeadlineModel
    {
        public int? DeadlineId { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public int TaskId { get; set; }
    }
}
