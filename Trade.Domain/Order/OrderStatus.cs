namespace Trade.Domain
{
    /// <summary>
    /// Перечисление статусов заказа.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Ожидает обработки
        /// </summary>
        Pending,

        /// <summary>
        /// В процессе обработки
        /// </summary>
        InProcess,

        /// <summary>
        /// В доставке
        /// </summary>
        InDelivery,

        /// <summary>
        /// Завершен
        /// </summary>
        Done
    }
}
