namespace Exam4.Models
{
    public class Options
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionsId { get; set; }
        public Questions? Questions { get; set; } = null;   
    }
}
