using System.Data.SQLite;
using System.Data.Common;
using System;
using System.IO;
using System.Linq;

namespace DbInitializer
{
    class Program
    {
        static void Main( string[] args )
        {
            var baseName = @"test.db";

            SQLiteConnection.CreateFile(baseName);
            Console.WriteLine(File.Exists(baseName) ? "База данных создана" : "Возникла ошиюка при создании базы данных");

            var connectionString = string.Format(@"Data Source={0};",baseName);

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(BaseIniciolizer(),connection);

                Console.WriteLine("Инициализация таблиц");
                command.ExecuteNonQuery();
                Console.WriteLine("Таблици инициализированы");

                Console.WriteLine();
                Console.WriteLine("Список таблиц:");
                command.CommandText = @"SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";
                using (var reader = command.ExecuteReader())
                {
                    foreach (DbDataRecord record in reader)
                    {
                        if ((string)record["name"] != "sqlite_sequence") // Таблица созданая SQLite
                            Console.WriteLine("Таблица: " + record["name"]);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Добовление данных в таблици");

                var columnNames = new[] { "Cource","Name" };
                var tableName = "Groups";
                var comGroups = string.Empty;
                for (int i = 1; i < 7; i++)
                {
                    comGroups += Insert(tableName,columnNames,new[] { $"{i}",$"{i}03" });
                }

                tableName = "Sciences";
                columnNames = new[] { "Name" };
                var comSciences = Insert(tableName,columnNames,new[] { "Матан" })
                                + Insert(tableName,columnNames,new[] { "Исследование операций" })
                                + Insert(tableName,columnNames,new[] { "Комплексный анализ" })
                                + Insert(tableName,columnNames,new[] { "Дифференциальные уравнения" })
                                + Insert(tableName,columnNames,new[] { "Компьютерная графика" });

                command.CommandText = comGroups + comSciences;
                command.ExecuteNonQuery();

                var comStudents = string.Empty;
                command.CommandText = "SELECT * FROM 'Groups';";
                using (var reader = command.ExecuteReader())
                {
                    tableName = "Students";
                    columnNames = new[] { "LastName","Name","SecondName","GroupId" };
                    foreach (DbDataRecord record in reader)
                    {
                        var id = record["Id"].ToString();
                        comStudents += Insert(tableName,columnNames,new[] { "Первокурсник","Карл","Первый",id })
                                    + Insert(tableName,columnNames,new[] { "Второкурсник","Евлампий","Федорович",id })
                                    + Insert(tableName,columnNames,new[] { "Третьекурсник","Георгий","Георгице",id })
                                    + Insert(tableName,columnNames,new[] { "Четверокурсник","Альберт","Заборович",id })
                                    + Insert(tableName,columnNames,new[] { "Пятикурсник","Рыцарь","Римский",id });
                    }
                }
                command.CommandText = comStudents;
                command.ExecuteNonQuery();

                var comGrades = string.Empty;
               /* command.CommandText = "SELECT count(Sciences.Id) FROM 'Sciences';";
                int[] IdSciences;
                using (var reader = command.ExecuteReader())
                {

                    IdSciences = new int[(int)reader[0]];
                }
                command.CommandText = "SELECT Sciences.Id FROM 'Sciences';";

                using (var reader = command.ExecuteReader())
                {
                    int i = 0;
                    foreach (int record in reader)
                    {
                        IdSciences[i] = record;
                            i++;
                    }
                }*/






                using (var reader = command.ExecuteReader())
                {
                    tableName = "Students";
                    columnNames = new[] { "LastName","Name","SecondName","GroupId" };



                    foreach (DbDataRecord record in reader)
                    {
                        var id = record["Id"].ToString();
                        comStudents += Insert(tableName,columnNames,new[] { "Первокурсник","Карл","Первый",id })
                                    + Insert(tableName,columnNames,new[] { "Второкурсник","Евлампий","Федорович",id })
                                    + Insert(tableName,columnNames,new[] { "Третьекурсник","Георгий","Георгице",id })
                                    + Insert(tableName,columnNames,new[] { "Четверокурсник","Альберт","Заборович",id })
                                    + Insert(tableName,columnNames,new[] { "Пятикурсник","Рыцарь","Римский",id });
                    }
                }



                command.CommandText = comGrades;
                command.ExecuteNonQuery();









                Console.WriteLine("Таблици заполнены");





                //Решение






                command.CommandText = @"select Groups.Name, count(Students.GroupId) from Groups inner join Students on Groups.Id = Students.GroupId group by Groups.Name";

                //command.CommandText = @"select g.Id from Groups g INNER JOIN Students s on g.Id = s.GroupId";
                using (var reader = command.ExecuteReader())
                {
                    foreach (DbDataRecord record in reader)
                    {
                        Console.WriteLine("Группа: " + record[0] + "\t Количество человек в группе: " + record[1]);
                    }
                }
            }
            Console.ReadKey();
        }

        static string BaseIniciolizer()
        {
            var sqlCommand = CreateTable("Groups",@"Cource INT NOT NULL, Name CHAR(50) NOT NULL UNIQUE")
                            + CreateTable("Sciences",@"Name CHAR(30) NOT NULL UNIQUE")
                            + CreateTable("Students",@"LastName CHAR(20) NOT NULL, Name CHAR(20) NOT NULL, SecondName CHAR(20), GroupId INTEGER NOT NULL," + ForeignKey("Group"))
                            + CreateTable("Grades",@"mark SMALLINT NOT NULL, StudentId INTEGER NOT NULL, ScienceId INTEGER NOT NULL, " + ForeignKey("Student") + "," + ForeignKey("Science"));

            return sqlCommand;
        }

        static string CreateTable( string tableName,string tableInfo )
        {
            return string.Format(@"CREATE TABLE {0}(Id INTEGER PRIMARY KEY AUTOINCREMENT, {1});",tableName,tableInfo);
        }

        static string ForeignKey( string table )
        {
            return string.Format(" FOREIGN KEY({0}Id) REFERENCES {0}s(Id)",table);
        }

        static string Insert( string tableName,string[] columnNames,string[] columnValues )
        {
            var collumnName = string.Join(",",columnNames.Select(x => $"'{x}'"));
            var columnValue = string.Join(",",columnValues.Select(x => $"'{x}'"));
            return string.Format("INSERT INTO '{0}' ({1}) VALUES ({2});",tableName,collumnName,columnValue);
        }
    }
}