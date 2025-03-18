namespace Trade.Domain
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title {  get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Скидка
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Product (string title, string description, decimal price, decimal discount, int count)
        {
            Title = title;
            Description = description;
            Price = price;
            Discount = discount;
            Count = count;

            CreatedAt = DateTime.Now.ToUniversalTime();
            UpdatedAt = DateTime.Now.ToUniversalTime();
        }
    }
}
