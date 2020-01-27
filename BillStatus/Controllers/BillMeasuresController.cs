using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillStatus.Models;
using BillStatus.Models.Contexts;

namespace BillStatus.Controllers
{
    public class BillMeasuresController : Controller
    {
        private readonly BillContext _context;

        public BillMeasuresController(BillContext context)
        {
            _context = context;
        }

        // GET: BillMeasures
        public async Task<IActionResult> Index()
        {
            var billContext = _context.BillMeasures.Include(b => b.Type);
            return View(await billContext.ToListAsync());
        }

        // GET: BillMeasures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billMeasure = await _context.BillMeasures
                .Include(b => b.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billMeasure == null)
            {
                return NotFound();
            }

            return View(billMeasure);
        }

        // GET: BillMeasures/Create
        public IActionResult Create()
        {
            ViewData["TypeID"] = new SelectList(_context.BillTypes, "Id", "Name");
            return View();
        }

        // POST: BillMeasures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeID,Value,CreatedAt")] BillMeasure billMeasure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billMeasure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeID"] = new SelectList(_context.BillTypes, "Id", "Name", billMeasure.TypeID);
            return View(billMeasure);
        }

        // GET: BillMeasures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billMeasure = await _context.BillMeasures.FindAsync(id);
            if (billMeasure == null)
            {
                return NotFound();
            }
            ViewData["TypeID"] = new SelectList(_context.BillTypes, "Id", "Name", billMeasure.TypeID);
            return View(billMeasure);
        }

        // POST: BillMeasures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeID,Value,CreatedAt")] BillMeasure billMeasure)
        {
            if (id != billMeasure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billMeasure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillMeasureExists(billMeasure.Id))
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
            ViewData["TypeID"] = new SelectList(_context.BillTypes, "Id", "Name", billMeasure.TypeID);
            return View(billMeasure);
        }

        // GET: BillMeasures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billMeasure = await _context.BillMeasures
                .Include(b => b.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billMeasure == null)
            {
                return NotFound();
            }

            return View(billMeasure);
        }

        // POST: BillMeasures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billMeasure = await _context.BillMeasures.FindAsync(id);
            _context.BillMeasures.Remove(billMeasure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillMeasureExists(int id)
        {
            return _context.BillMeasures.Any(e => e.Id == id);
        }
    }
}
