﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // Получение Id из таблицы Users по имени текущего пользователя
        public int GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return _context.Users.Where(p => p.Login == HttpContext.Request.Cookies["UnauthorizedId"]).Select(p => p.Id).FirstOrDefault();
            }
            else
            {
                return _context.Users.Where(p => p.Login == User.Identity.Name).Select(p => p.Id).FirstOrDefault();
            }
        }

        // Подсчёт суммы покупок в корзине пользователя
        public int GetTotalCost()
        {
            int userId = GetUserId();
            int totalCost = 0;
            var userShoppingCart = _context.ShoppingCart.Where(p => p.UserId == userId).Include(s => s.Product).ToList();
            foreach (var item in userShoppingCart)
            {
                totalCost += item.Quantity * item.Product.Price;
            }
            return totalCost;
        }

        #region Cart
        // Корзина
        public IActionResult Index()
        {
            var product = _context.Product.Include(p => p.ProductFraction).Include(p => p.ProductType);
            var cartItems = _context.ShoppingCart.Include(s => s.Product).Where(p => p.UserId == GetUserId());
            Dictionary<int, string> productImages = GetImagesByIds();
            CartItemViewModel pvm = new CartItemViewModel { ImageSources = productImages, CartItem = cartItems, Products = product };
            return View(pvm);
        }

        [HttpPost]
        // Добавление в корзину
        public void AddToCart(int quantity, int productId)
        {
            int userId = GetUserId();
            if (ShoppingCartExists(productId)) // Если товар уже существует, то увеличивается количество, а если нет, то создается новый
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

        // Удаление из корзины отдельного товара
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int? quantityDifference, int productId)
        {
            // Если передать quantityDifference == null, то товар просто удалится, если нет, то изменится количество
            int userId = GetUserId();
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

        // Перенос товаров, которые положили в корзину, будучи незалогиненным, в аккаунт
        public void ImportUnauthorizedCartToAccount()
        {
            IEnumerable<ShoppingCart> unauthorizedItemsList = _context.ShoppingCart.Where(p => p.UserId == _context.Users.Where(x => x.Login == HttpContext.Request.Cookies["UnauthorizedID"])
            .Select(z => z.Id).FirstOrDefault()).ToList();
            foreach (var item in unauthorizedItemsList)
            {
                item.UserId = GetUserId();
                _context.Update(item); 
                _context.SaveChanges();
            }
        }
        private bool ShoppingCartExists(int productId)
        {
            return _context.ShoppingCart.Any(e => e.ProductId == productId && e.UserId == GetUserId());
        }
        // Получение фото для каждого товара
        public Dictionary<int, string> GetImagesByIds()
        {
            using (var context = new WarhammerContext())
            {
                return context.ProductImage.ToDictionary(key => key.ProductId, value => value.ImageSource);
            }
        }
        #endregion
        #region Order
        // Оформление заказа
        public IActionResult Order()
        {
            var product = _context.Product.Include(p => p.ProductFraction).Include(p => p.ProductType);
            var cartItems = _context.ShoppingCart.Include(s => s.Product).Where(p => p.UserId == GetUserId());
            var address = new Address();
            var customer = new Customer();
            var check = new Check();
            var order = new Order();
            ViewBag.Total = GetTotalCost();

            Dictionary<int, string> productImages = GetImagesByIds();
            OrderViewModel ovm = new OrderViewModel { ImageSources = productImages, 
                CartItem = cartItems,
                Products = product, 
                Address = address, 
                Customer = customer,
                Order = order,
                Check = check
            };
            return View(ovm);
        }


        // Формируется заказ на основе данных формы
        public IActionResult MakeOrder(Customer customer, Address address)
        {
            int customerId = AddCustomerAndGetId(customer, address);
            var check1 = new Check
            {
                CustomerId = customerId,
                Date = DateTime.Now,
                IsPaid = false,
                PaymentMethodId = 1,
                Total = GetTotalCost()
            };
            _context.Check.Add(check1);
            _context.SaveChanges();

            _context.Order.Add(new Wahama.Order
            {

                UserId = _context.Users
                .Where(p => p.Login == User.Identity.Name)
                .Select(t => t.Id)
                .FirstOrDefault(),
                CheckId = check1.Id
            });
            _context.SaveChanges();
            return RedirectToAction("OrderSuccess");
        }

        // Создается адрес, клиент и возвращается его Id
        public int AddCustomerAndGetId(Customer customer, Address address)
        {
            Address thisAddress = new Address
            {
                Country = address.Country,
                City = address.City,
                Street = address.City,
                Station = address.Station,
                Building = address.Building,
                Flat = address.Flat,
                Floor = address.Floor,
                Zip = address.Zip
            };
            _context.Address.Add(thisAddress);
            _context.SaveChanges(); // Сначала создается адрес, потом сохраняется БД, берется айди и создается с ним Customer
            

            Customer customer1 = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone,
                AddressId = thisAddress.Id
            };
            _context.Customer.Add(customer1);
            _context.SaveChanges();
            return customer1.Id;
        }
        // View: заказ успешно оформлен
        public IActionResult OrderSuccess()
        {
            ViewBag.OrderId = _context.Order.Where(p => p.UserId == GetUserId()).Select(p => p.Id).FirstOrDefault();
            return View();
        }
        #endregion
    }
}
