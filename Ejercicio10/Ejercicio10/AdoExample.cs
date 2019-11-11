using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using Mono.Data.Sqlite;


namespace Ejercicio10
{
    public static class AdoExample
    {
        public static SqliteConnection connection;
        public static string DoSomeDataAccess()
        {
            string output = string.Empty;
            // determine the path for the database file
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "adodemo.db3");

            bool exists = File.Exists(dbPath);

            if (!exists)
            {

                // Need to create the database before seeding it with some data
                Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);
                connection = new SqliteConnection("Data Source=" + dbPath);


                // Open the database connection and create table with data
                connection.Open();
                CreateTable();
            }
            else
            {

                // Open connection to existing database file
                connection = new SqliteConnection("Data Source=" + dbPath);

                connection.Open();
                var command = "DROP TABLE [Items];";
                using (var c = connection.CreateCommand())
                {
                    c.CommandText = command;
                    var rowcount = c.ExecuteNonQuery();
                    Console.WriteLine("\tExecuted " + command);
                }
                CreateTable();

            }

            // query the database to prove data was inserted!
            using (var contents = connection.CreateCommand())
            {
                contents.CommandText = "SELECT [_id], [Symbol] from [Items]";
                var r = contents.ExecuteReader();

                while (r.Read())
                    output += String.Format("\n\tKey={0}; Value={1}",
                                      r["_id"].ToString(),
                                      r["Symbol"].ToString());

            }

            connection.Close();

            return output;
        }
        private static void CreateTable()
        {
            var commands = new[] {
            "CREATE TABLE [Items] (_id ntext, Symbol ntext);",
            "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('1', 'hugo')",
            "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('2', 'torrico')",
            "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('3', 'marquez')"
                };
            foreach (var command in commands)
            {
                using (var c = connection.CreateCommand())
                {
                    c.CommandText = command;
                    var rowcount = c.ExecuteNonQuery();
                }
            }
        }


    }
}
