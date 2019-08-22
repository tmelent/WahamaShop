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
    public class AlliancesController : Controller
    {
        private readonly WarhammerContext _context;

        public AlliancesController(WarhammerContext context)
        {
            _context = context;
        }

        // GET: Alliances

        public async Task<IActionResult> Index(string settingName)
        {
            int settingId = _context.ProductSetting.Where(p => p.Title == settingName).Select(p => p.Id).FirstOrDefault();
            var warhammerContext = _context.ProductAlliance.Where(p => p.ProductSettingId == settingId).Include(p => p.ProductSetting);
            return View(await warhammerContext.ToListAsync()); 
        }



        // GET: Alliances/Details/5
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

        // GET: Alliances/Create
        public IActionResult Create()
        {
            ViewData["ProductSettingId"] = new SelectList(_context.ProductSetting, "Id", "Title");
            return View();
        }

        // POST: Alliances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductSettingId, ImageSource, Title")] ProductAlliance productAlliance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productAlliance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductSettingId"] = new SelectList(_context.ProductSetting, "Id", "Title", productAlliance.ProductSettingId, "ImageSource");
            return View(productAlliance);
        }

        // GET: Alliances/Edit/5
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
            ViewData["ProductSettingId"] = new SelectList(_context.ProductSetting, "Id", "Title", productAlliance.ProductSettingId, "ImageSource");
            return View(productAlliance);
        }

        // POST: Alliances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductSettingId, ImageSource, Title")] ProductAlliance productAlliance)
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
            ViewData["ProductSettingId"] = new SelectList(_context.ProductSetting, "Id", "Title", productAlliance.ProductSettingId, "ImageSource");
            return View(productAlliance);
        }

        // GET: Alliances/Delete/5
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

        // POST: Alliances/Delete/5
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
