using System.ComponentModel.DataAnnotations;

namespace Trade.Domain
{
    /// <summary>
    /// DTO создания товара
    /// </summary>
    public class AddProductDTO
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be higher or equals than 0.01")]
        public decimal Price { get; set; }

        /// <summary>
        /// Скидка
        /// </summary>
        [Range(0, 100, ErrorMessage = "Discount must be higher or equals than 0")]
        public decimal Discount { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Count must be higher or equals than 0")]
        public int Count { get; set; }
    }
}
