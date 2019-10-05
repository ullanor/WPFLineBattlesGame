using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Media;

namespace MyGameLineBattles_WPF
{
    public partial class Game
    {
        private GameMusic mainMusic;
        //orks and demon test XD
        public bool OrcsAreEnemy { get; private set; }
        public bool DemonsAreEnemy { get; private set; }
        private List<Image> tempDefendersPictures;
        //world map test -------------------------------
        public WorldMapClass worldMap = new WorldMapClass();
        public List<int> GetMyLocations()
        {
            return worldMap.MyLocations;
        }
        //publiczne -----------------------------------       
        public string BattleReport { get; set; }
        public bool gameIsOver { get; set; }
        public Random gameRandom;
        public Button firingButton;
        public Label justEffectCounterLabel;
        public Label defendersLabel;
        public int PlayerWarbandMorale
        {
            get { return player.PlayerWarbandMorale; }
        }
        public int enemiesWarbandMorale
        {
            get { return generateEnemies.enemiesWarbandMorale; }
            set { generateEnemies.ManuallyChangeEnemyWarbandMorale(75); }
        }
        //----------------------------------------------------
        public int TrueDefendersWarbandMorale { get; private set; }
        private List<Image> enemiesPictures;
        private List<Image> defendersPictures;

        public Image FiringCloudEffect { get; private set; }

        public EnemyGenerator generateEnemies { get; private set; }
        private Player player;

        public DefenderCannon myCannon { get; private set; }
        public EnemyCannon enemyCannon { get; set; }

        public List<Enemy> enemies { get; private set; }
        public int enemiesMoraleBreakPoint { get; private set; }

        public List<Defender> defenders { get; private set; }
        public List<Defender> ReserveDefenders { get; private set; }
        public int defendersMoraleBreakPoint { get; private set; }

