using System.Collections.Generic;
using us_dead_kids.avatar;

namespace us_dead_kids.lib.lock_on {

      public static class Target {

            public static void Clear(Avatar caller) {
                  if (caller != null) {
                        caller.Targets = new List<Avatar>();
                  }
            }


            public static void Populate(Avatar caller) {
                  if (caller != null) {
                        
                  }
            }


            public static Avatar Closest(List<Avatar> targets) {
                  return null;
            }


      }

}