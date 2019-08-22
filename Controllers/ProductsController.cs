using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Wahama.Controllers
{
    public class ProductsController : Controller
    {
        private readonly WarhammerContext _context;

        public ProductsController(WarhammerContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Catalogue(string fraction)
        {
            int fractionId = _context.ProductFraction.Where(p => p.Title == fraction).Select(p => p.Id).FirstOrDefault();            
            var warhammerContext = _context.Product.Where(p => p.ProductFractionId == fractionId).Include(p => p.ProductFraction).Include(p => p.ProductType);
            return View(await warhammerContext.ToListAsync());
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {           
            var warhammerContext = _context.Product.Include(p => p.ProductFraction).Include(p => p.ProductType);
            if (warhammerContext == null)
            {
                return NotFound();
            }
            ViewData["ProductFraction"] = new SelectList(_context.ProductFraction.Select(p => p.Title));
            ViewData["ProductSetting"] = new SelectList(_context.ProductSetting.Select(p => p.Title));
            ViewData["ProductAlliance"] = new SelectList(_context.ProductAlliance.Select(p => p.Title));

            return View(await warhammerContext.ToListAsync());
        }

      
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductFraction)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductFractionId"] = new SelectList(_context.ProductFraction, "Id", "Title");
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Description");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductFractionId,ProductTypeId,Name,Price,Description, ShortDescription")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductFractionId"] = new SelectList(_context.ProductFraction, "Id", "Title", product.ProductFractionId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Description", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductFractionId"] = new SelectList(_context.ProductFraction, "Id", "Title", product.ProductFractionId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Description", product.ProductTypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductFractionId,ProductTypeId,Name,Price,Description, ShortDescription")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["ProductFractionId"] = new SelectList(_context.ProductFraction, "Id", "Title", product.ProductFractionId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "Id", "Description", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductFraction)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
