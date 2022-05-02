using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Avatar = moon.rock.avatar.Avatar;

namespace moon.rock.controller {

      public class PlayerController : MonoBehaviour {

            private Avatar _avatar;
            private Camera _camera;


            private void Start() {
                  _avatar = GetComponent<Avatar>();
                  if (_avatar == null) {
                        Debug.LogError("cannot assign controller to null avatar");
                  }

                  _camera = GameObject.Find("Camera").GetComponent<Camera>();
                  if (_camera == null) {
                        Debug.LogError("cannot assign controller, cannot find main camera");
                  }
            }


            // Left analog stick to move
            public void Move(InputAction.CallbackContext ctx) {
                  var direction = ctx.ReadValue<Vector2>();
                  _avatar.Move(new Vector3(direction.x, 0, direction.y), _camera.transform);
            }


            // Right analog stick to aim
            public void Aim(InputAction.CallbackContext ctx) {
                  var direction = ctx.ReadValue<Vector2>();
                  _avatar.Aim(direction, _camera.transform);
            }


            // Press L2 to fire Left hand weapon
            public void UseLeftHandWeapon(InputAction.CallbackContext ctx) {
                  _avatar.UseLeftHandWeapon(ctx.duration);
            }


            // Hold ▲ then L2 to cycle weapon in left hand
            public void CycleLeftWeapon(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  if (ctx.started) {
                        _avatar.CycleLeftWeapon();
                  }
            }


            // Press R2 to fire right hand weapon
            public void UseRightHandWeapon(InputAction.CallbackContext ctx) {
                  _avatar.UseRightHandWeapon(ctx.duration);
            }


            // Hold ▲ then R2 to cycle weapon in right hand
            public void CycleRightWeapon(InputAction.CallbackContext ctx) {
                  if (ctx.canceled) return;
                  if (ctx.started) {
                        _avatar.CycleRightWeapon();
                  }
            }


            // Press R3 to Perform Melee
            public void Melee() {
                  // R3 Multiple times to combo
            }


            // Press ⭕️ to evade
            public void Evade(InputAction.CallbackContext ctx) {
                  // Tap ⭕️ to back step
                  // Tap ⭕️ + Left stick in direction to evade
                  // Tap ⭕️ + R1 to quick evade right
                  // Tap ⭕️ + L1 to quick evade left
            }


            // Hold L1 + ⭕ to boost forward very quickly
            // Hold ⭕ then release for full boost.
            public void Boost(InputAction.CallbackContext ctx) { }


            // Hold L1 + ▲ to activate boosters.
            // Hold ▲ to adjust booster level. The higher the level, the more energy is consumed.
            public void ToggleBoosters(InputAction.CallbackContext ctx) { }


            // Press ❌ to jump
            public void Jump(InputAction.CallbackContext ctx) { }


            // Hold L1 + ❌ for a high jump
            public void HighJump(InputAction.CallbackContext ctx) { }


            // press ▲ to interact
            public void Interact(InputAction.CallbackContext ctx) { }


            // press L3 to crouch
            public void Crouch(InputAction.CallbackContext ctx) { }


            // Hold ◻︎ [Box] + (L2 | R2) to reload the equipped weapon in hand.
            public void Reload(int weapon) {
                  // ◻ + L2
                  // ◻ + R2
            }

      }

}