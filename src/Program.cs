using PasswordEncrypter.Algorithms;
using System;
using System.Linq;
using System.Reflection;

using static System.Activator;
using static System.Console;
using static System.Convert;

namespace PasswordEncrypter
{
    public class Program
    {
        private static readonly string _salt;
        private static readonly string _version;

        static Program()
        {
            _salt = "YOUR_SALT_HERE";
            _version = "0.1.0";
        }

        static void Main(string[] args)
        {
            WriteLine($"Password Encrypter {_version}");

            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("\n****************************************************\n");
            ForegroundColor = ConsoleColor.White;

            WriteLine("Choose an algorithm to hash your password:");
            var assembly = Assembly.GetAssembly(typeof(IAlgorithm));
            var types = assembly
                            .GetTypes()
                            .Where(a => a.IsClass && a.FullName.StartsWith("PasswordEncrypter.Algorithms"))
                            .ToList();
            var countType = types.Count;

            ForegroundColor = ConsoleColor.DarkGreen;
            for (var i = 0; i < countType; i++)
                Write($"\t- {i + 1}: {types[i].Name}\n");

            ForegroundColor = ConsoleColor.White;
            Write("\nYour option: ");
            ForegroundColor = ConsoleColor.White;
            var option = ToByte(ReadLine()) - 1;
            while (option >= countType || option < 0)
            {
                ForegroundColor = ConsoleColor.DarkGreen;
                Write("Your option is invalid, please choose an option from that list: ");
                ForegroundColor = ConsoleColor.White;
                option = ToByte(ReadLine()) - 1;
            }

            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("\n****************************************************\n");
            ForegroundColor = ConsoleColor.White;

            Write("Password: ");
            var password = ReadLine();
            var instanceObject = CreateInstance(types[option], true);
            var algorithm = (IAlgorithm)instanceObject;

            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("\n****************************************************\n");
            ForegroundColor = ConsoleColor.White;

            WriteLine("Choose a type of salt:");
            WriteLine($"\t- 1: {SaltType.Before.ToString()}");
            WriteLine($"\t- 2: {SaltType.After.ToString()}");
            Write("\nYour option: ");
            var saltType = ToByte(ReadLine());
            while (saltType > 2 || saltType < 1)
            {
                ForegroundColor = ConsoleColor.DarkGreen;
                Write("Your option is invalid, please choose an option from that list: ");
                ForegroundColor = ConsoleColor.White;
                saltType = ToByte(ReadLine());
            }

            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("\n****************************************************\n");
            ForegroundColor = ConsoleColor.White;

            ForegroundColor = ConsoleColor.Magenta;
            WriteLine($"Encrypted Password: \n{algorithm.Crypt(password, _salt, (SaltType)saltType)}");
            ForegroundColor = ConsoleColor.White;
            WriteLine($"Thanks for using Password Ecrypter {_version}");
            Read();
        }
    }
}