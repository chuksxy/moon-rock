using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using us_dead_kids.armament;
using AnimationState = us_dead_kids.lib.animation.AnimationState;
using Environment = us_dead_kids.environment.Environment;

namespace us_dead_kids.avatar {

      // KISS
      public partial class Avatar : MonoBehaviour {

            public const int LEFT_HAND_SLOT  = 0;
            public const int RIGHT_HAND_SLOT = 1;

            private Animator   _animator;
            private GameObject _avatar;

            private readonly Dictionary<string, AnimationState> _animationStates = new Dictionary<string, AnimationState>();

            public string ID { get; private set; } = "no.id.assigned";

            public Armament LeftArmament  { get; set; }
            public Armament RightArmament { get; set; }

            public List<Avatar> Targets      { get; set; }
            public bool         TrackTargets { get; set; }

            private static class AnimParams {

                  public static readonly int Move             = Animator.StringToHash("Move");
                  public static readonly int Sprint           = Animator.StringToHash("Sprint");
                  public static readonly int Evade            = Animator.StringToHash("Evade");
                  public static readonly int Guard            = Animator.StringToHash("Guard");
                  public static readonly int UseRightArmament = Animator.StringToHash("Right Armament");
                  public static readonly int UseLeftArmament  = Animator.StringToHash("Left Armament");
                  public static readonly int ArmamentIndex    = Animator.StringToHash("Armament Index");
                  public static readonly int Melee            = Animator.StringToHash("Melee");
                  public static readonly int MeleeIndex       = Animator.StringToHash("Melee Index");
                  public static readonly int UseSkill         = Animator.StringToHash("Use Skill");
                  public static readonly int SkillIndex       = Animator.StringToHash("Skill Index");
                  public static readonly int UseItem          = Animator.StringToHash("Use Item");
                  public static readonly int ItemIndex        = Animator.StringToHash("Item Index");
                  public static readonly int Hand             = Animator.StringToHash("Hand");
                  public static readonly int Interact         = Animator.StringToHash("Interact");
                  public static readonly int Reload           = Animator.StringToHash("Reload");

            }


            private static class Layers {

                  public const int Base         = 0;
                  public const int Combat       = 1;
                  public const int Melee        = 2;
                  public const int Reload       = 3;
                  public const int Skills       = 4;
                  public const int Evade        = 5;
                  public const int Interactions = 6;
                  public const int Death        = 7;

            }


            private readonly string _masterAvatarPath     = $"{Environment.Path()}/avatar/master";
            private readonly string _masterControllerPath = $"{Environment.Path()}/avatar/master-controller";


            public void New(AvatarState state) {
                  var existing = Registry.Read(state.ID);
                  if (existing != null) {
                        Debug.LogWarning($"Avatar with ID [{state.ID}] already exists.");
                        return;
                  }

                  Registry.Put(state);
                  ID = state.ID;
            }


            public void Load(string avatarID) {
                  var state = Registry.Read(avatarID);
                  if (state != null) return;

                  Debug.LogWarning($"Avatar [{avatarID}] does not exist in registry, creating one.");
                  state = new AvatarState() {
                        ID = avatarID,
                  };
                  Registry.Put(state);

                  ID = avatarID;
            }


            private void Start() {
                  _avatar      = Instantiate(Resources.Load<GameObject>(_masterAvatarPath), transform, true);
                  _avatar.name = $"{gameObject.name}.avatar";
                  _animator    = _avatar.GetComponent<Animator>();

                  _avatar.AddComponent<AvatarMarker>();

                  if (_animator == null) _animator = _avatar.AddComponent<Animator>();
                  _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(_masterControllerPath);
            }


