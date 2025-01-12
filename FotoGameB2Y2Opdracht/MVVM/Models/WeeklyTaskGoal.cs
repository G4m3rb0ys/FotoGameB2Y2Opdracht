using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FotoGameB2Y2Opdracht.MVVM.Models
{

    [Table("WeeklyTaskGoal")]

    public class WeeklyTaskGoal
    {

        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("TaskID")]
        public int TaskID { get; set; }

        [Column("Weeknumber")]
        public int Weeknumber { get; set; }

        [Column("Description")]
        public string Description { get; set; }

    }
}
