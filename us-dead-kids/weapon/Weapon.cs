using System.Linq;
using UnityEngine;

namespace us_dead_kids.weapon {

      public class Weapon {

            public string ID          { get; set; }
            public string CharacterID { get; set; }
            public bool   Melee       { get; set; }
            public bool   Ranged      { get; set; }
            public int    Priority    { get; set; }

            private const string GET_ALL_MELEE_WEAPONS_FOR_CHARACTER =
                  "select * "
                + "from weapon w "
                + "where w.character_id = ? "
                + "and w.melee = true";

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