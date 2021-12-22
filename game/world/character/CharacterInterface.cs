using game.world.item;
using UnityEngine;

// Single Writer
namespace game.world.character {
      public class CharacterInterface : MonoBehaviour {
            private readonly string _characterID;
            private readonly string _registryID;
            private readonly string _zoneId;

            public void SetPosition(Vector3 position) { }

            public void ResetCharacter() { }

            public void Move(Vector3 direction) {
                  var characterData = CharacterRegistry.GetRegistry(_registryID).GetData(_characterID);
                  var speed         = Character.EvaluateMovementSpeed(characterData);

                  // MoveCharacter
            }


            public void Jump() {
                  var character = CharacterRegistry.GetRegistry(_registryID).GetData(_characterID);
                  var jumpSpeed = Character.EvaluateJumpSpeed(character);
            }

            public void UseLeftSleeve()  { }
            public void UseRightSleeve() { }

            public void UseItem() { }

            public void CycleRightPod() { }

            public void CycleLeftPod() { }

            public void Hover() { }
            public void Dodge() { }

            public void Interact() { }

            public Hat SwapHat(int index) {
                  return new Hat();
            }

            public BaseLayer SwapBaseLayer(int index) {
                  return new BaseLayer();
            }

            public Sleeve SwapLeftSleeve(int index) {
                  return new Sleeve();
            }

            public Sleeve SwapRightSleeve(int index) {
                  return new Sleeve();
            }

            public OuterWear SwapOuterWear(int index) {
                  return new OuterWear();
            }

            public Shoes SwapShoes(int index) {
                  return new Shoes();
            }

            public void HandleDamageToHead()         { }
            public void HandleDamageToBody()         { }
            public void HandleDamageToLeftSleeve()   { }
            public void HandleDamageToRightSleeve()  { }
            public void HandleDamageToFeet()         { }
            public void HandleFatalDamage()          { }
            public void HandleDamageToPod(int index) { }
      }
}