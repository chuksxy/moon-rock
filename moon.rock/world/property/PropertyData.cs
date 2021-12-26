using System.Collections.Generic;
using System.Linq;

/*
 * Properties of Objects that exist in the world and their respective modifiers.
 */
namespace moon.rock.world.property {

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

                  public float Max     { get; set; }
                  public float Current { get; set; }

                  public bool                Disabled  { get; set; }
                  public Queue<IHealthEvent> Events    { get; set; }
                  public List<string>        Modifiers { get; set; }


                  // Restore Health to amount, or to full health.
                  public Health Restore(bool full, float value) {
                        Events.Enqueue(new Restored {Value = value, Full = full});
                        return this;
                  }


                  // Decrease Health by amount specified by appending a decrease event in the `Event Log`.
                  public Health Decrease(float amount) {
                        Events.Enqueue(new Decreased {Value = amount});
                        return this;
                  }


                  // Increase Health by amount specified by appending an increase event in the `Event Log`.
                  public Health Increase(float amount) {
                        Events.Enqueue(new Increased {Value = amount});
                        return this;
                  }


                  // Increase Health by amount specified by appending an increase event in the `Event Log`.
                  public Health MaxIncreaseBy(float amount) {
                        Events.Enqueue(new Increased {Value = amount});
                        return this;
                  }


                  // Read Current Health by evaluating all events in the log.
                  public float ReadCurrent() {
                        return Read().Current;
                  }


                  // Read Max Health by evaluating all events in the log.
                  public float ReadMax() {
                        return Read().Max;
                  }


                  // Read Health Snapshot with current and max evaluated then populated.
                  public Health Read() {
                        return Events.Aggregate(new Health(), (acc, healthEvent) => healthEvent.Eval(acc, healthEvent.Value));
                  }


                  internal Health UpdateCurrent(float value) {
                        return new Health {
                              Current = value, Max = Max, Disabled = Disabled, Events = Events, Modifiers = Modifiers
                        };
                  }


                  internal Health UpdateMax(float value) {
                        return new Health {
                              Current = Current, Max = value, Disabled = Disabled, Events = Events, Modifiers = Modifiers
                        };
                  }


                  internal Health UpdateEvents(Queue<IHealthEvent> value) {
                        return new Health {
                              Current = Current, Max = Max, Disabled = Disabled, Events = value, Modifiers = Modifiers
                        };
                  }


                  internal Health UpdateDisabled(bool value) {
                        return new Health {
                              Current = Current, Max = Max, Disabled = value, Events = Events, Modifiers = Modifiers
                        };
                  }


                  internal Health UpdateModifiers(List<string> value) {
                        return new Health {
                              Current = Current, Max = Max, Disabled = Disabled, Events = Events, Modifiers = value
                        };
                  }


                  public interface IHealthEvent {

                        string ID { get; }

                        float Value { get; }


                        // Eval current health and max by replaying all event, then set them respectively.
                        Health Eval(Health current, float value);

                  }

                  public struct Restored : IHealthEvent {

                        public string ID    => "health.current.restore.";
                        public float  Value { get; set; }


                        public Health Eval(Health current, float value) {
                              return Full
                                    ? current.UpdateCurrent(current.Max)
                                    : current.UpdateCurrent(value);
                        }


                        public bool Full { get; set; }

                  }

                  public struct Decreased : IHealthEvent {

                        private const string EVENT_ID = "health.current.decreased.";
                        public        string ID    => EVENT_ID;
                        public        float  Value { get; set; }


                        public Health Eval(Health current, float value) {
                              return current.UpdateCurrent(current.Current - value);
                        }

                  }

                  public struct Increased : IHealthEvent {

                        private const string EVENT_ID = "health.current.increased.";
                        public        string ID    => EVENT_ID;
                        public        float  Value { get; set; }


                        public Health Eval(Health current, float value) {
                              return current.UpdateCurrent(current.Current + value);
                        }

                  }

                  public struct MaxDecreasedBy : IHealthEvent {

                        private const string EVENT_ID = "health.max.decreased.";

                        public string ID => EVENT_ID;

                        // This should already be evaluated to (0.values) such as 0.25, 0.95;
                        public float Value { get; set; }


                        public Health Eval(Health current, float percent) {
                              return current.UpdateMax(current.Max - percent * current.Max);
                        }

                  }

                  public struct MaxIncreasedBy : IHealthEvent {

                        private const string EVENT_ID = "health.max.increased.";

                        public string ID => EVENT_ID;

                        // This should already be evaluated to (0.values) such as 0.25, 0.95;
                        public float Value { get; set; }


                        public Health Eval(Health current, float value) {
                              return current.UpdateMax(current.Max + value * current.Max);
                        }

                  }

            }

            public class MaxHealthMultiplier : IModify<Health, Health> {

                  public const string NAME = "multiplier.health.max";
                  public       float  Multiplier { get; set; }
                  public       bool   Restore    { get; set; }

                  public string ID => NAME;


                  // Apply Multiplier to max health and restore current health once.
                  public Health Apply(Health health) {
                        if (health.Disabled) return health;

                        return Restore ? health.Restore(true, 1).MaxIncreaseBy(Multiplier) : health.MaxIncreaseBy(Multiplier);
                  }

            }

            public class Energy {

                  public float Max     { get; set; }
                  public float Current { get; set; }

                  public List<float>  Events    { get; set; }
                  public List<string> Modifiers { get; set; }

            }

            public class MaxEnergyMultiplier : IModify<Energy, Energy> {

                  public const string NAME = "multiplier.energy.max";
                  internal     float  Multiplier { get; set; }
                  internal     bool   Restore    { get; set; }

                  public string ID => NAME;


                  // Apply Multiplier to max energy and restore max energy once.
                  public Energy Apply(Energy energy) {
                        if (energy.Current <= 0.0f || energy.Max <= 0.0f) return energy;

                        var maxEnergy = energy.Max * Multiplier;
                        if (!Restore)
                              return new Energy {
                                    Max = maxEnergy, Current = energy.Current, Modifiers = new List<string> {NAME}
                              };

                        Restore = false;
                        return new Energy {
                              Max = maxEnergy, Current = maxEnergy, Modifiers = new List<string> {NAME}
                        };
                  }

            }

      }

}