using WesternStore2._0.Data;
using WesternStore2._0.Models;

namespace WesternStore2._0.Service
{
    public class UserSeeder
    {
        public static async Task SeedAdmin(MongoCRUD crud)
        {
            var users = await crud.ReadAll<User>("Users");

            if (users.Any(u => u.IsAdmin))
                return;

            var admin = new User
            {
                UserName = "admin",
                PassWord = PasswordHelper.Hash("admin4416"),
                IsAdmin = true
            };

            await crud.Create("Users", admin);
        }
    }
}
