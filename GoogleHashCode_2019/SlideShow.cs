using System;
using System.Collections.Generic;
using System.IO;

namespace GoogleHashCode_2019
{
    public class SlideShow
    {
        public List<Slide> Slides;
        public SlideShow()
        {
            Slides = new List<Slide>();
        }

        public int getScore()
        {
            int score = 0;
            for(int i=0; i<Slides.Count-1; i++)
                score += Slides[i].getScore(Slides[i + 1]);
            return score;
        }

        public void addSlide(Slide s)
        {
            Slides.Add(s);
        }

        public void addSlide(Slide s, int index)
        {
            Slides.Insert(index, s);
        }

        public void saveToFile(string fileName="")
        {
            if (fileName == "")
                fileName = "slideshow_" + ImageCollection.getSource() + "_" + getScore()+".txt";
            List<string> data = new List<string>();
            data.Add(Slides.Count.ToString());
            foreach (Slide item in Slides)
                data.Add(item.Images[0].ID.ToString() +" "+ (item.Images[1] != null ? item.Images[1].ID.ToString() : ""));
            File.WriteAllLines(@"../../sources/"+fileName, data);
        }
    }
}
