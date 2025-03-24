namespace Trade.Domain
{
    public class OrderResultDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID пользователя
        /// </summary>
        public OrderUserDTO User { get; set; }

        /// <summary>
        /// ID использованного адреса
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Итоговая цена
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Список товаров
        /// </summary>
        public List<OrderResultProductDTO> Products { get; set; }
    }
}
