using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace us_dead_kids.attribute.health {

      public class Health {

            public string ID       { get; set; }
            public string EntityID { get; set; }
            public int    Current  { get; set; }
            public int    Max      { get; set; }


            private static class Cache {

                  public static readonly Dictionary<string, Health> CharacterIDsToHealth = new Dictionary<string, Health>();

            }

            public class Service {

                  public static void Setup() {
                        UsDeadKids.DB.Exec(db => {
                              const string sql =
                                    @"create table health (
                                        id string,
                                        entity_id string,
                                        current int,
                                        max int,
                                        primary key (entity_id, id)
)                                   ";

                              db.Execute(sql);
                        });
                  }


                  public static void Persist(Health h) {
                        UsDeadKids.DB.Exec(db => {
                              const string sql =
                                    @"insert into health (id, entity_id, current, max) values ( ?,?,?,?)";

                              db.Execute(sql, h.ID, h.EntityID, h.Current, h.Max);
                        });
                  }


            }


            internal static List<Health> Query(string entityID) {
                  const string sql = @"select * from health where entity_id=?";

                  return UsDeadKids.DB.Exec(db => db.Query<Health>(sql, entityID));
            }


            public static Health Get(string entityID) {
                  if (Cache.CharacterIDsToHealth.ContainsKey(entityID)) {
                        return Cache.CharacterIDsToHealth[entityID];
                  }

                  var health = Query(entityID);
                  if (health == null || health.Count == 0) {
                        Debug.LogWarning($"No health entry for entity [{entityID}] found.");
                        return null;
                  }

                  if (health.Count > 1) {
                        Debug.LogWarning($"Multiple health entries for entity [{entityID}] found.");
                  }

                  return health.First();
            }

      }

}