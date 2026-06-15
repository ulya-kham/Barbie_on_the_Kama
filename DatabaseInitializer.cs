using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Barbie_on_the_Kama
{
    public static class DatabaseInitializer
    {
        private static string dbPath = "barbie.db";

        public static void Initialize()
        {
            if (File.Exists(dbPath))
                return;

            SQLiteConnection.CreateFile(dbPath);

            using (SQLiteConnection conn =
                new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                string usersTable =
                @"
                CREATE TABLE users
                (
                    name VARCHAR(15) PRIMARY KEY,
                    password VARCHAR(255) NOT NULL,

                    level INTEGER NOT NULL,
                    points REAL NOT NULL,
                    money REAL NOT NULL,

                    worm INTEGER DEFAULT 10,
                    oparysh INTEGER DEFAULT 0,
                    kuznec INTEGER DEFAULT 0,
                    rucheinik INTEGER DEFAULT 0,
                    zhivets INTEGER DEFAULT 0,

                    rod1 INTEGER DEFAULT 1,
                    rod2 INTEGER DEFAULT 0,
                    rod3 INTEGER DEFAULT 0,
                    rod4 INTEGER DEFAULT 0,
                    rod5 INTEGER DEFAULT 0,

                    popl1 INTEGER DEFAULT 1,
                    popl2 INTEGER DEFAULT 0,
                    popl3 INTEGER DEFAULT 0,
                    popl4 INTEGER DEFAULT 0,
                    popl5 INTEGER DEFAULT 0
                );
                ";

                string recordsTable =
                @"
                CREATE TABLE records
                (
                    fish VARCHAR(50) PRIMARY KEY,
                    weight INTEGER NOT NULL,
                    name VARCHAR(15),
                    catch_date TEXT,
                    location TEXT
                );
                ";

                new SQLiteCommand(usersTable, conn).ExecuteNonQuery();
                new SQLiteCommand(recordsTable, conn).ExecuteNonQuery();

                InsertDefaultRecords(conn);
            }
        }

        private static void InsertDefaultRecords(SQLiteConnection conn)
        {
            string[] fishes =
    {
        "Гольян",
        "Голавль",
        "Плотва",
        "Подуст",
        "Хариус",
        "Язь",
        "Щука",
        "Окунь",
        "Елец",
        "Пескарь",
        "Таймень",
        "Ручьевая форель",
        "Подкаменщик",
        "Жерех"
    };

            foreach (string fish in fishes)
            {
                string query =
                @"INSERT INTO records
        (fish, weight, name, catch_date, location)
        VALUES
        (@fish, 0, '', NULL, '')";

                using (SQLiteCommand cmd =
                    new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fish", fish);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
