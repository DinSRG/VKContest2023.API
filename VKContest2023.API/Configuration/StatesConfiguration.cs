using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VKContest2023.API.Model;

namespace VKContest2023.API.Configuration
{
    public class StatesConfiguration : IEntityTypeConfiguration<UserState>
    {
        public void Configure(EntityTypeBuilder<UserState> builder)
        {
            builder.ToTable("user_state");

            builder.HasData
            (
                new UserGroup
                {
                    Id = 1,
                    Code = "Active",
                    Description = "User can use and be used!"
                },
                new UserGroup
                {
                    Id = 2,
                    Code = "Blocked",
                    Description = "Deleted user."
                }
            );
        }
    }
}
