using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wahama.Models;

namespace Wahama.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly WarhammerContext _context;
      
        public CategoriesController(WarhammerContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProductListing(string fraction)
        {
            int fractionId = _context.ProductFraction.Where(p => p.Title == fraction).Select(p => p.Id).FirstOrDefault();
            var filteredContext = _context.Product.Where(p => p.ProductFractionId == fractionId).Include(p => p.ProductFraction).Include(p => p.ProductType);
            var generalContext = _context.Product.Include(p => p.ProductFraction).Include(p => p.ProductType);            
            Dictionary<int, string> productImages = GetImagesByIds();      
            foreach(var productId in productImages.Keys)
            {
                generalContext.Where(p => p.Id == productId).FirstOrDefault().ImageSource = productImages[productId];
            }
            var alliance = _context.ProductFraction.Where(p => p.ProductAllianceId == _context.ProductFraction
            .Select(s => s.ProductAllianceId).FirstOrDefault());

            var setting = alliance.Where(p => p.ProductAlliance.ProductSetting.Id == p.ProductAlliance.ProductSettingId)
                .Select(s => s.ProductAlliance.ProductSetting.Title);

            ViewData["alliance"] = alliance.Select(p => p.ProductAlliance.Title).FirstOrDefault();
            ViewData["setting"] = setting.FirstOrDefault();
            ViewData["ProductFraction"] = new SelectList(_context.ProductFraction.Select(p => p.Title));
            ViewData["ProductSetting"] = new SelectList(_context.ProductSetting.Select(p => p.Title));
            ViewData["ProductAlliance"] = new SelectList(_context.ProductAlliance.Select(p => p.Title));
            if (!String.IsNullOrEmpty(fraction))
            {
                ProductsViewModel pvm = new ProductsViewModel { ImageSources = productImages, Products = filteredContext };
                return View(pvm);
            }
            else
            {
                ProductsViewModel pvm = new ProductsViewModel { ImageSources = productImages, Products = generalContext};
                return View(pvm);
            }
            
            
        }

        public IActionResult ProductView(int id, string fraction)
        {
            var product = _context.Product.Where(p => p.Id == id).Include(p => p.ProductFraction).Include(p => p.ProductType);
            Dictionary<int, string> productImages = GetImagesByIds();
            ProductsViewModel pvm = new ProductsViewModel { ImageSources = productImages, Products = product };
            ViewData["fraction"] = fraction;
            var alliance = _context.ProductFraction.Where(p => p.ProductAllianceId == _context.ProductFraction
            .Select(s => s.ProductAllianceId).FirstOrDefault());
                
            var setting = alliance.Where(p => p.ProductAlliance.ProductSetting.Id == p.ProductAlliance.ProductSettingId)
                .Select(s => s.ProductAlliance.ProductSetting.Title);

            ViewData["alliance"] = alliance.Select(p => p.ProductAlliance.Title).FirstOrDefault();
            ViewData["setting"] = setting.FirstOrDefault();
            return View(pvm);
            
        }

        public Dictionary<int, string> GetImagesByIds()
            {
                using (var context = new WarhammerContext())
                {
                    return context.ProductImage.ToDictionary(key => key.ProductId, value => value.ImageSource);
                }
            }

        
        
        public JsonResult GetAllianceList(string settingName)
        {
            int settingId = _context.ProductSetting.Where(p => p.Title == settingName).Select(p => p.Id).FirstOrDefault();
            List<ProductAlliance> allianceList = new List<ProductAlliance>();
            allianceList = _context.ProductAlliance.Where(p => p.ProductSettingId == settingId).ToList();
            return Json(new SelectList(allianceList, "Id", "Title"));
        }

        public JsonResult GetFractionList (string allianceName)
        {
            int allianceId = _context.ProductAlliance.Where(p => p.Title == allianceName).Select(p => p.Id).FirstOrDefault();
            List<ProductFraction> fractionList = new List<ProductFraction>();
            fractionList = _context.ProductFraction.Where(p => p.ProductAllianceId == allianceId).ToList();
            return Json(new SelectList(fractionList, "Id", "Title")); 
        }

        
    }
}

// модель для Imagesource, где все сразу