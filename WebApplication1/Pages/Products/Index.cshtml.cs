using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
// import SelectList
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public IndexModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }
        public IList<Product> Product { get;set; } = default!;
        public SelectList Categories { get; set; }

        public async Task OnGetAsync()
        {
            // if (_context.Product != null)
            // {
            //     Product = await _context.Product.ToListAsync();
            // }
            // Add a filtering by category
            IQueryable<string> categoryQuery = from p in _context.Product
                orderby p.Categorie
                select p.Categorie;
            
            var products = from p in _context.Product select p;
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Name.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                products = products.Where(x => x.Categorie == SelectedCategory);
            }
            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
            Product = await products.ToListAsync();
            
            
        }
    }
}
