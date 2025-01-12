using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FotoGameB2Y2Opdracht
{
    public static class Constants
    {
        private const string DBFileName = "FotoGameB2Y2Opdracht.db3";

        public const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        public static string DBPath
        {
            get
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, DBFileName);
                return dbPath;
            }
        }
    }
}
