using System.Collections.Generic;
using eden.equipment;

namespace moon.rock.character.equipment.shoe {

      public partial class Shoe {

            public string          Name          { get; set; }
            public float           SpeedModifier { get; set; }
            public HashSet<string> Modifiers     { get; set; }

      }

}