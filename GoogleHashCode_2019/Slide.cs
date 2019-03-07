using System;
using System.Collections.Generic;
using GoogleHashCode_2019.Properties;

namespace GoogleHashCode_2019
{
    public class Slide
    {
        public Image[] Images;

        public Slide(Image i)
        {
            Images = new Image[2];
            Images[0] = i;
            Images[1] = null;
        }
        public Slide(Image i1, Image i2)
        {
            Images = new Image[2];
            Images[0] = i1;
            Images[1] = i2;
        }

        public List<string> getTags()
        {
            if (Images[1] == null)
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

        public int getScore(Image img)
        {
            return getScore(new Slide(img));
        }

        public int getScore(Slide right)
        {
            List<string> rightTags = right.getTags();
            List<string> thisTags = getTags();

            int commonCount = 0;
            int differentCount = 0;
            foreach (string item in thisTags) {
                if (rightTags.Contains(item))
                    commonCount++;
                else
                    differentCount++; 
            }
            int score = Math.Min(differentCount, commonCount);
            differentCount = 0;
            foreach (string item in rightTags)
                if (!thisTags.Contains(item))
                    differentCount++;
            return Math.Min(score, differentCount);
        }


    }
}
