using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers;

public class PurchaseController : Controller
{
    WebApplication1Context _dbContext;
    public PurchaseController(WebApplication1Context _dbContext){
        this._dbContext = _dbContext;
    }
    
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var purchases = _dbContext.Purchases.Where(p => p.User == user).ToList();
            return View(purchases);
        }
        return Challenge();
        
    }
}