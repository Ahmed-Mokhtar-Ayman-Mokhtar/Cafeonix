using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cafeonix.Models
{
    public enum UserRole
    {
        Admin, //0
        User   //1
    }
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public String Username { get; set; }

        [Required]
        [MaxLength(255)]
        public String Password { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.User; // القيمة الافتراضية: مستخدم عادي

    }
}
