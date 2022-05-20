using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.armament {

      public class Armament : MonoBehaviour {

            public string ID     { get; set; }
            public Avatar Avatar { get; set; }


            public static Armament Read(Avatar avatar, string armamentID, int hand) {
                  Armament current = null;
                  switch (hand) {
                        case Avatar.LEFT_HAND_SLOT:
                              current = avatar.LeftArmament;
                              break;
                        case Avatar.RIGHT_HAND_SLOT:
                              current = avatar.RightArmament;
                              break;
                        default:
                              Debug.LogWarning($"cannot read armament assigned to hand [{hand}] of avatar [{avatar.ID}]");
                              break;
                  }

                  if (current != null && current.ID.Equals(armamentID)) {
                        return current;
                  }

                  var state = Registry.Read(avatar.ID, armamentID, hand);
                  if (state != null) return New(state, avatar);

                  Debug.LogWarning($"Armament [{armamentID}] not found for avatar [{avatar.ID}].");
                  return null;
            }


            private static Armament New(State state, Avatar avatar) {
                  var a = new GameObject().AddComponent<Armament>();
                  a.ID     = state.ID;
                  a.Avatar = avatar;
                  return a;
            }


            public class Registry : MonoBehaviour {

                  // 
                  public static State Read(string avatarID, string armamentID, int hand) {
                        return new State();
                  }

            }

      }

}