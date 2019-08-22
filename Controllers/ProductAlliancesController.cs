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
    public class ProductAlliancesController : Controller
    {
        private readonly WarhammerContext _context;

        public ProductAlliancesController(WarhammerContext context)
        {
            _context = context;
        }

        // GET: ProductAlliances
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var warhammerContext = _context.ProductAlliance.Include(p => p.ProductSetting);
            return View(await warhammerContext.ToListAsync());
        }

        // GET: ProductAlliances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productAlliance = await _context.ProductAlliance
                .Include(p => p.ProductSetting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productAlliance == null)
            {
                return NotFound();
            }

            return View(productAlliance);
        }

        // GET: ProductAlliances/Create
        public IActionResult Create()
        {
            ViewData["ProductSettingId"] = new SelectList(_context.ProductSetting, "Id", "Title");
            return View();
        }

        // POST: ProductAlliances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductSettingId,Title, ImageSource")] ProductAlliance productAlliance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productAlliance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductSettingId"] = new SelectList(_context.ProductSetting, "Id", "Title", productAlliance.ProductSettingId);
            return View(productAlliance);
        }

        // GET: ProductAlliances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productAlliance = await _context.ProductAlliance.FindAsync(id);
            if (productAlliance == null)
            {
                return NotFound();
            }
            ViewData["ProductSettingId"] = new SelectList(_context.ProductSetting, "Id", "Title", productAlliance.ProductSettingId);
            return View(productAlliance);
        }

        // POST: ProductAlliances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductSettingId,Title, ImageSource")] ProductAlliance productAlliance)
        {
            if (id != productAlliance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productAlliance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductAllianceExists(productAlliance.Id))
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
            ViewData["ProductSettingId"] = new SelectList(_context.ProductSetting, "Id", "Title", productAlliance.ProductSettingId);
            return View(productAlliance);
        }

        // GET: ProductAlliances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productAlliance = await _context.ProductAlliance
                .Include(p => p.ProductSetting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productAlliance == null)
            {
                return NotFound();
            }

            return View(productAlliance);
        }

        // POST: ProductAlliances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productAlliance = await _context.ProductAlliance.FindAsync(id);
            _context.ProductAlliance.Remove(productAlliance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductAllianceExists(int id)
        {
            return _context.ProductAlliance.Any(e => e.Id == id);
        }
    }
}
