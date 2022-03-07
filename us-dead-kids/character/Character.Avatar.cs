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


                  private void Awake() {
                        _animator = GetComponentInChildren<Animator>();
                  }


                  internal Animator GetAnimator() {
                        return _animator;
                  }

            }

      }

}