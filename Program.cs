using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using System.Collections.Concurrent;

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

        static void SayCount(List<object> objects)
        {
            switch(objects)
            {
                case List<object> s when (s.Count == 0):
                    WriteLine("list is empty");
                    break;
                case List<object> s when (s.Count == 1):
                    WriteLine("list has one item");
                    break;
                case List<object> s when (s.Count == 2):
                    WriteLine("list has two items");
                    break;
                case List<object> s when (s.Count > 2):
                    WriteLine("list has more than two items");
                    break;
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

    public class desconstruction
    {
        class Point
        {
            public int X { get; }
            public int Y { get; }

            public Point(int x, int y) { X = x; Y = y; }
            public void Deconstruct(out int x, out int y) { x = X; y = Y; }
        }

        static Point GetPoint()
        {
            return new Point(2, 8);
        }

        static void new_deconstruction()
        {
            var (x, y) = GetPoint();
            WriteLine($"x: {x}, y: {y}");
        }
    }

    public class digitseparators
    {
        const double twoToTheThirtySecond = 4_294_967_296;
        const double twoToTheSixteenth = 65_536;

        public static void newway()
        {
            var two = Math.Log(twoToTheThirtySecond, twoToTheSixteenth);
            WriteLine($"2^32 = {twoToTheThirtySecond}, 2^16 = {twoToTheSixteenth}, log{twoToTheSixteenth}({twoToTheThirtySecond}) = {two}");
        }
    }

    public class binaryliterals
    {
        const int thirtytwo = 0b100000;
        public static void newway()
        {
            WriteLine($"thirytwo: {thirtytwo}");
        }
    }
    
    public class expressionbodiedmembers
    {
        /// <summary>
        /// https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/
        /// </summary>
        class Person
        {
            private static ConcurrentDictionary<int, string> names = new ConcurrentDictionary<int, string>();
            private int id = 0;

            public Person(string name) => names.TryAdd(id, name);
            ~Person() => names.TryRemove(id, out _);
            public string Name
            {
                get => names[id];
                set => names[id] = value;
            }
        }
    }

    public class cleaner_throw_expressions
    {
        /// <summary>
        /// https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/
        /// </summary>
        class Person
        {
            public string Name { get; }
            public Person(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));
            public string GetFirstName()
            {
                var parts = Name.Split(" ");
                return (parts.Length > 0) ? parts[0] : throw new InvalidOperationException("No name!");
            }
            public string GetLastName() => throw new NotImplementedException();
        }
    }
}
