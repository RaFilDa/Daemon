using System;
using BackupAlgs.Tools;
using System.Collections.Generic;

namespace BackupAlgs.Windows
{
    public class LogWindow : Window
    {
        public Application Parent { get; set; }
        public string Header { get; set; }
        public int index { get; set; }
        public int offset { get; set; }
        private List<string> ReversedLogs { get; set; }
        public LogWindow(Application parent, string header)
        {
            Console.Clear();
            Parent = parent;
            Header = header;

            ReversedLogs = new List<string>(LogTools.Logs);
            ReversedLogs.Reverse();
        }

        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Header);
            Console.WriteLine("-".PadRight(Console.WindowWidth, '-'));

            for (int i = 0; i < Console.WindowHeight - 3; i++)
            {
                Console.WriteLine(" ".PadRight(Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 2);

            if(LogTools.Logs.Count > Console.WindowHeight - 5)
            {
                for (int i = offset; i < Console.WindowHeight - 5 + offset; i++)
                {
                    if(i == index + offset)
                        Console.ForegroundColor = ConsoleColor.White;
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(ReversedLogs[i]);
                }
            }
            else
                for (int i = 0; i < ReversedLogs.Count; i++)
                {
                    if (i == index)
                        Console.ForegroundColor = ConsoleColor.White;
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(ReversedLogs[i]);
                }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-".PadRight(Console.WindowWidth,'-'));
            Console.WriteLine("Enter - Go back");
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Enter)
            {
                Parent.ActiveWindow = new MainWindow(Parent, Header);
            }
            else if(info.Key == ConsoleKey.DownArrow)
            {
                index++;
                if (ReversedLogs.Count > Console.WindowHeight - 5)
                {
                    if (index == Console.WindowHeight - 5)
                    {
                        index--;
                        offset++;
                        if (offset + index == ReversedLogs.Count)
                            offset--;
                    }
                }
                else
                {
                    if (index == ReversedLogs.Count)
                    {
                        index--;
                    }
                }
            }
            else if(info.Key== ConsoleKey.UpArrow)
            {
                index--;
                if (index < 0)
                {
                    index++;
                    offset--;
                    if (offset < 0)
                        offset++;
                }
            }
        }
    }
}
