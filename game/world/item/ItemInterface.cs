using UnityEngine;

namespace game.world.item {

      public static partial class Item {

            public class Interface : MonoBehaviour {

                  private string _characterID = "";
                  private string _registryID  = "";

                  private Rigidbody _rigidbody;
                  private Collider  _collider;


                  internal void Init(string characterID, string registryID) {
                        _characterID = characterID;
                        _registryID  = registryID;

                        var rb  = gameObject.AddComponent<Rigidbody>();
                        var col = gameObject.AddComponent<Collider>();
                        
                        // Configure according to data
                  }

            }

      }

}