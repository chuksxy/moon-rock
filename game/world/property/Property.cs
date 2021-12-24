using UnityEngine;

namespace game.world.property {

      public static partial class Property {

            public interface IHaveWeight {

                  float Weight { get; }

            }

            public interface ICanStack {

                  bool CanStack { get; }

            }

            public interface IAmAnObject { }

            public interface IModify<in TInput, out TOutput> {

                  string  ID { get; }
                  TOutput Apply(TInput energy);

            }

            public class Health {

                  public int      Max       { get; set; }
                  public int      Current   { get; set; }
                  public string[] Modifiers { get; set; }

            }

            public class MaxHealthMultiplier : IModify<Health, Health> {

                  private const string NAME = "multiplier.health.max";
                  public        string ID => NAME;

                  public float Multiplier { get; set; }
                  public bool  Restore    { get; set; }


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

                  public float    Max       { get; set; }
                  public float    Current   { get; set; }
                  public string[] Modifiers { get; set; }

            }

            public class MaxEnergyMultiplier : IModify<Energy, Energy> {

                  private const string NAME = "multiplier.energy.max";

                  public string ID => NAME;

                  private float Multiplier { get; set; }
                  private bool  Restore    { get; set; }


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