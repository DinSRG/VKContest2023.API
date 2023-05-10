using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using VKContest2023.API.Model;
using VKContest2023.API.Services;

namespace VKContest2023.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        const int maxUsersPageSize = 20;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository ??
                throw new ArgumentNullException(nameof(usersRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get(int pageNumber = 1, int pageSize = 10)
        {
            if(pageSize > maxUsersPageSize)
            {
                pageSize = maxUsersPageSize;
            }

            var (userEntities, paginationMetadata) = await _usersRepository
                .GetUsersAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(userEntities);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _usersRepository.GetUserAsync(id);
            if (user == null)
            {
                return NotFound("Пользователь не найден!");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserToAdd userToAdd)
        {
            var users = await _usersRepository.GetAllUsersAsync();
            var groups = await _usersRepository.GetUserGroupsAsync();
            if (!groups.Select(x => x?.Id).Contains(userToAdd.UserGroupId))
            {
                return BadRequest($"Группы с id = {userToAdd.UserGroupId} не существует!");
            };

            // TODO: Шифрование пароля.
            var maxUserId = 0;
            foreach (var user in users)
            {
                if (user.UserGroupId == 1
                    && user.UserState?.Code != "Blocked"
                    && userToAdd.UserGroupId == 1)
                    return BadRequest("Админом может быть только один пользователь!");
                if (user.Login == userToAdd.Login)
                    return BadRequest("Пользователь с таким логином уже существует!");
                maxUserId = Math.Max(user.Id, maxUserId);
            }

            // При добавлении ничего не сказано о том, будут ли
            // учитываться логины пользователей с UserState = "Blocked".
            // Поэтому здесь они проверяются тоже.

            var newUser = new User
            {
                Id = ++maxUserId, 
                Login = userToAdd.Login,
                Password = userToAdd.Password,
                CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                UserGroupId = userToAdd.UserGroupId,
                UserStateId = 1
            };
            
            await Task.Delay(5000);
            await _usersRepository.AddUserAsync(newUser);
            await _usersRepository.SaveChangesAsync();
            return CreatedAtRoute("GetUser", new { id = newUser.Id }, newUser);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _usersRepository.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound("Пользователь не найден!");
            }
            _usersRepository.DeleteUser(userId, user);
            await _usersRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
