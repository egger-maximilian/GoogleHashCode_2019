using System;
namespace GoogleHashCode_2019
{
    public class ImageCollection
    {
        private static ImageCollection instance;
        private static readonly string[] sources = { "a", "a" };
        private string[] tags;

        private ImageCollection()
        {
            ImageCollection.instance = this;
        }

        public static ImageCollection getInstance()
        {
            if (instance != null) 
                return instance;
            else
            {
                return new ImageCollection();
            }
        }

        private static void LoadImagesFromSource(char source)
        {

        }
    }
}
