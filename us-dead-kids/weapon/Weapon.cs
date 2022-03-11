using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace us_dead_kids.weapon {

      public class Weapon {

            // Properties of a Weapon.
            public string ID          { get; set; }
            public string CharacterID { get; set; }
            public bool   Primary     { get; set; }
            public bool   Auxiliary   { get; set; }
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
            private static readonly Dictionary<string, List<Weapon>>
                  CharacterIDsToMeleeWeapons = new Dictionary<string, List<Weapon>>();

            private static readonly Dictionary<string, List<Weapon>>
                  CharacterIDsToPrimaryWeapons = new Dictionary<string, List<Weapon>>();

            private static readonly Dictionary<string, List<Weapon>>
                  CharacterIDsToSecondaryWeapons = new Dictionary<string, List<Weapon>>();

            public static class Service {

                  // Init Service and creating the `weapons` table.
                  public static void Init() {
                        UsDeadKids.DB.Exec(db => {
                              const string sql =
                                    @"create table weapons (
                                    id string not null,
                                    character_id  string not null,
                                    primary boolean,
                                    auxiliary boolean,
                                    melee boolean,
                                    ranged boolean,
                                    priority int,
                                    primary key(id)
                              )";

                              db.Execute(sql);
                        });
                  }

            }


            private static void CacheMelee(string characterID, List<Weapon> weapons, bool evict = false) {
                  if (CharacterIDsToMeleeWeapons.ContainsKey(characterID) && evict) {
                        CharacterIDsToMeleeWeapons.Remove(characterID);
                  }

                  if (!CharacterIDsToMeleeWeapons.ContainsKey(characterID)) {
                        CharacterIDsToMeleeWeapons.Add(characterID, weapons);
                  }
            }


            public static void Use(string weaponID) { }

            public static void Use(Weapon weapon) { }


            private static IEnumerable<Weapon> GetAllSecondary(string characterID) {
                  if (CharacterIDsToSecondaryWeapons.ContainsKey(characterID)) {
                        return CharacterIDsToSecondaryWeapons[characterID];
                  }

                  const string sql =
                        @"select * from weapons w where w.character_id=? and w.primary=false";

                  var weapons = UsDeadKids.DB.Exec(db => db.Query<Weapon>(sql, characterID));

                  return weapons ?? new List<Weapon>();
            }


            private static IEnumerable<Weapon> GetAllPrimary(string characterID) {
                  if (CharacterIDsToPrimaryWeapons.ContainsKey(characterID)) {
                        return CharacterIDsToPrimaryWeapons[characterID];
                  }

                  const string sql =
                        @"select * from weapons w where w.character_id=? and w.primary=true";

                  var weapons = UsDeadKids.DB.Exec(db => db.Query<Weapon>(sql, characterID));

                  return weapons ?? new List<Weapon>();
            }


            public static void UsePrimary(string characterID) {
                  var weapon = GetAllPrimary(characterID).OrderByDescending(w => w.Priority).FirstOrDefault(null);
                  if (weapon == null) {
                        Debug.LogWarning($"character [{characterID}] does not have a primary weapon assigned");
                        return;
                  }

                  Use(weapon);
            }


            public static void UseSecondary(string characterID) {
                  var weapon = GetAllSecondary(characterID).OrderByDescending(w => w.Priority).FirstOrDefault(null);
                  if (weapon == null) {
                        Debug.LogWarning($"character [{characterID}] does not have a secondary weapon assigned");
                        return;
                  }

                  Use(weapon);
            }


            public static void UseMelee(string characterID) {
                  UsDeadKids.DB.Exec(db => {
                        var meleeWeapons = CharacterIDsToMeleeWeapons.ContainsKey(characterID)
                              ? CharacterIDsToMeleeWeapons[characterID]
                              : db.Query<Weapon>(GET_ALL_MELEE_WEAPONS_FOR_CHARACTER, characterID);

                        var chosen = meleeWeapons.OrderByDescending(w => w.Priority).FirstOrDefault();
                        if (chosen == null) {
                              Debug.LogWarning($"No melee weapon assigned to character [{characterID}].");
                              return;
                        }

                        Use(chosen);
                        CacheMelee(characterID, meleeWeapons);
                  });
            }

      }

}