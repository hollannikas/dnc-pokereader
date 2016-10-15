using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace PokedexReader
{
    internal class Pokemon
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public override string ToString()
        {
            return "Pokemón #" + ID + " (" + Name + ") Height: " + Height + ", Weight: " + Weight;
        }
    }

    class Pokedex
    { 
        public static void Main()
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            foreach( int ID in Enumerable.Range(1, 25) ) {
                pokemons.Add(FetchPokemon(ID).Result);
            }
            
            pokemons.ForEach(pokemon => {
                Console.WriteLine(pokemon);
            });
        }

        public static async Task<Pokemon> FetchPokemon(int id)
        {
            using(HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync("http://pokeapi.co/api/v2/pokemon/" + id);
                Pokemon result = JsonConvert.DeserializeObject<Pokemon>(json);   
                return result;
            }
        }
    }
}