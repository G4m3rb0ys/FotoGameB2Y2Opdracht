using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        public string WeeklyGoalsJson { get; set; }

        [Ignore]
        public List<WeeklyTaskGoal> WeeklyGoals
        {
            get => string.IsNullOrWhiteSpace(WeeklyGoalsJson) ? new List<WeeklyTaskGoal>() : JsonSerializer.Deserialize<List<WeeklyTaskGoal>>(WeeklyGoalsJson);
            set => WeeklyGoalsJson = JsonSerializer.Serialize(value);
        }

        [Column("PhotoUrls")]
        public string PhotoUrlsJson { get; set; }

        [Ignore]
        public List<string> PhotoUrls
        {
            get => string.IsNullOrWhiteSpace(PhotoUrlsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(PhotoUrlsJson);
            set => PhotoUrlsJson = JsonSerializer.Serialize(value);
        }


    }

}
