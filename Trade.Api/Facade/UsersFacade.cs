using Microsoft.AspNetCore.Mvc;
using Trade.Domain;

namespace Trade.Api
{
    /// <summary>
    /// Контроллер для управления пользователями
    /// </summary>
    public interface IUsersFacade
    {
        /// <summary>
        /// Получает список пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        /// <response code="200">Возвращает массив пользователей</response>
        Task<ActionResult<IEnumerable<User>>> GetAllUsers();

        /// <summary>
        /// Получает пользователя по ID
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns>Объект пользователя</returns>
        /// <response code="200">Возвращает объект пользователя по ID</response>
        /// <response code="404">Пользователь по такому ID не найден</response>
        public Task<ActionResult<User?>> GetUser(Guid id);

        /// <summary>
        /// Добавляет нового пользователя
        /// </summary>
        /// <param name="newUser">Данные для нового пользователя</param>
        /// <returns>Объект пользователя</returns>
        /// <response code="201">Возвращает объект пользователя по ID</response>
        public Task<ActionResult<User>> AddUser(AddUserDTO newUser);

        /// <summary>
        /// Изменяет данные пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <param name="user">Новые данные для пользователя</param>
        /// <returns>Объект пользователя</returns>
        /// <response code="200">Возвращает обновленный объект пользователя</response>
        /// <response code="404">Пользователь по такому ID не найден</response>
        public Task<ActionResult<User>> ChangeUser(Guid id, User user);

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns></returns>
        /// <response code="204">Пользователь удален</response>
        /// <response code="404">Пользователь по такому ID не найден</response>
        public Task<ActionResult> DeleteUser(Guid id);
    }
}
