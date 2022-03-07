namespace us_dead_kids.character.attribute.equipment {

      public class Equipment {

            public string ID          { get; set; }
            public string CharacterID { get; set; }


            public static class Table {

                  public static void Init() {
                        UsDeadKids.DB.Exec(db => {
                              const string sql =
                                    @"create table equipment (
                                            character_id string not null,
                                            equipment_id string not null,
                                            equipped boolean,
                                            primary key (character_id, equipment_id)
                                    )";

                              db.Execute(sql);
                        });
                  }

            }

      }

}