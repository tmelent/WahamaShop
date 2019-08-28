using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wahama.Models;

namespace Wahama.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly WarhammerContext _context;

        public ShoppingCartsController(WarhammerContext context)
        {
            _context = context;
        }

        // Add Items To Cart
        [HttpPost]
        public void AddToCart(int quantity, int productId, string userId)
        {
            if (ShoppingCartExists(productId))
            {
                ShoppingCart alreadyExistedItem = _context.ShoppingCart.Where(p => p.ProductId == productId && p.UserId == userId).SingleOrDefault();
                alreadyExistedItem.Quantity += quantity;
                _context.Update(alreadyExistedItem);

            }
            else
            {
                ShoppingCart shoppingCartItem = new ShoppingCart { ProductId = productId, Quantity = quantity, UserId = userId };
                _context.Add(shoppingCartItem);

            }
            _context.SaveChanges();
        }

        // Remove Items from Cart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int? quantityDifference, int productId, string userId)
        {
            ShoppingCart alreadyExistedItem = _context.ShoppingCart.Where(p => p.ProductId == productId && p.UserId == userId).SingleOrDefault();
            if ((quantityDifference == null) || ((alreadyExistedItem.Quantity + quantityDifference) <= 0))
            {
                alreadyExistedItem.Quantity = 0;
            }
            else
            {
                alreadyExistedItem.Quantity += (int)quantityDifference;
            }
            if (alreadyExistedItem.Quantity == 0)
            {
                _context.Remove(alreadyExistedItem);
            }
            else
            {
                _context.Update(alreadyExistedItem);
            }
             await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public int GetTotalCost(string userId)
        {
            int totalCost = 0;
            var userShoppingCart = _context.ShoppingCart.Where(p => p.UserId == userId).Include(s => s.Product).ToList();
            foreach (var item in userShoppingCart)
            {
                totalCost += item.Quantity * item.Product.Price;
            }
            return totalCost;

        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var product = _context.Product.Include(p => p.ProductFraction).Include(p => p.ProductType);
            var cartItems = _context.ShoppingCart.Include(s => s.Product);
            Dictionary<int, string> productImages = GetImagesByIds();
            CartItemViewModel pvm = new CartItemViewModel { ImageSources = productImages, CartItem = cartItems, Products = product };            
            return View(pvm);
        }

        private bool ShoppingCartExists(int productId)
        {
            return _context.ShoppingCart.Any(e => e.ProductId == productId);
        }

        public Dictionary<int, string> GetImagesByIds()
        {
            using (var context = new WarhammerContext())
            {
                return context.ProductImage.ToDictionary(key => key.ProductId, value => value.ImageSource);
            }
        }
    }
}
