using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.BLL;
using TravelMeaning.DAL;
using TravelMeaning.Models.Data;
using TravelMeaning.Models.Model;
using TravelMeaning.Web.Controllers;

namespace TravelMeaning.Test
{
    [TestClass]
    public class UnitTest1
    {
        public static DbContextOptions<TMContext> CreateDbContextOptions(string databaseName)
        {
            var serviceProvider = new ServiceCollection().
                AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<TMContext>();
            builder.UseSqlServer(databaseName)
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
        [TestMethod]
        public async Task InsertUser()
        {
            var q = new UserManager(new UserService(new TMContext(CreateDbContextOptions("Server=.;Database=TravelMeaning;User Id=sa;Password=sa;"))));
            for (int i = 0; i < 100; i++)
            {
                await q.CreateAsync(new User
                {
                    Username = i.ToString(),
                    Password = "wer",
                    PhoneNumber = "ser"
                });
            }
        }
        [TestMethod]
        public async Task QueryAllUser()
        {
            var m = new UserManager(new UserService(new TMContext(CreateDbContextOptions("Server=.;Database=TravelMeaning;User Id=sa;Password=sa;"))));
            var list = await m.GetAll().ToListAsync();
            Assert.IsTrue(list.Count > 0);
        }
        [TestMethod]
        public async Task QueryAndInsert()
        {
            var m = new UserManager(new UserService(new TMContext(CreateDbContextOptions("Server=.;Database=TravelMeaning;User Id=sa;Password=sa;"))));
            await m.SignUp("sdfsdf", "sdfsdfdsf", "4585sd8f");
            var result = await m.GetAll().Where(x => x.Username == "sdfsdf").FirstOrDefaultAsync();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Signup()
        {
            var q = new UserManager(new UserService(new TMContext(CreateDbContextOptions("Server=.;Database=TravelMeaning;User Id=sa;Password=sa;"))));
            var u = new UserController(q);
            u.SignUp(new Models.ViewModels.User.SignUpViewModel
            {
                Username = "123",
                Password = "123",
                PhoneNumber = "123"
            });
        }
    }
}
