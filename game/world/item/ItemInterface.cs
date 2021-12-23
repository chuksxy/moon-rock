using game.world.character;
using UnityEngine;

namespace game.world.item {

      public static partial class Item {

            public class Interface : MonoBehaviour {

                  private string _itemID      = "no.item.ID";
                  private string _characterID = "no.character.ID";
                  private string _registryID  = "main.registry";
                  private Slot   _slotID      = Slot.None;
                  private int    _slotIndex   = -1;

                  private Rigidbody _rigidbody;
                  private Collider  _collider;


                  // Init item that has already been assigned to a character.
                  internal Interface Init(Slot slotID, int slotIndex, string characterID, string registryID) {
                        _characterID = characterID;
                        _registryID  = registryID;
                        _slotID      = slotID;
                        _slotIndex   = slotIndex;

                        var rb  = gameObject.AddComponent<Rigidbody>();
                        var col = gameObject.AddComponent<Collider>();

                        // Configure according to data
                        return this;
                  }


                  // Init item in the world.
                  internal Interface Init(Slot slotID, string itemID, string registryID) {
                        _slotID     = slotID;
                        _itemID     = itemID;
                        _registryID = registryID;
                        return this;
                  }

            }

      }

}