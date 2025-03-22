using System.ComponentModel.DataAnnotations;

namespace Trade.Domain
{
    /// <summary>
    /// DTO Создания адреса пользователя
    /// </summary>
    public class AddAddressDTO
    {
        /// <summary>
        /// Страна
        /// </summary>
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        /// <summary>
        /// Регион/Область
        /// </summary>
        [Required(ErrorMessage = "Region is required")]
        public string Region { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        [Required(ErrorMessage = "PostalCode is required")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "HouseNumber must be higher or equals than 0")]
        public int HouseNumber { get; set; }

        /// <summary>
        /// Номер квартиры (при наличии)
        /// </summary>
        public int? FlatNumber { get; set; }

        /// <summary>
        /// Комментарий к адресу
        /// </summary>
        public string AddressDescription { get; set; }
    }
}
