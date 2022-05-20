using System.Collections.Generic;
using UnityEngine;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.armament {

      public class Armament : MonoBehaviour {

            public string ID     { get; set; }
            public Avatar Avatar { get; set; }
            public int    Hand   { get; set; }


            public ArmamentState State() {
                  return Registry.Read(ID, Avatar.ID);
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

                  var state = Registry.Read(avatar.ID, armamentID);
                  if (state != null) return New(state, avatar, hand);

                  Debug.LogWarning($"Armament [{armamentID}] not found for avatar [{avatar.ID}].");
                  return null;
            }


            private static Armament New(ArmamentState state, Avatar avatar, int hand) {
                  var a = new GameObject().AddComponent<Armament>();
                  a.ID     = state.ArmamentID;
                  a.Avatar = avatar;
                  a.Hand   = hand;

                  Registry.Put(state);

                  return a;
            }


            public class Registry : MonoBehaviour {

                  private readonly Dictionary<string, ArmamentState> _compositeIDToArmament = new();

                  private static Registry _registry;


                  public static ArmamentState Read(string avatarID, string armamentID) {
                        if (_registry == null) {
                              Debug.LogWarning("Armament registry has not been created.");
                              return null;
                        }

                        var key = GetKey(avatarID, armamentID);
                        return _registry._compositeIDToArmament.ContainsKey(key)
                              ? _registry._compositeIDToArmament[key]
                              : new ArmamentState();
                  }


                  public static ArmamentState Put(ArmamentState s) {
                        if (_registry == null) {
                              Debug.LogWarning("Armament registry has not been created.");
                              return null;
                        }

                        var key = GetKey(s.AvatarID, s.ArmamentID);
                        if (_registry._compositeIDToArmament.ContainsKey(key)) {
                              return _registry._compositeIDToArmament[key];
                        }

                        _registry._compositeIDToArmament.Add(key, s);
                        return s;
                  }


                  private static string GetKey(string avatarID, string armamentID) {
                        return $"{avatarID}::{armamentID}.key";
                  }

            }

      }

}