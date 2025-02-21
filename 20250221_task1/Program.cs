using Microsoft.EntityFrameworkCore;

namespace _20250221_task1
{
    //Описать класс пользователь. Добавить данные в базу, считать данные, используя Entity Framework.
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User u1 = new User { Name = "Alex", Age = 30 };
                User u2 = new User { Name = "Adam", Age = 35 };
                db.Users.Add(u1);
                db.Users.Add(u2);
                db.SaveChanges();
                var users = db.Users.ToList();

                foreach (var user in users)
                {
                    Console.WriteLine(user.ToString());
                }
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age{ get; set; }
        public override string ToString()
        {
            return $"{Id}. {Name} {Age} y.o.";
        }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIRUAHUA;Initial Catalog=Task21Feb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
