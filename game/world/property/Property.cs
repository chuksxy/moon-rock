using System;
using System.Collections.Generic;

/*
 * Property behaviours and management.
 */
namespace game.world.property {

      public static partial class Property {
            
            
            // Get Modifier for property by it's ID.
            public static IModify<TIn, TOut> GetModifier<TIn, TOut>(string propertyID) {
                  if (!AllModifiers.ContainsKey(propertyID)) {
                        return IdentityModifier.Identity as IModify<TIn, TOut>;
                  }

                  return AllModifiers[propertyID] as IModify<TIn, TOut>;
            }

      }

}