using Microsoft.EntityFrameworkCore;
using Trade.Domain;

namespace Trade.Infrastructure.Repositories
{
    public class ProductsRepository
    {
        private readonly Context _context;

        public ProductsRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.OrderBy(p => p.Title).ToListAsync();
        }

        public async Task<Product?> GetById(Guid productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<Product> Add(AddProductDTO newProduct)
        {
            Product product = new Product(
                newProduct.Title,
                newProduct.Description,
                newProduct.Price,
                newProduct.Discount,
                newProduct.Count);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> Change(ChangeProductDTO changeProduct)
        {
            Product? existProduct = await _context.Products.FindAsync(changeProduct.Id);

            if (existProduct != null)
            {
                existProduct.Title = changeProduct.Title;
                existProduct.Description = changeProduct.Description;
                existProduct.Price = changeProduct.Price;
                existProduct.Discount = changeProduct.Discount;
                existProduct.Count = changeProduct.Count;

                existProduct.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return existProduct;
            }

            return null;
        }

        public async Task Delete(Guid productId)
        {
            Product? product = await _context.Products.FindAsync(productId);

            if (product != null)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
