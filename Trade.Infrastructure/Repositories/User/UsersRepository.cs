using Microsoft.EntityFrameworkCore;
using Trade.Domain;

namespace Trade.Infrastructure.Repositories
{
    public class UsersRepository
    {
        private readonly Context _context;

        public UsersRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.OrderBy(u => u.LastName).ToListAsync();
        }

        public async Task<User?> GetById(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> Add(AddUserDTO newUser)
        {
            User user = new User(newUser.FirstName, newUser.LastName, newUser.Email);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> Update(ChangeUserDTO user)
        {
            User? existUser = await _context.Users.FindAsync(user.Id);

            if (existUser != null)
            {
                existUser.FirstName = user.FirstName;
                existUser.LastName = user.LastName;
                existUser.Email = user.Email;
                existUser.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return existUser;
            }

            return null;
        }

        public async Task Delete(Guid userId)
        {
            User? user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}