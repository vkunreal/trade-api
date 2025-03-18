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

        public Guid AddressId { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Итоговая цена
        /// </summary>
        //public decimal TotalPrice
        //{
        //    get
        //    {
        //        return Products.Aggregate(0m, (acc, cur) => acc + (cur.Price * cur.Discount));
        //    }
        //}

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        public virtual User User { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Order(Guid userId, string comment)
        {
            Id = new();
            UserId = userId;
            Comment = comment;
        }
    }
}
