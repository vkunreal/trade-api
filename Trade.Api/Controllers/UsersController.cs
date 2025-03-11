using Microsoft.AspNetCore.Mvc;
using Trade.Domain;
using Trade.Infrastructure;
using Trade.Infrastructure.Repositories;

namespace Trade.Api.Controllers
{
    [ApiController]
    [Route("/api/users/")]
    public class UsersController : ControllerBase
    {
        private readonly UsersRepository _usersRepository;

        public UsersController(Context context)
        {
            _usersRepository = new UsersRepository(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _usersRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUser(Guid id)
        {
            return await _usersRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(AddUserDTO newUser)
        {
            User user = new User(newUser.FirstName, newUser.LastName, newUser.Email);

            await _usersRepository.Add(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

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