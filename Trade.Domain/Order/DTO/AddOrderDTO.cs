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
}
