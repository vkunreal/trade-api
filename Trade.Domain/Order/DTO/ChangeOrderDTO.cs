using System.ComponentModel.DataAnnotations;

namespace Trade.Domain
{
    /// <summary>
    /// DTO изменения заказа
    /// </summary>
    public class ChangeOrderDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "Id обязательное поле")]
        public Guid Id { get; set; }

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
    }
}
