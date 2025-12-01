namespace Wel.Battle.Game.Wpf.Classes.cs
{
    public class Pokemon
    {
        private double health;
        public string Name { get; set; }
        public TypeOfPokemonOrAttack PokemonType { get; set; }
        public int Level { get; set; }
        public int Attack4Counter { get; set; } = 0;
        public int Attack3Counter { get; set; } = 0;

        public double Health
        {
            get { return health; }
            set { 
                if(value > 100) health = 100;
                else if(value < 0)health = 0;
                else health = value;
            }
        }

        public Pokemon(string name, TypeOfPokemonOrAttack type ,int level, int health)
        {
            Name = name;
            PokemonType = type;
            Level = level;
            Health = health;
        }

        public bool isDead()
        {
            return Health <= 0;
        }
      
        public void TakeDamage(Attack attack)
        {
            double damage = attack.CalculateDamage(this);
            Health -= damage;
        }

        public void Heal(Attack attack)
        {
            Health += attack.Defense;
        }

        public override string ToString()
        {
            return $"{Name} lvl{Level}" + $"\nHP: {Health} / 100";
        }
    }
}
