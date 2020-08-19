using System;

namespace rpsQuest
{
    public class Monster : Creature
    {
        Random random = new Random();

        public string NextMove()
        {
            var nextMove = random.Next(0, 3);

            switch (nextMove)
            {
                case 0:
                    return "rock";
                case 1:
                    return "paper";
                default:
                    return "scissors";
            }
        }

        public Monster(int strength, int midLvl, string name) : base(strength, midLvl, name) { }


    }

}
