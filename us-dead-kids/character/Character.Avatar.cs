using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace us_dead_kids.character {

      public partial class Character {

            public class Avatar : MonoBehaviour {

                  [SerializeField] private string characterID;

                  private Animator _animator;

                  private static class Cache {

                        public static readonly Dictionary<string, Avatar> Store = new Dictionary<string, Avatar>();

                  }


                  // Setup Character's animation controller at runtime.
                  private static bool SetupAnimator(GameObject g) {
                        var animatorController =
                              Resources.Load("master_controller.controller");
                        if (animatorController == null) {
                              Debug.LogWarning("animation controller not found when generating character.");
                              return false;
                        }

                        var animator = g.AddComponent<Animator>();
                        animator.runtimeAnimatorController = animatorController as RuntimeAnimatorController;

                        return true;
                  }


                  // Setup character's rigid body as a child game object.
                  private static void SetupRigidBody(GameObject parent) {
                        var g = new GameObject();
                        g.transform.SetParent(parent.transform);
                        g.AddComponent<Rigidbody>();
                  }


                  // Create Avatar from character declaration
                  public static Avatar Create(Character c) {
                        var g = new GameObject();

                        if (!SetupAnimator(g)) return null;
                        SetupRigidBody(g);

                        var avatar = g.AddComponent<Avatar>();
                        if (string.IsNullOrEmpty(c.ID)) {
                              Debug.LogWarning("character ID cannot be blank");
                              return null;
                        }

                        avatar.characterID     = c.ID;
                        avatar.gameObject.name = avatar.characterID;

                        Cache.Store.Add(avatar.characterID, avatar);
                        return avatar;
                  }


                  // Get Avatar by [characterID].
                  internal static Avatar Get(string characterID) {
                        return Cache.Store.ContainsKey(characterID) ? Cache.Store[characterID] : null;
                  }


                  // Invoke [action] on a character if present.
                  public static void Invoke(string characterID, Action<Avatar> action) {
                        var avatar = Get(characterID);
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


                  // Move Avatar in direction.
                  public void Move(Vector3 direction) {
                        Service.Move(characterID, direction);
                  }


                  // Interact with an entity in the world.
                  // Pick up weapons in the world. E.g Picking up a Great-sword will change your melee move-set.
                  public void Interact() {
                        Service.Interact(characterID);
                  }


                  // Use Melee Attack.
                  public void UseMelee() {
                        Service.UseMeleeWeapon(characterID);
                  }


                  // Use Skill in the specified slot.
                  // 1 - Melee
                  // 2 - Gun Skill
                  // 3 -
                  // 4 - Grenade?
                  public void UseSkill(int slot) {
                        Service.UseSkill(characterID, slot);
                  }


                  public void UsePrimaryWeapon() {
                        Service.UsePrimaryWeapon(characterID);
                  }


                  // Use Secondary Weapon.
                  public void UseSecondaryWeapon() {
                        Service.UseSecondaryWeapon(characterID);
                  }


                  // Use Item equipped in slot
                  public void UseItem(int x, int y) {
                        Service.UseItem(characterID, x, y);
                  }


                  // Select Item in item slot.
                  public void SelectItem(int x, int y) { }


                  // Dodge an incoming attack.
                  public void Dodge(Vector3 direction) {
                        Service.Dodge(characterID, direction);
                  }


                  // Guard an incoming attack.
                  public void Guard() {
                        Service.Guard(characterID);
                  }


                  // Take Damage from an entity in the world.
                  public void TakeDamage(string culpritID) { }


                  // Set state to Alive or Dead.
                  public void SetState(bool alive) { }

            }

      }

}