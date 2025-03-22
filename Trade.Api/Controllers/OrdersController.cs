using Microsoft.AspNetCore.Mvc;
using Trade.Domain;
using Trade.Infrastructure;
using Trade.Infrastructure.Repositories;

namespace Trade.Api.Controllers
{
    [Route("api/orders/")]
    [ApiController]
    public class OrdersController(Context _context) : ControllerBase
    {
        private readonly OrdersRepository _ordersRepository = new(_context);
        private readonly UsersRepository _usersRepository = new(_context);

        [HttpGet]
        public async Task<IEnumerable<OrderResultDTO>> GetAllOrders()
        {
            return await _ordersRepository.GetAll();
        }

        [HttpGet("{orderId}")]
        public async Task<Order?> GetOrder(Guid orderId)
        {
            return await _ordersRepository.GetById(orderId);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder([FromBody] AddOrderDTO newOrder)
        {
            if ((await ValidateUser(newOrder.UserId)).Result is NotFoundResult)
            {
                return NotFound("USER_ERROR_MESSAGE");
            }

            Order createdOrder = await _ordersRepository.Add(newOrder);

            return CreatedAtAction(nameof(GetOrder), new { orderId = createdOrder.Id }, createdOrder);
        }

        private async Task<ActionResult<User?>> ValidateUser(Guid userId)
        {
            User? user = await _usersRepository.GetById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
