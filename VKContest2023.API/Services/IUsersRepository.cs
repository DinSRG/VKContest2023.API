using VKContest2023.API.Model;

namespace VKContest2023.API.Services
{
    public interface IUsersRepository
    {
        Task<(IEnumerable<User>, PaginationMetadata)> GetUsersAsync(int pageNumber, int pageSize);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserAsync(int userId);
        Task AddUserAsync(User user);
        void DeleteUser(int userId, User user);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<UserGroup?>> GetUserGroupsAsync();
    }
}
