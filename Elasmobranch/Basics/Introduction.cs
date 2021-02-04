using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Elasmobranch.Basics
{
    public class Introduction
    {
        protected Introduction()
        {
            IntroduceYourself();

            Console.WriteLine("You will be {0} in ten years' time", InputNumber("Enter Age:") + 10);

            Console.WriteLine(TenKMtimeToMetresPerSecond(InputNumber("Minutes: "), InputNumber("seconds: ")) + "m/s");
            Console.WriteLine(TenKMtimeToMilesPerHour(InputNumber("Minutes: "), InputNumber("seconds: ")) + "mph");

            PrintMultiplicationTable(InputNumber("Multiplication table for: "), InputNumber("Numbers of rows", 12));

            Console.WriteLine("A \u2248 {0:F2}", CircleArea(InputNumber("[area]   Circle radius: ", 5)));
            Console.WriteLine("C \u2248 {0:F2}", CircleCircumference(InputNumber("[circum] Circle radius: ", 5)));
        }

        public static void IntroduceYourself()
        {
            Console.WriteLine("### Timothy Langer's amazing coding assignment ###");
            Console.WriteLine("It is {0}", DateTime.Now);
            Console.WriteLine("The square root of 12345 is {0}", Math.Sqrt(12345));

            // Sequence 2, -3, 4, -5, ..., 100
            for (var i = 2; i < 100; i++)
                if (i % 2 == 0)
                    Console.WriteLine(i);
                else
                    Console.WriteLine(i * -1);
        }

        public static double InputNumber(string message, int defaultValue = 0)
        {
            if (int.TryParse(message, out var result)) return result;
            
            Console.WriteLine("You're a bad person, just to let you know.");
            Console.WriteLine("I didn't like your input so I'm using the default value. :P");
            return defaultValue;
        }

        public static double TenKMtimeToMetresPerSecond(double minutes, double seconds)
        {
            return 10000 / (minutes * 60 + seconds);
        }

        public static double TenKMtimeToMilesPerHour(double minutes, double seconds)
        {
            return 10 * 60 * 60 / 1.609344 / (minutes * 60 + seconds);
        }

        public static void PrintMultiplicationTable(double thing, double rows = 12)
        {
            for (var i = 1; i <= rows; i++)
                Console.WriteLine((thing * i).ToString(CultureInfo.InvariantCulture).PadLeft(8));
        }

        public static double CircleArea(double radius)
        {
            return Math.PI * Math.Pow(radius, 2);
        }

        public static double CircleCircumference(double radius)
        {
            return 2 * Math.PI * radius;
        }
    }
}