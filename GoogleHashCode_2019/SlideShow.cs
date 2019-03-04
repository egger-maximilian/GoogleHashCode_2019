﻿using System;
using System.Collections.Generic;

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
            for(int i=0; i<Slides.Count+1; i++)
                score += Slides[i].getScore(Slides[i + 1]);
            return score;
        }
    }
}
