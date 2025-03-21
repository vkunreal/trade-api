﻿using System.Text.Json.Serialization;

namespace Trade.Domain
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

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

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Address Address { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Order(Guid userId, Guid addressId, string comment, OrderStatus status)
        {
            Id = new();
            UserId = userId;
            AddressId = addressId;
            Comment = comment;
            Status = status;
        }
    }
}
