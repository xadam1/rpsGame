using System;

namespace rpsQuest
{
    class Program
    {
        private static Random _random = new Random();

        private static string[] _monsterNames = new string[] { "Giant", "Spider", "Deadly Elf",
                                                                "Medusa", "Cyclop", "Chimera", "Cerberus",
                                                                "Manticore", "Minotaur", "Griffin"
                                                            };

        // Main method takes care of basic commands
        static void Main(string[] args)
        {
            bool isRunning = true;

            Console.WriteLine("Welcome to Rock-Paper-Scissor RPG!");
            Printer.DisplayHelp();

            while (isRunning)
            {
                var input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "new game":
                        Game();
                        Console.WriteLine($"Hope you enjoyed! See you soon...");
                        Console.ReadKey();
                        return;

                    case "help":
                        Printer.DisplayHelp();
                        break;

                    case "exit":
                        isRunning = false;
                        break;

                    default:
                        Printer.PrintErrors(null, PrintingCodes.InvalidCmd);
                        break;
                }

            }
        }


        // Wipes old hero (if there is one) and creates brand new hero
        private static Hero CreateHero()
        {
            Console.WriteLine();
            Console.WriteLine("What Is Your Hero's Name?");

            Hero hero = new Hero(Console.ReadLine());
            Printer.PrintHeroStatus(hero);
            return hero;
        }


