namespace Trade.Domain
{
    /// <summary>
    /// DTO Создания пользователя
    /// </summary>
    public class AddUserDTO
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }
    }
}
