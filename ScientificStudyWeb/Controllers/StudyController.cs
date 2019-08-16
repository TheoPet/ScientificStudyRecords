using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScientificStudiesRecord.Data;
using ScientificStudiesRecord.Models;

namespace Controllers
{
    public class StudyController : Controller
    {
        private readonly ScientificStudiesRecordDbContext _context;

        public StudyController(ScientificStudiesRecordDbContext context)
        {
            _context = context;
        }

        // GET: Study
        public async Task<IActionResult> Index()
        {
            return View(await _context.Studies.ToListAsync());
        }

        // GET: Study/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Studies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (study == null)
            {
                return NotFound();
            }

            return View(study);
        }

        // GET: Study/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Study/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Study study)
        {
            if (ModelState.IsValid)
            {
                _context.Add(study);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(study);
        }

        // GET: Study/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Studies.FindAsync(id);
            if (study == null)
            {
                return NotFound();
            }
            return View(study);
        }

        // POST: Study/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Study study)
        {
            if (id != study.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(study);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyExists(study.Id))
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
            return View(study);
        }

        // GET: Study/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Studies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (study == null)
            {
                return NotFound();
            }

            return View(study);
        }

        // POST: Study/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var study = await _context.Studies.FindAsync(id);
            _context.Studies.Remove(study);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyExists(int id)
        {
            return _context.Studies.Any(e => e.Id == id);
        }
    }
}