        // takes care when 'new game' is called. Logic of the game
        public static void Game()
        {
            var hero = CreateHero();

            var healFlag = true;
            var gameOn = true;
            bool newGameInFightWasCalled = false;

            while (gameOn)
            {
                if (hero.Hitpoints <= 0)        // Hero is dead check
                {
                    Console.WriteLine($"{hero.Name} won't be able to fight again... Wanna play again? (Y)");
                    var assurance = Console.ReadLine();
                    if (assurance == "Y")
                    {
                        hero = CreateHero();
                    }
                    else { return; }

                }

                if (hero.Level >= 10)       // Won the Game Check
                {
                    Console.WriteLine("So what now? You already beat this game... Wanna play again? (Y)");
                    var anotherGame = Console.ReadLine();
                    if (anotherGame == "Y") { hero = CreateHero(); healFlag = true; }
                    else { return; }
                }

                var input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "new game":
                        Console.WriteLine($"Are you sure? {hero.Name} and his progress will be destroyed, FOREVER - really long time...\t(Y/N)");
                        var assurance = Console.ReadLine();
                        if (assurance == "Y")
                        {
                            Console.WriteLine("Okey, dokey...");
                            Console.WriteLine();
                            hero = CreateHero();
                            healFlag = true;
                        }
                        break;

                    case "fight":
                        newGameInFightWasCalled = Fight(hero);
                        if (newGameInFightWasCalled)
                        {
                            hero = CreateHero();
                        }
                        healFlag = true;
                        break;

                    case "healer":
                        Healer(hero, healFlag);
                        healFlag = false;
                        break;

                    case "lvlup rock":
                    case "lvlup paper":
                    case "lvlup scissors":
                        hero.LvlUpDamage(input.Split(' ')[1]);
                        break;

                    case "status":
                        Printer.PrintHeroStatus(hero);
                        break;

                    case "help":
                        Printer.DisplayHelp();
                        break;

                    case "exit":
                        gameOn = false;
                        break;

                    default:
                        Printer.PrintErrors(null, PrintingCodes.InvalidCmd);
                        break;
                }
            }

        }


        // Heals hero and prints his current HP. Also handles, only healing once between fights, and full HP.
        public static void Healer(Hero hero, bool flag)
        {
            if (!flag) { Console.WriteLine("Sorry, you can only heal once between fights!"); return; }

            if (hero.Hitpoints == hero.MaxHP) { Console.WriteLine("You already have full HP."); return; }

            var healedHP = _random.Next(Convert.ToInt32(hero.MaxHP * 0.75), hero.MaxHP);

            if (healedHP <= hero.Hitpoints) { Console.WriteLine("You were unlucky, and healer did not heal you this time! :'("); }
            else
            {
                hero.Hitpoints = healedHP;
                Console.WriteLine($"{hero.Name}'s HP was healed to {hero.Hitpoints}/{hero.MaxHP} HP");
            }
            Console.WriteLine();
        }


        // One sigle battle within the whole Fight. Returns who won current round.
        public static int Battle(string heroMove, string monsterMove, Hero hero, Monster monster)
        {
            heroMove = heroMove.ToLower();
            monsterMove = monsterMove.ToLower();

            switch (heroMove)
            {
                case "r":
                case "rock":
                    return monsterMove == "paper" ? 2 :
                            monsterMove == "scissors" ? 1 : 0;
                case "p":
                case "paper":
                    return monsterMove == "scissors" ? 2 :
                            monsterMove == "rock" ? 1 : 0;
                case "s":
                case "scissors":
                    return monsterMove == "rock" ? 2 :
                            monsterMove == "paper" ? 1 : 0;

                case "status":
                    return -2;

                case "help":
                    return -3;

                case "new game":
                    return -5;
            }

            return -1;      // should never happen. Only if typing error occured.
        }


        // Whole fight with monster. Hadnles logic + basic printing of ongoing status.
        public static bool Fight(Hero hero)
        {
            int monsterNameIndex = _random.Next(0, _monsterNames.Length);
            Monster monster = new Monster(Convert.ToInt32(hero.CalculateStrenght() * 0.7), hero.Level, _monsterNames[monsterNameIndex]);
            Console.WriteLine();

            Console.WriteLine($"{hero.Name} encountered an enemy.");
            Printer.PrintMonsterInfo(monster);

            string heroMove;
            string monsterMove;
            int winner;
            int dmg;
            bool monsterKilled = false;
            bool heroKilled = false;
            int roundCounter = 1;

            while (!heroKilled && !monsterKilled)
            {
                Console.WriteLine($"Round {roundCounter}: What do you choose? (Rock / Paper / Scissors)");
                heroMove = Console.ReadLine().ToLower();
                monsterMove = monster.NextMove();

                winner = Battle(heroMove, monsterMove, hero, monster);

                // print enemy move if no error occured (Negative winner -> error/specific behaviour)
                if (winner >= 0) { Console.WriteLine($"{monster.Name} has chosen {monsterMove}.\n"); }

                if (winner == -5)   // new game was called
                {
                    return true;
                }

                switch (winner)
                {
                    case -3:    // help
                        Printer.DisplayHelp();
                        continue;

                    case -2:    // status in battle
                        Printer.PrintHeroStatus(hero);
                        Printer.PrintMonsterInfo(monster);
                        continue;

                    case -1:    // Invalid input 
                        Console.WriteLine("What the heck? You never played RPS? Just pick one Rock / Paper / Scissors... -_-");
                        continue;

                    case 1:     // Hero won
                        dmg = hero.GetDmg(heroMove);
                        Console.WriteLine($"{hero.Name} won! {monster.Name} was hit for {dmg}HP.");
                        monsterKilled = monster.DealDmg(dmg);
                        if (!monsterKilled)
                        {
                            Console.WriteLine($"{monster.Name} has still {monster.Hitpoints} HP! FINISH IT!");
                        }
                        break;

                    case 2:     // Monster won
                        dmg = monster.GetDmg(monsterMove);
                        Console.WriteLine($"{monster.Name} beat you on this one! {hero.Name} has taken -{dmg}HP damage.");
                        heroKilled = hero.DealDmg(dmg);
                        if (!heroKilled)
                        {
                            Console.WriteLine($"{hero.Name} has {hero.Hitpoints} HP left. Careful here!");
                        }
                        break;

                    default:    // tie
                        Console.WriteLine($"Aggrrh... This one is a tie!");
                        break;
                }
                Console.WriteLine();
                roundCounter++;
            }

            if (heroKilled)
            {
                Console.WriteLine($"{hero.Name} was killed in ugly slaughter against {monster.Name}! Very sad...\n");
                Printer.PrintHeroStatus(hero);
                Console.WriteLine();
                return false;
            }

            var earnedXP = monster.CalculateStrenght();
            earnedXP = _random.Next(Convert.ToInt32(earnedXP * 0.65), earnedXP);
            Console.WriteLine($"Great job {hero.Name}! You have killed {monster.Name}, and gained {earnedXP}XP.");
            hero.AddExp(earnedXP);

            if (hero.Level < 10)
            {
                Console.WriteLine("Continue by using any command form 'help'");
            }

            return false;
        }

    }
}
