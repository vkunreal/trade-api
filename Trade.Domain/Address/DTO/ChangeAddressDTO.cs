using System.ComponentModel.DataAnnotations;

namespace Trade.Domain
{
    /// <summary>
    /// DTO Обновления адреса пользователя
    /// </summary>
    public class ChangeAddressDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "Id обязательное поле")]
        public Guid Id { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        [Required(ErrorMessage = "Country обязательное поле")]
        public string Country { get; set; }

        /// <summary>
        /// Регион/Область
        /// </summary>
        [Required(ErrorMessage = "Region обязательное поле")]
        public string Region { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [Required(ErrorMessage = "City обязательное поле")]
        public string City { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        [Required(ErrorMessage = "PostalCode обязательное поле")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        [Required(ErrorMessage = "Street обязательное поле")]
        public string Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "HouseNumber должен быть больше или равен 0")]
        public int HouseNumber { get; set; }

        /// <summary>
        /// Номер квартиры (при наличии)
        /// </summary>
        public int? FlatNumber { get; set; }

        /// <summary>
        /// Комментарий к адресу
        /// </summary>
        public string AddressDescription { get; set; } = string.Empty;
    }
}
