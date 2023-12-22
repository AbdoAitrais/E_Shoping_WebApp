using WebApplication1.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly WebApplication1Context _dbContext;
        private readonly UserManager<User> _userManager;

        public CartController(WebApplication1Context dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult AddToCart(int productId, int quantity)
        {
            // Retrieve product details based on productId from your database
            Product product = _dbContext.Product.FirstOrDefault(p => p.Id == productId);

            // Retrieve the cart from session or create a new one if it doesn't exist
            List<CartItem> cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");

            // Ensure the cart list is initialized
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            // Find the cart item corresponding to the product id
            CartItem cartItem = cart.FirstOrDefault(c => c.Product.Id == productId);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cart.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                // If the item already exists in the cart, then add one to the quantity
                cartItem.Quantity += quantity;
            }

            // Save the updated cart back to session
            HttpContext.Session.SetObject("Cart", cart);

            // Redirect the user to the cart page
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");
            
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Purchase(Dictionary<int, int> products)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // redirect to /Identity/Account/Login without a return URL
                return Challenge  ();
            }
            
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var purchase = new Purchase
            {
                User = user,
                PurchaseDate = DateTime.UtcNow,
                PurchasedProducts = new List<PurchasedProduct>()
            };

            foreach (var productId in products.Keys)
            {
                var product = await _dbContext.Product.FindAsync(productId);

                if (product == null)
                {
                    return NotFound($"Product with ID {productId} not found.");
                }

                var purchasedProduct = new PurchasedProduct
                {
                    Product = product,
                    Quantity = products[productId],
                    // Set other properties related to the purchased product
                };

                purchase.PurchasedProducts.Add(purchasedProduct);
            }

            _dbContext.Purchases.Add(purchase);
            await _dbContext.SaveChangesAsync();
            // Clear the cart
            HttpContext.Session.SetObject("Cart", new List<CartItem>());

            return RedirectToAction("Index", "Product");
        }
    }
}
