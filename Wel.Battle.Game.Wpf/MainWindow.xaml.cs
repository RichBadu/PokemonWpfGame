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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wel.Battle.Game.Wpf.Classes.cs;

namespace Wel.Battle.Game.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Classes.cs.Game game;

        public MainWindow()
        {
            InitializeComponent();

            var window = new GameWindow();
            window.AttackButton = btnAttack;
            window.Attack1Button = Attack1Button;
            window.Attack2Button = Attack2Button;
            window.Attack3Button = Attack3Button;
            window.Attack4Button = Attack4Button;
            window.SwapButton1 = Swap1Button;
            window.SwapButton2 = Swap2Button;
            window.SwapButton3 = Swap3Button;
            window.HealButton = BtnHeal;
            window.SwapButton = btnSwap;
            window.PokemonXText = tbkPokemonNameX;
            window.PokemonYText = tbkPokemonNameY;
            window.PokemonXImage = imgPokemonX;
            window.PokemonYImage = imgPokemonY;
            window.PokemonXTypeImage = imgPokemonTypeX;
            window.PokemonYTypeImage = imgPokemonTypeY;
            window.PokemonXHealthBar = PokemonXHealthBar;
            window.PokemonYHealthBar = PokemonYHealthBar;
            window.ResetGameButton = btnResetGame;
            window.EndGameText = tbkEndgame;
            window.EngameResult = tbkresult;
            window.LblEffectiveness = lblEffectiveness;
            window.lblEffectivnessOponent = lblEffectivenessOpponent;
            game = new Classes.cs.Game(window);
            game.Start();
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e) { game.OnAttackButtonClick(); }
        private void Attack1Button_Click(object sender, RoutedEventArgs e) { game.OnAttack1ButtonClick(); }
        private void Attack2Button_Click(object sender, RoutedEventArgs e) { game.OnAttack2ButtonClick(); }
        private void Attack3Button_Click(object sender, RoutedEventArgs e) { game.OnAttack3ButtonClick(); }
        private void Attack4Button_Click(object sender, RoutedEventArgs e) { game.OnAttack4ButtonClick(); }
        private void BtnHeal_Click(object sender, RoutedEventArgs e) { game.OnHealButtonClick(); }
        private void ResetButton_Click(object sender, RoutedEventArgs e) { game.onResetGameButtonClick(); }
        private void Swap1Button_Click(object sender, RoutedEventArgs e) { game.OnSwapButtonClick(1); }
        private void Swap2Button_Click(object sender, RoutedEventArgs e) { game.OnSwapButtonClick(2); }
        private void Swap3Button_Click(object sender, RoutedEventArgs e) { game.OnSwapButtonClick(3); }
        private void btnSwap_Click(object sender, RoutedEventArgs e) { game.OnSwapClick(); }
    }
}
