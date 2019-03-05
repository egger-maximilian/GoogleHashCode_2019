using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GoogleHashCode_2019.Properties
{
    public class SlideShowGenerator
    {
        private BackgroundWorker bw;
        public SlideShowGenerator()
        {
            bw = new BackgroundWorker();
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.DoWork+=Bw_DoWork;

        }
            
        public void generateSlideshow(char i)
        {
            ImageCollection.getInstance(i);
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }


        void Bw_Other_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Slide> s = new List<Slide>();
            List<Image> v = new List<Image>();
            int averageTags = ImageCollection.getAverageTagCount();
            foreach (Image item in ImageCollection.getImages())
            {
                if (item.Orientation == 'H')
                    s.Add(new Slide(item));
                else
                    v.Add(item);
            }

            while (v.Count > 1)
            {
                var first = v.FirstOrDefault();
                var other = v.OrderByDescending(x => first.Merge(x.Tags).Count).FirstOrDefault();
                s.Add(new Slide(first,other));
                v.Remove(first);
                v.Remove(other);
            }
        }

        void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Slide> s = new List<Slide>();
            List<Image> v = new List<Image>();
            int averageTags = ImageCollection.getAverageTagCount();
            foreach (Image item in ImageCollection.getImages())
            {
                if (item.Orientation == 'H')
                    s.Add(new Slide(item));
                else
                    v.Add(item);
            }
            v.Sort((a, b) => { return a.Tags.Count.CompareTo(b.Tags.Count()); });
            if (s.Count == 0)
                s.Add(new Slide(v[0], v[v.Count - 1]));
            SlideShow show = new SlideShow();
            show.addSlide(s[0]);
<<<<<<< HEAD
           
=======
            int tmpScore = 0;
            int tmpIndex = 0;
            for(int i=1; i<s.Count; i++)
            {
                if (show.Slides[show.Slides.Count - 1].getScore(s[i]) > tmpScore)
                    tmpIndex = i;
            }
>>>>>>> c3e586a... .

        }
        void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Progresss: "+e.ProgressPercentage);
        }
        void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Complete!"+e.Result);
        }

    }
}
