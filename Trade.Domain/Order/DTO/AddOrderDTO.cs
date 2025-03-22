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
        public Guid UserId { get; set; }

        /// <summary>
        /// ID использованного адреса
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public OrderStatus Status { get; set; }

        public List<AddOrderProductDTO> Products { get; set; }
    }

    public class AddOrderProductDTO
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
