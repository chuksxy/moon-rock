using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace us_dead_kids.weapon {

      public class Weapon {

            // Properties of a Weapon.
            public string ID          { get; set; }
            public string CharacterID { get; set; }
            public bool   Melee       { get; set; }
            public bool   Ranged      { get; set; }
            public int    Priority    { get; set; }

            // Queries
            private const string GET_ALL_MELEE_WEAPONS_FOR_CHARACTER =
                  "select * "
                + "from weapon w "
                + "where w.character_id = ? "
                + "and w.melee = true";

            // Caches
            private static readonly Dictionary<string, HashSet<Weapon>>
                  CharacterIDToMeleeWeapons = new Dictionary<string, HashSet<Weapon>>();

            private static readonly Dictionary<string, HashSet<Weapon>>
                  CharacterIDToRangeWeapons = new Dictionary<string, HashSet<Weapon>>();


            private static void CacheMelee(string characterID, HashSet<Weapon> weapons, bool evict = false) {
                  if (CharacterIDToMeleeWeapons.ContainsKey(characterID) && evict) {
                        CharacterIDToMeleeWeapons.Remove(characterID);
                  }

                  if (!CharacterIDToMeleeWeapons.ContainsKey(characterID)) {
                        CharacterIDToMeleeWeapons.Add(characterID, weapons);
                  }
            }


            public static void Use(string weaponID) { }

            public static void Use(Weapon weapon) { }


            public static void UseMeleeWeapon(string characterID) {
                  UsDeadKids.DB.Exec(db => {
                        var meleeWeapons = db.Query<Weapon>(GET_ALL_MELEE_WEAPONS_FOR_CHARACTER, characterID);
                        var chosen       = meleeWeapons.OrderByDescending(w => w.Priority).FirstOrDefault();
                        if (chosen == null) {
                              Debug.LogWarning($"No melee weapon assigned to character [{characterID}].");
                              return;
                        }

                        Use(chosen);
                  });
            }

      }

}