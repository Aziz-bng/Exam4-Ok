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
    public class OptionsController : Controller
    {
        private readonly Exam4Context _context;

        public OptionsController(Exam4Context context)
        {
            _context = context;
        }

        // GET: Options
        public async Task<IActionResult> Index()
        {
            var exam4Context = _context.Options.Include(o => o.Questions);
            return View(await exam4Context.ToListAsync());
        }

        // GET: Options/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var options = await _context.Options
                .Include(o => o.Questions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (options == null)
            {
                return NotFound();
            }

            return View(options);
        }

        // GET: Options/Create
        public IActionResult Create()
        {
            ViewData["QuestionsId"] = new SelectList(_context.Questions, "Id", "Id");
            return View();
        }

        // POST: Options/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,IsCorrect,QuestionsId")] Options options)
        {
            if (ModelState.IsValid)
            {
                _context.Add(options);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionsId"] = new SelectList(_context.Questions, "Id", "Id", options.QuestionsId);
            return View(options);
        }

        // GET: Options/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var options = await _context.Options.FindAsync(id);
            if (options == null)
            {
                return NotFound();
            }
            ViewData["QuestionsId"] = new SelectList(_context.Questions, "Id", "Id", options.QuestionsId);
            return View(options);
        }

        // POST: Options/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,IsCorrect,QuestionsId")] Options options)
        {
            if (id != options.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(options);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionsExists(options.Id))
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
            ViewData["QuestionsId"] = new SelectList(_context.Questions, "Id", "Id", options.QuestionsId);
            return View(options);
        }

        // GET: Options/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var options = await _context.Options
                .Include(o => o.Questions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (options == null)
            {
                return NotFound();
            }

            return View(options);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var options = await _context.Options.FindAsync(id);
            if (options != null)
            {
                _context.Options.Remove(options);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionsExists(int id)
        {
            return _context.Options.Any(e => e.Id == id);
        }
    }
}
