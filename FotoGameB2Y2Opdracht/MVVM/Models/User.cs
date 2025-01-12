using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FotoGameB2Y2Opdracht.MVVM.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Username")]
        public string Username { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Credits")]
        public int credits { get; set; } = 60;
        [Column("isAdmin")]
        public bool isAdmin { get; set; } = false;
        [Column("isSuper")]
        public bool isSuper { get; set; } = false;
        [Column("isDisabled")]
        public bool isDisabled { get; set; } = false;
        [Column("isLoggedIn")]
        public bool isLoggedIn { get; set; } = false;
    }
}
