using System.ComponentModel.DataAnnotations;

namespace VKContest2023.API.Model
{
    public class UserToAdd
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string? Login { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        public string? Password { get; set; }
        [Required]
        public int UserGroupId { get; set; }
    }
}
