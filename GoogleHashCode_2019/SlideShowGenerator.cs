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
            for(int i=1; i<s.Count; i++)
            {
                int tmpScore = 
            }

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
