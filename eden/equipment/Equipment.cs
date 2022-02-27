using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace eden.equipment {

      public partial class Equipment : Eden.IService {

            private readonly Factory _factory;


            private Equipment(Factory factory) {
                  _factory = factory;
            }


            public Factory GetFactory() {
                  return _factory;
            }

      }

}