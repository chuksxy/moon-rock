using System;
using UnityEngine;

namespace us_dead_kids.character {

      public partial class Character {

            public class Avatar : MonoBehaviour {

                  [SerializeField] private string characterID;

                  private Animator _animator;


                  // Get the Unity's representation of a Character by it's ID.
                  public static Avatar Get(string characterID) {
                        throw new NotImplementedException();
                  }


                  public static void Invoke(string characterID, Action<Avatar> action) {
                        var avatar = Avatar.Get(characterID);
                        if (avatar == null) {
                              Debug.LogWarning($"avatar with ID [{characterID}] not found.");
                              return;
                        }

                        action.Invoke(avatar);
                  }


                  private void Awake() {
                        _animator = GetComponentInChildren<Animator>();
                  }


                  internal Animator GetAnimator() {
                        return _animator;
                  }

            }

      }

}