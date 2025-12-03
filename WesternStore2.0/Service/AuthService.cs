using WesternStore2._0.Data;
using WesternStore2._0.InputHelper;
using WesternStore2._0.Models;

namespace WesternStore2._0.Service
{
    public class AuthService
    {

        private const string UserCollection = "User";


        public static async Task<User?> Login(MongoCRUD crud)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== LOGIN ===\n");

                Console.Write("Username: ");
                string? inputName = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(inputName))
                {
                    Console.WriteLine("Username cannot be empty.");
                    Console.Write("Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                List<User> users;
                try
                {
                    users = await crud.ReadAll<User>(UserCollection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading users from database: {ex.Message}");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    return null;
                }

                var user = users.FirstOrDefault(u => u.UserName.Equals(inputName, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    Console.WriteLine("User does not exist.\n");
                    Console.Write("Would you like to register? (yes/no): ");
                    string answer = Console.ReadLine().Trim().ToLower();

                    if (answer == "yes")
                    {
                        //await RegisterCustomerAsync(crud);
                    }
                    else
                    {
                        return null;
                    }
                    continue;
                }

                int attempts = 0;
                const int MaxAttempts = 3;

                while (attempts < MaxAttempts)
                {
                    Console.Write("Password: ");
                    var inputPassword = Input.ReadPassword();

                    if (string.IsNullOrWhiteSpace(inputPassword) || user.PassWord != inputPassword)
                    {
                        attempts++;
                        Console.WriteLine($"Wrong password. Attempt left: {MaxAttempts - attempts}");
                        Console.ReadKey();

                        if (attempts >= MaxAttempts)
                        {
                            Console.WriteLine("Too many failed attempts. Returning to main menu...");
                            Console.ReadKey();
                            return null;
                        }

                        Console.Clear();
                        Console.WriteLine("=== LOGIN === \n");
                        Console.WriteLine($"Username: {user.UserName}");
                        continue;
                    }

                    Console.WriteLine($"\nLogin succesfuiull! Welcome {user.UserName}");
                    Console.ReadKey();
                    return user;


                }
            }


        }
    }
}
