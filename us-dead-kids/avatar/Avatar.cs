using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using us_dead_kids.armament;
using us_dead_kids.environment;
using AnimationState = us_dead_kids.lib.animation.AnimationState;
using Environment = us_dead_kids.environment.Environment;

namespace us_dead_kids.avatar {

      // KISS
      public class Avatar : MonoBehaviour {

            public const int LEFT_HAND_SLOT  = 0;
            public const int RIGHT_HAND_SLOT = 1;

            private Animator   _animator;
            private GameObject _avatar;

            private readonly Dictionary<int, AnimationState> _animationStates = new();
            private readonly Dictionary<int, bool>           _animationLocks  = new();

            public string ID { get; private set; } = "no.id.assigned";

            public Armament LeftArmament  { get; set; }
            public Armament RightArmament { get; set; }

            public List<Avatar> Targets      { get; set; }
            public bool         TrackTargets { get; set; }

            private static class AnimParams {

                  public static readonly int Move          = Animator.StringToHash("Move");
                  public static readonly int Sprint        = Animator.StringToHash("Sprint");
                  public static readonly int Evade         = Animator.StringToHash("Evade");
                  public static readonly int Guard         = Animator.StringToHash("Guard");
                  public static readonly int Fire          = Animator.StringToHash("Fire");
                  public static readonly int Hand          = Animator.StringToHash("Hand");
                  public static readonly int ArmamentIndex = Animator.StringToHash("Armament Index");
                  public static readonly int Melee         = Animator.StringToHash("Melee");
                  public static readonly int MeleeIndex    = Animator.StringToHash("Melee Index");
                  public static readonly int UseSkill      = Animator.StringToHash("Use Skill");
                  public static readonly int SkillIndex    = Animator.StringToHash("Skill Index");
                  public static readonly int UseItem       = Animator.StringToHash("Use Item");
                  public static readonly int ItemIndex     = Animator.StringToHash("Item Index");
                  public static readonly int Interact      = Animator.StringToHash("Interact");
                  public static readonly int Reload        = Animator.StringToHash("Reload");

            }


            private static class Locks {

                  public const int Base         = 0;
                  public const int L_FIRE       = 1;
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
                  var existing = AvatarRegistry.Read(state.ID);
                  if (existing != null) {
                        Debug.LogWarning($"Avatar with ID [{state.ID}] already exists.");
                        return;
                  }

                  AvatarRegistry.Put(state);
                  ID = state.ID;
            }


            public void Load(string avatarID) {
                  var state = AvatarRegistry.Read(avatarID);
                  if (state != null) return;

                  Debug.LogWarning($"Avatar [{avatarID}] does not exist in registry, creating one.");
                  state = new AvatarState {
                        ID             = avatarID,
                        Name           = $"avatar_{avatarID}.transient",
                        CurrentHealth  = 200,
                        MaxHealth      = 200,
                        MaxSpeed       = 12,
                        CurrentSpeed   = 12,
                        MaxStamina     = 162,
                        CurrentStamina = 162,
                        Equipment =
                              new List<string>() {"white.distressed.vest", "isengrim.distressed.shorts", "neon.ape.runners"},
                        Items = new List<string>() {"half-smoked-spliff.wet", "broken-weed-grinder", "limitless-pill.half"},
                  };
                  AvatarRegistry.Put(state);

                  ID = avatarID;
            }


            private void Start() {
                  _avatar      = Instantiate(Resources.Load<GameObject>(_masterAvatarPath), transform, true);
                  _avatar.name = $"{gameObject.name}.avatar";
                  _animator    = _avatar.GetComponent<Animator>();

                  _avatar.AddComponent<AvatarMarker>();

                  if (_animator == null) _animator = _avatar.AddComponent<Animator>();
                  _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(_masterControllerPath);

                  // TODO:: Remove this shit.
                  Load(gameObject.name);
            }


