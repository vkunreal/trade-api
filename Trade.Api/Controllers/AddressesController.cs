using Microsoft.AspNetCore.Mvc;
using Trade.Domain;
using Trade.Infrastructure;
using Trade.Infrastructure.Repositories;

namespace Trade.Api.Controllers
{
    /// <summary>
    /// Контроллер для управления адресами пользователя
    /// </summary>
    [ApiController]
    [Route("/api/users/{userId}/addresses/")]
    public class AddressesController(Context _context) : ControllerBase
    {
        private readonly AddressesRepository _addressesRepository = new(_context);
        private readonly UsersRepository _usersRepository = new(_context);

        private readonly string USER_ERROR_MESSAGE = "Пользователь по такому ID не найден";
        private readonly string ADDRESS_ID_ERROR_MESSAGE = "ID в URL и в теле запроса не совпадают";
        private readonly string ADDRESS_NOT_FOUND_ERROR_MESSAGE = "Адрес по такому ID не найден";

        /// <summary>
        /// Получает список адресов пользователя
        /// </summary>
        /// <returns>Список адресов пользователя</returns>
        /// <response code="200">Возвращает массив адресов пользователя</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAllAddresses(Guid userId)
        {
            return await _addressesRepository.GetAll(userId);
        }

        /// <summary>
        /// Получает адрес пользователя по ID
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="addressId">ID адреса пользователя</param>
        /// <returns>Объект адреса пользователя</returns>
        /// <response code="200">Возвращает объект адреса пользователя по ID</response>
        /// <response code="404">Адрес по такому ID не найден</response>
        [HttpGet("{addressId}")]
        public async Task<ActionResult<Address?>> GetAddress(Guid userId, Guid addressId)
        {
            if ((await ValidateUser(userId)).Result is NotFoundResult)
            {
                return NotFound(USER_ERROR_MESSAGE);
            }

            Address? address = await _addressesRepository.GetById(addressId);

            if (address == null || address.UserId != userId)
            {
                return NotFound(ADDRESS_NOT_FOUND_ERROR_MESSAGE);
            }

            return address;
        }

        /// <summary>
        /// Добавляет новый адрес пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="newAddress">Данные для нового адреса пользователя</param>
        /// <returns>Объект адреса пользователя</returns>
        /// <response code="201">Возвращает объект адреса пользователя</response>
        /// <response code="404">Пользователь с таким ID не найден</response>
        [HttpPost]
        public async Task<ActionResult<Address>> AddAddress(Guid userId, [FromBody] AddAddressDTO newAddress)
        {
            if ((await ValidateUser(userId)).Result is NotFoundResult)
            {
                return NotFound(USER_ERROR_MESSAGE);
            }

            Address createdAddress = await _addressesRepository.Add(userId, newAddress);

            return CreatedAtAction(nameof(GetAddress), new { userId, addressId = createdAddress.Id }, createdAddress);
        }

        /// <summary>
        /// Изменяет данные адреса пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="addressId">ID адреса пользователя</param>
        /// <param name="changeAddress">Новые данные для адреса пользователя</param>
        /// <returns>Объект адреса пользователя</returns>
        /// <response code="200">Возвращает обновленный объект адреса пользователя</response>
        /// <response code="400">ID в URL и в теле запроса не совпадают</response>
        /// <response code="404">Адрес пользователя по такому ID не найден</response>
        /// <response code="404">Пользователь с таким ID не найден</response>
        [HttpPut("{addressId}")]
        public async Task<ActionResult<Address>> ChangeAddress(Guid userId, Guid addressId, [FromBody] ChangeAddressDTO changeAddress)
        {
            if ((await ValidateUser(userId)).Result is NotFoundResult)
            {
                return NotFound(USER_ERROR_MESSAGE);
            }

            if (addressId != changeAddress.Id)
            {
                return BadRequest(ADDRESS_ID_ERROR_MESSAGE);
            }

            Address? updatedAddress = await _addressesRepository.Update(changeAddress);

            if (updatedAddress == null)
            {
                return NotFound(ADDRESS_NOT_FOUND_ERROR_MESSAGE);
            }

            return Ok(updatedAddress);
        }

        /// <summary>
        /// Удаляет адрес пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="addressId">ID адреса пользователя</param>
        /// <returns></returns>
        /// <response code="204">Адрес пользователя удален</response>
        /// <response code="404">Адрес пользователя по такому ID не найден</response>
        [HttpDelete("{addressId}")]
        public async Task<ActionResult> DeleteAddress(Guid userId, Guid addressId)
        {
            Address? address = await _addressesRepository.GetById(addressId);

            if (address != null)
            {
                await _addressesRepository.Delete(addressId);
                return NoContent();
            }

            return NotFound(ADDRESS_NOT_FOUND_ERROR_MESSAGE);
        }

        private async Task<ActionResult<User?>> ValidateUser(Guid userId)
        {
            User? user = await _usersRepository.GetById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
