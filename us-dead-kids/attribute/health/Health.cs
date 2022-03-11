using System;

namespace us_dead_kids.attribute.health {

      public class Health {

            public string ID       { get; set; }
            public string EntityID { get; set; }
            public int    Current  { get; set; }
            public int    Max      { get; set; }

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


            public static Health Get(string entityID) {
                  throw new NotImplementedException();
            }

      }

}