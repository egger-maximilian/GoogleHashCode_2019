using System;

namespace GoogleHashCode_2019
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Google Hash Code 2019");
            Console.WriteLine("Enter collection (a-e): ");
            char c = Console.ReadKey().KeyChar;
            ImageCollection x = ImageCollection.getInstance(c);
        }
    }
}
