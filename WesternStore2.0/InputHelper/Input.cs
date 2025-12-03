namespace WesternStore2._0.InputHelper
{
    public class Input
    {

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKey key;

            while (true)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Enter)
                {
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        Console.WriteLine("\nPassword cannot be empty. Try again!");
                        password = "";
                        continue;
                    }

                    Console.WriteLine();
                    return password;
                }

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            }

        }
    }
}
