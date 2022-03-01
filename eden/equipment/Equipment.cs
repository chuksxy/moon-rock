using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace eden.equipment {

      public partial class Equipment : Eden.IService {

            private readonly Service _service;


            private Equipment(Service service) {
                  _service = service;
            }


            public Service Tables() {
                  return _service;
            }

      }

}