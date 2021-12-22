using UnityEngine;

namespace game.world {
      public class WorldInterface : MonoBehaviour {
            private WorldData _data = WorldData.Blank;

            public void Begin() { }
            public void End()   { }

            internal WorldInterface Init(WorldData data) {
                  if (_data.IsBlank() && !data.IsBlank()) {
                        _data = data;
                  }

                  return this;
            }
      }
}