using WebApplication1.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Linq;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly WebApplication1Context _dbContext;

        public CartController(WebApplication1Context dbContext)
        {
            _dbContext = dbContext;
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

        // Other cart actions...
    }
}
