using System.ComponentModel.DataAnnotations;

namespace Trade.Domain
{
    /// <summary>
    /// DTO добавления заказ
    /// </summary>
    public class AddOrderDTO
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        [Required(ErrorMessage = "UserId обязательное поле")]
        public Guid UserId { get; set; }

        /// <summary>
        /// ID использованного адреса
        /// </summary>
        [Required(ErrorMessage = "AddressId обязательное поле")]
        public Guid AddressId { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// Список товаров в заказе
        /// </summary>
        [Required(ErrorMessage = "Products обязательное поле")]
        public List<AddOrderProductDTO> Products { get; set; }
    }

    /// <summary>
    /// DTO товара при добавлении заказа
    /// </summary>
    public class AddOrderProductDTO
    {
        /// <summary>
        /// ID товара
        /// </summary>
        [Required(ErrorMessage = "ProductId обязательное поле")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        [Required(ErrorMessage = "Quantity обязательное поле")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity должно быть больше нуля")]
        public int Quantity { get; set; }
    }
}
