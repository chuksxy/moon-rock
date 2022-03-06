using System;

namespace us_dead_kids.attribute.health {

      public class Health {

            public string ID       { get; set; }
            public string EntityID { get; set; }
            public int    Current  { get; set; }
            public int    Max      { get; set; }


            public static Health Get(string entityID) {
                  throw new NotImplementedException();
            }

      }

}