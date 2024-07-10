using System.ComponentModel.DataAnnotations.Schema;

namespace Exam4.Models
{
    public class Exams
    {
        
            public int Id { get; set; }

            public string? ExamTitle { get; set; }
            public List<Questions>? Questions { get; set; } = new List<Questions>();
            [NotMapped]
            public List<Options>? SelectedOptions { get; set; } = new List<Options>();

            
            
        
    }
}
