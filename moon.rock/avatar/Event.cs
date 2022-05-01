using UnityEngine;

namespace moon.rock.avatar {

      public partial class Avatar {

            public delegate void OnMoved(Avatar avatar, Vector3 direction);

            public delegate void OnJumped(Avatar avatar, Vector3 direction);

            public OnMoved  Moved  { get; set; }
            public OnJumped Jumped { get; set; }


            private void InitEventBus() {
                  Moved  = (a, dir) => { };
                  Jumped = (a, dir) => { };
            }

      }

}