using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wahama;

namespace Wahama.Controllers
{
    public class FractionsController : Controller
    {
        private readonly WarhammerContext _context;

        public FractionsController(WarhammerContext context)
        {
            _context = context;
        }

        // GET: Fractions
        public async Task<IActionResult> Index(string allianceName)
        {
            int allianceId = _context.ProductAlliance.Where(t => t.Title == allianceName).Select(t => t.Id).FirstOrDefault();
            var warhammerContext = _context.ProductFraction.Where(t => t.ProductAllianceId == allianceId).Include(p => p.ProductAlliance);
            return View(await warhammerContext.ToListAsync());
        }

        // GET: Fractions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFraction = await _context.ProductFraction
                .Include(p => p.ProductAlliance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productFraction == null)
            {
                return NotFound();
            }

            return View(productFraction);
        }

        // GET: Fractions/Create
        public IActionResult Create()
        {
            ViewData["ProductAllianceId"] = new SelectList(_context.ProductAlliance, "Id", "Title");
            return View();
        }

        // POST: Fractions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductAllianceId, ImageSource, Title")] ProductFraction productFraction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productFraction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductAllianceId"] = new SelectList(_context.ProductAlliance, "Id", "Title", productFraction.ProductAllianceId);
            return View(productFraction);
        }

        // GET: Fractions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFraction = await _context.ProductFraction.FindAsync(id);
            if (productFraction == null)
            {
                return NotFound();
            }
            ViewData["ProductAllianceId"] = new SelectList(_context.ProductAlliance, "Id", "Title", productFraction.ProductAllianceId);
            return View(productFraction);
        }

        // POST: Fractions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductAllianceId, ImageSource, Title")] ProductFraction productFraction)
        {
            if (id != productFraction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productFraction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductFractionExists(productFraction.Id))
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
            ViewData["ProductAllianceId"] = new SelectList(_context.ProductAlliance, "Id", "Title", productFraction.ProductAllianceId);
            return View(productFraction);
        }

        // GET: Fractions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFraction = await _context.ProductFraction
                .Include(p => p.ProductAlliance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productFraction == null)
            {
                return NotFound();
            }

            return View(productFraction);
        }

        // POST: Fractions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productFraction = await _context.ProductFraction.FindAsync(id);
            _context.ProductFraction.Remove(productFraction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductFractionExists(int id)
        {
            return _context.ProductFraction.Any(e => e.Id == id);
        }
    }
}
