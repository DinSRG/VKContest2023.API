using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VKContest2023.API.Model;

namespace VKContest2023.API.Configuration
{
    public class GroupsConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("user_group");

            builder.HasData
            (
                new UserGroup
                {
                    Id = 1,
                    Code = "Admin",
                    Description = "Actual Administrator!"
                },
                new UserGroup
                {
                    Id = 2,
                    Code = "User",
                    Description = "I'm only user after all..."
                }
            );
        }
    }
}
