using UnityEngine;

namespace game.world {

      public static partial class World {

            public class WorldInterface : MonoBehaviour {

                  private Data _data = Data.Blank;

                  public void Begin() { }
                  public void End()   { }


                  internal WorldInterface Init(Data data) {
                        if (_data.IsBlank() && !data.IsBlank()) _data = data;
                        return this;
                  }

            }

      }

}