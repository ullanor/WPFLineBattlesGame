using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyGameLineBattles_WPF
{
    /// <summary> test dopasowywania do rozdzielczości ekranu
    /// Height="{Binding SystemParameters.PrimaryScreenHeight}" 
    ///   Width="{Binding SystemParameters.PrimaryScreenWidth}"Height="768" Width="1360"
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        //WorldMapClass worldMap;
        List<Image> enemiesPictures;
        List<Image> defendersPictures;
        //uri for different defenders color style test
        Uri blue = new Uri(@"Resources\hillyBackground.png", UriKind.Relative);
        Uri grey = new Uri(@"Resources\stonyBackground.png", UriKind.Relative);
        Brush defaultPlane;
        int countToTwo = 0;
        public MainWindow()
        {
            InitializeComponent();
            createObjects();
            //worldMap = new WorldMapClass();
            game = new Game(defendersPictures, enemiesPictures, firingCloudImage,
                justEffectCounterLabel,defendersLabel,firingButton);
            ChangeDefendersButtonsEnable(false);
            UpdatePlayerMoney();
            defaultPlane = backDefault.Background;
        }

        private void createObjects()
        {
            enemiesPictures = new List<Image>
            {
                enemy1Picture,
                enemy2Picture,
                enemy3Picture,
                enemy4Picture,
                enemy5Picture,
                enemy6Picture,
                enemy7Picture,
                enemy8Picture,
                enemy9Picture,
                enemy10Picture,
                enemy11Picture,
                enemy12Picture
            };
            //enemiesPictures[0] = enemy1Picture;
            defendersPictures = new List<Image>
            {
                warrior1Picture,
                warrior2Picture,
                warrior3Picture,
                warrior4Picture,
                warrior5Picture,
                warrior6Picture,
                warrior7Picture,
                warrior8Picture,
                warrior9Picture,
                warrior10Picture,
                warrior11Picture,
                warrior12Picture,
                new Image { } //13 dodatkowy dla wczytywania rezerwistow!
            };
            FormHideAllWarriors();            
        }
        private void UpdateReserveDefenders()
        {
            DefendersReserveList.Items.Clear();
            foreach (Defender reserveDefender in game.ReserveDefenders)
                DefendersReserveList.Items.Add(reserveDefender.Name+" HP: "+ reserveDefender.HealthPoints
                    +" BS: "+reserveDefender.MyShootingSkill +" EX: "+reserveDefender.ExperiencePoints);
            DefendersReserveList.SelectedIndex = 0;
            if (DefendersReserveList.Items.Count == 0)
            {
                DefendersReserveList.IsEnabled = false;
                RemoveReserver.Visibility = Visibility.Collapsed;
                MoveToReserve.Content = "Do rezerwy";
            }
            else
                DefendersReserveList.IsEnabled = true;
        }
        private void UpdatePlayerMoney()
        {
            PlayerMoney.Content = game.UpdatePlayerMoneyAndDefendersCount();
            DefendersList.Items.Clear();
            foreach (Defender defender in game.defenders)
                DefendersList.Items.Add(defender.Name);
            DefendersList.SelectedIndex = 0;
            if (game.myCannon != null)
            {
                cannonInfoLabel.Content = game.ShowCannonInfo();
                ChangeCannonButtonsEnable(true);
            }
            else
            {
                cannonInfoLabel.Content = "No Cannon";
                ChangeCannonButtonsEnable(false);
            }
            if (game.defenders.Count != 0)
                ChangeDefendersButtonsEnable(true);
            else
                ChangeDefendersButtonsEnable(false);
            if (game.defenders.Count != 0)
                CheckDefenderRank();
        }

        private void FormHideAllWarriors()
        {
            warrior1Picture.Visibility = Visibility.Collapsed;
            warrior2Picture.Visibility = Visibility.Collapsed;
            warrior3Picture.Visibility = Visibility.Collapsed;
            warrior4Picture.Visibility = Visibility.Collapsed;
            warrior5Picture.Visibility = Visibility.Collapsed;
            warrior6Picture.Visibility = Visibility.Collapsed;
            warrior7Picture.Visibility = Visibility.Collapsed;
            warrior8Picture.Visibility = Visibility.Collapsed;
            warrior9Picture.Visibility = Visibility.Collapsed;
            warrior10Picture.Visibility = Visibility.Collapsed;
            warrior11Picture.Visibility = Visibility.Collapsed;
            warrior12Picture.Visibility = Visibility.Collapsed;

            cannonPicture.Visibility = Visibility.Collapsed;
            cannonFireButton.Visibility = Visibility.Collapsed;

            enemy1Picture.Visibility = Visibility.Collapsed;
            enemy2Picture.Visibility = Visibility.Collapsed;
            enemy3Picture.Visibility = Visibility.Collapsed;
            enemy4Picture.Visibility = Visibility.Collapsed;
            enemy5Picture.Visibility = Visibility.Collapsed;
            enemy6Picture.Visibility = Visibility.Collapsed;
            enemy7Picture.Visibility = Visibility.Collapsed;
            enemy8Picture.Visibility = Visibility.Collapsed;
            enemy9Picture.Visibility = Visibility.Collapsed;
            enemy10Picture.Visibility = Visibility.Collapsed;
            enemy11Picture.Visibility = Visibility.Collapsed;
            enemy12Picture.Visibility = Visibility.Collapsed;

            enemyCannonPicture.Visibility = Visibility.Collapsed;

            justEffectCounterLabel.Visibility = Visibility.Collapsed;
            defendersLabel.Visibility = Visibility.Collapsed;

            firingCloudImage.Visibility = Visibility.Collapsed;
            firingButton.Visibility = Visibility.Collapsed;

            endBattleRetreat.Visibility = Visibility.Collapsed;
            //test
            startBattleButton.IsEnabled = false;
            worldMapButton.IsEnabled = true;
            DefendersReserveList.IsEnabled = false;
            RemoveReserver.Visibility = Visibility.Collapsed;
        }
        //--------------------------------------------------- START BATTLE SETTINGS ----------------
        private void CheckWhatCannonIsFighting()
        {
            if (game.generateEnemies.BattleDifficultyLevel == (DifficultyLevel)2 || game.generateEnemies.BattleDifficultyLevel == (DifficultyLevel)3)
            {
                game.enemyCannon = new EnemyCannon(enemyCannonPicture);
                if (game.OrcsAreEnemy)
                {
                    enemyCannonPicture.Source = new BitmapImage(new Uri(@"Resources\OrcEnemyCannon.png", UriKind.Relative));
                    game.enemyCannon.ChangeEnemyCannonStats("Warboss Cannon", 50, 60);
                }
                else if (game.DemonsAreEnemy)
                {
                    enemyCannonPicture.Source = new BitmapImage(new Uri(@"Resources\scaryDemon.png", UriKind.Relative));
                    game.enemyCannon.ChangeEnemyCannonStats("Demon", 90, 80);
                }
                else
                    enemyCannonPicture.Source = new BitmapImage(new Uri(@"Resources\RedEnemyCannon.png", UriKind.Relative));
            }
        }
        private void startBattleButton_Click(object sender, RoutedEventArgs e)
        {
            if (game.defenders.Count < 1)
            {
                MessageBox.Show("You have no warriors to fight!");
                return;
            }

            GameMenuBox.IsEnabled = false;
            saveGameButton.IsEnabled = false;
            loadGameButton.IsEnabled = false;
            
            firingButton.Visibility = Visibility.Visible;
            defendersLabel.Visibility = Visibility.Visible;
            justEffectCounterLabel.Visibility = Visibility.Visible;
            endBattleRetreat.Visibility = Visibility.Visible;

            background.Source = new BitmapImage(blue);

            firingButton.IsEnabled = true;
            game.CreateEnemy();
            CheckWhatCannonIsFighting();
            if (game.enemyCannon != null)
                enemyCannonPicture.Visibility = Visibility.Visible;
            if (game.myCannon != null)
            {
                cannonPicture.Visibility = Visibility.Visible;
                cannonFireButton.Visibility = Visibility.Visible;
            }
            game.UpdateEnemyAndDefendersData();
        }
        //-------------------------------------- SHOOTING BUTTON CLICK EFFECTS ---------------------
        private void firingButton_Click(object sender, RoutedEventArgs e)
        {
            cannonFireButton.IsEnabled = true;
            int WhoShootFirst = game.gameRandom.Next(2);
            if (game.enemyCannon != null && game.EnemyCannonCanShoot()
                && countToTwo % 2== 0  && (string)firingButton.Content != "Victory!")
            {
                game.BattleReport = "ENEMY QUICKLY SHOOT FIRST!";
                if (game.EnemyCannonIsShooting())
                {
                    game.WeLostTheBattle();
                    UpdateBelligerentsData();
                    return;
                }
                game.EnemySetCannonShoot(false);
                firingButton.IsEnabled = false;
                return;
            }
            if (game.enemiesWarbandMorale == 70 && WhoShootFirst == 1)
            {
                game.BattleReport = "ENEMY QUICKLY SHOOT FIRST!";
                if (game.EnemyIsShooting())
                {
                    game.WeLostTheBattle();
                    UpdateBelligerentsData();
                    return;
                }                                 
                game.enemiesWarbandMorale = 75;
                firingButton.IsEnabled = false;
                return;
            }

            firingButton.IsEnabled = false;
            if (game.PlayerIsShooting())
            {
                game.WeWonTheBattle();
                endBattleRetreat.Content = "Victorious return";
            }

        }

        private void UpdateBelligerentsData()
        {
            countToTwo++;
            belligerentsData.Content = game.BattleReport;
            if (game.myCannon == null && game.enemyCannon != null)
            {               
                game.EnemySetCannonShoot(true);
            }
        }
        private void BuyRecruitDefenderButton_Click(object sender, RoutedEventArgs e)
        {
            game.CreateRecruitDefender();
            UpdatePlayerMoney();
            DefendersList.SelectedIndex = game.defenders.Count - 1;           
        }
        private void BuyDefenderButton_Click(object sender, RoutedEventArgs e)
        {
            game.CreateDefender();
            UpdatePlayerMoney();
            DefendersList.SelectedIndex = game.defenders.Count-1;
        }

        private void BuyEliteDefenderButton_Click(object sender, RoutedEventArgs e)
        {
            game.CreateEliteDefender();
            UpdatePlayerMoney();
            DefendersList.SelectedIndex = game.defenders.Count-1;
        }

        private void BuyGeneralDefenderButton_Click(object sender, RoutedEventArgs e)
        {
            game.CreateGeneralDefender();
            UpdatePlayerMoney();
            DefendersList.SelectedIndex = game.defenders.Count - 1;
        }

        private void BuyCannonDefenderButton_Click(object sender, RoutedEventArgs e)
        {
            game.CreateCannonDefender(cannonPicture);
            UpdatePlayerMoney();
            cannonInfoLabel.Content = game.ShowCannonInfo();
        }
        //end battle return CLICK -----------------------------------------------
        private void endBattleRetreat_Click(object sender, RoutedEventArgs e)
        {
            GameMenuBox.IsEnabled = true;
            saveGameButton.IsEnabled = true;
            if ((string)endBattleRetreat.Content == "Retreat! (-100g)")
            {               
                if (game.GetMyLocations().Count == 0)
                {
                    MessageBox.Show("Your kingdom has no territory! Game Over!");
                    GameMenuBox.IsEnabled = false;
                    saveGameButton.IsEnabled = false;
                }
                game.BattleRetreatEffect();
                if (!game.PlayerMustPayMoney(100))
                {
                    MessageBox.Show("Your kingdom bankrupt! Game Over!");
                    GameMenuBox.IsEnabled = false;
                    saveGameButton.IsEnabled = false;
                }
            }
            game.enemyCannon = null;
            game.HealAllDefenders();
            //if (game.myCannon != null)
            //game.SetCannonShoot(true);
            //countToTwo = 0;
            countToTwo = 0;
            endBattleRetreat.Content = "Retreat! (-100g)";
            FormHideAllWarriors();
            loadGameButton.IsEnabled = true;
            //saveGameButton.IsEnabled = true;
            background.Source = new BitmapImage(grey);
            belligerentsData.Content = "";
            UpdatePlayerMoney();
            UpdateReserveDefenders();
        }

        private void firingButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (firingButton.IsEnabled == false)
            {
                endBattleRetreat.IsEnabled = false;
                cannonFireButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                endBattleRetreat.IsEnabled = true;
                if ((string)firingButton.Content == "Victory!")
                {
                    if(game.WeWonTheBattle())
                        defendersBannerImage.Source = new BitmapImage(new Uri(@"Resources\gameWinImg.png", UriKind.Relative));
                    firingButton.Content = "Fire !";
                    cannonFireButton.Visibility = Visibility.Collapsed;
                    firingButton.Visibility = Visibility.Collapsed;
                    endBattleRetreat.Content = "Victorious return";
                }
                if (game.myCannon != null && firingButton.Visibility == Visibility.Visible)
                    cannonFireButton.Visibility = Visibility.Visible;
                UpdateBelligerentsData();
            }
                
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void updateDefenderStatsLabel(object sender, SelectionChangedEventArgs e)
        {
            if (game.defenders.Count == 0)
            {
                DefenderStatsLabel.Content = "no data";
                return;
            }
            if (DefendersList.SelectedIndex < 0)
                return;
            DefenderStatsLabel.Content = game.ShowDefenderStats(DefendersList.SelectedIndex);
            CheckDefenderRank();
           // MoveToReserve.Content = "Do rezerwy";
        }
        //------------------- defenders changing buttons----------------------
        private void CheckDefenderRank()
        {
            if (game.defenders[DefendersList.SelectedIndex] is IAmEliteDefender)
                PromoteDefenders.IsEnabled = false;
            else
                PromoteDefenders.IsEnabled = true;
            if (game.defenders[DefendersList.SelectedIndex] is IAmGeneralDefender)
                TrainDefendersMorale.IsEnabled = true;
            else
                TrainDefendersMorale.IsEnabled = false;
        }
        //--RESERVE
        private void MoveToReserve_Click(object sender, RoutedEventArgs e)
        {
            if ((string)MoveToReserve.Content == "Reinstate")
            {
                game.MoveDefenderFromReserve(DefendersReserveList.SelectedIndex);               
            }
            else
            {
                DefendersReserveList.IsEnabled = true;
                game.MoveDefenderToReserve(DefendersList.SelectedIndex);
            }
            UpdatePlayerMoney();
            UpdateReserveDefenders();
        }
        private void ReserveListGotFocus(object sender, RoutedEventArgs e)
        {
            MoveToReserve.Content = "Reinstate";
            MoveToReserve.IsEnabled = true;
            RemoveReserver.Visibility = Visibility.Visible;
        }

        private void DefendersListGotFocus(object sender, RoutedEventArgs e)
        {
            MoveToReserve.Content = "To reserve";
            RemoveReserver.Visibility = Visibility.Collapsed;
        }
        private void RemoveReserver_Click(object sender, RoutedEventArgs e)
        {
            game.ReserveDefenders.RemoveAt(DefendersReserveList.SelectedIndex);
            UpdateReserveDefenders();
            UpdatePlayerMoney();
        }
        //---
        private void TrainDefenders_Click(object sender, RoutedEventArgs e)
        {
            int defenderNumber = DefendersList.SelectedIndex;
            game.TrainDefenders(defenderNumber);
            UpdatePlayerMoney();
            DefendersList.SelectedIndex = defenderNumber;
        }
        private void TrainDefendersMorale_Click(object sender, RoutedEventArgs e)
        {
            game.TrainDefendersMorale();
            UpdatePlayerMoney();
        }
        private void RemoveDefenders_Click(object sender, RoutedEventArgs e)
        {
            game.defenders.RemoveAt(DefendersList.SelectedIndex);
            UpdatePlayerMoney();           
        }
        private void PromoteDefenders_Click(object sender, RoutedEventArgs e)
        {
            int defenderNumber = DefendersList.SelectedIndex;
            game.PromoteDefenders(defenderNumber);
            UpdatePlayerMoney();
        }
        private void TrainCannon_Click(object sender, RoutedEventArgs e)
        {
            game.TrainCannon();
            UpdatePlayerMoney();
        }

        private void RepairCannon_Click(object sender, RoutedEventArgs e)
        {
            game.RepairCannon();
            UpdatePlayerMoney();
        }
        private void DeleteCannon_Click(object sender, RoutedEventArgs e)
        {
            game.RemoveCannon();
            UpdatePlayerMoney();
        }
        private void ChangeDefendersButtonsEnable(bool isEnabled)
        {
            if(isEnabled)
            {
                PromoteDefenders.IsEnabled = true;
                RemoveDefenders.IsEnabled = true;
                TrainDefenders.IsEnabled = true;
                TrainDefendersMorale.IsEnabled = true;
                MoveToReserve.IsEnabled = true;
            }
            else
            {
                PromoteDefenders.IsEnabled = false;
                RemoveDefenders.IsEnabled = false;
                TrainDefenders.IsEnabled = false;
                TrainDefendersMorale.IsEnabled = false;
                MoveToReserve.IsEnabled = false;
            }
        }
        private void ChangeCannonButtonsEnable(bool isEnabled)
        {
            if (isEnabled)
            {
                TrainCannon.IsEnabled = true;
                RepairCannon.IsEnabled = true;
                DeleteCannon.IsEnabled = true;
            }
            else
            {
                TrainCannon.IsEnabled = false;
                RepairCannon.IsEnabled = false;
                DeleteCannon.IsEnabled = false;
            }
        }
        //-------------------------- operate cannon ---------------------------------
        private void cannonFireButton_Click(object sender, RoutedEventArgs e)
        {
            if (game.myCannon == null)
            {
                MessageBox.Show("Działo zostało zniszczone!");
                cannonFireButton.Visibility = Visibility.Collapsed;
                return;
            }
            if (firingButton.Visibility == Visibility.Collapsed 
                || (string)firingButton.Content == "Victory!")
            {
                MessageBox.Show("Battle is finished!");
                cannonFireButton.Visibility = Visibility.Collapsed;
                return;
            }
            cannonFireButton.IsEnabled = false;
            firingButton.IsEnabled = false;
            if (game.PlayerCannonIsShooting())
            {
                game.WeWonTheBattle();
                endBattleRetreat.Content = "Victorious return";
            }
        }
        private void moveCannonClick(object sender, MouseButtonEventArgs e)
        {
            RotateTransform cannonPositionUp = new RotateTransform(-15);
            RotateTransform cannonPositionMiddle = new RotateTransform(-7.5);
            RotateTransform cannonPositionDown = new RotateTransform(0);
            int cannonPosition = game.GetCannonPosition();
            if (cannonPosition == 2)
                cannonPicture.RenderTransform = cannonPositionUp;
            else if (cannonPosition == 0)
                cannonPicture.RenderTransform = cannonPositionMiddle;
            else
                cannonPicture.RenderTransform = cannonPositionDown;
            game.ChangeCannonPosition();
        }
        // --------------- SAVE and load operations ----------------------------------------------
        private string SaveGameProperly(string path)
        {
            DateTime nowToSave = DateTime.Now;
            string[] files = Directory.GetFiles(path);           
            short saveAmount = (short)files.Length;           
            int lowestNumber = files.Length;
            if (saveAmount > 8)
            {
                int choosed = lowestNumber-1;
                for (int i =0;i<files.Length;i++)
                {
                    files[i] = files[i].Remove(files[i].Length - 5);
                    int lastNumber = Convert.ToInt32(files[i].Remove(0, files[i].Length - 1));
                    if (lastNumber < lowestNumber)
                    {
                        lowestNumber = lastNumber;
                        choosed = i;
                    }
                }
                //MessageBox.Show(files[choosed] + ".save");
                File.Delete(files[choosed] + ".save");
                MessageBox.Show("There are more than 8 game saves\nThis save will override the save with lowest No.!");
            }
            path += @"\" + nowToSave.ToString("dddd_dd/MM/yyyy__HH/mm/ss") + " No "+  lowestNumber + ".save";
            return path;
        }
        private void saveGameButton_Click(object sender, RoutedEventArgs e)
        {
            SaveGame save = game.SaveDefendersInfo();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path += @"\GeldoniaLBSaves";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = SaveGameProperly(path);
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream output = File.OpenWrite(path))
            {
                try
                { formatter.Serialize(output, save); }
                catch(System.Runtime.Serialization.SerializationException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            MessageBox.Show("Game successfully saved!");
        }

        private void loadGameButton_Click(object sender, RoutedEventArgs e)
        {            
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path += @"\GeldoniaLBSaves";
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Game was not saved yet!");
                return;
            }
            // -- test
            Microsoft.Win32.OpenFileDialog openSaveDialog = new Microsoft.Win32.OpenFileDialog
            {
                InitialDirectory = path,
                Filter = "Game save files (*.save)|*.save",
            };
            var result = openSaveDialog.ShowDialog();
            if (result != true)
                return;
            path = openSaveDialog.FileName;          
            SaveGame savetoLoad;
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream input = File.OpenRead(path))
            {
                savetoLoad = (SaveGame)formatter.Deserialize(input);
            }
            game.SetObjectsAfterSaveLoad(savetoLoad,cannonPicture);
            cannonInfoLabel.Content = game.ShowCannonInfo();
            UpdatePlayerMoney();
            UpdateReserveDefenders();
            startBattleButton.IsEnabled = false;
            worldMapButton.IsEnabled = true;
            GameMenuBox.IsEnabled = true;
            MessageBox.Show("Game Loaded Successfully!");
            saveGameButton.IsEnabled = true;
        }
        //operate world map ----------------------------------------------------
        private void worldMapButton_Click(object sender, RoutedEventArgs e)
        {
            WorldMapWindow worldMapWindow = new WorldMapWindow(game.GetMyLocations(),game.worldMap);
            worldMapWindow.ShowDialog();
            short targetLocation = (short)game.worldMap.targetLocation;
            if (targetLocation == 66)
                return;
            if (game.EnemyIsAttackingMyLocation())
            {
                worldMapButton.IsEnabled = false;
                startBattleButton.Content = "Defend " + (LocationName)game.worldMap.targetLocation;
            }
            else
                startBattleButton.Content = "Start Battle of " + (LocationName)targetLocation;
            startBattleButton.IsEnabled = true;
            SetTargetLocationBackground((short)game.worldMap.targetLocation);
            game.IsThisOrksLocation();
        }
        private void SetTargetLocationBackground(short loc)
        {
            if (loc >= 8 && loc <= 10) // desert
            {
                LinearGradientBrush myLinearGradientBrush =
                new LinearGradientBrush();
                myLinearGradientBrush.StartPoint = new Point(0.5, 0);
                myLinearGradientBrush.EndPoint = new Point(0.5, 1);

                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(0, 151, 255), 0.0));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(34, 136, 106), 0.127));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(151,151,10), 1));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(181, 181, 103), 0.149));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(151, 151, 10), 0.284));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(182, 182, 103), 0.749));


                backDefault.Background = myLinearGradientBrush;
            }
            else if (loc >= 4 && loc <= 7) //mountains
            {
                LinearGradientBrush myLinearGradientBrush =
                new LinearGradientBrush();
                myLinearGradientBrush.StartPoint = new Point(0.5, 0);
                myLinearGradientBrush.EndPoint = new Point(0.5, 1);

                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(0, 151, 255), 0.0));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(34, 136, 106), 0.127));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(100, 100, 100), 1));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(128, 155, 69), 0.149));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(102, 114, 50), 0.284));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(151, 151, 151), 0.749));

                backDefault.Background = myLinearGradientBrush;
            }
            else if (loc == 11) //hell
            {
                LinearGradientBrush myLinearGradientBrush =
                new LinearGradientBrush();
                myLinearGradientBrush.StartPoint = new Point(0.5, 0);
                myLinearGradientBrush.EndPoint = new Point(0.5, 1);

                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(0, 0, 0), 0.0));//255/31/31
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(0, 0, 0), 0.127));//77/15/15
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(0, 0, 0), 1));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(191, 69, 0), 0.149));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(205, 129, 19), 0.284));
                myLinearGradientBrush.GradientStops.Add(
                    new GradientStop(Color.FromRgb(141, 34, 23), 0.749));

                backDefault.Background = myLinearGradientBrush;
            }
            else
                backDefault.Background = defaultPlane;
        }
        // ------------------------------------------------- MUZYKA -----------------
        private void MusicFuncButton_Click(object sender, RoutedEventArgs e)
        {
            game.SetMusicOnOf();
        }
    }
}