            private bool IsAlive() {
                  var s = AvatarRegistry.Read(ID);

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


            public void Rotate(Vector2 direction, bool interpolate, float speed) {
                  Rotate(new Vector3(direction.x, 0, direction.y), interpolate, speed);
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
                        Lock(Locks.Evade, () => GetAnimator().SetTrigger(AnimParams.Evade))
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
                              Lock(Locks.Skills, () => {
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
                        Lock(Locks.Melee, () => {
                              GetAnimator().SetInteger(AnimParams.MeleeIndex, 0);
                              GetAnimator().SetTrigger(AnimParams.Melee);
                        })
                  );
            }


            public void RightFire() {
                  const float lockDuration = 0.25f;
                  Exec(() => {
                        InvokeArmament(GetArmament(RIGHT_HAND_SLOT), RIGHT_HAND_SLOT, () =>
                              Lock(GetAnimator().GetLayerIndex("Fire"), () => {
                                          var i = IndexArmament(GetArmament(RIGHT_HAND_SLOT));
                                          GetAnimator().SetTrigger(AnimParams.Fire);
                                          GetAnimator().SetInteger(AnimParams.ArmamentIndex, i);
                                          GetAnimator().SetInteger(AnimParams.Hand, RIGHT_HAND_SLOT);
                                    },
                                    lockDuration));
                  });
            }


            public void CycleRightArmament() {
                  Exec(() => {
                        GetAnimator().SetTrigger(AnimParams.Fire);
                        GetAnimator().SetInteger(AnimParams.Hand, RIGHT_HAND_SLOT);
                  });
            }


            public void LeftFire() {
                  const float lockDuration = 0.25f;
                  Exec(() => {
                        InvokeArmament(GetArmament(LEFT_HAND_SLOT), LEFT_HAND_SLOT, () => {
                              Lock(GetAnimator().GetLayerIndex("Fire"), () => {
                                          var i = IndexArmament(GetArmament(LEFT_HAND_SLOT));
                                          GetAnimator().SetTrigger(AnimParams.Fire);
                                          GetAnimator().SetInteger(AnimParams.ArmamentIndex, i);
                                          GetAnimator().SetInteger(AnimParams.Hand, LEFT_HAND_SLOT);
                                    },
                                    lockDuration);
                        });
                  });
            }


            public void CycleLeftArmament() {
                  Exec(() => {
                        GetAnimator().SetTrigger(AnimParams.Fire);
                        GetAnimator().SetInteger(AnimParams.Hand, LEFT_HAND_SLOT);
                  });
            }


            private string GetArmament(int hand) {
                  // TODO:: Implement this!
                  return $"weapon.{hand}";
            }


            private int IndexArmament(string armamentID) {
                  // TODO:: Implement this!
                  return 0;
            }


            public void ReloadLeft() {
                  Exec(() => {
                        Lock(Locks.Reload, () => {
                              GetAnimator().SetTrigger(AnimParams.Reload);
                              GetAnimator().SetInteger(AnimParams.Hand, LEFT_HAND_SLOT);
                        });
                  });
            }


            public void ReloadRight() {
                  Exec(() => {
                        Lock(Locks.Reload, () => {
                              GetAnimator().SetTrigger(AnimParams.Reload);
                              GetAnimator().SetInteger(AnimParams.Hand, RIGHT_HAND_SLOT);
                        });
                  });
            }


            public void Interact() {
                  Exec(() => { GetAnimator().SetTrigger(AnimParams.Interact); });
            }


            public void SetAnimState(AnimationState s) {
                  if (_animationStates.ContainsKey(s.StateHash)) return;
                  _animationStates.Add(s.StateHash, s);
            }


            public AnimationState AnimState(int nameHash) {
                  if (_animationStates.ContainsKey(nameHash)) return _animationStates[nameHash];
                  Debug.LogWarning($"Attempting to access animation state [{nameHash}] not assigned to avatar.");
                  return null;
            }


            public void InvokeCoroutine(Func<IEnumerator> action) {
                  StartCoroutine(action.Invoke());
            }


            private void InvokeArmament(string armamentID, int hand, Action action) {
                  if (Environment.Current == Env.STAGING) {
                        // TODO:: Implement and remove this
                        action.Invoke();
                  }

                  var armament = Armament.Read(this, armamentID, hand);
                  if (armament == null) {
                        Debug.LogWarning($"Armament [{armamentID}] not found. Cannot invoke on avatar [{ID}].");
                        return;
                  }

                  if (armament.State().IsReady()) {
                        action.Invoke();
                  }
            }


            private void Lock(int lockID, Action action, float duration = 0.05f, bool reset = true) {
                  if (IsLocked(lockID)) {
                        return;
                  }

                  if (GetAnimator().GetLayerWeight(lockID) < 1.0f) {
                        GetAnimator().SetLayerWeight(lockID, 1.0f);
                  }

                  action.Invoke();

                  if (!reset) return;

                  IEnumerator Reset() {
                        yield return new WaitForSeconds(duration);

                        if (IsLocked(lockID)) {
                              Debug.Log($"unlocking layer [{lockID}] for [{_avatar.name}]");
                              ToggleLock(lockID, false);
                        }
                  }

                  StartCoroutine(Reset());
            }


            private bool IsLocked(int lockID) {
                  return _animationLocks.ContainsKey(lockID) && _animationLocks[lockID];
            }


            public void ToggleLock(int lockID, bool value) {
                  if (!_animationLocks.ContainsKey(lockID)) {
                        _animationLocks.Add(lockID, value);
                        return;
                  }

                  _animationLocks[lockID] = value;
            }

      }

}