using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace new_csharp_features
{
    class async_main_method
    {
        static async Task Main(string[] args)
        {
            await Task.Run(() => WriteLine("Hello World!"));
            return;
        }
    }

    class default_literal_expressions
    {
        Dictionary<string, string> emptyCtr = new Dictionary<string, string>();
        Dictionary<string, string> withDefault = default(Dictionary<string, string>);
        Dictionary<string, string> withDefaultLiteral = default;
    }

    class more_tuple_goodies
    {
        void inferred_tuple_element_names()
        {
            var count = 5;
            var name = "Salad";
            var pair = (count, name);

            WriteLine($"count: {pair.count}, name: {pair.name}");
        }
    }
}
