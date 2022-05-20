using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.armament {

      public class Armament : MonoBehaviour {

            public string ID     { get; set; }
            public Avatar Avatar { get; set; }
            public int    Hand   { get; set; }


            public ArmamentState State() {
                  return Registry.Read(ID, Avatar.ID, Hand);
            }


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
                  if (state != null) return New(state, avatar, hand);

                  Debug.LogWarning($"Armament [{armamentID}] not found for avatar [{avatar.ID}].");
                  return null;
            }


            private static Armament New(ArmamentState state, Avatar avatar, int hand) {
                  var a = new GameObject().AddComponent<Armament>();
                  a.ID     = state.ID;
                  a.Avatar = avatar;
                  a.Hand   = hand;

                  return a;
            }


            public class Registry : MonoBehaviour {

                  // 
                  public static ArmamentState Read(string avatarID, string armamentID, int hand) {
                        return new ArmamentState();
                  }

            }

      }

}