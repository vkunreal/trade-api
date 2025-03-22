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
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
