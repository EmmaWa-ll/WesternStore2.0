using WesternStore2._0.Data;
using WesternStore2._0.InputHelper;
using WesternStore2._0.Models;

namespace WesternStore2._0.Service
{
    public class AuthService
    {

        private const string UserCollection = "Users";



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
                    Input.Pause();
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
                    Input.Pause();
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
                        await RegisterCustomer(crud);
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
                        Console.Write($"Wrong password. Attempt left: {MaxAttempts - attempts} \n");
                        Input.Pause();


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

                    if (user.IsAdmin)
                    {
                        Console.WriteLine($"\nSheriff {user.UserName} is back in town!");
                    }
                    else
                    {
                        Console.WriteLine($"\nSaddle up, {user.UserName}! You’re logged in!");

                    }
                    Input.Pause();
                    return user;


                }
            }
        }

        public static async Task<User?> RegisterCustomer(MongoCRUD crud)
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine("=== REGISTER CUSTOMER ===\n");

                Console.Write("Username: ");
                string username = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("\nUsername can not be empty.");
                    Input.Pause();
                    continue;
                }

                //om redan finns
                var allUsers = await crud.ReadAll<User>(UserCollection);
                if (allUsers.Any(u => u.UserName == username))
                {
                    Console.WriteLine("The username is already taken.");
                    Input.Pause();
                    continue;
                }

                string password;
                while (true)
                {


                    Console.Write("Password: ");
                    password = Input.ReadPassword();

                    if (string.IsNullOrWhiteSpace(password))
                    {
                        Console.WriteLine("Password can not me empty.\n");
                        Input.Pause();
                        Console.Clear();
                        Console.WriteLine("=== REGISTER CUSTOMER === \n");
                        Console.WriteLine($"Username: {username}");
                        continue;
                    }
                    break;

                }
                var newUser = new User
                {
                    UserName = username,
                    PassWord = password,
                    IsAdmin = false
                };
                await crud.Create("Users", newUser);
                Console.WriteLine("\nRegistration sucesssfull!");
                Input.Pause();
                return newUser;
            }
        }






    }
}