        public Game(List<Image> defendersPictures, List<Image> enemiesPictures, Image firingCloudEffect, Label justEffectCounterLabel, Label defendersLabel, Button firingButton)
        {
            this.FiringCloudEffect = firingCloudEffect;
            this.justEffectCounterLabel = justEffectCounterLabel;
            this.defendersLabel = defendersLabel;
            this.firingButton = firingButton;
            this.defendersPictures = defendersPictures;
            this.enemiesPictures = enemiesPictures;

            player = new Player("Player", 500);
            defenders = new List<Defender>();
            ReserveDefenders = new List<Defender>();
            gameRandom = new Random();
            generateEnemies = new EnemyGenerator();
            //testlocation
            worldMap.MyLocations.Add(0);           
            CreateAuxiliaryClasses();
        }
        //---test music
        private void CreateAuxiliaryClasses()
        {
            mainMusic = new GameMusic();       
            //+ obiekt klasy operations na defenders
        }
        public void SetMusicOnOf()
        {
            mainMusic.SetMusicOnOf();
        }
        // -------------------------------------- PLAYER ACTIONS
        public int GetPlayerMoney()
        {
            return player.PlayerMoney;
        }
        public bool PlayerMustPayMoney(int amount)
        {
            player.PayFor(100);
            if (player.PlayerMoney < 0)
            {             
                return false;
            }
            return true;
        }
        public string UpdatePlayerMoneyAndDefendersCount()
        {
            string dataToReturn = string.Empty;
            int eliteCount = 0;
            dataToReturn = "Treasury: " + GetPlayerMoney() + " gold" + Environment.NewLine;
            if (defenders != null)
                foreach (Defender defender in defenders)
                    if (defender is IAmEliteDefender)
                        eliteCount++;
            dataToReturn += "Number of Elite: " + eliteCount + Environment.NewLine
                + "Number of Troopers: " + (defenders.Count - eliteCount);
            dataToReturn += "\nNumber of reservist: " + ReserveDefenders.Count;//player.PlayerLeaderSkill;
            return dataToReturn;
        }
        //----------------------------------------------- CREATING DEFENDERS ---
        public void CreateRecruitDefender()
        {
            //test music
            //mainMusic.Stream = Properties.Resources.musket4;
            //mainMusic.PlaySync();
            //tttt
            if (defenders.Count >= 12)
            {
                MessageBox.Show("Troopers limit reached!");
                return;
            }
            if (player.PlayerMoney < (int)DefenderPrice.Recruit)
            {
                MessageBox.Show("No funds!");
                return;
            }

            int myNumberOfPicture = defenders.Count;
            defenders.Add(new RecruitDefender("Recruit " + (DefenderName)gameRandom.Next(25), 40, defendersPictures[myNumberOfPicture], 20,0));
            player.PayFor((int)DefenderPrice.Recruit);
        }
        public void CreateDefender()
        {
            if (defenders.Count >= 12)
            {
                MessageBox.Show("Troopers limit reached!");
                return;
            }
            if (player.PlayerMoney < (int)DefenderPrice.Normal)
            {
                MessageBox.Show("No funds!");
                return;
            }

            int myNumberOfPicture = defenders.Count;
            defenders.Add(new Defender("Fusilier " + (DefenderName)gameRandom.Next(25), 40, defendersPictures[myNumberOfPicture], 30,0));
            player.PayFor((int)DefenderPrice.Normal);
        }
        public void CreateEliteDefender()
        {
            if (defenders.Count >= 12)
            {
                MessageBox.Show("Troopers limit reached!");
                return;
            }
            if (player.PlayerMoney < (int)DefenderPrice.Elite)
            {
                MessageBox.Show("No funds!");
                return;
            }

            int myNumberOfPicture = defenders.Count;
            defenders.Add(new EliteDefender("Grenadier " + (DefenderName)gameRandom.Next(25), 50, defendersPictures[myNumberOfPicture], 50,0));
            player.PayFor((int)DefenderPrice.Elite);
        }
        public void CreateGeneralDefender()
        {
            if (defenders.Count >= 12)
            {
                MessageBox.Show("Troopers limit reached!");
                return;
            }
            if (player.PlayerMoney < (int)DefenderPrice.General)
            {
                MessageBox.Show("No funds!");
                return;
            }
            if (defenders.Count == 0)
                defenders.Add(new GeneralDefender("General " + (DefenderName)gameRandom.Next(25), 50, defendersPictures[defenders.Count], 60,0,70));
            else
            {
                if (defenders[0] is IAmGeneralDefender)
                    return;
                Defender defenderToSave = defenders[0];
                defenders[0] = new GeneralDefender("General " + (DefenderName)gameRandom.Next(25), 50, defendersPictures[0], 60,0,60);
                defenderToSave.SelectNewPictureForMe(defendersPictures[defenders.Count]);
                defenders.Add(defenderToSave);
            }
            player.PayFor((int)DefenderPrice.General);
        }
        public void CreateCannonDefender(Image myImage)
        {
            if (myCannon != null)
            {
                MessageBox.Show("Cannons limit reached!");
                return;
            }
            if (player.PlayerMoney < (int)DefenderPrice.Cannon)
            {
                MessageBox.Show("No funds!");
                return;
            }
            myCannon = new DefenderCannon("Cannon", 50, 50, myImage);
            player.PayFor((int)DefenderPrice.Cannon);
        }
        //changing defenders stats actions ----------------------------------------------------------
        public void PromoteDefenders(int selectedDefender)
        {
            if (defenders[selectedDefender].MyShootingSkill < 50/*50*/)
            {
                MessageBox.Show("50 Ballistic Skill Required!");
                return;
            }

            OperationsOnDefenders op = new OperationsOnDefenders();
            defenders = op.PromoteDefender(defenders, selectedDefender, defendersPictures);


            /*MessageBox.Show(defenders[selectedDefender].Name + " Promoted to Grenadier!");
            string defenderTrueName = defenders[selectedDefender].Name.Remove(0,9); //dlugosc nazwy strzelec
            int defenderTrueShootingSkill = defenders[selectedDefender].MyShootingSkill;
            int defenderTrueExperience = defenders[selectedDefender].ExperiencePoints;
            defenders.RemoveAt(selectedDefender);
            defenders.Add(new EliteDefender("Grenadier " + defenderTrueName, 50, defendersPictures[defenders.Count], defenderTrueShootingSkill));
            defenders[defenders.Count - 1].ChangeDefenderExperiencePoints(defenderTrueExperience);
            */
        }
        public void MoveDefenderToReserve(int defenderNumber)
        {
            if (ReserveDefenders.Count >= 12)
            {
                MessageBox.Show("Reserve troops limit reached!");
                return;
            }
            ReserveDefenders.Add(defenders[defenderNumber]);
            defenders.RemoveAt(defenderNumber);
        }
        public void MoveDefenderFromReserve(int defenderNumber)
        {
            if (defenders.Count == 12)
            {
                MessageBox.Show("Troopers limit reached!");
                return;
            }
            if (ReserveDefenders.Count == 0)
            {
                MessageBox.Show("No reserve warriors left");
                return;
            }
            defenders.Add(ReserveDefenders[defenderNumber]);
            ReserveDefenders.RemoveAt(defenderNumber);
        }
        public void TrainDefendersMorale()
        {
            if (defenders[0].ExperiencePoints < 2)
            {
                MessageBox.Show("2 Learn Points Required!");
                return;
            }
            IAmGeneralDefender temp = defenders[0] as IAmGeneralDefender;
            if (!temp.IncreaseDefenderMorale())
            {
                MessageBox.Show("Fully trained!");
                return;
            }
            defenders[0].ChangeDefenderExperiencePoints(-2);
            //MessageBox.Show(defenders[0].Name + " was trained! + 5 Morale!");
        }
        public void TrainDefenders(int selectedDefender)
        {
            if (defenders[selectedDefender].ExperiencePoints < 1)
            {
                MessageBox.Show("Learn Point Required!");
                return;
            }
            if (defenders[selectedDefender].MyShootingSkill == 100)
            {
                MessageBox.Show("Fully trained!");
                return;
            }
            defenders[selectedDefender].IncreaseDefenderShootinSkill();
            defenders[selectedDefender].ChangeDefenderExperiencePoints(-1);
            //MessageBox.Show(defenders[selectedDefender].Name + " was trained! + 5 Ballistic");
        }
        public void TrainCannon()
        {
            if (myCannon.ExperiencePoints < 3)
            {
                MessageBox.Show("3 Learn Points Required!");
                return;
            }
            if (myCannon.MyShootingSkill == 100)
            {
                MessageBox.Show("Fully trained!");
                return;
            }
            myCannon.TrainCannonBS();
            myCannon.ChangeDefenderCannonExperiencePoints(-3);
            //MessageBox.Show(myCannon.Name + " was trained! + 5 Ballistic");
        }
        public void RepairCannon()
        {
            if (myCannon.HealthPoints == 50)
            {
                MessageBox.Show("Cannon is fully repaired!");
                return;
            }
            if (player.PlayerMoney < 50)
            {
                MessageBox.Show("No funds!");
                return;
            }
            player.PayFor(50);
            myCannon.RepairCannonHP();
        }
        public void RemoveCannon()
        {
            myCannon = null;
        }
        public void HealAllDefenders()
        {
            short countHealed = 0;
            foreach (Defender defender in defenders)
            {
                if (defender.HealthPoints < 40)
                {
                    if (GetPlayerMoney() - 10 < 0)
                        continue;
                    player.PayFor(10);
                    countHealed++;                   
                }
                defender.RestoreDefenderHealthPoints(40);
                if (defender is IAmEliteDefender)
                    defender.RestoreDefenderHealthPoints(50);               
            }
            if (countHealed > 0)
                MessageBox.Show("Paid " + countHealed * 10 + " for warband treatment!");
        }
        public string ShowDefenderStats(int selectedDefender)
        {
            string extraMoraleInfo = string.Empty;
            if (defenders[selectedDefender] is IAmGeneralDefender)
            {
                GeneralDefender temporary = defenders[selectedDefender] as GeneralDefender;
                extraMoraleInfo = "\nWarband Morale: " + temporary.GeneralWarbandMorale;
            }
            return "Hp: " + defenders[selectedDefender].HealthPoints
                + "\nBs: " + defenders[selectedDefender].MyShootingSkill
                + "\nEx: " + defenders[selectedDefender].ExperiencePoints
                + extraMoraleInfo;
        }
        public string ShowCannonInfo()
        {
            if (myCannon == null)
                return "No Cannon";
            return myCannon.Name + "\nHp: " + myCannon.HealthPoints
                + "\nBs: " + myCannon.MyShootingSkill
                + "\nEx: " + myCannon.ExperiencePoints;
        }
       
        
        //----------------------------------- loading and saving game
        public void PlayerSetMoneyAfterLoad(int loadedAmount)
        {
            player.SetMoneyAfterLoad(loadedAmount);
        }
        public void GameSetLoadedCannon(DefendersCannonSaveClass loadedCannon, Image cannonPicture)
        {
            myCannon = null;
            if (loadedCannon != null)
                myCannon = new DefenderCannon(loadedCannon.Name, loadedCannon.HealthPoints, loadedCannon.MyShootingSkill, loadedCannon.ExperiencePoints, cannonPicture);
        }
        public void GameSetLoadedDefenders(List<DefendersArmySaveClass> defendersToLoad)
        {
            defenders.Clear();
            foreach (DefendersArmySaveClass defender in defendersToLoad)
            {
                if (defender.DefenderType == 'G')
                    defenders.Add(new GeneralDefender(defender.Name, defender.HealthPoints, defendersPictures[defenders.Count], defender.MyShootingSkill, defender.ExperiencePoints, defender.GeneralMoraleSkill));
                else if (defender.DefenderType == 'E')
                    defenders.Add(new EliteDefender(defender.Name, defender.HealthPoints, defendersPictures[defenders.Count], defender.MyShootingSkill, defender.ExperiencePoints));
                else if (defender.DefenderType == 'R')
                    defenders.Add(new RecruitDefender(defender.Name, defender.HealthPoints, defendersPictures[defenders.Count], defender.MyShootingSkill, defender.ExperiencePoints));
                else 
                    defenders.Add(new Defender(defender.Name, defender.HealthPoints, defendersPictures[defenders.Count], defender.MyShootingSkill, defender.ExperiencePoints));

            }
        }
        public void GameSetLoadedReserveDefenders(List<DefendersArmySaveClass> RdefendersToLoad)
        {
            ReserveDefenders.Clear();
            foreach (DefendersArmySaveClass defender in RdefendersToLoad)
            {
                if (defender.DefenderType == 'G')
                    ReserveDefenders.Add(new GeneralDefender(defender.Name, defender.HealthPoints, defendersPictures[12], defender.MyShootingSkill, defender.ExperiencePoints, defender.GeneralMoraleSkill));
                else if (defender.DefenderType == 'E')
                    ReserveDefenders.Add(new EliteDefender(defender.Name, defender.HealthPoints, defendersPictures[12], defender.MyShootingSkill, defender.ExperiencePoints));
                else if (defender.DefenderType == 'R')
                    ReserveDefenders.Add(new RecruitDefender(defender.Name, defender.HealthPoints, defendersPictures[12], defender.MyShootingSkill, defender.ExperiencePoints));
                else
                    ReserveDefenders.Add(new Defender(defender.Name, defender.HealthPoints, defendersPictures[12], defender.MyShootingSkill, defender.ExperiencePoints));//defendersPictures[defenders.Count]

            }
        }
        public void SetObjectsAfterSaveLoad(SaveGame loadedSave, Image cannonPicture)
        {
            GameSetLoadedCannon(loadedSave.cannonToSave, cannonPicture);
            GameSetLoadedDefenders(loadedSave.defendersToSave);
            GameSetLoadedReserveDefenders(loadedSave.reserveDefendersToSave);
            PlayerSetMoneyAfterLoad(loadedSave.moneyToSave);
            worldMap.SetNewLoadedMyLocations(loadedSave.MyLocationsToSave);
        }
        public SaveGame SaveDefendersInfo()
        {
            SaveGame finalSave = new SaveGame();
            if (ReserveDefenders != null)
            {
                List<DefendersArmySaveClass> reserveDefendersToSave = new List<DefendersArmySaveClass>();
                foreach (Defender defender in ReserveDefenders)
                {
                    if (defender is IAmGeneralDefender)
                    {
                        GeneralDefender temp = defender as GeneralDefender;
                        reserveDefendersToSave.Add(new DefendersArmySaveClass(temp.Name, temp.HealthPoints, temp.MyShootingSkill, temp.ExperiencePoints, 'G', temp.GeneralWarbandMorale));
                    }
                    else if (defender is IAmRecruitDefender)
                        reserveDefendersToSave.Add(new DefendersArmySaveClass(defender.Name, defender.HealthPoints, defender.MyShootingSkill, defender.ExperiencePoints, 'R', 0));
                    else if (defender is IAmEliteDefender)
                        reserveDefendersToSave.Add(new DefendersArmySaveClass(defender.Name, defender.HealthPoints, defender.MyShootingSkill, defender.ExperiencePoints, 'E', 0));
                    else
                        reserveDefendersToSave.Add(new DefendersArmySaveClass(defender.Name, defender.HealthPoints, defender.MyShootingSkill, defender.ExperiencePoints, 'N', 0));
                }
                finalSave.reserveDefendersToSave = reserveDefendersToSave;
            }
            if (myCannon != null)
            {
                //MessageBox.Show("hej");
                DefendersCannonSaveClass cannonSave = new DefendersCannonSaveClass(myCannon.Name, myCannon.HealthPoints, myCannon.MyShootingSkill, myCannon.ExperiencePoints);
                finalSave.cannonToSave = cannonSave;
            }
            if (defenders != null)
            {
                List<DefendersArmySaveClass> defendersToSave = new List<DefendersArmySaveClass>();
                foreach (Defender defender in defenders)
                {
                    if (defender is IAmGeneralDefender)
                    {
                        GeneralDefender temp = defender as GeneralDefender;
                        defendersToSave.Add(new DefendersArmySaveClass(temp.Name, temp.HealthPoints, temp.MyShootingSkill,temp.ExperiencePoints, 'G', temp.GeneralWarbandMorale));
                    }
                    else if (defender is IAmRecruitDefender)
                        defendersToSave.Add(new DefendersArmySaveClass(defender.Name, defender.HealthPoints, defender.MyShootingSkill, defender.ExperiencePoints, 'R', 0));
                    else if (defender is IAmEliteDefender)
                        defendersToSave.Add(new DefendersArmySaveClass(defender.Name, defender.HealthPoints, defender.MyShootingSkill, defender.ExperiencePoints, 'E', 0));
                    else
                        defendersToSave.Add(new DefendersArmySaveClass(defender.Name, defender.HealthPoints, defender.MyShootingSkill, defender.ExperiencePoints, 'N', 0));
                }
                finalSave.defendersToSave = defendersToSave;
            }
            finalSave.moneyToSave = GetPlayerMoney();
            //zapis liczby zdobytych terenow z mapy gry
            finalSave.MyLocationsToSave = GetMyLocations();
            return finalSave;
        }
    }
}
