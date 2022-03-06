using eden;

namespace us_dead_kids.equipment {

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