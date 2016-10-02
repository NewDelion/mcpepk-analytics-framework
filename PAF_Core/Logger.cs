using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAF_Core
{
    public class Logger
    {
        private static object lockObj = new object();

        public static void WriteLine(string message, ConsoleColor text_color = ConsoleColor.White, ConsoleColor back_color = ConsoleColor.Black)
        {
            lock (lockObj)
            {
                Console.ForegroundColor = text_color;
                Console.BackgroundColor = back_color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }

        public static void Write(string message, ConsoleColor text_color = ConsoleColor.White, ConsoleColor back_color = ConsoleColor.Black)
        {
            lock (lockObj)
            {
                Console.ForegroundColor = text_color;
                Console.BackgroundColor = back_color;
                Console.Write(message);
                Console.ResetColor();
            }
        }

        public static void WriteError(string message)
        {
            Write(message, ConsoleColor.Red, ConsoleColor.White);
        }

        public static void WriteLineError(string message)
        {
            WriteLine(message, ConsoleColor.Red, ConsoleColor.White);
        }

        public static void WriteWarn(string message)
        {
            Write(message, ConsoleColor.Yellow);
        }

        public static void WriteLineWarn(string message)
        {
            WriteLine(message, ConsoleColor.Yellow);
        }

        public static void WriteInfo(string message)
        {
            Write(message, ConsoleColor.Cyan);
        }

        public static void WriteLineInfo(string message)
        {
            WriteLine(message, ConsoleColor.Cyan);
        }

        public static void Clear()
        {
            lock (lockObj)
            {
                Console.Clear();
            }
        }

        public static void SetWindowSize(int width, int height)
        {
            lock (lockObj)
            {
                Console.SetWindowSize(width, height);
            }
        }

        public static void SetBufferSize(int width, int height)
        {
            lock (lockObj)
            {
                Console.SetBufferSize(width, height);
            }
        }

        public static void SetTitle(string title)
        {
            lock (lockObj)
            {
                Console.Title = title;
            }
        }
    }
}
