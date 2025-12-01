using System.Windows.Controls;

namespace Wel.Battle.Game.Wpf.Classes.cs {
    public class GameWindow {
        public Button AttackButton { get; set; }
        public Button Attack1Button { get; set; }
        public Button Attack2Button { get; set; }
        public Button Attack3Button { get; set; }
        public Button Attack4Button { get; set; }
        public Button HealButton { get; set; }
        public Button SwapButton { get; set; }
        public Button ResetGameButton {  get; set; }
        public TextBlock PokemonXText { get; set; }
        public TextBlock PokemonYText { get; set; }
        public Image PokemonXImage { get; set; }
        public Image PokemonYImage { get; set; }
        public Image PokemonXTypeImage { get; set; }
        public Image PokemonYTypeImage { get; set; }

        public Border PokemonYHealthBar { get; set; }
        public Border PokemonXHealthBar { get; set; }
        public TextBlock EndGameText { get; set; }
        public TextBlock EngameResult {  get; set; }
        public Label LblEffectiveness { get; internal set; }
        public Label lblEffectivnessOponent { get; internal set; }

        public Button SwapButton1 { get; set; }
        public Button SwapButton2 { get; set; }
        public Button SwapButton3 { get; set; }
        
        public GameWindow() {}
    }
}
