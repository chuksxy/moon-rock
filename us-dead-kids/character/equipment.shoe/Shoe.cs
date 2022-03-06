using System.Collections.Generic;

namespace us_dead_kids.character.equipment.shoe {

      public partial class Shoe {

            public string          Name          { get; set; }
            public float           SpeedModifier { get; set; }
            public HashSet<string> Modifiers     { get; set; }

      }

}