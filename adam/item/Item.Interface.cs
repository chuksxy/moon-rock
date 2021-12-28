using adam.props;
using moon.rock.world;
using UnityEngine;

namespace adam.item {

      public static partial class Item {

            public class Interface : MonoBehaviour {

                  private string   _characterID = "no.character.ID";
                  private Collider _collider;

                  private string _itemID     = "no.item.ID";
                  private string _objectID   = "no.object.ID";
                  private string _registryID = "main.registry";

                  private Rigidbody _rigidbody;
                  private Slot      _slotID    = Slot.None;
                  private int       _slotIndex = -1;


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
                  internal Interface Init(
                        Slot                 slotID,
                        string               objectID,
                        string               itemID,
                        string               registryID,
                        string               zoneID,
                        Props.IAmAnObject @object
                  ) {
                        _slotID     = slotID;
                        _objectID   = objectID;
                        _itemID     = itemID;
                        _registryID = registryID;

                        World.Registry.Get(registryID).RegisterObject(objectID, zoneID, @object);

                        return this;
                  }

            }

      }

}