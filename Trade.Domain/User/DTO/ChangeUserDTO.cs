﻿using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Id обязательное поле")]
        public Guid Id { get; set; }

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
