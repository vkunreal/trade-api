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
                .Include(order => order.Address)
                .Include(order => order.User)
                .Include(order => order.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Select(order => new OrderResultDTO
                {
                    Id = order.Id,
                    User = new OrderUserDTO
                    {
                        Id = order.User.Id,
                        FirstName = order.User.FirstName,
                        LastName = order.User.LastName,
                        Email = order.User.Email,
                    },
                    Address = order.Address,
                    Comment = order.Comment,
                    Status = order.GetStatusName(),
                    TotalPrice = order.TotalPrice,
                    CreatedAt = order.CreatedAt,
                    UpdatedAt = order.UpdatedAt,
                    Products = order.OrderItems.Select(orderItem => new OrderResultProductDTO
                    {
                        Id = orderItem.Product.Id,
                        Title = orderItem.Product.Title,
                        Description = orderItem.Product.Description,
                        Price = orderItem.Product.Price,
                        Discount = orderItem.Product.Discount,
                        Quantity = orderItem.Quantity
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<OrderResultDTO?> GetById(Guid orderId)
        {

            Order? order = await _context.Orders
                .Include(order => order.Address)
                .Include(order => order.User)
                .Include(order => order.OrderItems)
                    .ThenInclude(orderItem => orderItem.Product)
                .FirstOrDefaultAsync(order => order.Id == orderId);

            if (order == null) return null;

            return new OrderResultDTO
            {
                Id = order.Id,
                User = new OrderUserDTO
                {
                    Id = order.User.Id,
                    FirstName = order.User.FirstName,
                    LastName = order.User.LastName,
                    Email = order.User.Email,
                },
                Address = order.Address,
                Comment = order.Comment,
                Status = order.GetStatusName(),
                TotalPrice = order.TotalPrice,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                Products = order.OrderItems.Select(orderItem => new OrderResultProductDTO
                {
                    Id = orderItem.Product.Id,
                    Title = orderItem.Product.Title,
                    Description = orderItem.Product.Description,
                    Price = orderItem.Product.Price,
                    Discount = orderItem.Product.Discount,
                    Quantity = orderItem.Quantity
                }).ToList()
            };
        }

        public async Task<OrderResultDTO?> Add(AddOrderDTO newOrder)
        {
            Order order = new(newOrder.UserId, newOrder.AddressId, newOrder.Comment);

            _context.Orders.Add(order);

            for (int i = 0; i < newOrder.Products.Count; i++)
            {
                var product = newOrder.Products[i];

                OrderItem orderItem = new(order.Id, product.ProductId, product.Quantity);

                _context.OrderItems.Add(orderItem);
            }

            await _context.SaveChangesAsync();

            return await GetById(order.Id);
        }

        public async Task<OrderResultDTO?> Update(ChangeOrderDTO changeOrder)
        {
            Order? existOrder = await _context.Orders.FindAsync(changeOrder.Id);

            if (existOrder != null)
            {
                existOrder.AddressId = changeOrder.AddressId;
                existOrder.Comment = changeOrder.Comment;
                existOrder.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return await GetById(existOrder.Id);
            }

            return null;
        }

        public async Task<OrderResultDTO?> UpdateOrderStatus(Guid orderId, OrderStatus orderStatus)
        {
            Order? existOrder = await _context.Orders.FindAsync(orderId);

            if (existOrder != null)
            {
                existOrder.Status = orderStatus;
                existOrder.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return await GetById(existOrder.Id);
            }

            return null;
        }

        public async Task Delete(Guid orderId)
        {
            Order? order = await _context.Orders.FindAsync(orderId);

            if (order != null)
            {
                _context.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
