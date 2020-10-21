using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static DemoContext db;

        static async Task Main(string[] args)
        {
            db = new DemoContext();


            // LINQ for IQueryable

            // LINQ
            // ~ SELECT user.FullName FROM Users as user WHERE user.Balance > 0 ORDER BY user.BirthDate
            IQueryable<string> linq1 =
                from u in db.Users
                where u.Balance > 0
                orderby u.BirthDate
                select u.FullName;
            string[] linq1Value = linq1.ToArray();

            // LINQ Extension Methods
            // ~ SELECT user.FullName FROM Users as user WHERE user.Balance > 0 ORDER BY user.BirthDate
            IQueryable<string> linq2 =
                db.Users
                .Where(u => u.Balance > 0)
                .OrderBy(u => u.BirthDate)
                .Select(u => u.FullName);
            List<string> linq2Value = await linq2.ToListAsync();

            // ~ SELECT * FROM Users WHERE Balance > 0
            IQueryable<User> linq3where = db.Users.Where(user => user.Balance > 0);
            // ~ SELECT * FROM (linq3where) ORDER BY BirthDate
            IQueryable<User> linq3whereOrder = linq3where.OrderBy(user => user.BirthDate);
            // ~ SELECT FullName FROM (linq3order)
            IQueryable<string> linq3whereOrderSelect = linq3whereOrder.Select(user => user.FullName);
            var linq3value = await linq3whereOrderSelect.ToListAsync();

            // not IQueryable!!!
            IEnumerable<string> badLinq = db.Users
                .ToArray()
                .Where(u => u.Balance > 0)
                .OrderBy(u => u.BirthDate)
                .Select(u => u.FullName);

            User firstUser = await db.Users.FirstAsync(); // exception if no users in table
            firstUser = await db.Users.FirstOrDefaultAsync(); // no exception if no users in table

            User lastUser = await db.Users.OrderByDescending(u => u.Id).FirstAsync();
            lastUser = await db.Users.OrderByDescending(u => u.Id).FirstOrDefaultAsync();

            //User userById = await db.Users.SingleAsync(u => u.Id == 1002);
            //userById = await db.Users.SingleOrDefaultAsync(u => u.Id == 1002);
            //userById = await db.Users.FirstOrDefaultAsync(u => u.Id == 1002);
            //userById = await db.Users.FindAsync(1002);
            User userById = await db.Users.FindAsync(1002);

            List<User> zeroBalanceUsers = await db.Users.Where(u => u.Balance == 0).ToListAsync();

            List<User> zeroBalanceUsersPage3 = await db.Users.Where(u => u.Balance == 0).Skip(5 * 2).Take(5).ToListAsync();

            List<Post> latestPosts = await db.Users.Include(u => u.Posts).Select(u => u.Posts.FirstOrDefault()).ToListAsync();
        }
    }
}
