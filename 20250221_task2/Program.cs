using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace _20250221_task2
{
    internal class Program
    {
        /*
         Описать класс “Меню Блюд”. Используя 2 отдельных using, 
        добавить данные в таблицу, как один объект, так и коллекцию. 
        Считать из таблицы информацию в коллекцию, проверить перед этим доступность базы данных. 
        Получить все блюда, в названии которых содержится слово “Суп”. Получить блюдо по Id. 
        Получить самое последнее блюдо из таблицы.
         */

        public static string connectionString = "Data Source=MIRUAHUA;Initial Catalog=Task21Feb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        static void Main(string[] args)
        {
            //using (ApplicationContext db = new ApplicationContext())
            //{
            //    Dish d1 = new Dish { Name = "Greek Salad", Description = "Tomato, greens, onion, oil" };
            //    db.Dish.Add(d1);
            //    db.SaveChanges();
            //}

            //using (ApplicationContext db = new ApplicationContext())
            //{
            //    List<Dish> dishes = new List<Dish>()
            //    {
            //        new Dish { Name = "Borscht", Description = "Ukrainian soup" },
            //        new Dish { Name = "Tonaktsu ramen", Description = "Japanese soup" },
            //        new Dish { Name = "Palak paneer", Description = "Greens, cheese, curry" },
            //        new Dish { Name = "Pilaf", Description = "Rice, lamb" },
            //        new Dish { Name = "Xiaolongbao", Description = "Chinese dumplings" },
            //        new Dish { Name = "Pho ga", Description = "Vietnameese chicken soup" },
            //        new Dish { Name = "Tom yang", Description = "Thai shrimp soup" }
            //    };

            //    foreach (Dish dish in dishes)
            //    {
            //        db.Dish.Add(dish);
            //    }
            //    db.SaveChanges();
            //}

            GetSoup();
            Console.WriteLine("\n**************************\n");
            GetDishById(4);
            GetDishById(6);
            GetLastDish();
        }

        static void GetLastDish()
        {
            //Получить самое последнее блюдо из таблицы.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandtext = $"""
                    SELECT TOP 1 [Id]
                          ,[Name]
                          ,[Description] 
                    FROM Dish
                    ORDER BY Id DESC 
                    """;
                SqlCommand command = new SqlCommand(commandtext, connection);
                command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        string columnName1 = reader.GetName(0);
                        string columnName2 = reader.GetName(1);
                        string columnName3 = reader.GetName(2);
                        Console.WriteLine("\nLast record:\n");
                        Console.WriteLine($"> {columnName1} | {columnName2} | {columnName3}");
                        while (reader.Read())
                        {                     
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);

                            Console.WriteLine($"> {id} | {name} | {description}");
                        }
                    }
                }
            }
        }

        static void GetDishById(int id)
        {
            //Получить блюдо по Id. 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandtext = $"""
                    SELECT Id, [Name], [Description] FROM Dish
                    WHERE Id = @id
                    """;
                SqlCommand command = new SqlCommand(commandtext, connection);
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        string columnName1 = reader.GetName(0);
                        string columnName2 = reader.GetName(1);
                        string columnName3 = reader.GetName(2);
                        Console.WriteLine($"> {columnName1} | {columnName2} | {columnName3}");
                        while (reader.Read())
                        {
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);

                            Console.WriteLine($"> {id} | {name} | {description}");
                        }
                    }
                }
            }
        }
        static void GetSoup()
        {
            //Получить все блюда, в названии которых содержится слово “Суп”

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string commandtext = $"""
                    SELECT Id, [Name], [Description] FROM Dish
                    WHERE [Description]  like '%soup%'
                    """;
                SqlCommand command = new SqlCommand(commandtext, connection);
                command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        string columnName1 = reader.GetName(0);
                        string columnName2 = reader.GetName(1);
                        string columnName3 = reader.GetName(2);
                        Console.WriteLine($"> {columnName1} | {columnName2} | {columnName3}");
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);

                            Console.WriteLine($"> {id} | {name} | {description}");
                        }
                    }
                }
            }
        }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<Dish> Dish => Set<Dish>();
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=Task21Feb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
    public class Dish
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public override string ToString()
        {
            return $"{Id}. {Name}: {Description}";
        }
    }
}
