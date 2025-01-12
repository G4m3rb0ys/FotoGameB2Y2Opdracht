using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoGameB2Y2Opdracht.MVVM.Models
{
    public class ClaimPhoto
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("ClaimID")]
        public int ClaimId { get; set; }

        [Column("PhotoURL")]
        public string PhotoUrl { get; set; }

        [Column("Date")]
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
