using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ProductService
{
    private readonly WebApplication1Context _dbContext;

    public ProductService(WebApplication1Context dbContext)
    {
        _dbContext = dbContext;
    }

    public Product GetProductById(int productId)
    {
        return _dbContext.Product.FirstOrDefault(p => p.Id == productId);
    }
}