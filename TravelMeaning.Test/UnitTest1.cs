using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using TravelMeaning.BLL;
using TravelMeaning.DAL;
using TravelMeaning.Models.Data;
using TravelMeaning.Models.Model;

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
            var q = new UserService(new TMContext(CreateDbContextOptions("Server=.;Database=TravelMeaning;User Id=sa;Password=sa;")));
            await q.CreateAsync(new User
            {
                Username = "wer",
                Password = "wer",
                PhoneNumber = "ser"
            });
            var w = await q.GetAll().FirstAsync();
            Assert.IsNotNull(w);
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
            await m.Regiseter("sdfsdf", "sdfsdfdsf", "4585sd8f");
            var result = await m.GetAll().Where(x => x.Username == "sdfsdf").FirstOrDefaultAsync();
            Assert.IsNotNull(result);
        }
    }
}
