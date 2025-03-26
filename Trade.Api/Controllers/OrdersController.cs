using Microsoft.AspNetCore.Mvc;
using Trade.Domain;
using Trade.Infrastructure;
using Trade.Infrastructure.Repositories;

namespace Trade.Api.Controllers
{
    /// <summary>
    /// Контроллер для управления заказами пользователей
    /// </summary>
    [ApiController]
    [Route("api/orders/")]
    public class OrdersController(Context _context) : ControllerBase
    {
        private readonly OrdersRepository _ordersRepository = new(_context);
        private readonly UsersRepository _usersRepository = new(_context);
        private readonly ProductsRepository _productsRepository = new(_context);

        private readonly string USER_ERROR_MESSAGE = "Пользователь по такому ID не найден";
        private readonly string ORDER_NOT_FOUND = "Заказ по такому ID не найден";
        private readonly string ORDER_ID_ERROR_MESSAGE = "ID в URL и в теле запроса не совпадают";
        private readonly string PRODUCT_NOT_FOUND_ERROR_MESSAGE = "ID в URL и в теле запроса не совпадают";
        private readonly string PRODUCT_HAS_ENDED_ERROR_MESSAGE = "Товар закончился";

        /// <summary>
        /// Получает список всех заказов
        /// </summary>
        /// <returns>Список заказов</returns>
        /// <response code="200">Возвращает массив заказов</response>
        [HttpGet]
        public async Task<IEnumerable<OrderResultDTO>> GetAllOrders()
        {
            return await _ordersRepository.GetAll();
        }

        /// <summary>
        /// Получает заказ по ID
        /// </summary>
        /// <param name="orderId">ID заказа</param>
        /// <returns>Объект заказа</returns>
        /// <response code="200">Возвращает объект заказа по ID</response>
        /// <response code="404">Заказ по такому ID не найден</response>
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResultDTO?>> GetOrder(Guid orderId)
        {
            OrderResultDTO? order = await _ordersRepository.GetById(orderId);

            if (order != null) {
                return order;
            }

            return NotFound(ORDER_NOT_FOUND);
        }

        /// <summary>
        /// Добавляет новый заказ
        /// </summary>
        /// <param name="newOrder">DTO добавляемого заказа</param>
        /// <returns>Объект заказа пользователя</returns>
        /// <response code="201">Возвращает объект заказа</response>
        /// <response code="404">Товар по такому ID не найден</response>
        /// <response code="400">Товар по такому ID закончился</response>
        [HttpPost]
        public async Task<ActionResult<OrderResultDTO?>> AddOrder([FromBody] AddOrderDTO newOrder)
        {
            if ((await ValidateUser(newOrder.UserId)).Result is NotFoundResult)
            {
                return NotFound(USER_ERROR_MESSAGE);
            }

            for (int i = 0; i < newOrder.Products.Count; i++)
            {
                var addProduct = newOrder.Products[i];
                Product? product = await _productsRepository.GetById(addProduct.ProductId);

                if (product == null)
                {
                    return NotFound(PRODUCT_NOT_FOUND_ERROR_MESSAGE);
                }

                if (product.Count < addProduct.Quantity)
                {
                    return BadRequest(PRODUCT_HAS_ENDED_ERROR_MESSAGE);
                }

                await _productsRepository.ChangeCount(product.Id, product.Count - addProduct.Quantity);
            }

            OrderResultDTO? createdOrder = await _ordersRepository.Add(newOrder);

            return CreatedAtAction(nameof(GetOrder), new { orderId = createdOrder!.Id }, createdOrder);
        }

        /// <summary>
        /// Изменяет данные заказа
        /// </summary>
        /// <param name="orderId">ID заказа пользователя</param>
        /// <param name="changeOrder">DTO изменяемого заказа</param>
        /// <returns>Объект заказа</returns>
        /// <response code="200">Возвращает обновленный объект заказа</response>
        /// <response code="400">ID в URL и в теле запроса не совпадают</response>
        /// <response code="404">Пользователь по такому ID не найден</response>
        /// <response code="404">Заказ по такому ID не найден</response>
        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderResultDTO>> ChangeOrder(Guid orderId, [FromBody] ChangeOrderDTO changeOrder)
        {
            if ((await ValidateUser(changeOrder.UserId)).Result is NotFoundResult)
            {
                return NotFound(USER_ERROR_MESSAGE);
            }

            if (orderId != changeOrder.Id)
            {
                return BadRequest(ORDER_ID_ERROR_MESSAGE);
            }

            OrderResultDTO? updatedOrder = await _ordersRepository.Update(changeOrder);

            if (updatedOrder == null)
            {
                return NotFound(ORDER_ID_ERROR_MESSAGE);
            }

            return Ok(updatedOrder);
        }

        /// <summary>
        /// Изменяет статус заказа
        /// </summary>
        /// <param name="orderId">ID заказа пользователя</param>
        /// <param name="orderStatus">Новый статус заказа</param>
        /// <returns>Объект заказа</returns>
        /// <response code="200">Возвращает обновленный объект заказа</response>
        /// <response code="404">Заказ по такому ID не найден</response>
        [HttpPut("{orderId}/updateStatus")]
        public async Task<ActionResult<OrderResultDTO?>> UpdateOrderStatus(Guid orderId, [FromBody] ChangeOrderStatus orderStatus)
        {
            OrderResultDTO? order = await _ordersRepository.UpdateOrderStatus(orderId, orderStatus.Status);

            if (order != null)
            {
                return order;
            }

            return NotFound(ORDER_ID_ERROR_MESSAGE);
        }

        /// <summary>
        /// Удаляет заказ
        /// </summary>
        /// <param name="orderId">ID заказа</param>
        /// <returns></returns>
        /// <response code="204">Заказ удален</response>
        /// <response code="404">Заказ по такому ID не найден</response>
        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(Guid orderId)
        {
            OrderResultDTO? order = await _ordersRepository.GetById(orderId);

            if (order != null)
            {
                await _ordersRepository.Delete(orderId);
                return NoContent();
            }

            return NotFound(ORDER_ID_ERROR_MESSAGE);
        }

        private async Task<ActionResult<User?>> ValidateUser(Guid userId)
        {
            User? user = await _usersRepository.GetById(userId);

            if (user == null)
            {
                return NotFound(USER_ERROR_MESSAGE);
            }

            return user;
        }
    }
}
