using UnityEngine;

namespace moon.rock.controller {

      public class Controller : MonoBehaviour {


            // Left analog stick to move
            public void Move(Vector3 direction) {
                  // calculate movement speed
            }


            // Right analog stick to aim
            public void Aim(Vector3 cursor) { }


            // Press L2 to fire Left hand weapon
            public void UseLeftHandWeapon() { }


            // Hold ▲ then L2 to cycle weapon in left hand
            public void CycleLeftWeapon() { }


            // Hold ▲ then R2 to cycle weapon in right hand
            public void CycleRightWeapon() { }


            // Press R2 to fire right hand weapon
            public void UseRightHandWeapon() { }


            // Press R3 to Perform Melee
            public void Melee() {
                  // R3 Multiple times to combo
            }


            // Press ⭕️ to evade
            public void Evade(Vector3 direction) {
                  // Tap ⭕️ to back step
                  // Tap ⭕️ + Left stick in direction to evade
                  // Tap ⭕️ + R1 to quick evade right
                  // Tap ⭕️ + L1 to quick evade left
            }


            // Hold L1 + ⭕ to boost forward very quickly
            // Hold ⭕ then release for full boost.
            public void Boost(bool full) { }


            // Hold L1 + ▲ to activate boosters.
            // Hold ▲ to adjust booster level. The higher the level, the more energy is consumed.
            public void ToggleBoosters(bool on, int level) { }


            // Press ❌ to jump
            public void Jump() { }


            // Hold L1 + ❌ for a high jump
            public void HighJump() { }


            // press ▲ to interact
            public void Interact() { }


            // press L3 to crouch
            public void Crouch() {
                  // crouching slightly increases suit's speed.
            }


            // Hold ◻︎ [Box] + (L2 | R2) to reload the equipped weapon in hand.
            public void Reload(int weapon) {
                  // ◻ + L2
                  // ◻ + R2
            }


            // Hold L1 + ◻︎ to reload all weapons
            public void ReloadAll() { }


            // Hold L1 to guard
            // Energy recovery is halted when guarding.
            // Shield becomes a different colour
            public void Guard() { }


            // Cycle parts left and right
            // e.g cycle right weapon or left weapon, then use the up and down arrow keys to configure firing mode.
            // e.g cycle boosters and toggle them on or off.
            // e.g cycle to missiles and use either up or down to fire them.

            public void CycleParts(int direction) { }


            // Adjust the config of the selected part or use them in some cases.
            public void ConfigurePart(int value) { }

      }

}