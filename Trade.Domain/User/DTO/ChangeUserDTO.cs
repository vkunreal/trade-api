using System.ComponentModel.DataAnnotations;

namespace Trade.Domain
{
    /// <summary>
    /// DTO Изменения пользователя
    /// </summary>
    public class ChangeUserDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

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
