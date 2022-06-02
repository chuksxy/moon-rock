using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Avatar = us_dead_kids.avatar.Avatar;

namespace us_dead_kids.controller {

      public class PlayerController : MonoBehaviour {

            private const float ROT_SPEED = 12.0f;

            private Avatar _avatar;


            private void Start() {
                  _avatar = GameObject.FindWithTag("Player")?.GetComponent<Avatar>();
                  if (_avatar == null) {
                        Debug.LogWarning($"Attempting to control null avatar assigned to [{name}]");
                  }
            }


            // Left analog stick to move
            public void Move(InputAction.CallbackContext ctx) {
                  var direction = ctx.ReadValue<Vector2>();
                  if (direction != Vector2.zero) {
                        _avatar.Rotate(direction, true, ROT_SPEED * 3.0f);
                  }

                  _avatar.Move(new Vector3(direction.x, 0, direction.y), false);
            }


            // Right analog stick to aim
            public void Aim(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  // Allow y height aiming
                  _avatar.Rotate(ctx.ReadValue<Vector2>(), true, ROT_SPEED * 2.0f);
            }


            // Press L2 to fire Left hand weapon
            public void LeftFire(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  if (ctx.started) {
                        _avatar.LeftFire();
                  }
            }


            // Hold ▲ then L2 to cycle weapon in left hand
            public void CycleLeftWeapon(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  if (ctx.started) {
                        _avatar.CycleLeftArmament();
                  }
            }


            // Press R2 to fire right hand weapon
            public void RightFire(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  if (ctx.started) {
                        _avatar.RightFire();
                  }
            }


            // Hold ▲ then R2 to cycle weapon in right hand
            public void CycleRightWeapon(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  if (ctx.started) {
                        _avatar.CycleRightArmament();
                  }
            }


            public void Melee(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  _avatar.Melee();
            }


            public void Guard(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  _avatar.Guard(true);
            }


            // Press ⭕️ to evade
            public void Evade(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  // Tap ⭕️ to back step
                  // Tap ⭕️ + Left stick in direction to evade
                  _avatar.Evade();
            }


            // press ▲ to interact
            public void Interact(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  _avatar.Interact();
            }


            // Hold ◻︎ [Box] + (L2 | R2) to reload the equipped weapon in hand.
            public void Reload(InputAction.CallbackContext ctx) {
                  // ◻ + L2
                  // ◻ + R2
                  if (ctx.canceled) return;
                  _avatar.ReloadLeft();
                  _avatar.ReloadRight();
            }

      }

}