using SQLite;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace FotoGameB2Y2Opdracht.MVVM.Models
{
    [Table("ClaimWeeklyGoal")]
    public class ClaimWeeklyGoal
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("ClaimId")]
        public int ClaimId { get; set; }

        [Column("WeekNumber")]
        public int WeekNumber { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }

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
