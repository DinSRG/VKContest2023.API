using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKContest2023.API.Model
{
    [Table("user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("login")]
        public string? Login { get; set; }
        
        [Column("password")]
        public string? Password { get; set; }
        [Column("created_date")]
        public DateOnly CreatedDate { get; set; }


        [Column("user_group_id")]
        [ForeignKey("UserGroupKey")]
        public int UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }


        [Column("user_state_id")]
        [ForeignKey("UserStateKey")]
        public int UserStateId { get; set; }
        public UserState? UserState { get; set; }
       
    }
}
