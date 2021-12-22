using game.world.item;
using UnityEngine;

// Single Writer
namespace game.world.character {

      public static partial class Character {

            public class Interface : MonoBehaviour {

                  private Animator            _animator;
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


                  public Item.Hat SwapHat(int index) {
                        return new Item.Hat();
                  }


                  public Item.BaseLayer SwapBaseLayer(int index) {
                        return new Item.BaseLayer();
                  }


                  public Item.Sleeve SwapLeftSleeve(int index) {
                        return new Item.Sleeve();
                  }


                  public Item.Sleeve SwapRightSleeve(int index) {
                        return new Item.Sleeve();
                  }


                  public Item.OuterWear SwapOuterWear(int index) {
                        return new Item.OuterWear();
                  }


                  public Item.Shoes SwapShoes(int index) {
                        return new Item.Shoes();
                  }


                  public void HandleDamageToHead()         { }
                  public void HandleDamageToBody()         { }
                  public void HandleDamageToLeftSleeve()   { }
                  public void HandleDamageToRightSleeve()  { }
                  public void HandleDamageToFeet()         { }
                  public void HandleFatalDamage()          { }
                  public void HandleDamageToPod(int index) { }


                  internal Interface Init(Data data, string registryID, string zoneId = "main.zone") {
                        RegisterCharacter(data, registryID, zoneId);
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


                  private void RegisterCharacter(Data data, string registryID, string zoneId) {
                        _characterID = data.ID;
                        _registryID  = registryID;

                        WorldRegistry.GetRegistry(registryID).RegisterCharacter(_characterID, zoneId, data);
                  }


                  private void AssembleCharacter(Data data) {
                        _animator            = gameObject.AddComponent<Animator>();
                        _characterController = gameObject.AddComponent<CharacterController>();
                  }

            }

      }

}