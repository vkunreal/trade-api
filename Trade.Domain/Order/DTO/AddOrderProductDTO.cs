using System.ComponentModel.DataAnnotations;

namespace Trade.Domain
{
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
