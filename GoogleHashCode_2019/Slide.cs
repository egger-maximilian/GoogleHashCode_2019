using System;
using System.Collections.Generic;
using GoogleHashCode_2019.Properties;

namespace GoogleHashCode_2019
{
    public class Slide
    {
        public Image[] Images;

        public Slide()
        {

        }

        public List<string> getTags()
        {
            if (Images[1] != null)
                return new List<string>(Images[0].Tags);
            else { 
            List<string> tags = new List<string>(Images[0].Tags);
            tags.AddRange(Images[1].Tags);
                return tags;
        }
        }

        public int getScore(Slide left, Slide right)
        {
            return left.getScore(this) + getScore(right);
        }

        public int getScore(Slide right)
        {
            return Math.Min(commonTagCount(right), differenceTagCount(right));
            //redo
        }

        public int commonTagCount(Slide s)
        {
            int count = 0;
            List<string> tagsOther = s.getTags();
            foreach(string t in getTags())
                if (tagsOther.Contains(t))
                    count++;

            return count;
        }
        public int differenceTagCount(Slide s)
        {

        }
    }
}
