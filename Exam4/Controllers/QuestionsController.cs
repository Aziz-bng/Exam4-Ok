using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exam4.Data;
using Exam4.Models;

namespace Exam4.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly Exam4Context _context;

        public QuestionsController(Exam4Context context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var exam4Context = _context.Questions.Include(q => q.Exams);
            return View(await exam4Context.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.Exams)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            ViewData["ExamsId"] = new SelectList(_context.Exams, "Id", "Id");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,ExamsId")] Questions questions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExamsId"] = new SelectList(_context.Exams, "Id", "Id", questions.ExamsId);
            return View(questions);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }
            ViewData["ExamsId"] = new SelectList(_context.Exams, "Id", "Id", questions.ExamsId);
            return View(questions);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,ExamsId")] Questions questions)
        {
            if (id != questions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(questions.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExamsId"] = new SelectList(_context.Exams, "Id", "Id", questions.ExamsId);
            return View(questions);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.Exams)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            if (questions != null)
            {
                _context.Questions.Remove(questions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
