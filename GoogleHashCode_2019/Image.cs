using System;
using System.Collections.Generic;

namespace GoogleHashCode_2019.Properties
{
    public class Image
    {
        public List<string> Tags;
        public char Orientation;
        public Image(List<string> tags, char orientation)
        {
            Tags = new List<string>();
            Orientation = orientation;
        }
    }
}
