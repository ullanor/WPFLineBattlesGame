using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MyGameLineBattles_WPF
{
    public class CannonCloudEffect : CloudEffect
    {
        private bool DemonsAreTrueEnemy;
        public CannonCloudEffect(bool enemyIsShooting, int amountOfShooters, Game game) : base(enemyIsShooting, 99, game)
        {
            DemonsAreTrueEnemy = game.DemonsAreEnemy;
            if (enemyIsShooting)
            {
                if (DemonsAreTrueEnemy)
                {
                    DemonsAreTrueEnemy = false;
                    //dzwiek wystrzału demona ;)
                    SoundPlayer sndPlayer = new SoundPlayer(MyGameLineBattles_WPF.Properties.Resources.demon_die_2);
                    sndPlayer.Play();

                }
                if (base.OrksAreTrueEnemy == true)
                {
                    OrksAreTrueEnemy = false;
                    //dzwiek wystrzału działa orków
                    SoundPlayer sndPlayer = new SoundPlayer(MyGameLineBattles_WPF.Properties.Resources.musket4);
                    sndPlayer.Play();
                    game.FiringCloudEffect.Source = new BitmapImage(new Uri(@"Resources\firingCloud.png", UriKind.Relative));
                }
                game.FiringCloudEffect.Margin = new System.Windows.Thickness(678, 585, 0, 0);
            }
            else
                game.FiringCloudEffect.Margin = new System.Windows.Thickness(35, 585, 0, 0); 
        }
        public override bool EnemyShootUsAll()
        {
            if (game.enemyCannon != null)
            {
                //game.EnemySetCannonShoot(false);
                return game.EnemyCannonIsShooting();
            }
            else
            {              
                game.firingButton.IsEnabled = true;
                return false;
            }
        }
    }
}
