using Microsoft.AspNetCore.Mvc;
using Trade.Domain;
using Trade.Infrastructure;
using Trade.Infrastructure.Repositories;

namespace Trade.Api.Controllers
{
    /// <summary>
    /// Контроллер для управления пользователями
    /// </summary>
    [ApiController]
    [Route("/api/users/")]
    public class UsersController(Context _context) : ControllerBase
    {
        private readonly UsersRepository _usersRepository = new (_context);

        /// <summary>
        /// Получает список пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        /// <response code="200">Возвращает массив пользователей</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _usersRepository.GetAll();
        }

        /// <summary>
        /// Получает пользователя по ID
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns>Объект пользователя</returns>
        /// <response code="200">Возвращает объект пользователя по ID</response>
        /// <response code="404">Пользователь по такому ID не найден</response>
        [HttpGet("{userId}")]
        public async Task<ActionResult<User?>> GetUser(Guid userId)
        {
            return await _usersRepository.GetById(userId);
        }

        /// <summary>
        /// Добавляет нового пользователя
        /// </summary>
        /// <param name="newUser">Данные для нового пользователя</param>
        /// <returns>Объект пользователя</returns>
        /// <response code="201">Возвращает объект пользователя по ID</response>
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] AddUserDTO newUser)
        {
            User user = await _usersRepository.Add(newUser);

            return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        }

        /// <summary>
        /// Изменяет данные пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="user">Новые данные для пользователя</param>
        /// <returns>Объект пользователя</returns>
        /// <response code="200">Возвращает обновленный объект пользователя</response>
        /// <response code="404">Пользователь по такому ID не найден</response>
        [HttpPut("{userId}")]
        public async Task<ActionResult<User>> ChangeUser(Guid userId, [FromBody] ChangeUserDTO changeUser)
        {
            if (userId != changeUser.Id)
            {
                return BadRequest(userId);
            }

            User? updatedUser = await _usersRepository.Update(changeUser);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns></returns>
        /// <response code="204">Пользователь удален</response>
        /// <response code="404">Пользователь по такому ID не найден</response>
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            User? user = await _usersRepository.GetById(userId);

            if (user != null)
            {
                await _usersRepository.Delete(userId);
                return NoContent();
            }

            return NotFound();
        }
    }
}