using System.Collections.Generic;

namespace us_dead_kids.weapon.attribute.ammo {

      public class Ammo {

            public string ID         { get; set; }
            public string WeaponID   { get; set; }
            public string Tags       { get; set; }
            public int    RoundCount { get; set; }
            public int    ClipCount  { get; set; }

            public static class Service {

                  private static readonly Dictionary<string, Ammo> WeaponIDToAmmo =
                        new Dictionary<string, Ammo>();


                  public static void Setup() {
                        UsDeadKids.DB.Exec(db => {
                              const string sql =
                                    @"create table ammo (
                                        id string,
                                        weapon_id string,
                                        tags string,
                                        round_count int,
                                        clip_count int,
                                        primary key (id, weapon_id)
                                    )";

                              db.Execute(sql);
                        });
                  }


                  public static Ammo Get(string weaponID) {
                        if (WeaponIDToAmmo.ContainsKey(weaponID)) {
                              return WeaponIDToAmmo[weaponID];
                        }

                        // TODO:: Implement Query
                        return null;
                  }

            }

      }

}