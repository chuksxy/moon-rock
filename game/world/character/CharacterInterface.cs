using game.world.item;
using UnityEngine;

// Single Writer
namespace game.world.character {

      public static partial class Character {

            public class Interface : MonoBehaviour {

                  private Animator _animator;

                  private CharacterController _characterController;
                  private string              _characterID = "";
                  private string              _registryID  = "";
                  private Rigidbody[]         _rigidbodies;

                  public void SetPosition(Vector3 position) { }

                  public void ResetCharacter() { }


                  public void Move(Vector3 direction, float modifier) {
                        Character.Move(this, direction, modifier);
                  }


                  public void Jump(Vector3 direction, float modifier) {
                        Character.Jump(this, direction, modifier);
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


                  internal Interface Init(Character.Data data, string characterID, string registryID, string zoneId) {
                        RegisterCharacter(data, characterID, registryID, zoneId);
                        AssembleCharacter(data);
                        return this;
                  }


                  internal string GetCharacterID() {
                        return _characterID;
                  }


                  internal string GetRegistryID() {
                        return _registryID;
                  }


                  internal CharacterController GetController() {
                        return _characterController;
                  }


                  internal Animator GetAnimator() {
                        return _animator;
                  }


                  private void RegisterCharacter(Character.Data data, string characterID, string registryID, string zoneId) {
                        _characterID = characterID;
                        _registryID  = registryID;

                        WorldRegistry.GetRegistry(registryID).RegisterCharacter(characterID, zoneId, data);
                  }


                  private void AssembleCharacter(Character.Data data) { }

            }

      }

}