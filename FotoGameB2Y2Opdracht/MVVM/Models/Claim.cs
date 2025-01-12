using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoGameB2Y2Opdracht.MVVM.Models
{
    [Table("Claim")]
    public class Claim
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("TaskID")]
        public int TaskId { get; set; }

        [Column("UserID")]
        public int UserId { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Deadline")]
        public DateTime Deadline { get; set; }

        [Column("Points")]
        public int Points { get; set; }

        [Column("IsWeekly")]
        public bool IsWeekly { get; set; }

        [Column("PhotoUrls")]
        public string PhotoUrls { get; set; }
    }

}
