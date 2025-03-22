using System.Text.Json.Serialization;

namespace Trade.Domain
{
    /// <summary>
    /// Позиция в заказе
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID заказа
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// ID товара
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public OrderItem(Guid orderId, Guid productId, int quantity)
        {
            Id = new();
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
