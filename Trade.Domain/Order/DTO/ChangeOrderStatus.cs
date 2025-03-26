using System.ComponentModel.DataAnnotations;

namespace Trade.Domain
{
    /// <summary>
    /// DTO изменения статуса заказа
    /// </summary>
    public class ChangeOrderStatus
    {
        /// <summary>
        /// Новый статус заказа
        /// </summary>
        [Required(ErrorMessage = "Status обязательное поле")]
        required public OrderStatus Status { get; set; }
    }
}
