using System;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

namespace us_dead_kids.avatar {

      // KISS
      public class Avatar : MonoBehaviour {

            private Animator   _animator;
            private GameObject _avatar;

            private static class AnimParams {

                  public static readonly int MoveX    = Animator.StringToHash("Move X");
                  public static readonly int MoveZ    = Animator.StringToHash("Move Z");
                  public static readonly int Run      = Animator.StringToHash("Run");
                  public static readonly int Jump     = Animator.StringToHash("Jump");
                  public static readonly int Evade    = Animator.StringToHash("Evade");
                  public static readonly int Guard    = Animator.StringToHash("Guard");
                  public static readonly int UseSkill = Animator.StringToHash("Use Skill");
                  public static readonly int UseItem  = Animator.StringToHash("Use Item");
                  public static readonly int Interact = Animator.StringToHash("Use Item");

            }


            private void Start() {
                  _avatar = new GameObject();
                  _avatar.transform.SetParent(transform);

                  _animator = _avatar.AddComponent<Animator>();
            }


            private bool IsAlive() {
                  return true;
            }


            private void Exec(Action action) {
                  if (IsAlive()) {
                        action.Invoke();
                  }
            }


            private Animator GetAnimator() {
                  if (_animator == null) {
                        Debug.LogWarning($"Attempting to access null animator assigned to [{gameObject.name}]");
                  }

                  return _animator;
            }


            // Move avatar in direction
            // Tilt Right
            // Tilt Left
            // Foot IK
            public void Move(Vector3 direction, bool run) {
                  Exec(() => {
                        GetAnimator().SetFloat(AnimParams.MoveX, direction.x);
                        GetAnimator().SetFloat(AnimParams.MoveZ, direction.z);
                        GetAnimator().SetBool(AnimParams.Run, run);
                  });
            }


            // Adjust Torso to match aim direction
            // Head look at aim direction
            public void Aim(Vector3 direction) { }


            // Project an orb that negates damage
            public void Guard(bool guard) {
                  Exec(() => { GetAnimator().SetBool(AnimParams.Guard, guard); });
            }


            // Hold to run
            public void Evade() {
                  Exec(() => { GetAnimator().SetTrigger(AnimParams.Evade); });
            }


            // Make avatar jump
            public void Jump() {
                  Exec(() => { GetAnimator().SetTrigger(AnimParams.Jump); });
            }


            public void UseItem() {
                  Exec(() => { GetAnimator().SetTrigger(AnimParams.UseItem); });
            }


            // Context sensitive skills. E.g
            // Using a lightning skill outside guarantees its effectiveness
            // Using telekinesis skills will pull in all the weapons around the player within a specific radius
            public void UseSkill(int slot) {
                  Exec(() => { GetAnimator().SetTrigger(AnimParams.UseSkill); });
            }


            public void Interact() {
                  Exec(() => { GetAnimator().SetTrigger(AnimParams.Interact); });
            }

      }

}