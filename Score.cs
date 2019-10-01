using System;
using System.Collections.Generic;
using System.Text;

namespace ExtremeTetris
{
    class Score
    {
        private uint defaultScore;
        private uint currentScore;
        private int scoreBase;

        public Score()
        {
            currentScore = 0;
            defaultScore = currentScore;
            scoreBase = 1000;
        }

        /// <summary>
        /// Adds score depending on count param
        /// </summary>
        /// <param name="count"></param>
        public void addScore(short count)
        {
            double multiplier;
            if (count > 1)
                multiplier = 1.0 + (0.25 * (double)count);
            else
                multiplier = 1.0;
            currentScore += (uint)(multiplier * (scoreBase));
        }

        /// <summary>
        /// Returns score
        /// </summary>
        /// <returns>Score of uint type</returns>
        public uint getScore()
        {
            return currentScore;
        }

        /// <summary>
        /// Cleares current score
        /// </summary>
        public void clear()
        {
            currentScore = defaultScore;
        }
    }
}
