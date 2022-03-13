using System;
using BackupAlgs.Backup;

namespace BackupAlgs.Windows
{
    internal class MainWindow : Window
    {
        public Application Parent { get; set; }
        public string Header { get; set; }
        public string[] options = {
        "FULL Backup",
        "DIFFERENCIAL Backup",
        "INCREMENTAL Backup",
        "Set Paths",
        "Show Log",
        };
        public int index = 0;
        public int MenuHeight { get; set; }

        public MainWindow(Application parent, string header)
        {
            Console.Clear();
            Parent = parent;
            Header = header;
            MenuHeight = options.Length + 3;
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
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-".PadRight(Header.Length, '-'));
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
                Full fb = new Full(MenuHeight);
            }
            else if (info.Key == ConsoleKey.Enter && index == 1)
            {
                Differential db = new Differential(MenuHeight);
            }
            else if (info.Key == ConsoleKey.Enter && index == 2)
            {
                Incremental ib = new Incremental(MenuHeight);
            }
            else if (info.Key == ConsoleKey.Enter && index == 3)
            {
                Parent.ActiveWindow = new PathWindow(Parent, Header);
            }
            else if (info.Key == ConsoleKey.Enter && index == 4)
            {
                Parent.ActiveWindow = new LogWindow(Parent, Header);
            }
        }
    }
}
