using Exam4.Data;
using Exam4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam4.Controllers
{
    public class HomeController : Controller
    {
        private readonly Exam4Context _context;

        public HomeController(Exam4Context context)
        {
            _context = context;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var exams = await _context.Exams
                .Include(e => e.Questions)
                    .ThenInclude(q => q.Options)
                .ToListAsync();
            return View(exams);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAnswersAsync(IFormCollection form)
        {
            // Parse the submitted answers
            var answers = new List<Options>();
            foreach (var key in form.Keys)
            {
                if (key.StartsWith("question_"))
                {
                    var questionId = int.Parse(key.Replace("question_", ""));
                    var optionId = int.Parse(form[key]);
                    answers.Add(new Options { QuestionsId = questionId, Id = optionId });
                }
            }

            // Calculate the score
            int correctAnswersCount = 0;
            foreach (var answer in answers)
            {
                var question = await _context.Questions
                    .Include(q => q.Options)
                    .FirstOrDefaultAsync(q => q.Id == answer.QuestionsId);
                if (question != null)
                {
                    var selectedOption = question.Options.FirstOrDefault(o => o.Id == answer.Id);
                    if (selectedOption != null && selectedOption.IsCorrect)
                    {
                        correctAnswersCount++;
                    }
                }
            }

            // Return the score as JSON
            return Json(new { success = true, correctAnswersCount = correctAnswersCount });
        }
    }
}
