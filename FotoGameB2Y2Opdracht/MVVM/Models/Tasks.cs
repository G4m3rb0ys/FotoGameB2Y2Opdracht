using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FotoGameB2Y2Opdracht.MVVM.Models
{
    [Table("Tasks")]
    public class Tasks
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Deadline")]
        public DateTime Deadline { get; set; }

        [Column("Cost")]
        public int Cost { get; set; } = 20;

        [Column("Reward")]
        public int Reward { get; set; } = 10;

        [Column("CreatorID")]
        public int CreatorID { get; set; }

        [Column("IsWeekly")]
        public bool IsWeekly { get; set; }

        [Ignore]
        public List<WeeklyTaskGoal> WeeklyGoals { get; set; }
    }
}
