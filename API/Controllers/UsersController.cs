using API.Database;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/users/")]
    public class UsersController: ControllerBase
    {
        private readonly Context _context;
        private readonly UsersRepository _usersRepository;

        public UsersController(Context context) {
            _context = context;
            _usersRepository = new UsersRepository(context);
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetAll() {
        //    return await _context.Users.ToListAsync();
        //}

        //[HttpPost]
        //public async Task<ActionResult<User>> CreateUser ()
        //{

        //    _context.Users.Add(user);

        //    await _context.SaveChangesAsync();

        //    return Ok(user);
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers ()
        {
            return await _usersRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUser (Guid id)
        {
            return await _usersRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User newUser)
        {
            User user = await _usersRepository.Add(newUser);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> ChangeUser (Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _usersRepository.Update(user);

            return user;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser (Guid id)
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
