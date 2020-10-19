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

            var total = await db.Users.CountAsync();
            Console.WriteLine($"Users count: {total}");

            // 1. Добавить юзера
            // 2. Вытащить Id добавленного юзера
            var janeDoe = new User { FullName = "Jane Doe", BirthDate = new DateTime(1993, 03, 24) };
            //await AddUser(janeDoe);

            // 3. Создать пост от имени юзера
            var newPost = new Post { Title = "Hello world #2", Content = "Foo\nBar\nBaz" };
            await AddUserPost(janeDoe, newPost);

            // 4. Обновить пост юзера
            var johnDoePosts = await GetUserPosts(janeDoe.Id);
            var firstPost = johnDoePosts[0];
            firstPost.Title = "Hello World #2!!!";
            await UpdatePost(firstPost);

            // 5. Удалить пост юзера
            await RemovePost(firstPost);

            await db.DisposeAsync();
        }

        static async Task AddUser(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }

        static async Task<IList<Post>> GetUserPosts(int userId)
        {
            return await db.Posts.Where(p => p.UserId == userId).ToListAsync();
        }

        static async Task AddUserPost(User user, Post post)
        {
            db.Posts.Add(post);
            post.User = user;
            await db.SaveChangesAsync();
        }

        static async Task UpdatePost(Post post)
        {
            db.Posts.Update(post);
            await db.SaveChangesAsync();
        }

        static async Task RemovePost(Post post)
        {
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
        }
    }
}
