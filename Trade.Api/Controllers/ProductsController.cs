using Microsoft.AspNetCore.Mvc;
using Trade.Domain;
using Trade.Infrastructure;
using Trade.Infrastructure.Repositories;

namespace Trade.Api
{
    /// <summary>
    /// Контроллер для управления товарами
    /// </summary>
    [ApiController]
    [Route("/api/products/")]
    public class ProductsController(Context _context) : ControllerBase
    {
        private readonly ProductsRepository _productsRepository = new(_context);

        private readonly string PRODUCT_ID_ERROR_MESSAGE = "ID в URL и в теле запроса не совпадают";
        private readonly string PRODUCT_NOT_FOUND_ERROR_MESSAGE = "Товар по такому ID не найден";

        /// <summary>
        /// Получает список товаров
        /// </summary>
        /// <returns>Список товаров</returns>
        /// <response code="200">Возвращает массив товаров</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _productsRepository.GetAll();
        }

        /// <summary>
        /// Получает товар по ID
        /// </summary>
        /// <param name="productId">ID товара</param>
        /// <returns>Объект товара</returns>
        /// <response code="200">Возвращает объект товара по ID</response>
        /// <response code="404">Товар по такому ID не найден</response>
        [HttpGet("{productId}")]
        public async Task<ActionResult<Product?>> GetProduct(Guid productId)
        {
            Product? product = await _productsRepository.GetById(productId);

            if (product != null)
            {
                return product;
            }

            return NotFound(PRODUCT_NOT_FOUND_ERROR_MESSAGE);
        }

        /// <summary>
        /// Добавляет новый товар
        /// </summary>
        /// <param name="newProduct">Данные для нового товара</param>
        /// <returns>Объект товара</returns>
        /// <response code="201">Возвращает объект товара</response>
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromBody] AddProductDTO newProduct)
        {
            Product product = await _productsRepository.Add(newProduct);

            return CreatedAtAction(nameof(AddProduct), new { productId = product.Id }, product);
        }

        /// <summary>
        /// Изменяет данные товара
        /// </summary>
        /// <param name="productId">ID товара</param>
        /// <param name="changeProduct">Новые данные для товара</param>
        /// <returns>Объект товара</returns>
        /// <response code="200">Возвращает обновленный объект товара</response>
        /// <response code="400">ID в URL и в теле запроса не совпадают</response>
        /// <response code="404">Товар по такому ID не найден</response>
        [HttpPut("{productId}")]
        public async Task<ActionResult<Product?>> ChangeProduct(Guid productId, [FromBody] ChangeProductDTO changeProduct)
        {
            if (productId != changeProduct.Id)
            {
                return BadRequest(PRODUCT_ID_ERROR_MESSAGE);
            }

            Product? product = await _productsRepository.Change(changeProduct);

            if (product == null)
            {
                return NotFound(PRODUCT_NOT_FOUND_ERROR_MESSAGE);
            }

            return Ok(product);
        }

        /// <summary>
        /// Удаляет товар
        /// </summary>
        /// <param name="productId">ID товара</param>
        /// <returns></returns>
        /// <response code="204">Товар удален</response>
        /// <response code="404">Товар по такому ID не найден</response>
        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct (Guid productId)
        {
            Product? product = await _productsRepository.GetById(productId);

            if (product != null)
            {
                await _productsRepository.Delete(productId);
                return NoContent();
            }

            return NotFound(PRODUCT_NOT_FOUND_ERROR_MESSAGE);
        }
    }
}
