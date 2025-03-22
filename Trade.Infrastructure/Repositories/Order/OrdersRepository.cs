using Microsoft.EntityFrameworkCore;
using Trade.Domain;

namespace Trade.Infrastructure.Repositories
{
    public class OrdersRepository
    {
        private readonly Context _context;

        public OrdersRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<OrderResultDTO>> GetAll()
        {

            return await _context.Orders
                .Include(o => o.Address)
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Select(o => new OrderResultDTO
                {
                    Id = o.Id,
                    User = new OrderUserDTO
                    {
                        Id = o.User.Id,
                        FirstName = o.User.FirstName,
                        LastName = o.User.LastName,
                        Email = o.User.Email,
                    },
                    Address = new Address(
                        o.Address.UserId,
                        o.Address.Country,
                        o.Address.Region,
                        o.Address.City,
                        o.Address.PostalCode,
                        o.Address.Street,
                        o.Address.HouseNumber,
                        o.Address.FlatNumber,
                        o.Address.AddressDescription),
                    Comment = o.Comment,
                    Status = o.Status,
                    CreatedAt = o.CreatedAt,
                    UpdatedAt = o.UpdatedAt,
                    Products = o.OrderItems.Select(oi => new OrderResultProductDTO
                    {
                        Id = oi.Product.Id,
                        Title = oi.Product.Title,
                        Description = oi.Product.Description,
                        Price = oi.Product.Price,
                        Discount = oi.Product.Discount
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<Order?> GetById(Guid orderId)
        {

            return await _context.Orders
            .Include(o => o.Address)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
             .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order> Add(AddOrderDTO newOrder)
        {
            Order order = new(newOrder.UserId, newOrder.AddressId, newOrder.Comment, newOrder.Status);

            _context.Orders.Add(order);

            for (int i = 0; i < newOrder.Products.Count; i++)
            {
                var product = newOrder.Products[i];

                OrderItem orderItem = new(order.Id, product.ProductId, product.Quantity);

                _context.OrderItems.Add(orderItem);
            }

            await _context.SaveChangesAsync();

            return order;
        }
    }
}
