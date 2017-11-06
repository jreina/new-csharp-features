using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

namespace new_csharp_features
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");
        }
    }

    public class tuples
    {
        static void OldWay()
        {
            var pair = Tuple.Create(0, 1);
            List<Tuple<int, int>> pairs = new List<Tuple<int, int>>
            {
                pair
            };

            Func<Tuple<int, int>, int> add = twople => twople.Item1 + twople.Item2;

            add(pair);
        }

        static void NewWay()
        {
            var pair = (0, 1);

            List<(int, int)> pairs = new List<(int, int)>
            { pair };

            Func<(int a, int b), int> add = twople => twople.a + twople.b;

            add(pair);
        }

        static void bonus_deconstruction()
        {
            var pair = (0, 1);

            var (x, y) = pair;

            WriteLine($"x: {x}, y: {y}");
        }
    }

    public class outvars
    {
        static void funcWithOutVar(out double x, out double y)
        {
            x = 1;
            y = Math.E;
        }
        static void oldway()
        {
            double x, y;
            funcWithOutVar(out x, out y);
            Console.WriteLine($"x: {x}, y: {y}");
        }
        static void newway()
        {
            funcWithOutVar(out var x, out var y);
            Console.WriteLine($"x: {x}, y: {y}");
        }
    }

    public class discards
    {
        static bool Count<T>(IEnumerable<T> list, out int count)
        {
            count = list.Aggregate(0, (memo, val) => ++memo);
            return count > 0;
        }

        static void oldway()
        {
            var list = new int[] { 0, 1, 2 };
            int _;

            var hasElements = Count(list, out _);

            if (hasElements) Console.WriteLine("I have elements!");
        }

        static void newway()
        {
            var list = new int[] { 0, 1, 2 };

            var hasElements = Count(list, out _);

            if (hasElements) Console.WriteLine("I have elements!");
        }
    }

    public class patternmatching
    {
        static void newway(object shape)
        {
            switch (shape)
            {
                case Circle c:
                    WriteLine($"circle with radius {c.Radius}");
                    break;
                case Rectangle s when (s.Length == s.Height):
                    WriteLine($"{s.Length} x {s.Height} square");
                    break;
                case Rectangle r:
                    WriteLine($"{r.Length} x {r.Height} rectangle");
                    break;
                default:
                    WriteLine("<unknown shape>");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(shape));
            }
        }

        private class Circle
        {
            public int Radius;
        }

        private class Rectangle
        {
            public int Length;
            public int Height;
        }
    }

    public class localfunctions
    {
        // https://blogs.msdn.microsoft.com/dotnet/2016/08/24/whats-new-in-csharp-7-0/#user-content-local-functions
        public int Fibonacci(int x)
        {
            if (x < 0) throw new ArgumentException("Less negativity please!", nameof(x));
            return Fib(x).current;

            (int current, int previous) Fib(int i)
            {
                if (i == 0) return (1, 0);
                var (p, pp) = Fib(i - 1);
                return (p + pp, p);
            }
        }
    }
}
