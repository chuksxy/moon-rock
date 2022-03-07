using System;

namespace us_dead_kids.attribute.speed {

      public class Speed {

            public string ID          { get; set; }
            public string CharacterID { get; set; }
            public float  Current     { get; set; }
            public float  Max         { get; set; }


            public static Speed Get(string characterID) {
                  throw new NotImplementedException();
            }

      }

}