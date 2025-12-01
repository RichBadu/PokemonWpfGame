using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using Wel.Battle.Game.Wpf.Lists;

namespace Wel.Battle.Game.Wpf.Classes.cs
{
    public class Game
    {
        Random rnd = new Random();

        private GameWindow window;

        private Pokemon[] opponentPokemon;
        private Pokemon[] myPokemon;

        private bool freeze;

        private int opponentCurrentPokemon = 0;
        private int myCurrentPokemon = 0;
        List<int> pokemonIndexes = new List<int>();

        private Attacks attacks = new Attacks(true);

        public Game(GameWindow window)
        {
            this.window = window;
        }

        public void Start()
        {
            // Generate 3 random pokemon for me and opponent
            opponentPokemon = new Pokemon[3];
            myPokemon = new Pokemon[3];

            for (int i = 0; i < opponentPokemon.Length; i++) { opponentPokemon[i] = attacks.Pokemons[getRandomPokemonIndex()]; }
            for (int i = 0; i < myPokemon.Length; i++) { myPokemon[i] = attacks.Pokemons[getRandomPokemonIndex()]; }

            displayPokemon();
            window.LblEffectiveness.Visibility = Visibility.Hidden;
            window.lblEffectivnessOponent.Visibility = Visibility.Hidden;
        }

        private int getRandomPokemonIndex()
        {
            var i = rnd.Next(0, attacks.Pokemons.Count);

            if (pokemonIndexes.Contains(i))
            {
                return getRandomPokemonIndex();
            }

            pokemonIndexes.Add(i);

            return i;
        }

        public void OnAttackButtonClick()
        {
            window.AttackButton.Visibility = Visibility.Hidden;

            Pokemon selectedPokemon = GetMyPokemon();
            List<Attack> availableAttacks = attacks.GetAvailableAttacks(selectedPokemon);
            Button button;

            for (int i = 0; i < availableAttacks.Count; i++)
            {
                Button attackButton = GetAttackButton(i + 1);
                attackButton.Content = availableAttacks[i].AttackName;
                attackButton.Visibility = Visibility.Visible;
            }

            if (selectedPokemon.Attack4Counter >= 3)
            {
                button = GetAttackButton(4);
                ShowAccesRestricted(button);
            }
            if (selectedPokemon.Attack3Counter >= 3)
            {
                button = GetAttackButton(3);
                ShowAccesRestricted(button);
            }

            if (selectedPokemon.Attack4Counter < 3)
            {
                button = GetAttackButton(4);
                System.Windows.Media.Color color = System.Windows.Media.Color.FromRgb(221, 221, 221);
                button.Background = new SolidColorBrush(color);
            }
            if (selectedPokemon.Attack3Counter < 3)
            {
                button = GetAttackButton(3);
                System.Windows.Media.Color color = System.Windows.Media.Color.FromRgb(221, 221, 221);

                button.Background = new SolidColorBrush(color);
            }
        }

        private Button GetAttackButton(int attackNumber)
        {
            switch (attackNumber)
            {
                case 1:
                    return window.Attack1Button;
                case 2:
                    return window.Attack2Button;
                case 3:
                    return window.Attack3Button;
                case 4:
                    return window.Attack4Button;
                default:
                    throw new ArgumentOutOfRangeException("attackNumber", "Invalid attack number");
            }
        }

        public void OnAttack1ButtonClick()
        {
            onAttackStart(GetSelectedAttack(GetMyPokemon(), 0));
        }

        public void OnAttack2ButtonClick()
        {
            onAttackStart(GetSelectedAttack(GetMyPokemon(), 1));
        }

        public void OnAttack3ButtonClick()
        {
            Pokemon pokemon = GetMyPokemon();
            if (pokemon.Attack3Counter < 3)
            {
                pokemon.Attack3Counter++;
                onAttackStart(GetSelectedAttack(GetMyPokemon(), 2));
            }
        }

        public void OnAttack4ButtonClick()
        {
            Pokemon pokemon = GetMyPokemon();
            if (pokemon.Attack4Counter < 3)
            {
                pokemon.Attack4Counter++;
                onAttackStart(GetSelectedAttack(GetMyPokemon(), 3));
            }
        }
        private void ShowAccesRestricted(Button button)
        {
            button.Content = "Unavailable";
            button.Background = Brushes.Gray;
        }

