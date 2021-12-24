using System.Collections.Generic;
using UnityEngine;

/*
 * Properties of Objects that exist in the world and their respective modifiers.
 */
namespace game.world.property {

      public partial class Property {

            // I Have Weight, so I am affected by gravity and physics.
            public interface IHaveWeight {

                  float Weight { get; }

            }

            // I Can Stack, so I can be stacked with other items in the same category/slot.
            public interface ICanStack {

                  bool CanStack { get; }

            }

            // I Am an Object in this world, so I should persist within this world.
            public interface IAmAnObject { }

            public interface IModify<in TInput, out TOutput> {

                  string  ID { get; }
                  TOutput Apply(TInput health);

            }

            public class IdentityModifier : IModify<object, object> {

                  public const string NAME = "modifier.identity";

                  public static readonly IdentityModifier Identity = new IdentityModifier();

                  public string ID => NAME;


                  public object Apply(object health) {
                        return health;
                  }

            }

            public class Health {

                  public int Max     { get; set; }
                  public int Current { get; set; }

                  public string[] Events    { get; set; }
                  public string[] Modifiers { get; set; }

            }

            public class MaxHealthMultiplier : IModify<Health, Health> {

                  public const string NAME = "multiplier.health.max";

                  public string ID         => NAME;
                  public float  Multiplier { get; set; }
                  public bool   Restore    { get; set; }


                  // Apply Multiplier to max health and restore current health once.
                  public Health Apply(Health health) {
                        if (health.Current <= 0 || health.Max <= 0) {
                              return health;
                        }

                        var maxHealth = Mathf.RoundToInt(health.Max * Multiplier);
                        if (!Restore)
                              return new Health {
                                    Max = maxHealth, Current = maxHealth, Modifiers = new string[1] {NAME}
                              };

                        Restore = false;
                        return new Health {
                              Max = maxHealth, Current = health.Current, Modifiers = new string[1] {NAME}
                        };
                  }

            }

            public class Energy {

                  public float Max     { get; set; }
                  public float Current { get; set; }

                  public string[] Events    { get; set; }
                  public string[] Modifiers { get; set; }

            }

            public class MaxEnergyMultiplier : IModify<Energy, Energy> {

                  public const string NAME = "multiplier.energy.max";

                  public  string ID         => NAME;
                  private float  Multiplier { get; set; }
                  private bool   Restore    { get; set; }


                  // Apply Multiplier to max energy and restore max energy once.
                  public Energy Apply(Energy energy) {
                        if (energy.Current <= 0.0f || energy.Max <= 0.0f) {
                              return energy;
                        }

                        var maxEnergy = energy.Max * Multiplier;
                        if (!Restore)
                              return new Energy {
                                    Max = maxEnergy, Current = energy.Current, Modifiers = new string[1] {NAME}
                              };

                        Restore = false;
                        return new Energy {
                              Max = maxEnergy, Current = maxEnergy, Modifiers = new string[1] {NAME}
                        };
                  }

            }

      }

}