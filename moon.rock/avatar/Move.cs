using UnityEngine;

namespace moon.rock.avatar {

      public partial class Avatar {


            public void Move(Vector3 direction) {
                  // calculate movement speed
                  if (BoostersActivated()) {
                        _rb.AddForce(direction * MaxSpeed());
                  }
                  else {
                        _rb.AddForce(direction * MinSpeed());
                  }

                  if (_rb.velocity.magnitude >= MaxSpeed()) {
                        // How am I approaching locomotion???
                  }

                  Debug.Log($"I am moving in direction ({direction.x}, {direction.y}, {direction.z})");
                  MovedEvents(this, direction);
            }


      }

}