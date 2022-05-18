namespace us_dead_kids.util {

      public class AnimationState {

            public AnimationState(string name, int stateHash) {
                  Name      = name;
                  StateHash = stateHash;
            }


            public string Name           { get; }
            public int    StateHash      { get; }
            public bool   IsCancelled    { get; set; }
            public float  NormalisedTime { get; set; }

      }

}