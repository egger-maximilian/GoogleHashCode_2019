using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GoogleHashCode_2019.Properties;

namespace GoogleHashCode_2019
{
    public class ImageCollection
    {
        private static ImageCollection instance;
        private static readonly string[] sources = {
        "a_example.txt",
        "c_memorable_moments.txt",  
        "e_shiny_selfies.txt", 
        "b_lovely_landscapes.txt", 
        "d_pet_pictures.txt"};
        private static char currentSource = '.';


        private Dictionary<string, List<int>> lookup;

        //lookup table for tags
        
        private List<Image> images;

        private ImageCollection(char source)
        {
            lookup = new Dictionary<string, List<int>>();
            loadImagesFromSource(source);
        }

        public static char getSource() { return currentSource; }
        public static List<Image> getImagesByTags(List<string> tags)
        {
            List<int> indices = new List<int>();
            List<Image> img = instance.images;
            List<Image> toReteturn = new List<Image>();
            foreach (string item in tags)
                Parallel.ForEach(instance.lookup[item], (index) =>
                {
                    toReteturn.Add(img[index]);
                });
            return toReteturn;
        }
        public static Dictionary<string, List<int>> getLookup()
        {
            return instance.lookup;
        }
        public static List<Image> getImages()
        {
            return instance.images;
        }
        public static ImageCollection getInstance(char source)
        {
            if (instance != null && currentSource == source)
                return instance; 
            return instance =new ImageCollection(source);

        }

        private void loadImagesFromSource(char source)
        {
            int sourceIndex = -1;
            switch (source)
            {
                case 'a':
                    sourceIndex = 0;
                    break;
                case 'b':
                    sourceIndex = 3;
                    break;
                case 'c':
                    sourceIndex = 1;
                    break;
                case 'd':
                    sourceIndex = 4;
                    break;
                case 'e':
                    sourceIndex = 2;
                    break;
                default:
                    break;
            }
            Console.WriteLine("Reading images...");
            string[] data = File.ReadAllLines(@"../../sources/" + sources[sourceIndex]);
            Console.WriteLine("Images read. Allocating resources...");
            images = new List<Image>();
            for (int i = 1; i < data.Length; i++)
            {
                string[] img = data[i].Split(' ');
                List<string> t = new List<string>(img);
                t.RemoveAt(0);
                t.RemoveAt(0);
                foreach (string s in t)
                {
                    if (lookup.ContainsKey(s))
                        lookup[s].Add(i);
                    else
                        lookup.Add(s, new List<int>() { i });
                }
                images.Add(new Image(t,img[0][0], images.Count));
            }
            currentSource = source;
            Console.WriteLine("Images loaded.");
        }
    }
}
