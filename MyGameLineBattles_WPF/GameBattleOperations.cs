using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MyGameLineBattles_WPF
{
    public partial class Game
    {
        //----------------------------- BATTLE -------------------------------
        //------------------------------------------------ setting battlefield
        //enemy is attacking my location! -------------------
        public bool EnemyIsAttackingMyLocation()
        {
            if (GetMyLocations().Count <= 3)
                return false;
            if (gameRandom.Next(3) == 1)
            {
                worldMap.DeleteDefendedLocation(gameRandom);
                return true;
            }
            return false;
        }
        //------------------------------------ ORKS & DEMONS LOCATION
        public void IsThisOrksLocation()
        {
            if (worldMap.IsThisOrksLocation())
                OrcsAreEnemy = true;
            else
                OrcsAreEnemy = false;
            IsThisDemonsLocation();
        }
        public void IsThisDemonsLocation()
        {
            if (worldMap.IsThisDemonsLocation())
                DemonsAreEnemy = true;
            else
                DemonsAreEnemy = false;
        }
        //-----------------------------------------------------------
        public void BattleRetreatEffect()
        {
            short countCaptured = 0;
            for (int i = 0; i < defenders.Count; i++)
                if (gameRandom.Next(3) == 1)
                {
                    countCaptured++;
                    defenders.Remove(defenders[i]);
                }
            if (countCaptured > 0)
                MessageBox.Show(countCaptured + " warriors were captured or killed by the enemy!");
        }
        public void CreateEnemy()
        {
            enemies = generateEnemies.GenerateRandomEnemies(gameRandom, enemies, enemiesPictures, worldMap.targetLocation);
            UpdateDefendersLocalizations();
            //UpdateEnemyAndDefendersData();

            enemiesMoraleBreakPoint = enemies.Count / 2;
            defendersMoraleBreakPoint = defenders.Count / 2;
        }
        public void UpdateEnemyAndDefendersData()
        {
            string defendersAndEnemiesData = string.Empty;
            foreach (Defender defender in defenders)
                defendersAndEnemiesData += defender.Name + " HP: " + defender.HealthPoints + " Ballistic: " + defender.MyShootingSkill + "%" + Environment.NewLine;
            if (myCannon != null)
                defendersAndEnemiesData += Environment.NewLine + myCannon.Name + " HP: "
                    + myCannon.HealthPoints + " Ballistic: " + myCannon.MyShootingSkill + "%" + Environment.NewLine;
            defendersLabel.Content = defendersAndEnemiesData;

            defendersAndEnemiesData = string.Empty;
            foreach (Enemy enemy in enemies)
                defendersAndEnemiesData += enemy.Name + " HP: " + enemy.HealthPoints + " Ballistic: " + enemy.MyShootingSkill + "%" + Environment.NewLine;
            if (enemyCannon != null)
                defendersAndEnemiesData += Environment.NewLine + enemyCannon.Name + " HP: "
                    + enemyCannon.HealthPoints + " Ballistic: " + enemyCannon.MyShootingSkill + "%" + Environment.NewLine;
            justEffectCounterLabel.Content = defendersAndEnemiesData;

        }
        public void ShowDefendersOnTheBattlefield()
        {
            foreach (Defender defender in defenders)
                defender.ShowMeOnTheBattlefield();

            foreach (Enemy enemy in enemies)
                enemy.ShowMeOnTheBattlefield();
        }
        //----IMPORTANT!!!!!!!!!!
        public void UpdateDefendersLocalizations()
        {
            TrueDefendersWarbandMorale = PlayerWarbandMorale;
            for (int i = 0; i < defenders.Count; i++)
            {
                if (defenders[i] is IAmGeneralDefender)
                {
                    GeneralDefender temporary = defenders[i] as GeneralDefender;
                    TrueDefendersWarbandMorale = temporary.GeneralWarbandMorale;
                }
                defenders[i].SetMyRealPicture();
                defenders[i].SelectNewPictureForMe(defendersPictures[i]);
                defenders[i].RemoveArrow();
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].SetMyRealPicture();
                enemies[i].SelectNewPictureForMe(enemiesPictures[i]);
            }
            ShowDefendersOnTheBattlefield();
        }

        //---------------------------------------- shooting
        public bool PlayerIsShooting()
        {
            BattleReport = string.Empty;
            BattleReport += "Difficulty: " + generateEnemies.BattleDifficultyLevel
                + "\nDefenders Morale Break Point: " + defendersMoraleBreakPoint
                + "\nDefenders Morale: " + TrueDefendersWarbandMorale
                + "\nEnemies Morale Break Point: " + enemiesMoraleBreakPoint + Environment.NewLine;
            CloudEffect oblok = new CloudEffect(false, defenders.Count, this);
            Enemy enemy;
            for (int i = 0; i < defenders.Count; i++)
            {

                int hits = gameRandom.Next(1, 101);
                BattleReport += hits + Environment.NewLine;
                if (hits > defenders[i].MyShootingSkill)
                    continue;
                enemy = enemies[gameRandom.Next(enemies.Count)];
                BattleReport += defenders[i].Name + " hit enemy: " + enemy.Name + Environment.NewLine;
                if (enemy.AssessDamage(OrcsAreEnemy))
                {
                    BattleReport += defenders[i].Name + " kill enemy: " + enemy.Name + Environment.NewLine;
                    defenders[i].ChangeDefenderExperiencePoints(1);
                    enemy.RestoreDefaultImagePosition();
                    enemies.Remove(enemy);
                    if (enemies.Count == 0)
                    {
                        gameIsOver = true;
                        return true;
                    }

                }
            }
            return false;

        }
        public void DefendersGetHittedByArrow()
        {
            foreach (Defender defender in defenders)
                if (defender.GetHitedByArrow)
                    defender.SetArrowHitImage();
            foreach (Image defenderImage in tempDefendersPictures)
                defenderImage.Source = new BitmapImage(new Uri(@"Resources\BlueDefenderDeadArrow.png", UriKind.Relative));
        }
        public bool EnemyIsShooting()
        {
            BattleReport += Environment.NewLine;
            CloudEffect oblok = new CloudEffect(true, enemies.Count, this);
            tempDefendersPictures = new List<Image>();
            Defender defender;
            for (int i = 0; i < enemies.Count; i++)
            {
                int hits = gameRandom.Next(1, 101);
                BattleReport += hits + Environment.NewLine;
                if (hits > enemies[i].MyShootingSkill)
                    continue;
                defender = defenders[gameRandom.Next(defenders.Count)];
                BattleReport += enemies[i].Name + " hit trooper: " + defender.Name + Environment.NewLine;
                if (defender.AssessDamage(OrcsAreEnemy))
                {
                    BattleReport += enemies[i].Name + " kill trooper: " + defender.Name + Environment.NewLine;
                    tempDefendersPictures.Add(defender.GetMyScaryPicture());
                    defender.RestoreDefaultImagePosition();
                    defenders.Remove(defender);
                    if (defenders.Count == 0)
                    {
                        gameIsOver = true;
                        return true;
                    }
                }

            }
            return false;
        }
        public bool WeWonTheBattle()
        {
            int lootedMoney;
            if (generateEnemies.BattleDifficultyLevel == (DifficultyLevel)0)
            {
                player.GetPaid(500);
                player.IncreasePlayerLeaderSkill(1);
                lootedMoney = 500;
            }
            else if (generateEnemies.BattleDifficultyLevel == (DifficultyLevel)1)
            {
                player.GetPaid(600);
                player.IncreasePlayerLeaderSkill(2);
                lootedMoney = 600;
            }
            else if (generateEnemies.BattleDifficultyLevel == (DifficultyLevel)2)
            {
                player.GetPaid(700);
                player.IncreasePlayerLeaderSkill(2);
                lootedMoney = 700;
            }
            else
            {
                player.GetPaid(2000);
                player.IncreasePlayerLeaderSkill(3);
                lootedMoney = 2000;
            }

            MessageBox.Show("Victory !!! " + generateEnemies.BattleDifficultyLevel
                + "\nLoot: " + lootedMoney);

            justEffectCounterLabel.Content = "Enemy Morale Test Failed! \nDefenders Win!";
            firingButton.Visibility = Visibility.Collapsed;
            if (DemonsAreEnemy)
                justEffectCounterLabel.Content += "\n\nBut we didn't kill the main demon!";
            else
                worldMap.AddConqueredLocation();
            if (GetMyLocations().Count == 12)
            {
                MessageBox.Show("All of the World is Yours! + 500 gold bonus!");
                player.GetPaid(500);
                return true;
            }
            return false;

        }

        public void WeLostTheBattle()
        {

            //MessageBox.Show("Totalna porażka !!!");
            defendersLabel.Content = "Our Morale Test Failed! \nDefenders Lost!";
            firingButton.Visibility = Visibility.Collapsed;

        }

        //------------------------------------------------ cannon ========================
        public bool PlayerCannonIsShooting()
        {
            myCannon.ShootingRollBack();
            BattleReport = string.Empty;
            BattleReport += "Difficulty: " + generateEnemies.BattleDifficultyLevel
                + "\nDefenders Morale Break Point: " + defendersMoraleBreakPoint
                + "\nEnemies Morale Break Point: " + enemiesMoraleBreakPoint + Environment.NewLine;
            int hits = gameRandom.Next(1, 101);
            BattleReport += hits + Environment.NewLine;
            CannonCloudEffect cannonCloudEffect = new CannonCloudEffect(false, 99, this);
            if (hits > myCannon.MyShootingSkill)
                return false;
            int[] targets = WhomCanIHit(GetCannonPosition(), enemies.Count, false);
            if (targets == null)
                return false;

            Enemy enemy;
            for (int i = 0; i < targets.Length; i++)
            {
                myCannon.ChangeDefenderCannonExperiencePoints(1);
                enemy = enemies[targets[i]];
                BattleReport += myCannon.Name + " hit enemy: " + enemy.Name + Environment.NewLine;
                enemy.AssessDamage(false);
                if (enemy.AssessDamage(false))
                {
                    BattleReport += "Enemy killed: " + enemy.Name + Environment.NewLine;
                    //defenders[i].ChangeDefenderExperiencePoints(1);
                    enemy.RestoreDefaultImagePosition();
                    enemies.Remove(enemy);
                    if (enemies.Count == 0)
                    {
                        gameIsOver = true;
                        return true;
                    }

                }
            }
            return false;
        }

        public bool EnemyCannonIsShooting()
        {
            enemyCannon.ShootingRollBack();
            BattleReport += Environment.NewLine;
            int hits = gameRandom.Next(1, 101);
            BattleReport += hits + Environment.NewLine;
            CannonCloudEffect cannonCloudEffect = new CannonCloudEffect(true, 99, this);
            if (hits > enemyCannon.MyShootingSkill)
                return false;
            int[] targets = WhomCanIHit(gameRandom.Next(3), defenders.Count, true);
            if (targets == null)
                return false;

            Defender defender;
            for (int i = 0; i < targets.Length; i++)
            {
                defender = defenders[targets[i]];
                BattleReport += enemyCannon.Name + " hit trooper: " + defender.Name + Environment.NewLine;
                defender.AssessDamage(false);
                if (defender.AssessDamage(false))
                {
                    BattleReport += "Trooper killed: " + defender.Name + Environment.NewLine;
                    defender.RestoreDefaultImagePosition();
                    defenders.Remove(defender);
                    if (defenders.Count == 0)
                    {
                        gameIsOver = true;
                        return true;
                    }
                }

            }
            return false;
        }

        public int[] WhomCanIHit(int cannonPosition, int targetListCount, bool enemy)
        {
            if (cannonPosition == 0)
            {
                int randomResult = gameRandom.Next(3);
                if (randomResult == 0)
                {
                    if (targetListCount >= 7)
                        return new int[2] { 6, 0 };
                    else
                        return new int[1] { 0 };
                }
                else if (randomResult == 1)
                {
                    if (targetListCount >= 8)
                        return new int[2] { 7, 1 };
                    else if (targetListCount >= 2)
                        return new int[1] { 1 };
                    else
                    {
                        BattleReport += "The shot was not aimed at the enemy!" + Environment.NewLine;
                        return null;
                    }
                }
                else
                {
                    if (targetListCount >= 9)
                        return new int[2] { 8, 2 };
                    else if (targetListCount >= 3)
                        return new int[1] { 2 };
                    else
                    {
                        BattleReport += "The shot was not aimed at the enemy!" + Environment.NewLine;
                        return null;
                    }
                }
            }
            else if (cannonPosition == 1)
            {
                int randomResult = gameRandom.Next(3);
                if (randomResult == 0)
                {
                    if (targetListCount >= 10)
                        return new int[2] { 9, 3 };
                    else if (targetListCount >= 4)
                        return new int[1] { 3 };
                    else
                    {
                        BattleReport += "The shot was not aimed at the enemy!" + Environment.NewLine;
                        return null;
                    }
                }
                else if (randomResult == 1)
                {
                    if (targetListCount >= 11)
                        return new int[2] { 10, 4 };
                    else if (targetListCount >= 5)
                        return new int[1] { 4 };
                    else
                    {
                        BattleReport += "The shot was not aimed at the enemy!" + Environment.NewLine;
                        return null;
                    }
                }
                else
                {
                    if (targetListCount >= 12)
                        return new int[2] { 11, 5 };
                    else if (targetListCount >= 6)
                        return new int[1] { 5 };
                    else
                    {
                        BattleReport += "The shot was not aimed at the enemy!" + Environment.NewLine;
                        return null;
                    }
                }
            }
            else
            {
                if (enemy)
                {
                    if (myCannon != null)
                    {
                        BattleReport += "Our Cannon was hit!" + Environment.NewLine;
                        if (myCannon.AssessDamage())
                        {
                            BattleReport += "Our Cannon was destroyed!" + Environment.NewLine;
                            myCannon.ChangeImageToDestroyed();
                            myCannon.RestoreDefaultImagePosition();
                            myCannon = null;
                        }
                        return null;
                    }
                    else
                    {
                        BattleReport += "The shot was not aimed at the enemy!" + Environment.NewLine;
                        return null;
                    }
                }
                else
                {
                    if (enemyCannon != null)
                    {
                        if (DemonsAreEnemy)
                            BattleReport += "Main Demon was hit!" + Environment.NewLine;
                        else
                            BattleReport += "Enemy Cannon was hit!" + Environment.NewLine;
                        if (enemyCannon.AssessDamage())
                        {
                            enemyCannon.ChangeImageToDestroyed(DemonsAreEnemy);
                            if (DemonsAreEnemy)
                            {
                                BattleReport += "Main Demon was destroyed!\nWe are victorious!" + Environment.NewLine;
                                DemonsAreEnemy = false;
                                SoundPlayer sndPlayer = new SoundPlayer(MyGameLineBattles_WPF.Properties.Resources.demon_die_2);
                                sndPlayer.Play();
                                enemiesMoraleBreakPoint = 66;
                                enemiesWarbandMorale = 5;
                            }
                            else
                                BattleReport += "Enemy Cannon was destroyed!" + Environment.NewLine;
                            enemyCannon.RestoreDefaultImagePosition();
                            enemyCannon = null;
                        }
                        myCannon.ChangeDefenderCannonExperiencePoints(1);
                        return null;
                    }
                    else
                    {
                        BattleReport += "The shot was not aimed at the enemy!" + Environment.NewLine;
                        return null;
                    }
                }
            }

        }
        public void ChangeCannonPosition()
        {
            myCannon.ChangeCannonPosition();
        }
        public int GetCannonPosition()
        {
            return myCannon.CannonPosition;
        }

        public bool EnemyCannonCanShoot()
        {
            return enemyCannon.CannonCanShoot;
        }
        public void EnemySetCannonShoot(bool shoot)
        {
            enemyCannon.SetCannonShoot(shoot);
        }
        public int GetEnemyCannonPosition()
        {
            return enemyCannon.CannonPosition;
        }

    }
}
