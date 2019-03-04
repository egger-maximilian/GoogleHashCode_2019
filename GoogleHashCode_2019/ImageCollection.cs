using System;
using System.Collections.Generic;
using System.IO;

namespace GoogleHashCode_2019
{
    public class ImageCollection
    {
        private static ImageCollection instance;
        private static readonly string[] sources = {"a_example.txt","c_memorable_moments.txt",  "e_shiny_selfies.txt"," b_lovely_landscapes.txt", "d_pet_pictures.txt"};
        private static char currentSource = '.';

        private Dictionary<string, int> tags;

        private ImageCollection(char source)
        {
            tags = new Dictionary<string, int>();
            LoadImagesFromSource(source);
            ImageCollection.instance = this;
        }

        public static ImageCollection getInstance(char source)
        {
            if (instance != null && currentSource == source) 
                return instance;
            else
            {
                return new ImageCollection(source);
            }
        }

        private static void loadImagesFromSource(char source)
        {
            //TODO-> load images form code
            //add 1 to corresponding tags
            int i = -1;
            switch (source)
            {
                case 'a':i = 0;
                    break;
                case 'b':i = 3;
                    break;
                case 'c':i = 1;
                    break;
                case 'd': i = 4;
                    break;
                case 'e': i = 2;
                    break;
                default:
                    break;
            }

            string[] data = File.ReadAllLines(@"" + sources[i]);
        }
    }
}
