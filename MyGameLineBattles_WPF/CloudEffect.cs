using System;
using System.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MyGameLineBattles_WPF
{
    public class CloudEffect
    {
        protected Game game;
        private DispatcherTimer effectTimer;
        private int firingCloudTimeCounter = 0;
        private bool enemyIsShooting;
        private int cloudLong;
        protected bool OrksAreTrueEnemy;
    
       
        private int setTheSizeOfCloud(int amount,bool isItEnemy)
        {
            if (isItEnemy)
            {
                if (amount == 1)
                    return 850;
                if (amount == 2)
                    return 800;
                if (amount == 3)
                    return 750;//250 150

                return 0;
            }
            else
            {
                if (amount == 1)
                    return 1450;
                if (amount == 2)
                    return 1400;
                if (amount == 3)
                    return 1350;//850 750

                return 0;
            }
        }
        public CloudEffect(bool enemyIsShooting, int amountOfShooters, Game game)
        {
            this.game = game;
            this.enemyIsShooting = enemyIsShooting;
            OrksAreTrueEnemy = game.OrcsAreEnemy;
            cloudLong = setTheSizeOfCloud(amountOfShooters,enemyIsShooting);
            SoundPlayer sndPlayer = new SoundPlayer(MyGameLineBattles_WPF.Properties.Resources.musket4);
            if (enemyIsShooting)
            {
                if (OrksAreTrueEnemy)
                {
                    sndPlayer = new SoundPlayer(MyGameLineBattles_WPF.Properties.Resources.bow_shoot_08);
                    sndPlayer.Play();
                    game.FiringCloudEffect.Source = new BitmapImage(new Uri(@"Resources\arrowCloud.png", UriKind.Relative));
                } 
                else
                    sndPlayer.Play();
                game.FiringCloudEffect.Margin = new System.Windows.Thickness(650, 350, cloudLong, 0);
            }
            else
            {
                sndPlayer.Play();
                game.FiringCloudEffect.Margin = new System.Windows.Thickness(34, 350, cloudLong, 0);
            }

            game.FiringCloudEffect.Visibility = Visibility.Visible;

            effectTimer = new System.Windows.Threading.DispatcherTimer();
            effectTimer.Tick += new EventHandler(effectTimer_Tick);
            effectTimer.Interval = new TimeSpan(0, 0, 0, 0, 400);//zmien aby przyspieszyc efekty strzelania
            effectTimer.Start();
        }

        private void effectTimer_Tick(object sender, EventArgs e)
        {

            if (firingCloudTimeCounter > 0 && firingCloudTimeCounter <= 2)
            {
                if (OrksAreTrueEnemy && enemyIsShooting)
                {
                    game.FiringCloudEffect.Margin = new System.Windows.Thickness(300, 350, cloudLong, 0);
                }
                else
                    game.FiringCloudEffect.Source = new BitmapImage(new Uri(@"Resources\firingCloudVanish.png", UriKind.Relative));
            }
            if (firingCloudTimeCounter == 3)
            {
                if (OrksAreTrueEnemy && enemyIsShooting)
                {
                    game.FiringCloudEffect.Margin = new System.Windows.Thickness(0, 350, cloudLong, 0);
                    game.DefendersGetHittedByArrow();
                }
                else
                    game.FiringCloudEffect.Source = new BitmapImage(new Uri(@"Resources\firingCloudVanished.png", UriKind.Relative));
            }
            if (firingCloudTimeCounter > 4 && firingCloudTimeCounter <= 5)
            {
                game.FiringCloudEffect.Visibility = Visibility.Collapsed;

               foreach (Enemy enemy in game.enemies)
                    enemy.RestoreDefaultImagePosition();

                foreach (Defender defender in game.defenders)
                    defender.RestoreDefaultImagePosition();

                if (game.myCannon != null)
                    game.myCannon.RestoreDefaultImagePosition();
                if (game.enemyCannon != null)
                    game.enemyCannon.RestoreDefaultImagePosition();

                game.UpdateDefendersLocalizations();
                if (game.gameIsOver)
                {
                    game.gameIsOver = false;
                }
                else
                    game.UpdateEnemyAndDefendersData();
            }
            if (firingCloudTimeCounter > 5)
            {
                effectTimer.Stop();
                firingCloudTimeCounter = 0;
                game.FiringCloudEffect.Source = new BitmapImage(new Uri(@"Resources\firingCloud.png", UriKind.Relative));


                if (!enemyIsShooting)
                {
                    if (game.enemies.Count <= game.enemiesMoraleBreakPoint)
                    {
                        if (game.enemies.Count == 0)
                        {
                            game.firingButton.IsEnabled = true;
                            return;
                        }
                        if (game.gameRandom.Next(1, 101) >= game.enemiesWarbandMorale)
                        {
                            game.justEffectCounterLabel.Content = "Enemy Morale Test Failed! \nDefenders Win!";
                            game.BattleReport += game.enemiesWarbandMorale + " Enemy morale test failed!";                           
                            game.firingButton.Content = "Victory!";
                            game.firingButton.IsEnabled = true;
                            return;
                        }
                    }
                    if (EnemyShootUsAll())
                    {
                        effectTimer.Stop();
                        game.WeLostTheBattle();
                        return;
                    }
                        

                }
                else
                {
                    if (game.defenders.Count <= game.defendersMoraleBreakPoint)
                        if (game.gameRandom.Next(1, 101) >= game.TrueDefendersWarbandMorale)
                        {
                            game.BattleReport += game.TrueDefendersWarbandMorale + " Our morale test failed!" + Environment.NewLine;
                            game.WeLostTheBattle();
                        }
                    game.firingButton.IsEnabled = true;

                }
            }
            firingCloudTimeCounter++;

        }
        public virtual bool EnemyShootUsAll()
        {
            return game.EnemyIsShooting();
        }
    }
}
