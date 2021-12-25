using UnityEngine;

namespace moon.rock.world {

      public static partial class World {

            public class Interface : MonoBehaviour {

                  private Data _data = Data.Blank;

                  public void Begin() { }
                  public void End()   { }


                  internal Interface Init(Data data) {
                        if (_data.IsBlank() && !data.IsBlank()) _data = data;
                        return this;
                  }

            }

      }

}