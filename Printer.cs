using System;

namespace rpsQuest
{
    public static class Printer
    {
        public static void PrintHeroLvlUp(Hero hero)
        {
            if (hero.Level == 10)
            {
                Console.WriteLine($"OH MY GOSH! {hero.Name.ToUpper()} WON THE WHOLE GAME! {hero.Name.ToUpper()} IS LVL 10!");
                Console.WriteLine("Seriously good job, I was not sure if it's even possible. Well Done!");
                Console.WriteLine();
                return;
            }
            Console.WriteLine($"Well Done! {hero.Name} is now lvl {hero.Level}! With Max HP at {hero.MaxHP}");
            Console.WriteLine($"You currently have {hero.SkillPoints} skill points. Use them to up your damage on rock, paper or scissors.");
            Console.WriteLine($"Current XP:\t{hero.Experience}/{hero.NeededEXP}");
            Console.WriteLine();
        }

        public static string GetHeroName()
        {
            Console.WriteLine("What Is Your Hero's Name?");
            return Console.ReadLine();
        }

        public static void PrintErrors(Hero hero, PrintingCodes identifier)
        {
            switch (identifier)
            {
                case PrintingCodes.NotEnoughSkillPoints:
                    Console.WriteLine("You don't have enough skill points to upgrade your weapon!");
                    break;
                case PrintingCodes.InvalidCmd:
                    Console.WriteLine("Invalid command. Try Again.");
                    break;
                default:
                    break;
            }

        }

        public static void DisplayHelp()
        {
            Console.WriteLine("=============================================HELP=============================================");
            Console.WriteLine("||'new game' - starts brand new game, progress will be lost.                                ||");
            Console.WriteLine("||'fight' - starts a fight. Cannot be called, when already in fight.                        ||");
            Console.WriteLine("||'healer' - heals your hero. Only between fights.                                          ||");
            Console.WriteLine("||'lvlup rock/paper/scissors' - upgrades your damage on desired weapon. Requires SkillPoint.||");
            Console.WriteLine("||'rock/paper/scissors' - picks weapon for current round. Only in fight                     ||");
            Console.WriteLine("||'status' - shows status of your hero. Only when in game.                                  ||");
            Console.WriteLine("||'help' - shows this table.                                                                ||");
            Console.WriteLine("||'exit' - exits the game.                                                                  ||");
            Console.WriteLine("=============================================HELP=============================================");


            Console.WriteLine();
        }


        public static void PrintMonsterInfo(Monster monster)
        {
            Console.WriteLine();
            Console.WriteLine($"-------------------------------------Monster's Stats-----------------------------------------");
            Console.WriteLine($"{monster.Name}, lvl {monster.Level}:");
            Console.WriteLine($"\t{monster.Hitpoints} HP");
            Console.WriteLine($"\tRock Damage: {monster.RockDamage}\t Paper Damage: {monster.PaperDamage}\tScissors Damage: {monster.ScissorsDamage}");
            Console.WriteLine($"---------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        public static void PrintHeroStatus(Hero hero)
        {
            Console.WriteLine();
            Console.WriteLine($"--------------------------------------Hero's  Stats------------------------------------------");
            Console.WriteLine($"{hero.Name}, lvl {hero.Level} \t {hero.Hitpoints}/{hero.MaxHP} HP");
            Console.WriteLine($"\tDamage:\t\tRock: {hero.RockDamage},  Paper: {hero.PaperDamage},  Scissors: {hero.ScissorsDamage}");
            Console.WriteLine($"\tSkill Points: {hero.SkillPoints}\t\tXP: {hero.Experience}  (To next lvl {hero.Name} needs: {hero.NeededEXP-hero.Experience} XP)");
            Console.WriteLine($"---------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        public static void LvlUpWeapon(Hero hero, string weapon)
        {
            Console.WriteLine();
            Console.WriteLine($"{hero.Name} upgreaded his {weapon} damage!\t{weapon} damage: {hero.GetDmg(weapon)}");
            Console.WriteLine();
        }
    }
}
