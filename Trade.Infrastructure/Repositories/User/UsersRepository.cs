using Microsoft.EntityFrameworkCore;
using Trade.Domain;

namespace Trade.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
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

        public async Task<User> Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Update(User user)
        {
            User? existUser = await _context.Users.FindAsync(user.Id);
            user.UpdatedAt = DateTime.Now.ToUniversalTime();

            if (existUser != null)
            {
                _context.Entry(existUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                return user;
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