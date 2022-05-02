using UnityEngine;

namespace moon.rock.avatar {

      public partial class Avatar {

            public delegate void OnMoved(Avatar avatar, Vector3 direction);

            public delegate void OnJumped(Avatar avatar, Vector3 direction);

            public OnMoved  MovedEvents  { get; set; }
            public OnJumped JumpedEvents { get; set; }


            private void InitEventBus() {
                  MovedEvents  = (a, dir) => { };
                  JumpedEvents = (a, dir) => { };
            }

      }

}