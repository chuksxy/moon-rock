namespace us_dead_kids.lib.animation {

      public class AnimationState {

            public AnimationState(string name, int nameHash) {
                  Name     = name;
                  NameHash = nameHash;
            }


            public string Name           { get; }
            public int    NameHash       { get; }
            public bool   IsCancelled    { get; set; }
            public float  NormalisedTime { get; set; }

            public float ExitTime { get; set; }


            public bool Exit() {
                  return NormalisedTime >= ExitTime;
            }

      }

}