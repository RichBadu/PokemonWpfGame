using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Wel.Battle.Game.Wpf.Classes.cs;


namespace Wel.Battle.Game.Wpf.Lists
{
    public class Attacks
    {
        TypeOfPokemonOrAttack electric;
        TypeOfPokemonOrAttack grass;
        TypeOfPokemonOrAttack fire;
        TypeOfPokemonOrAttack water;

        private List<TypeOfPokemonOrAttack> types; 
        private List<Pokemon> pokemons;
        private List<Attack> attacks;
        private List<Attack> availableattacks;
        
        public List<TypeOfPokemonOrAttack> Types
        {
            get { return types; }
                       
        }
        public List<Pokemon> Pokemons
        {
            get { return pokemons; }
        }
        private List<Attack> Attacklist 
        { 
            get {  return attacks; } 
        }

        public List<Attack> AvailableAttacks
        {
            get { return availableattacks; }
        }
             

        public Attacks(bool createData)
        {
            
            PopulateTypes();

            SeedPokemons();
            if (createData)
            {
                SeedAttacks();
            }
            else
            {
                attacks = new List<Attack>();
            }

            
        }

        

        public void PopulateTypes()
        {
           types = new List<TypeOfPokemonOrAttack>();
           electric = new TypeOfPokemonOrAttack("Electric", new List<string> {"Water"}, new List<string> {"Grass"});
           grass = new TypeOfPokemonOrAttack("Grass", new List<string> { "Electric" }, new List<string> { "Fire" });
           fire = new TypeOfPokemonOrAttack("Fire", new List<string> { "Grass" }, new List<string> { "Water" });
           water = new TypeOfPokemonOrAttack("Water", new List<string> { "Fire" }, new List<string> { "Electric" });

            types.Add(electric);
            types.Add(grass);
            types.Add(fire);
            types.Add(water);
        }

        private void SeedPokemons() 
        {
            pokemons = new List<Pokemon>()
            {
                //electric 
                new Pokemon("Pikachu",electric, 20, 100),
                new Pokemon("Jolteon",electric, 20, 100),
                new Pokemon("Electrabuzz",electric, 20, 100),

                //grass
                new Pokemon("Weepinbell",grass, 20, 100),
                new Pokemon("Scyther",grass, 20, 100),
                new Pokemon("Venusaur",grass, 20, 100),

                //water
                new Pokemon("Seadra",water, 20, 100),
                new Pokemon("Poliwhirl",water, 20, 100),
                new Pokemon("Blastoise",water, 20, 100),

                //fire
                new Pokemon("Flareon",fire, 20, 100),
                new Pokemon("Magmar",fire, 20, 100),
                new Pokemon("Charizard",fire, 20, 100),
            };
        }

        private void SeedAttacks() 
        {
            attacks = new List<Attack>()
            {
                //comment x0.5 = not very effective
                //comment x2 = Super effective

                //electric
                new Attack("Volt Tackle", electric, 10, 0), //0.5 vs grass / 2 vs water
                new Attack("Thunder Fang", electric, 5, 10), //0.5 vs grass / 2 vs water
                new Attack("Thunder Bolt", electric, 15, 0), //0.5 vs grass / 2 vs water
                new Attack("Thunder", electric, 25, 0), //0.5 vs grass / 2 vs water

                //grass
                new Attack("Absorb", grass, 5, 10), //0.5 vs fire / 2 vs electric
                new Attack("Mega Drain",grass, 10, 5), //0.5 vs fire / 2 vs electric
                new Attack("Vine Whip",grass, 15, 0), //0.5 vs fire / 2 vs electric
                new Attack("Magic Leaf",grass, 25 , 0), //0.5 vs fire / 2 vs electric

                //water
                new Attack("Aqua Tail",water, 10, 0), //0.5 vs electric / 2 vs fire
                new Attack("Water Pulse",water, 5, 10), //0.5 vs electric / 2 vs fire
                new Attack("Surf",water, 15, 0), //0.5 vs electric / 2 vs fire
                new Attack("Hydro Pump",water, 25, 0), //0.5 vs electric / 2 vs fire

                //fire
                new Attack("Ember",fire, 10,0), //0.5 vs water // 2 vs grass
                new Attack("Fire Fang",fire, 5, 10), //0.5 vs water // 2 vs grass
                new Attack("Flamethrower",fire, 15,0), //0.5 vs water // 2 vs grass
                new Attack("Blast Burn",fire, 25,0), //0.5 vs water // 2 vs grass
            };

            
        }

        public List<Attack> GetAvailableAttacks(Pokemon pokemon)
        {
            List<Attack> availableAttacks = new List<Attack>();
            foreach (Attack attack in attacks)
                {
                    if(attack.AttackType.TypeName == pokemon.PokemonType.TypeName)
                    {  
                        availableAttacks.Add(attack); 
                    }
                }

            return availableAttacks;
 
        }
  




        

    }
}
