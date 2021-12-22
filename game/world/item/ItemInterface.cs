using UnityEngine;

namespace game.world.item {

      public static partial class Item {

            public class Interface : MonoBehaviour {

                  private Rigidbody _rigidbody;
                  private Collider  _collider;


                  internal void Init(string characterID) {
                        var rb  = gameObject.AddComponent<Rigidbody>();
                        var col = gameObject.AddComponent<Collider>();
                        // Configure according to data
                  }

            }

      }

}