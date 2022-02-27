using UnityEngine;

namespace eden.locomotion {

      public static partial class Eden {

            public class Locomotion : eden.Eden.IService {

                  // Hide our constructor.
                  private Locomotion() { }


                  // New Locomotion service.
                  public static eden.Eden.IService New() {
                        return new Locomotion();
                  }


                  // Move an entity in the world in the direction specified and apply modifiers.
                  public void Move(string entityID, Vector3 direction, float modifier) { }


                  // Jump in direction specified and apply modifier.
                  public void Jump(string entityID, Vector3 direction, float modifier) { }


            }

      }

}