// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

namespace Employ
{
    public class Employee
    {
        private string _name = "";  // Private variable

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}

namespace Person
{
    public class Anything
    {
        
        static Anything()
        {
            Console.WriteLine("Static constructor called another file");
        }
        public Anything()
        {
            Console.WriteLine("Public constructor called another file");
        }

        public void DoAnything()
        {
            Console.WriteLine("test");
        }


    }

}
