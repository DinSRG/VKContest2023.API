using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VKContest2023.API.Model;

namespace VKContest2023.API.Configuration
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            var currDate = DateOnly.FromDateTime(DateTime.Now);

            builder.HasData
            (
                new User
                {
                    Id = 1,
                    Login = "Old Ilya",
                    Password = "asdfasdf",
                    CreatedDate = currDate,
                    UserGroupId = 1,
                    UserStateId = 2
                },
                new User
                {
                    Id = 2,
                    Login = "Ilya",
                    Password = "345235234",
                    CreatedDate = currDate,
                    UserGroupId = 1,
                    UserStateId = 1
                },
                new User
                {
                    Id = 3,
                    Login = "You",
                    Password = "345235234df",
                    CreatedDate = currDate,
                    UserGroupId = 2,
                    UserStateId = 1
                },
                new User
                {
                    Id = 4,
                    Login = "UserUser",
                    Password = "3452f",
                    CreatedDate = currDate,
                    UserGroupId = 2,
                    UserStateId = 1
                }
            );
        }
    }
}
