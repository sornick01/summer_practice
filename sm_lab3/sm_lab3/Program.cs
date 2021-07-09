using System;
using System.Collections.Generic;

namespace sm_lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan LifeTime = new TimeSpan(0, 1, 0);
            Cache<string> Cache = new Cache<string>(LifeTime, 3);

            try
            {
                Cache.Save("a", "Array");
                Cache.Save("b", "Binary Tree");
                Cache.Save("c", "Cache");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                string message = Cache.Get("c");
                Console.WriteLine("c: " + message);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Cache.Save("c", "Compilation"); 
                /*должны получить исключение, т.к. такой ключ уже существует*/
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            try
            {
                Cache.Save("d", "Duplicate");
                //удалится старейшая запись, т.к. достигнут максимальный размер
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                string message = Cache.Get("z");
                Console.WriteLine("z: " + message);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}