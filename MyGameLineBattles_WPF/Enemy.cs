using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MyGameLineBattles_WPF
{
    public class Enemy
    {
        protected Image myScaryPicture;
        public int MyLeftMargin { get; protected set; }
        public int MyTopMargin { get; protected set; }

        public string Name { get; protected set; }
        public int HealthPoints { get; protected set; }
        public int MyShootingSkill { get; protected set; }
        public Enemy(string name, int healthPoints, Image myScaryPicture, int myShootingSkill)
        {
            Name = name;
            HealthPoints = healthPoints;
            MyShootingSkill = myShootingSkill;
            this.myScaryPicture = myScaryPicture;
            //myScaryPicture.Visibility = Visibility.Visible;
            //MyLeftMargin = (int)myScaryPicture.Margin.Left;
            //MyTopMargin = (int)myScaryPicture.Margin.Top;

            SetMyRealPicture();
        }

        public void ShowMeOnTheBattlefield()
        {
            myScaryPicture.Visibility = Visibility.Visible;
        }

        public void SelectNewPictureForMe(Image myNewPicture)
        {
            this.myScaryPicture = myNewPicture;
            MyLeftMargin = (int)myScaryPicture.Margin.Left;
            MyTopMargin = (int)myScaryPicture.Margin.Top;

            if (!(this is IAmEliteDefender))
                SetMyRealPicture();
            else
                SetMyRealPicture();
        }

        public virtual void SetMyRealPicture()
        {
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\RedEnemyF.png", UriKind.Relative));
            myScaryPicture.Visibility = Visibility.Collapsed;
        }

        public void RestoreDefaultImagePosition()
        {
           
            myScaryPicture.Margin = new System.Windows.Thickness(MyLeftMargin, MyTopMargin, 0, 0);
            
        }

        public virtual bool AssessDamage(bool orksAreEnemy)
        {

            int left = (int)myScaryPicture.Margin.Left+10;
            int top = (int)myScaryPicture.Margin.Top;
            myScaryPicture.Margin = new System.Windows.Thickness(left, top, 0, 0);

            HealthPoints -= 40;
            if (HealthPoints <= 0)
            {
                //myScaryPicture.Visibility = System.Windows.Visibility.Collapsed;     
                myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\RedEnemyDead.png", UriKind.Relative));
                return true;
            }
            return false;
            
        }
    }

    public class EliteEnemy : Enemy, IAmEliteEnemy
    {
        public EliteEnemy(string name,Image myScaryPicture,int myShootingSkill) : base(name,50,myScaryPicture,myShootingSkill)
        {}

        public override void SetMyRealPicture()
        {
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\RedEnemyG.png", UriKind.Relative));
            myScaryPicture.Visibility = Visibility.Collapsed;
        }
    }

    public class GeneralEnemy : Enemy, IAmEliteEnemy
    {
        public GeneralEnemy(string name, Image myScaryPicture, int myShootingSkill) : base(name, 50, myScaryPicture, myShootingSkill)
        { }
        public override void SetMyRealPicture()
        {
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\RedEnemyLeader.png", UriKind.Relative));
            myScaryPicture.Visibility = Visibility.Collapsed;
        }
    }
    //--- ORCS -----------------------------------------------
    public class OrcWarriorEnemy : Enemy
    {
        public OrcWarriorEnemy(string name, int healthPoints, Image myScaryPicture, int myShootingSkill) : base(name, healthPoints, myScaryPicture, myShootingSkill)
        { }
        public override void SetMyRealPicture()
        {
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\OrcEnemyNormal.png", UriKind.Relative));
            myScaryPicture.Visibility = Visibility.Collapsed;
        }
        public override bool AssessDamage(bool orksAreEnemy)
        {

            int left = (int)myScaryPicture.Margin.Left + 10;
            int top = (int)myScaryPicture.Margin.Top;
            myScaryPicture.Margin = new System.Windows.Thickness(left, top, 0, 0);

            HealthPoints -= 40;
            if (HealthPoints <= 0)
            {
                //myScaryPicture.Visibility = System.Windows.Visibility.Collapsed;     
                myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\OrcEnemyDead.png", UriKind.Relative));
                return true;
            }
            return false;

        }
    }
    public class OrcEliteEnemy : OrcWarriorEnemy
    {
        public OrcEliteEnemy(string name, int healthPoints, Image myScaryPicture, int myShootingSkill) : base(name, healthPoints, myScaryPicture, myShootingSkill)
        { }
        public override void SetMyRealPicture()
        {
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\OrcEnemyElite.png", UriKind.Relative));
            myScaryPicture.Visibility = Visibility.Collapsed;
        }
    }
    //-- DAEMONS ---------------------------------------------
    public class DemonWarriorEnemy : Enemy
    {
        public DemonWarriorEnemy(string name, Image myScaryPicture, int myShootingSkill) : base(name,69,myScaryPicture,myShootingSkill)
        { }
        public override void SetMyRealPicture()
        {
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\DemonWarrior.png", UriKind.Relative));
            myScaryPicture.Visibility = Visibility.Collapsed;
        }
        public override bool AssessDamage(bool orksAreEnemy)
        {

            int left = (int)myScaryPicture.Margin.Left + 10;
            int top = (int)myScaryPicture.Margin.Top;
            myScaryPicture.Margin = new System.Windows.Thickness(left, top, 0, 0);

            HealthPoints -= 40;
            if (HealthPoints <= 0)
            {
                //myScaryPicture.Visibility = System.Windows.Visibility.Collapsed;     
                myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\DemonWarriorDead.png", UriKind.Relative));
                return true;
            }
            return false;

        }
    }
}
