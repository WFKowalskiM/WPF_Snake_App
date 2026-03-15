using System;
using WPFLibrary;
using System.Threading;

namespace WpfApp2
{

    class ConsoleApp
    {
        public static int choice = 0;
        public static void PrintTask(String[] list)
        {
            Console.WriteLine("Press Up & Down to change option, Enter to select option");
            for (int i = 0; i < 3; i++)
            {
                if (choice % 3 == i)
                    Console.WriteLine(" --> " + list[i]);
                else Console.WriteLine(list[i]);
            }
        }
        [STAThread]
        public static void ShowWpfWindow()
        {
            Thread wpfThread = new Thread(() =>
            {
                Window1 mainWindow = new Window1();
                mainWindow.Closed += (sender, e) =>
                {
                    ContinueMainLoop();
                };

                mainWindow.Show();
                System.Windows.Threading.Dispatcher.Run();
            });

            wpfThread.SetApartmentState(ApartmentState.STA);
            wpfThread.Start();
        }
        static void ContinueMainLoop()
        {
            Console.Clear();
            PrintTask(new[] { "Start Game", "Credits", "Exit Game" });
        }
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            Window1 bWin = new Window1();
            Windows_API_components.AllocConsole();
            String[] list = { "Start Game", "Credits", "Exit Game" };
            bool choiceChanged = false;
            bool no = true;
            Console.WriteLine("Attached Woo");
            while (no)
            {
                Console.Clear();
                PrintTask(list);
                while (!choiceChanged)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(intercept: true).Key;
                        switch (key)
                        {
                            case ConsoleKey.UpArrow:
                                choice--;
                                if (choice < 0)
                                    choice = 2;
                                choiceChanged = true;
                                break;
                            case ConsoleKey.DownArrow:
                                choice++;
                                choiceChanged = true;
                                break;
                            case ConsoleKey.Enter:
                                if (choice % 3 == 0)
                                {
                                    IntPtr consoleWindow = Windows_API_components.GetConsoleWindow();
                                    Console.Clear();
                                    ShowWpfWindow();
                                    while (!SnakeWPF.STOP) { }
                                    Windows_API_components.SetForegroundWindow(consoleWindow);
                                }
                                if (choice % 3 == 1)
                                {
                                    Console.Clear();
                                    Console.WriteLine(SnakeWPF.Credits());
                                }
                                if (choice % 3 == 2)
                                {
                                    Environment.Exit(0);
                                }
                                break;
                        }
                    }
                }
                choiceChanged = false;
            }
        }
    }
}
