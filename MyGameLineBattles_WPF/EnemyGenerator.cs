using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MyGameLineBattles_WPF
{
    public class EnemyGenerator
    {
        public EnemyGenerator()
        {

        }
        public DifficultyLevel BattleDifficultyLevel { get; private set; }
        public int enemiesWarbandMorale { get; private set; }
        public void ManuallyChangeEnemyWarbandMorale(int amount) { enemiesWarbandMorale = amount; }

        private List<Enemy> CreateHardEnemies(int result, List<Image> enemiesPictures)
        {
            List<Enemy> enemies;
            if (result == 1)
            {
                enemies = new List<Enemy>
                {
                new GeneralEnemy("General",enemiesPictures[0],60),
                new EliteEnemy("Grenadier",enemiesPictures[1],50),
                new EliteEnemy("Grenadier",enemiesPictures[2],50),
                new EliteEnemy("Grenadier",enemiesPictures[3],50),
                new Enemy("Fusilier",40,enemiesPictures[4],30),
                new Enemy("Fusilier",40,enemiesPictures[5],30),
                new Enemy("Fusilier",40,enemiesPictures[6],30),
                new Enemy("Fusilier",40,enemiesPictures[7],30),
                new Enemy("Fusilier",40,enemiesPictures[8],30),
                new Enemy("Fusilier",40,enemiesPictures[9],30),
                new Enemy("Fusilier",40,enemiesPictures[10],30),
                new Enemy("Fusilier",40,enemiesPictures[11],30)
                };
                enemiesWarbandMorale = 70;
                BattleDifficultyLevel = (DifficultyLevel)2;
                return enemies;
            }
            else
            {
                enemies = new List<Enemy>
                {
                new GeneralEnemy("General",enemiesPictures[0],60),
                new EliteEnemy("Grenadier",enemiesPictures[1],50),
                new EliteEnemy("Grenadier",enemiesPictures[2],50),
                new EliteEnemy("Grenadier",enemiesPictures[3],50),
                new EliteEnemy("Grenadier",enemiesPictures[4],50),
                new EliteEnemy("Grenadier",enemiesPictures[5],50),
                new Enemy("Fusilier",40,enemiesPictures[6],30),
                new Enemy("Fusilier",40,enemiesPictures[7],30),
                new Enemy("Fusilier",40,enemiesPictures[8],30),
                new Enemy("Recruit",40,enemiesPictures[9],20),
                new Enemy("Recruit",40,enemiesPictures[10],20),
                new Enemy("Recruit",40,enemiesPictures[11],20)
                };
                enemiesWarbandMorale = 70;
                BattleDifficultyLevel = (DifficultyLevel)2;
                return enemies;

            }
        }
        private List<Enemy> CreateNormalEnemies(int result, List<Image> enemiesPictures)
        {
            List<Enemy> enemies;
            if (result == 1)
            {
                enemies = new List<Enemy>
                {
                new EliteEnemy("Captain",enemiesPictures[0],55),
                new EliteEnemy("Grenadier",enemiesPictures[1],50),
                new Enemy("Fusilier",40,enemiesPictures[2],30),
                new Enemy("Fusilier",40,enemiesPictures[3],30),
                new Enemy("Fusilier",40,enemiesPictures[4],30),
                new Enemy("Fusilier",40,enemiesPictures[5],30),
                new Enemy("Fusilier",40,enemiesPictures[6],30),
                new Enemy("Fusilier",40,enemiesPictures[7],30),
                new Enemy("Recruit",40,enemiesPictures[8],20),
                new Enemy("Recruit",40,enemiesPictures[9],20),
                new Enemy("Recruit",40,enemiesPictures[10],20),
                new Enemy("Recruit",40,enemiesPictures[11],20)
                };
                enemiesWarbandMorale = 50;
                BattleDifficultyLevel = (DifficultyLevel)1;
                return enemies;
            }
            else
            {
                enemies = new List<Enemy>
                {
                new GeneralEnemy("Black Harry",enemiesPictures[0],60),
                new EliteEnemy("Grenadier",enemiesPictures[1],50),
                new EliteEnemy("Grenadier",enemiesPictures[2],50),
                new Enemy("Fusilier",40,enemiesPictures[3],30),
                new Enemy("Recruit",40,enemiesPictures[4],20),
                new Enemy("Recruit",40,enemiesPictures[5],20),
                new Enemy("Recruit",40,enemiesPictures[6],20),
                new Enemy("Recruit",40,enemiesPictures[7],20),
                new Enemy("Recruit",40,enemiesPictures[8],20),
                new Enemy("Recruit",40,enemiesPictures[9],20),
                new Enemy("Recruit",40,enemiesPictures[10],20),
                new Enemy("Recruit",40,enemiesPictures[11],20)
                };
                enemiesWarbandMorale = 50;
                BattleDifficultyLevel = (DifficultyLevel)1;
                return enemies;

            }
        }
        private List<Enemy> CreateEasyEnemies(int result, List<Image> enemiesPictures)
        {
            List<Enemy> enemies;
            if (result == 1)
            {
                enemies = new List<Enemy>
                {
                new EliteEnemy("Sergeant",enemiesPictures[0],50),
                new Enemy("Recruit",40,enemiesPictures[1],20),
                new Enemy("Recruit",40,enemiesPictures[2],20),
                new Enemy("Recruit",40,enemiesPictures[3],20),
                new Enemy("Recruit",40,enemiesPictures[4],20),
                new Enemy("Recruit",40,enemiesPictures[5],20),
                new Enemy("Recruit",40,enemiesPictures[6],20)
                };
                enemiesWarbandMorale = 40;
                BattleDifficultyLevel = (DifficultyLevel)0;
                return enemies;
            }
            else
            {
                enemies = new List<Enemy>
                {
                new Enemy("Cadet",40,enemiesPictures[1],30),
                new Enemy("Recruit",40,enemiesPictures[2],20),
                new Enemy("Recruit",40,enemiesPictures[3],20),
                new Enemy("Recruit",40,enemiesPictures[4],20),
                new Enemy("Recruit",40,enemiesPictures[5],20),
                new Enemy("Recruit",40,enemiesPictures[6],20),
                new Enemy("Recruit",40,enemiesPictures[7],20),
                new Enemy("Recruit",40,enemiesPictures[8],20),
                new Enemy("Recruit",40,enemiesPictures[9],20),
                new Enemy("Recruit",40,enemiesPictures[10],20)
                };
                enemiesWarbandMorale = 40;
                BattleDifficultyLevel = (DifficultyLevel)0;
                return enemies;

            }
        }
        private List<Enemy> CreateOrksHardEnemies(List<Image> enemiesPictures)
        {
            List<Enemy> enemies;
            enemies = new List<Enemy>
                {
                new OrcEliteEnemy("Elite",55,enemiesPictures[0],50),
                new OrcEliteEnemy("Elite",55,enemiesPictures[1],50),
                new OrcEliteEnemy("Elite",55,enemiesPictures[2],50),
                new OrcEliteEnemy("Elite",55,enemiesPictures[3],50),
                new OrcEliteEnemy("Elite",55,enemiesPictures[4],50),
                new OrcEliteEnemy("Elite",55,enemiesPictures[5],50),
                new OrcWarriorEnemy("Younger Warrior",40,enemiesPictures[6],25),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[7],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[8],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[9],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[10],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[11],30)
                };
            enemiesWarbandMorale = 65;
            BattleDifficultyLevel = (DifficultyLevel)2;
            return enemies;
        }
        private List<Enemy> CreateOrksEasyEnemies(int result, List<Image> enemiesPictures)
        {
            List<Enemy> enemies;
            if (result == 1)
            {
                enemies = new List<Enemy>
                {
                new OrcWarriorEnemy("Chief",50,enemiesPictures[0],50),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[1],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[2],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[3],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[4],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[5],30),
                new OrcWarriorEnemy("Younger Warrior",40,enemiesPictures[6],25)
                };
                enemiesWarbandMorale = 25;
                BattleDifficultyLevel = (DifficultyLevel)0;
                return enemies;
            }
            else
            {
                enemies = new List<Enemy>
                {
                new OrcWarriorEnemy("Big Tooth",55,enemiesPictures[0],50),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[1],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[2],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[3],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[4],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[5],30),
                new OrcWarriorEnemy("Younger Warrior",40,enemiesPictures[6],25),
                new OrcWarriorEnemy("Younger Warrior",40,enemiesPictures[7],25),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[8],30)
                };
                enemiesWarbandMorale = 25;
                BattleDifficultyLevel = (DifficultyLevel)0;
                return enemies;

            }
        }
        private List<Enemy> CreateOrksNormalEnemies(int result, List<Image> enemiesPictures)
        {
            List<Enemy> enemies;
            if (result == 1)
            {
                enemies = new List<Enemy>
                {
                new OrcEliteEnemy("Boss",85,enemiesPictures[0],60),
                new OrcEliteEnemy("Elite",55,enemiesPictures[1],50),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[2],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[3],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[4],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[5],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[6],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[7],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[8],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[9],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[10],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[11],30)
                };
                enemiesWarbandMorale = 40;
                BattleDifficultyLevel = (DifficultyLevel)1;
                return enemies;
            }
            else
            {
                enemies = new List<Enemy>
                {
                new OrcEliteEnemy("Boss",85,enemiesPictures[0],60),
                new OrcEliteEnemy("Elite",55,enemiesPictures[1],50),
                new OrcEliteEnemy("Elite",55,enemiesPictures[2],50),
                new OrcEliteEnemy("Elite",55,enemiesPictures[3],50),
                new OrcEliteEnemy("Elite",55,enemiesPictures[4],50),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[5],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[6],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[7],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[8],30),
                new OrcWarriorEnemy("Warrior",40,enemiesPictures[9],30)
                };
                enemiesWarbandMorale = 40;
                BattleDifficultyLevel = (DifficultyLevel)1;
                return enemies;

            }
        }
        private List<Enemy> CreateDemonEnemies(List<Image> enemiesPictures)
        {
            List<Enemy> enemies;
            enemies = new List<Enemy>
                {
                new DemonWarriorEnemy("Older minion",enemiesPictures[0],66),
                new DemonWarriorEnemy("Minion",enemiesPictures[1],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[2],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[3],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[4],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[5],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[6],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[7],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[8],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[9],50)
                };
            enemiesWarbandMorale = 5;
            BattleDifficultyLevel = (DifficultyLevel)1;
            return enemies;
        }
        private List<Enemy> CreateDemonBossEnemies(List<Image> enemiesPictures)
        {
            List<Enemy> enemies;
            enemies = new List<Enemy>
                {
                new DemonWarriorEnemy("Minion",enemiesPictures[0],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[1],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[2],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[3],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[4],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[5],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[6],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[7],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[8],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[9],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[10],50),
                new DemonWarriorEnemy("Minion",enemiesPictures[11],50)
                };
            enemiesWarbandMorale = 100;
            BattleDifficultyLevel = (DifficultyLevel)3;
            return enemies;
        }

        public List<Enemy> GenerateRandomEnemies(Random gameRandom, IEnumerable<Enemy> enemies, List<Image> enemiesPictures,int targetLocation)
        {
            int[] redLoc = { 0, 1, 2, 3, 8, 9 };
            int randomResult = gameRandom.Next(2);
            if (Array.Exists(redLoc, location => location == targetLocation))//redcoats!
            {
                if (targetLocation <= 2)
                    return CreateEasyEnemies(randomResult, enemiesPictures);
                else if (targetLocation == 8)
                    return CreateNormalEnemies(randomResult, enemiesPictures);
                else
                    return CreateHardEnemies(randomResult, enemiesPictures);
            }
            else if (targetLocation >= 4 && targetLocation <= 7) //orcs!
            {
                if (targetLocation == 4)
                    return CreateOrksEasyEnemies(randomResult, enemiesPictures);
                else if (targetLocation == 6)
                    return CreateOrksHardEnemies(enemiesPictures);
                else
                    return CreateOrksNormalEnemies(randomResult, enemiesPictures);
            }
            else //demons!
            {
                if (targetLocation == 10)
                    return CreateDemonEnemies(enemiesPictures);
                else
                    return CreateDemonBossEnemies(enemiesPictures);
            }
        }
    }

}
