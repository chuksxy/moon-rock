/*
 * Property behaviours and management.
 */

using tartarus.props;

namespace moon.rock.world.property {

      public static partial class Property {


            // Get Modifier for property by it's ID.
            public static IModify<TIn, TOut> GetModifier<TIn, TOut>(string modifierID) {
                  return modifierID switch {
                        MaxHealthMultiplier.NAME => new MaxHealthMultiplier() as IModify<TIn, TOut>,
                        MaxEnergyMultiplier.NAME => new MaxEnergyMultiplier() as IModify<TIn, TOut>,
                        _                        => IdentityModifier.Identity as IModify<TIn, TOut>
                  };
            }


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