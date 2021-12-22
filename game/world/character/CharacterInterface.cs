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


                  // Set Position of character to the specified point in the world.
                  public void SetPosition(Vector3 position) { }


                  // Reset Character to base state by restoring max health and energy. 
                  public void ResetCharacter() { }


                  // Move character in direction while applying the movement speed modifier.
                  public void Move(Vector3 direction, float modifier) {
                        Character.Move(this, direction, modifier);
                  }


                  // Jump in direction while applying the jump speed modifier.
                  public void Jump(Vector3 direction, float modifier) {
                        Character.Jump(this, direction, modifier);
                  }


                  // Use Left Sleeve Weapon.
                  public void UseLeftSleeve() { }


                  // Use Right Sleeve Weapon.
                  public void UseRightSleeve() { }


                  // Use Current Pod Item.
                  public void UseItem() { }


                  // Cycle Right Pod on the Right Sleeve.
                  public void CycleRightPod() { }


                  // Cycle Left Pod on the Left Sleeve.
                  public void CycleLeftPod() { }


                  // Hover while jumping or falling.
                  public void Hover() { }


                  // Dodge in direction and apply dodge speed modifier.
                  public void Dodge(Vector3 direction, float modifier) { }


                  // Boost in direction and apply boost speed modifier.
                  public void Boost(Vector3 direction, float modifier) { }


                  // Interact with objects in the world.
                  public void Interact() { }


                  // Swap Hat such as Balaclavas, Shades, Masks and Scarves in stack.
                  public Item.Hat SwapHat(int index) {
                        return new Item.Hat();
                  }


                  // Swap Base Layer such as T-shirts, Compression Layers and Vests in stack.
                  public Item.BaseLayer SwapBaseLayer(int index) {
                        return new Item.BaseLayer();
                  }


                  // Swap Left Sleeve in stack.
                  public Item.Sleeve SwapLeftSleeve(int index) {
                        return new Item.Sleeve();
                  }


                  // Swap Right Sleeve in stack.
                  public Item.Sleeve SwapRightSleeve(int index) {
                        return new Item.Sleeve();
                  }


                  // Swap Outer Wear such as Jackets, Sweaters and Coats in the stack.
                  public Item.OuterWear SwapOuterWear(int index) {
                        return new Item.OuterWear();
                  }


                  // Swap Shoes such as Socks, Feet, Shoes and Boots in the stack.
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