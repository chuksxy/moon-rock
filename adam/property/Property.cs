/*
 * Property behaviours and management.
 */

using tartarus.props;

namespace adam.property {

      public static partial class Property {


            public static class Armour {

                  public static Props.Boolean Enabled(bool enabled) {
                        return new Props.Boolean {
                              ID = "armour.enabled", Value = enabled
                        };
                  }


                  public static Props.Float Current(float current) {
                        return new Props.Float {ID = "armour.current", Value = current};
                  }


                  public static Props.Float Max(float max) {
                        return new Props.Float {ID = "armour.max", Value = max};
                  }

            }

      }

}