using System.Collections.Generic;

namespace us_dead_kids.character {

      public partial class Character {

            private static readonly Dictionary<string, Character> Store = new Dictionary<string, Character>();

            public static class Cache {

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