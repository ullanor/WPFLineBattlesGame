using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyGameLineBattles_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy WorldMapWindow.xaml
    /// </summary>
    public partial class WorldMapWindow : Window
    {
        WorldMapClass worldMap;
        List<Image> worldLocations;
        Uri playerColor = new Uri(@"Resources\blueFlagWPF.png", UriKind.Relative);
        //Uri enemyColor = new Uri(@"Resources\ULTRAicon.ico", UriKind.Relative);
        public WorldMapWindow(List<int> myLocations,WorldMapClass worldMap)
        {
            InitializeComponent();
            this.worldMap = worldMap;
            CollectWorldLocations();
            SetWorldLocations(myLocations);
        }

        private void playerSelectBattleLocation1(object sender, MouseButtonEventArgs e)
        {
            confirmBattleLocation(maplocation1,0);
        }

        private void playerSelectBattleLocation2(object sender, MouseButtonEventArgs e)
        {
            confirmBattleLocation(maplocation2,1);
        }

        private void playerSelectBattleLocation3(object sender, MouseButtonEventArgs e)
        {
            if (!worldMap.MyLocations.Contains(1))
            {
                MessageBox.Show("You have to conquer nearby location!");
                return;
            }
            confirmBattleLocation(maplocation3,2);
        }
        private void playerSelectBattleLocation4(object sender, MouseButtonEventArgs e)
        {
            if (!worldMap.MyLocations.Contains(2))
            {
                MessageBox.Show("You have to conquer nearby location!");
                return;
            }
            confirmBattleLocation(maplocation4, 3);
        }
        private void playerSelectBattleLocation5(object sender, MouseButtonEventArgs e)
        {
            if (!worldMap.MyLocations.Contains(1))
            {
                MessageBox.Show("You have to conquer nearby location!");
                return;
            }
            confirmBattleLocation(maplocation5, 4);
        }
        private void playerSelectBattleLocation6(object sender, MouseButtonEventArgs e)
        {
            if (!worldMap.MyLocations.Contains(4))
            {
                MessageBox.Show("You have to conquer nearby location!");
                return;
            }
            confirmBattleLocation(maplocation6, 5);
        }

        private void playerSelectBattleLocation7(object sender, MouseButtonEventArgs e)
        {
            if (!worldMap.MyLocations.Contains(5))
            {
                MessageBox.Show("You have to conquer nearby location!");
                return;
            }
            confirmBattleLocation(maplocation7, 6);
        }

        private void playerSelectBattleLocation8(object sender, MouseButtonEventArgs e)
        {
            if (!worldMap.MyLocations.Contains(6))
            {
                MessageBox.Show("You have to conquer nearby location!");
                return;
            }
            confirmBattleLocation(maplocation8, 7);
        }

        private void playerSelectBattleLocation9(object sender, MouseButtonEventArgs e)
        {
            if (!worldMap.MyLocations.Contains(7))
            {
                MessageBox.Show("You have to conquer nearby location!");
                return;
            }
            confirmBattleLocation(maplocation9, 8);
        }

        private void playerSelectBattleLocation10(object sender, MouseButtonEventArgs e)
        {
            if (!worldMap.MyLocations.Contains(8))
            {
                MessageBox.Show("You have to conquer nearby location!");
                return;
            }
            confirmBattleLocation(maplocation10, 9);
        }

        private void playerSelectBattleLocation11(object sender, MouseButtonEventArgs e)
        {
            if (!worldMap.MyLocations.Contains(7))
            {
                MessageBox.Show("You have to conquer nearby location!");
                return;
            }
            confirmBattleLocation(maplocation11, 10);
        }

        private void playerSelectBattleLocation12(object sender, MouseButtonEventArgs e)
        {
            if (!worldMap.MyLocations.Contains(10))
            {
                MessageBox.Show("You have to conquer nearby location!");
                return;
            }
            confirmBattleLocation(maplocation12, 11);
        }
        //----------------------- confirm location --------------------
        private void confirmBattleLocation(Image location,int locationNumber)
        {
            if (worldMap.MyLocations.Contains(locationNumber))
            {
                MessageBox.Show("This is already mine!");
                return;
            }
            worldMap.targetLocation = locationNumber;
            Close();
        }

        private void CollectWorldLocations()
        {
            worldLocations = new List<Image>();
            worldLocations.Add(maplocation1);
            worldLocations.Add(maplocation2);
            worldLocations.Add(maplocation3);
            worldLocations.Add(maplocation4);
            worldLocations.Add(maplocation5);
            worldLocations.Add(maplocation6);
            worldLocations.Add(maplocation7);
            worldLocations.Add(maplocation8);
            worldLocations.Add(maplocation9);
            worldLocations.Add(maplocation10);
            worldLocations.Add(maplocation11);
            worldLocations.Add(maplocation12);
        }
        private void SetWorldLocations(List<int> myLocations)
        {
            if (myLocations.Count == worldLocations.Count)
                worldConqueringDataLabel.Content = "You conquered all of the world!";
            foreach (int location in myLocations)
            {
                worldLocations[location].Source = new BitmapImage(playerColor);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            worldMap.targetLocation = 66;
            Close();
        }
    }
}
