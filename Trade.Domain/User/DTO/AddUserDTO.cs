using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "FirstName обязательное поле")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "LastName обязательное поле")]
        public string LastName { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        [Required(ErrorMessage = "Email обязательное поле")]
        public string Email { get; set; }
    }
}
