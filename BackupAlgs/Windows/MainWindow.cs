using System;
using BackupAlgs.Tools;
using BackupAlgs.Backup;

namespace BackupAlgs.Windows
{
    internal class MainWindow : Window
    {
        public string Header { get; set; }
        public string[] options = {
        "FULL Backup",
        "DIFFERENCIAL Backup",
        "ADDITIONAL Backup",
        "Set Paths",
        "Show Log"
        };
        public int index = 0;
        
        public MainWindow(string header)
        {
            Header = header;
            Draw();
        }

        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Header);
            Console.WriteLine("-".PadRight(Header.Length, '-'));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < options.Length; i++)
            {
                if (i == index)
                    Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(options[i]);
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
        }

        public void ThrowError(string errorMessage)
        {
            Console.SetCursorPosition(0, 2 + options.Length + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            Console.Clear();
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.DownArrow)
            {
                index++;
                if (index == options.Length)
                    index--;
            }
            else if (info.Key == ConsoleKey.UpArrow)
            {
                index--;
                if (index < 0)
                    index++;
            }
            else if (info.Key == ConsoleKey.Enter && index == 0)
            {
                if (PathTools.PathCheckSource() == "ERROR")
                    ThrowError("ERROR: Source path missing");
                else
                {
                    Full fb = new Full();
                }
            }
            else if (info.Key == ConsoleKey.Enter && index == 1)
            {
                if (PathTools.PathCheckSource() == "ERROR")
                    ThrowError("ERROR: Source path missing");
                else
                {

                }
            }
            else if (info.Key == ConsoleKey.Enter && index == 2)
            {
                if (PathTools.PathCheckSource() == "ERROR")
                    ThrowError("ERROR: Source path missing");
                else
                {

                }
            }
            else if (info.Key == ConsoleKey.Enter && index == 3)
            {

            }
            else if (info.Key == ConsoleKey.Enter && index == 4)
            {

            }
        }
    }
}
