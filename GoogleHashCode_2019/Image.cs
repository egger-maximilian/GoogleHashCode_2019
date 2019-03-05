using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode_2019.Properties
{
    public class Image
    {
        public int ID;
        public List<string> Tags;
        public char Orientation;
        public Image(List<string> tags, char orientation, int ID)
        {
            this.ID = ID;
            Tags = tags;
            Orientation = orientation;
        }

        public override string ToString()
        {
            string t = "";
            Tags.ForEach((s) =>
            {
                t += s+" ";
            });
            return "Image: " + Orientation + " " + t;
        }

        public List<string> Merge(List<string> argTags)
        {
            return Tags.Union(argTags).ToList();
        }
    }
}
