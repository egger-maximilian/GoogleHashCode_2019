using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleHashCode_2019.Properties
{
    public class SlideShowGenerator
    {
        private System.Diagnostics.Stopwatch Stopwatch;
        private BackgroundWorker bw;
        private SlideShow show;
        public SlideShowGenerator()
        {
            Stopwatch = new System.Diagnostics.Stopwatch();

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

            int verticals = v.Count;
            Console.WriteLine("Merge vertical pictures: "+verticals +" of "+ ImageCollection.getImages().Count);
            while (v.Count > 1)
            {
                Image first = v.FirstOrDefault();
                //maximize non common tags
                Image other = v.OrderBy(x => first.Merge(x.Tags).Count).FirstOrDefault(img => img.ID != first.ID);
                //was descending first (c: 1.9k -> 2.9k)
                s.Add(new Slide(first, other));

                v.Remove(first);
                v.Remove(other);
                bw.ReportProgress((int)Math.Truncate(100 * ((double)(verticals-v.Count) / verticals)));

            }

            //foreach (Image item in v)
              //  s.Add(new Slide(item)); // create slides out of all vertical images




            Stopwatch.Start();
            show = new SlideShow();
            Dictionary<string, List<int>> lookup = new Dictionary<string, List<int>>();
            for (int i = 0; i < s.Count; i++)
            {
                foreach(string t in s[i].getTags())
                {
                    if (lookup.ContainsKey(t))
                        lookup[t].Add(i);
                    else
                        lookup[t] = new List<int>() { i };
                }
            }
            show.addSlide(s[0]);
            int slidesCount = s.Count;
            s.RemoveAt(0);
            int tmpScore = 0;
            int tmpIndex =  0;
            Console.WriteLine("Generating Slideshow: ");
            List<Slide> subSet=new List<Slide>();
            Slide nextSlide=null;
            List<int> relevantIndices = new List<int>();
            while (s.Count>0)
            {
                tmpIndex = 0;
                tmpScore = 0;
                Slide current = show.Slides.LastOrDefault();
                subSet = s.Where((arg) => arg.getTags().Intersect(current.getTags()).Any()).ToList(); //can sometimes not find slides!
                //foreach (string tg in current.getTags())
                 //   relevantIndices.AddRange(lookup[tg]);
                //foreach (int ind in relevantIndices)
                 //   subSet.Add(s[ind]);
                //subSet = s.Where((arg) => arg.Images.Intersect(lookup.).Any());
                //stays the same for a bit, will change ever so slightly, don't recalc everything, remove not good, add new ones (old tags rem, new tags add)
                if (subSet.Count >  0)
                {
                   /* Parallel.ForEach(subSet, (arg1, arg2, arg3) => {
                        if (current.getScore(arg1) > tmpScore)
                        {
                            tmpScore = current.getScore(arg1);
                            nextSlide = arg1;
                        }
                    });
                    show.addSlide(nextSlide);
                    s.Remove(nextSlide);
                   */ 
                    Parallel.For(0, subSet.Count, (index) =>
                    {
                        int sc = current.getScore(subSet[index]);
                        if (sc > tmpScore)
                        {
                            tmpScore = sc;
                            tmpIndex = index;
                        }
                    }); 
                    show.Slides.Add(subSet[tmpIndex]);

                    s.Remove(subSet[tmpIndex]);
                
                    }
                else
                {
                    Console.WriteLine("No common tags found");
                    if(show.Slides.Count < s.Count)
                        Console.WriteLine("LOOOOOP");
                    show.Slides.Add(s.FirstOrDefault());
                    s.RemoveAt(0);
                }


                bw.ReportProgress((int)Math.Truncate(100 * ((double)show.Slides.Count / slidesCount)));
            }




            /*for (int i=1; i<s.Count; i++)
            {
                tmpScore = 0;
                tmpIndex = 0;
                Parallel.For(0, show.Slides.Count, (index) =>
                 {
                     if (show.Slides[index].getScore(s[i]) > tmpScore) {
                         tmpScore = show.Slides[index].getScore(s[i]);
                        tmpIndex = index;
                    }
                 });
                show.addSlide(s[i], tmpIndex);
                bw.ReportProgress((int)Math.Truncate(100 * ((double)i / s.Count)));
           } */
            Stopwatch.Stop();
            Console.WriteLine("Done!\nScore: "+show.getScore());
            Console.WriteLine("Time elapsed: "+Stopwatch.Elapsed.ToString());

        }


        void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("["+Stopwatch.Elapsed.ToString()+"]Progresss: "+e.ProgressPercentage);
        }
        void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Complete!");
            Console.WriteLine("Saving slideshow to file...");
            show.saveToFile();
            Console.WriteLine("Done.");
        }

    }
}
