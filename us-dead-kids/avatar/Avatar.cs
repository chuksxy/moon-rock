using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using us_dead_kids.skill;
using Environment = us_dead_kids.environment.Environment;
using Object = System.Object;

namespace us_dead_kids.avatar {

      // KISS
      public class Avatar : MonoBehaviour {

            private const int LEFT_HAND_SLOT  = 0;
            private const int RIGHT_HAND_SLOT = 1;

            private Animator   _animator;
            private GameObject _avatar;

            private readonly Dictionary<string, SkillState> _skillStates = new Dictionary<string, SkillState>();


            private static class AnimParams {

                  public static readonly int MoveX            = Animator.StringToHash("Move X");
                  public static readonly int MoveZ            = Animator.StringToHash("Move Z");
                  public static readonly int Run              = Animator.StringToHash("Run");
                  public static readonly int Evade            = Animator.StringToHash("Evade");
                  public static readonly int Guard            = Animator.StringToHash("Guard");
                  public static readonly int UseRightArmament = Animator.StringToHash("Right Armament");
                  public static readonly int UseLeftArmament  = Animator.StringToHash("Left Armament");
                  public static readonly int ArmamentIndex    = Animator.StringToHash("Armament Index");
                  public static readonly int Melee            = Animator.StringToHash("Melee");
                  public static readonly int MeleeIndex       = Animator.StringToHash("Melee Index");
                  public static readonly int UseSkill         = Animator.StringToHash("Use Skill");
                  public static readonly int SkillIndex       = Animator.StringToHash("Skill");
                  public static readonly int UseItem          = Animator.StringToHash("Use Item");
                  public static readonly int ItemIndex        = Animator.StringToHash("Item Index");
                  public static readonly int Hand             = Animator.StringToHash("Hand");
                  public static readonly int Interact         = Animator.StringToHash("Interact");
                  public static readonly int Reload           = Animator.StringToHash("Reload");

            }

            private readonly string _masterAvatarPath     = $"{Environment.Path()}/avatar/master";
            private readonly string _masterControllerPath = $"{Environment.Path()}/avatar/master-controller";


            private void Start() {
                  _avatar      = Instantiate(Resources.Load<GameObject>(_masterAvatarPath), transform, true);
                  _avatar.name = $"{gameObject.name}.avatar";
                  _animator    = _avatar.GetComponent<Animator>();

                  if (_animator == null) _animator = _avatar.AddComponent<Animator>();
                  _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(_masterControllerPath);
            }


            private static bool IsAlive() {
                  return true;
            }


            private static void Exec(Action action) {
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


            public void UseItem() {
                  Exec(() => {
                        var i = IndexItem(GetCurrentItem());
                        GetAnimator().SetTrigger(AnimParams.UseItem);
                        GetAnimator().SetInteger(AnimParams.ItemIndex, i);
                  });
            }


            private int IndexItem(string itemID) {
                  return -1;
            }


            private string GetCurrentItem() {
                  return "";
            }


            // Context sensitive skills. E.g
            // Using a lightning skill outside guarantees its effectiveness
            // Using telekinesis skills will pull in all the weapons around the player within a specific radius
            public void UseSkill(int slot) {
                  var index = IndexSkill(GetSkill(slot));
                  Exec(() => {
                        GetAnimator().SetTrigger(AnimParams.UseSkill);
                        GetAnimator().SetInteger(AnimParams.SkillIndex, index);
                  });
            }


            private static string GetSkill(int slot) {
                  return "";
            }


            private static int IndexSkill(string name) {
                  return -1;
            }


            public void Melee() {
                  Exec(() => { GetAnimator().SetTrigger(AnimParams.Melee); });
            }


            public void UseRightArmament() {
                  Exec(() => {
                        GetAnimator().SetTrigger(AnimParams.UseRightArmament);
                        GetAnimator().SetInteger(AnimParams.ArmamentIndex, GetAndIndexArmament(LEFT_HAND_SLOT));
                        GetAnimator().SetInteger(AnimParams.Hand, RIGHT_HAND_SLOT);
                  });
            }


            public void CycleRightArmament() {
                  Exec(() => {
                        GetAnimator().SetTrigger(AnimParams.UseRightArmament);
                        GetAnimator().SetInteger(AnimParams.Hand, RIGHT_HAND_SLOT);
                  });
            }


            public void UseLeftArmament() {
                  Exec(() => {
                        GetAnimator().SetTrigger(AnimParams.UseLeftArmament);
                        GetAnimator().SetInteger(AnimParams.ArmamentIndex, GetAndIndexArmament(LEFT_HAND_SLOT));
                        GetAnimator().SetInteger(AnimParams.Hand, LEFT_HAND_SLOT);
                  });
            }


            public void CycleLeftArmament() {
                  Exec(() => {
                        GetAnimator().SetTrigger(AnimParams.UseRightArmament);
                        GetAnimator().SetInteger(AnimParams.Hand, LEFT_HAND_SLOT);
                  });
            }


            private int GetAndIndexArmament(int hand) {
                  return hand;
            }


            public void ReloadLeft() {
                  Exec(() => {
                        GetAnimator().SetTrigger(AnimParams.Reload);
                        GetAnimator().SetInteger(AnimParams.Hand, LEFT_HAND_SLOT);
                  });
            }


            public void ReloadRight() {
                  Exec(() => {
                        GetAnimator().SetTrigger(AnimParams.Reload);
                        GetAnimator().SetInteger(AnimParams.Hand, RIGHT_HAND_SLOT);
                  });
            }


            public void Interact() {
                  Exec(() => { GetAnimator().SetTrigger(AnimParams.Interact); });
            }


            public void SetSkillState(SkillState s) {
                  if (_skillStates.ContainsKey(s.ID)) return;
                  Debug.Log($"Skill [{s.ID}] used for the first time, tracking state.");
                  _skillStates.Add(s.ID, s);
            }


            public SkillState GetSkillState(string skillID) {
                  if (_skillStates.ContainsKey(skillID)) return _skillStates[skillID];
                  Debug.LogWarning($"Attempting to access skill [{skillID}] not assigned to avatar.");
                  return null;
            }


            public void InvokeAsync(Func<IEnumerator> action) {
                  StartCoroutine(action.Invoke());
            }


      }

}