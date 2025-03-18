using Microsoft.AspNetCore.Mvc;
using Trade.Api;
using Trade.Domain;
using Trade.Infrastructure;
using Trade.Infrastructure.Repositories;

namespace Trade.Api.Controllers
{
    /// <inheritdoc />
    [ApiController]
    [Route("/api/users/")]
    public class UsersController(Context context) : ControllerBase, IUsersFacade
    {
        private readonly UsersRepository _usersRepository = new UsersRepository(context);

        /// <inheritdoc />
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _usersRepository.GetAll();
        }

        /// <inheritdoc />
        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUser(Guid id)
        {
            return await _usersRepository.GetById(id);
        }

        /// <inheritdoc />
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(AddUserDTO newUser)
        {
            User user = new User(newUser.FirstName, newUser.LastName, newUser.Email);

            await _usersRepository.Add(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        /// <inheritdoc />
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> ChangeUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _usersRepository.Update(user);

            return user;
        }

        /// <inheritdoc />
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            User? user = await _usersRepository.GetById(id);

            if (user != null)
            {
                await _usersRepository.Delete(id);
                return NoContent();
            }

            return NotFound();
        }
    }
}