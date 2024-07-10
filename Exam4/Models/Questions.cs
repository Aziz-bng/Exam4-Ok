using Microsoft.Extensions.Options;

namespace Exam4.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public ICollection<Options>? Options { get; set; }

        public int ExamsId { get; set; }

        public Exams? Exams { get; set; }
    }
}
