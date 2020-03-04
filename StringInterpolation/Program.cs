using System;

using CustomStringInterpolation;

namespace StringInterpolation
{
    public class Program
    {
        private static readonly IInterpolatedStringFormatter formatter =
            new InterpolatedStringFormatter(
                new GetCompositeFormatStringDescriptionAction(),
                new InstanceToDictionaryConverter());

        static void Main(string[] args)
        {
            var person = new Person { Name = "Martin" };
            var net = new Thing { Name = "Internet", Owner = person };
            var something = new Something<string> { Name = "Doe" };

            Console.WriteLine(formatter.Format("Hello {Name}", person));
            Console.WriteLine(formatter.Format("Hello {Name}", something));
            Console.WriteLine(formatter.Format("Hello {Name}", net));
            Console.WriteLine(formatter.Format("Hello {Name}, owned by {Owner.Name}", net));
        }
    }

    internal class Person
    {
        public string Name { get; set; }
    }

    internal class Thing
    {
        public string Name { get; set; }

        public Person Owner { get; set; }
    }

    internal class Something<T>
    {
        public T Name { get; set; }
    }
}
