

/*
 * Property behaviours and management.
 */
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

      }

}