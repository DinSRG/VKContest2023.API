using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using VKContest2023.API.DBData;
using VKContest2023.API.Model;

namespace VKContest2023.API.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DBConfigContext _context;

        public UsersRepository(DBConfigContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.UserState)
                .Include(u => u.UserGroup)
                .Where(u => u.UserState.Code != "Blocked" && u.Id == userId)
                .FirstOrDefaultAsync();
            // Сравнение со строкой не очень производительный вариант, однако таким
            // образом избегаю случая, если UserState.id в разных БД отличаются.
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserState)
                .Include(u => u.UserGroup)
                .ToListAsync();
        }

        // Реализация пагинации:
        public async Task<(IEnumerable<User>, PaginationMetadata)> GetUsersAsync(int pageNumber, int pageSize)
        {

            var totalItemCount = await _context.Users.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var resultCollection = await _context.Users
                .Include(u => u.UserState)
                .Include(u => u.UserGroup)
                .Where(u => u.UserState.Code != "Blocked")
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (resultCollection, paginationMetadata);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void DeleteUser(int userId, User user)
        {
            user.UserStateId = 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<IEnumerable<UserGroup?>> GetUserGroupsAsync()
        {
            return await _context.UserGroups
                .ToListAsync();
        }
    }
}
