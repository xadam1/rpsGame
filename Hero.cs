using System;

namespace rpsQuest
{
    public partial class Hero : Creature
    {
        public int SkillPoints { get; set; } = 0;
        public int NeededEXP { get; set; } = 7;

        private Random random = new Random();

        public int Experience { get; private set; }


        // Methods
        public Hero(string name) : base(strength: 10, medianLevel: 1, name: name) { }

        public void AddExp(int amount)
        {
            Experience += amount;
            if (Experience >= NeededEXP)
            {
                Experience -= NeededEXP;
                LvlUp();
            }

        }


        private void LvlUp()
        {
            Level++;
            SkillPoints++;
            MaxHP = Convert.ToInt32(Math.Round(NeededEXP * 1.25));
            NeededEXP = Convert.ToInt32(Math.Round(NeededEXP * 1.3));
            Console.WriteLine();
            Printer.PrintHeroLvlUp(this);
        }

        public void LvlUpDamage(string weapon)
        {
            if (SkillPoints <= 0)
            {
                Printer.PrintErrors(this, PrintingCodes.NotEnoughSkillPoints);
                return;
            }

            switch (weapon)
            {
                case "rock":
                    RockDamage++;
                    Printer.LvlUpWeapon(this, "rock");
                    break;
                case "paper":
                    PaperDamage++;
                    Printer.LvlUpWeapon(this, "paper");
                    break;
                case "scissors":
                    ScissorsDamage++;
                    Printer.LvlUpWeapon(this, "scissors");
                    break;

                default:
                    Printer.PrintErrors(this, PrintingCodes.InvalidCmd);
                    return;
            }
            SkillPoints--;
        }


    }
}
