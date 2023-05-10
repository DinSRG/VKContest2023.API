using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VKContest2023.API.Model
{
    [Table("user_group")]
    public class UserGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("code")]
        public string? Code { get; set; }
        [Column("description")]
        public string Description { get; set; } = string.Empty;
    }
}
