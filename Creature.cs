using System;

namespace rpsQuest
{
    public class Creature
    {
        Random _random = new Random();

        private string _name;

        public int RockDamage { get; set; }
        public int PaperDamage { get; set; }
        public int ScissorsDamage { get; set; }

        public int Hitpoints { get; set; }
        public int MaxHP { get; set; }

        public int Level { get; set; }

        public string Name
        {
            get => _name;
            set { if (string.IsNullOrEmpty(_name)) { _name = value; } }
        }

        public int CalculateStrenght() => RockDamage + PaperDamage + ScissorsDamage + MaxHP;

        // returns true if creature is dead, false if still alive
        public bool DealDmg(int amount)
        {
            Hitpoints -= amount;

            return Hitpoints <= 0;
        }

        // returns dmg on certain move
        public int GetDmg(string move)
        {
            switch (move)
            {
                case "r":
                case "rock":
                    return RockDamage;
                case "p":
                case "paper":
                    return PaperDamage;
                default:
                    return ScissorsDamage;
            }
        }


        public Creature(int strength, int medianLevel, string name)
        {
            Name = name;

            var lvlDiff = _random.Next(-2, 3);
            medianLevel += lvlDiff;
            strength += lvlDiff;

            if (medianLevel <= 0) { medianLevel = 1; }      // correction of negative lvl
            if (strength < 4) { strength = 4; }             // correction of low power

            Level = medianLevel;

            var hp = _random.Next(Convert.ToInt32(strength / 2), strength - 2);
            Hitpoints = hp;
            MaxHP = Hitpoints;
            strength -= hp;

            var rockDmg = _random.Next(1, strength - 1);
            RockDamage = rockDmg;
            strength -= rockDmg;

            var paperDmg = _random.Next(1, strength);
            PaperDamage = paperDmg;
            strength -= paperDmg;

            ScissorsDamage = strength;

        }
    }
}
