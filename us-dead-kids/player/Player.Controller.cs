using UnityEngine;
using us_dead_kids.character;

namespace us_dead_kids.player {

      public static class Player {

            public class Controller : MonoBehaviour {

                  public void Move(Vector3 direction) {
                        Character.Service.Move("player", direction);
                  }

            }


      }

}