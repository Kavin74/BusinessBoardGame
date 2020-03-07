using System;

namespace BusinessGame
{
    public class Die
    {
        private int _rollCount;
        private bool _isRandom;

        private int[] dieRolls = new int[] { 4, 4, 4, 6, 7, 8, 5, 11, 10, 12, 2, 3, 5, 6, 7, 8, 5, 11, 10, 12, 2, 3, 5, 6, 7, 8, 5, 11, 10, 12 };
        public Die(int numberOfDie, bool isRandom)
        {
            this.NumberOfDie = numberOfDie;
            this._rollCount = 0;
            this._isRandom = isRandom;
        }

        public int NumberOfDie  { get; set; }

        public int Roll()
        {
            if (_isRandom)
            {
                return new Random().Next(NumberOfDie, NumberOfDie * 6);
            }

            int rollScore = GetRolls(this._rollCount);
            this._rollCount++;
            return rollScore;
        }

        private int GetRolls(int rollCount)
        {
            return dieRolls[rollCount % dieRolls.Length];
        }
    }
}