        public void OnHealButtonClick()
        {
            GetMyPokemon().Health += 20;
            onAttackEnd();
        }

        public void OnSwapButtonClick(int index) {
            myCurrentPokemon = index-1;
            displayPokemon();
            onAttackEnd();
        }

        public void OnSwapClick() {
            window.AttackButton.Visibility = Visibility.Hidden;

            window.Attack1Button.Visibility = Visibility.Hidden;
            window.Attack2Button.Visibility = Visibility.Hidden;
            window.Attack3Button.Visibility = Visibility.Hidden;
            window.Attack4Button.Visibility = Visibility.Hidden;

            window.SwapButton1.Visibility = Visibility.Visible;
            window.SwapButton2.Visibility = Visibility.Visible;
            window.SwapButton3.Visibility = Visibility.Visible;

            Button[] buttons = new Button[] { window.SwapButton1, window.SwapButton2, window.SwapButton3 };

            for (int i = 0; i < myPokemon.Length; i++) {
                var button = buttons[i];
                if (button == null) break;

                button.IsEnabled = true;
                button.Opacity = 1;

                if (myCurrentPokemon == i) {
                    button.IsEnabled = false;
                    button.Opacity = .5;
                }

                setSwapButton(myPokemon[i], button);
            }
        }

        private void setSwapButton(Pokemon pokemon, Button button) {
            var img = new Image();
            img.Source = new BitmapImage(new Uri($"pack://application:,,,/pictures/{pokemon.Name}.png"));

            var label = new Label();
            label.Content = $"{pokemon.Name} {pokemon.Health}/{100}";

            var panel = new WrapPanel();
            panel.Children.Add(img);
            panel.Children.Add(label);

            button.Content = panel;

            if (pokemon.isDead()) {
                button.IsEnabled = false;
                button.Opacity = .1;
            }
        }

        private Attack GetSelectedAttack(Pokemon pokemon, int i)
        {
            if (i > 3 || i < 0) i = 0;
            return attacks.GetAvailableAttacks(pokemon)[i];
        }

        private void onAttackStart(Attack attack)
        {
            if (freeze) return;
            resetButtons();

            GetOpponentPokemon().TakeDamage(attack);
            GetMyPokemon().Heal(attack);
            ShowEffectiveness(attack, GetMyPokemon(), window.LblEffectiveness);
            onAttackEnd();
        }
        private async void onAttackEnd()
        {
            resetButtons();

            if (GetOpponentPokemon().Health == 0)
            {
                if (opponentCurrentPokemon < opponentPokemon.Length - 1) opponentCurrentPokemon++;
                else
                {
                    onGameEnd(true);
                }
            }
            else
            {
                displayPokemon();

                // wait for opponent to attack
                freeze = true;
                await Task.Delay(2000);
                freeze = false;

                Attack attack = GetSelectedAttack(GetOpponentPokemon(), rnd.Next(0, 4));
                GetMyPokemon().TakeDamage(attack);
                GetOpponentPokemon().Heal(attack);
                ShowEffectiveness(attack, GetOpponentPokemon(), window.lblEffectivnessOponent);
            }
            if (GetMyPokemon().isDead())
            {
                if (myCurrentPokemon < myPokemon.Length - 1) myCurrentPokemon++;
                else
                {
                    onGameEnd(false);
                }
            }
            
            displayPokemon();
        }

        private void onGameEnd(bool win)
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = 15;
            window.PokemonXImage.Effect = blur;
            window.PokemonYImage.Effect = blur;

            endGameButtons();

