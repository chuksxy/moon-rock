using System.Collections.Generic;

namespace us_dead_kids.avatar {

      [System.Serializable]
      public class AvatarState {

            public string ID   { get; set; }
            public string Name { get; set; }

            public int CurrentHealth { get; set; }
            public int MaxHealth     { get; set; }

            public int MaxSpeed     { get; set; }
            public int CurrentSpeed { get; set; }

            public int MaxStamina     { get; set; }
            public int CurrentStamina { get; set; }

            public List<string> Equipment { get; set; }
            public List<string> Items     { get; set; }

      }

}