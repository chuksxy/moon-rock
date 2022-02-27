using UnityEngine;
using UnityEngine.Rendering;

namespace eden.equipment {

      public partial class Equipment {


            // Equipment Factory to create equipment of different types in the world.
            public interface IEquipmentFactory<TEquipmentType> {

                  TEquipmentType GetEquipment(string entityID);

            }

            // Equipment Factory for all equipment in the world.
            public interface IEquipmentFactory : IEquipmentFactory<object> {

                  new object GetEquipment(string entityID);

            }

            public class Factory {

                  private readonly SerializedDictionary<string, IEquipmentFactory> _factories;


                  // Private Constructor, do not expose.
                  private Factory(SerializedDictionary<string, IEquipmentFactory> factories) {
                        _factories = factories;
                  }


                  // New Equipment Factory.
                  public static Factory New() {
                        return new Factory(new SerializedDictionary<string, IEquipmentFactory>());
                  }


                  // Get Factory for a specific equipment type.
                  public IEquipmentFactory<TEquipmentType> Get<TFactory, TEquipmentType>()
                        where TFactory : IEquipmentFactory<TEquipmentType> {
                        var factoryName = typeof(TFactory).Name;
                        if (_factories.ContainsKey(factoryName))
                              return _factories[factoryName] as IEquipmentFactory<TEquipmentType>;

                        Debug.LogWarning($"Equipment Factory [{factoryName}] is not registered with Eden.");
                        return default;
                  }


                  // Register an equipment Factory of a specific type.
                  public void Register<TFactory>(TFactory factory) where TFactory : IEquipmentFactory {
                        var factoryName = typeof(TFactory).Name;
                        if (_factories.ContainsKey(factoryName)) return;

                        _factories.Add(factoryName, factory);
                  }

            }

      }

}