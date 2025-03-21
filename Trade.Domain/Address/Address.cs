﻿using System.Text.Json.Serialization;

namespace Trade.Domain
{
    /// <summary>
    /// Адрес пользователя
    /// </summary>
    public class Address
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
        /// Страна
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Регион/Область
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        public int HouseNumber { get; set; }

        /// <summary>
        /// Номер квартиры (при наличии)
        /// </summary>
        public int? FlatNumber { get; set; }

        /// <summary>
        /// Комментарий к адресу
        /// </summary>
        public string AddressDescription { get; set; }

        public Address(Guid userId, string country, string region, string city, string postalCode, string street, int houseNumber, int? flatNumber, string addressDescription)
        {
            Id = new();
            UserId = userId;

            Country = country;
            Region = region;
            City = city;
            PostalCode = postalCode;
            Street = street;
            HouseNumber = houseNumber;
            FlatNumber = flatNumber;
            AddressDescription = addressDescription;
        }

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
