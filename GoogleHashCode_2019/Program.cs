﻿using System;
using System.Collections.Generic;
using GoogleHashCode_2019.Properties;

namespace GoogleHashCode_2019
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Google Hash Code 2019");
            Console.WriteLine("Enter collection (a-e): ");
            char c = Console.ReadKey().KeyChar;
            Console.WriteLine();
            ImageCollection x = ImageCollection.getInstance(c);
            foreach (Image i in ImageCollection.getImages())
                Console.WriteLine(i);

        }
    }
}
