using System.ComponentModel.DataAnnotations;

namespace Trade.Domain
{
    /// <summary>
    /// DTO изменения товара
    /// </summary>
    public class ChangeProductDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "Id обязательное поле")]
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Required(ErrorMessage = "Title обязательное поле")]
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [Required(ErrorMessage = "Description обязательное поле")]
        public string Description { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price должен быть больше или равен 0.01")]
        public decimal Price { get; set; }

        /// <summary>
        /// Скидка
        /// </summary>
        [Range(0, 100, ErrorMessage = "Discount должен быть больше или равен 0")]
        public decimal Discount { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Count должен быть больше или равен 0")]
        public int Count { get; set; }
    }
}
