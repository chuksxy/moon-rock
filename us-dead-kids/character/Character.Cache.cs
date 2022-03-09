using System.Collections.Generic;

namespace us_dead_kids.character {

      public partial class Character {


            public static class Cache {

                  private static readonly Dictionary<string, Character> Store = new Dictionary<string, Character>();


                  // Put Character into the in-memory cache and optionally force an eviction if an entry already exists.
                  internal static void Put(Character character, bool evict = false) {
                        if (Store.ContainsKey(character.ID) && evict) {
                              Store.Remove(character.ID);
                        }

                        if (!Store.ContainsKey(character.ID)) {
                              Store.Add(character.ID, character);
                        }
                  }


                  internal static Dictionary<string, Character> Get() {
                        return Store;
                  }

            }

      }

}