            private bool IsAlive() {
                  var s = Registry.Read(ID);

                  if (s == null) {
                        return false;
                  }

                  return s.CurrentHealth > 0;
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


            public void Rotate(Vector3 direction, bool interpolate, float speed) {
                  var target = Quaternion.LookRotation(direction);
                  _avatar.transform.rotation = interpolate
                        ? Quaternion.Lerp(_avatar.transform.rotation, target, Time.deltaTime * speed)
                        : target;
            }


            // Move avatar in direction
            // Tilt Right
            // Tilt Left
            // Foot IK
            public void Move(Vector3 direction, bool sprint) {
                  Exec(() => {
                        var move = Mathf.Max(Mathf.Abs(direction.x), Mathf.Abs(direction.z));
                        GetAnimator().SetFloat(AnimParams.Move, move);
                        GetAnimator().SetBool(AnimParams.Sprint, sprint);
                  });
            }


            // Adjust Torso to match aim direction
            // Head look at aim direction
            public void Aim(Vector3 direction) {
                  // Use this to rotate the player's head towards their target.
            }


            // Project an orb that negates damage
            public void Guard(bool guard) {
                  Exec(() => { GetAnimator().SetBool(AnimParams.Guard, guard); });
            }


            // Hold to run
            public void Evade() {
                  Exec(() =>
                        LayerInvoke(Layers.Evade, () => GetAnimator().SetTrigger(AnimParams.Evade))
                  );
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
                  Exec(() => InvokeArmament(GetArmament(LEFT_HAND_SLOT), LEFT_HAND_SLOT, () => {
                              LayerInvoke(Layers.Skills, () => {
                                    GetAnimator().SetTrigger(AnimParams.UseSkill);
                                    GetAnimator().SetInteger(AnimParams.SkillIndex, index);
                              });
                        })
                  );
            }


            private static string GetSkill(int slot) {
                  return "";
            }


            private static int IndexSkill(string name) {
                  return -1;
            }


            public void Melee() {
                  Exec(() =>
                        LayerInvoke(Layers.Melee, () => {
                              GetAnimator().SetInteger(AnimParams.MeleeIndex, 0);
                              GetAnimator().SetTrigger(AnimParams.Melee);
                        })
                  );
            }


            public void UseRightArmament() {
                  Exec(() => {
                        InvokeArmament(GetArmament(RIGHT_HAND_SLOT), RIGHT_HAND_SLOT,
                              () => LayerInvoke(Layers.Combat, () => {
                                    var i = IndexArmament(GetArmament(RIGHT_HAND_SLOT));
                                    GetAnimator().SetTrigger(AnimParams.UseRightArmament);
                                    GetAnimator().SetInteger(AnimParams.ArmamentIndex, i);
                                    GetAnimator().SetInteger(AnimParams.Hand, RIGHT_HAND_SLOT);
                              }));
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
                        InvokeArmament(GetArmament(LEFT_HAND_SLOT), LEFT_HAND_SLOT, () => {
                              LayerInvoke(Layers.Combat, () => {
                                    var i = IndexArmament(GetArmament(LEFT_HAND_SLOT));
                                    GetAnimator().SetTrigger(AnimParams.UseLeftArmament);
                                    GetAnimator().SetInteger(AnimParams.ArmamentIndex, i);
                                    GetAnimator().SetInteger(AnimParams.Hand, LEFT_HAND_SLOT);
                              });
                        });
                  });
            }


            public void CycleLeftArmament() {
                  Exec(() => {
                        GetAnimator().SetTrigger(AnimParams.UseRightArmament);
                        GetAnimator().SetInteger(AnimParams.Hand, LEFT_HAND_SLOT);
                  });
            }


            private string GetArmament(int hand) {
                  // TODO:: Implement this!
                  return hand + "weapon";
            }


            private int IndexArmament(string armamentID) {
                  // TODO:: Implement this!
                  return armamentID.GetHashCode();
            }


            public void ReloadLeft() {
                  Exec(() => {
                        LayerInvoke(Layers.Reload, () => {
                              GetAnimator().SetTrigger(AnimParams.Reload);
                              GetAnimator().SetInteger(AnimParams.Hand, LEFT_HAND_SLOT);
                        });
                  });
            }


            public void ReloadRight() {
                  Exec(() => {
                        LayerInvoke(Layers.Reload, () => {
                              GetAnimator().SetTrigger(AnimParams.Reload);
                              GetAnimator().SetInteger(AnimParams.Hand, RIGHT_HAND_SLOT);
                        });
                  });
            }


            public void Interact() {
                  Exec(() => { GetAnimator().SetTrigger(AnimParams.Interact); });
            }


            public void SetAnimState(AnimationState s) {
                  if (_animationStates.ContainsKey(s.Name)) return;
                  _animationStates.Add(s.Name, s);
            }


            public AnimationState AnimState(string stateName) {
                  if (_animationStates.ContainsKey(stateName)) return _animationStates[stateName];
                  Debug.LogWarning($"Attempting to access animation state [{stateName}] not assigned to avatar.");
                  return null;
            }


            public void InvokeAsync(Func<IEnumerator> action) {
                  StartCoroutine(action.Invoke());
            }


            private void InvokeArmament(string armamentID, int hand, Action action) {
                  var armament = Armament.Read(this, armamentID, hand);
                  if (armament == null) {
                        Debug.LogWarning($"Armament [{armamentID}] not found. Cannot invoke on avatar [{ID}].");
                        return;
                  }

                  if (armament.State().IsReady()) {
                        action.Invoke();
                  }
            }


            private void LayerInvoke(int layer, Action action, bool reset = true) {
                  GetAnimator().SetLayerWeight(layer, 1.0f);
                  action.Invoke();
                  if (reset) {
                        GetAnimator().SetLayerWeight(layer, 0.0f);
                  }
            }

      }

}