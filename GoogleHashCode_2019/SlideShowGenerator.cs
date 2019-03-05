using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

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

        }
            
        public void generateSlideshow(char i)
        {
            ImageCollection.getInstance(i);
            bw.WorkerReportsProgress = true;

            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }


        void Bw_Other_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Slide> s = new List<Slide>();
            List<Image> v = new List<Image>();
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
                var other = v.OrderByDescending(x => first.Merge(x.Tags).Count).FirstOrDefault(img => img.ID != first.ID);
                s.Add(new Slide(first, other));
                v.Remove(first);
                v.Remove(other);
            }
            SlideShow show = new SlideShow();
            show.addSlide(s[0]);
            int tmpScore = 0;
            int tmpIndex = 0;
            for (int i=1; i<s.Count; i++)
            {
                Parallel.For(0, show.Slides.Count, (index) =>
                 {
                     if (show.Slides[index].getScore(s[i]) > tmpScore)
                         tmpIndex = i;
                 });
                show.addSlide(s[i], tmpIndex);
                bw.ReportProgress(s.Count / i * 100);
            }
            Console.WriteLine("DONE! "+show.getScore());
        }
        void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Progresss: "+e.ProgressPercentage);
        }
        void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Complete!");
        }

    }
}