            if (win)
            {
                Debug.WriteLineIf(win, "you won");
                window.EngameResult.Text = "YOU WIN!!!";
            }
            else
            {
                window.EngameResult.Text = "YOU LOOSE!!!";
            }
        }
        private void endGameButtons()
        {
            window.ResetGameButton.Visibility = Visibility.Visible;
            window.EndGameText.Visibility = Visibility.Visible;
            window.EngameResult.Visibility = Visibility.Visible;

            window.AttackButton.Visibility = Visibility.Hidden;
            window.Attack1Button.Visibility = Visibility.Hidden;
            window.Attack2Button.Visibility = Visibility.Hidden;
            window.Attack3Button.Visibility = Visibility.Hidden;
            window.Attack4Button.Visibility = Visibility.Hidden;
            window.HealButton.Visibility = Visibility.Hidden;
            window.SwapButton.Visibility = Visibility.Hidden;
            
        }
        public void onResetGameButtonClick()
        {
            opponentPokemon = new Pokemon[0];
            myPokemon = new Pokemon[0];
            opponentCurrentPokemon = 0;
            myCurrentPokemon = 0;
            
            pokemonIndexes = new List<int>();
            attacks = new Attacks(true);

            Start();
            resetButtons();

            BlurEffect blur = new BlurEffect();
            blur.Radius = 0;
            window.PokemonXImage.Effect = blur;
            window.PokemonYImage.Effect = blur;
        }
        private void resetButtons()
        {
            window.AttackButton.Visibility = Visibility.Visible;
            window.HealButton.Visibility = Visibility.Visible;
            window.SwapButton.Visibility = Visibility.Visible;

            window.Attack1Button.Visibility = Visibility.Hidden;
            window.Attack2Button.Visibility = Visibility.Hidden;
            window.Attack3Button.Visibility = Visibility.Hidden;
            window.Attack4Button.Visibility = Visibility.Hidden;
            window.ResetGameButton.Visibility = Visibility.Hidden;
            window.EndGameText.Visibility = Visibility.Hidden;
            window.EngameResult.Visibility = Visibility.Hidden;

            window.HealButton.Opacity = 1;
            window.SwapButton.Opacity = 1;

            window.HealButton.IsEnabled = true;
            window.SwapButton.IsEnabled = true;

            window.SwapButton1.Visibility = Visibility.Hidden;
            window.SwapButton2.Visibility = Visibility.Hidden;
            window.SwapButton3.Visibility = Visibility.Hidden;
        }

        private void displayPokemon()
        {
            window.PokemonYText.Text = GetOpponentPokemon().ToString();
            window.PokemonYImage.Source = new BitmapImage(new Uri($"pack://application:,,,/pictures/{GetOpponentPokemon().Name}.png"));
            window.PokemonYTypeImage.Source = new BitmapImage(new Uri($"pack://application:,,,/pictures/types/{GetOpponentPokemon().PokemonType}.png"));
            window.PokemonYHealthBar.Width = (GetOpponentPokemon().Health / 100) * 200;

            window.PokemonXText.Text = GetMyPokemon().ToString();
            window.PokemonXImage.Source = new BitmapImage(new Uri($"pack://application:,,,/pictures/{GetMyPokemon().Name}.png"));
            window.PokemonXTypeImage.Source = new BitmapImage(new Uri($"pack://application:,,,/pictures/types/{GetMyPokemon().PokemonType}.png"));
            window.PokemonXHealthBar.Width = (GetMyPokemon().Health / 100) * 200;
        }

        public Pokemon GetMyPokemon() { return myPokemon[myCurrentPokemon]; }

        public Pokemon GetOpponentPokemon() { return opponentPokemon[opponentCurrentPokemon]; }

        public async void ShowEffectiveness(Attack attack, Pokemon pokemon, Label label) {
            SolidColorBrush brush;
            string effectiveness;
            double damage = attack.CalculateDamage(pokemon);

            if (damage >= 20) {
                effectiveness = "Supereffective";
                brush = new SolidColorBrush(Color.FromRgb(0, 204, 0));
            }
            else if (damage > 10) {
                effectiveness = "Effective";
                brush = new SolidColorBrush(Color.FromRgb(255, 128, 0));
            }
            else {
                effectiveness = "Not effective";
                brush = new SolidColorBrush(Color.FromRgb(255, 51, 51));
            }

            label.Background = brush;
            label.Visibility = Visibility.Visible;

            label.Content = $"Damage: {damage}\nEffective:{effectiveness}\nHealth: +{attack.Defense}";

            await Task.Delay(2000);
            label.Visibility = Visibility.Hidden;
        }
    }
}
