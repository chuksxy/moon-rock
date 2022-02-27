using UnityEngine;
using UnityEngine.Rendering;

namespace eden.equipment {

      public partial class Equipment {


            // Equipment Factories to create equipment of different types in the world.
            public interface IEquipmentFactory {

                  TEquipmentType GetEquipment<TEquipmentType>(string entityID);

            }

            // Default equipment type to serve as our identity type.
            public class Default : IEquipmentFactory {

                  public static readonly Default Factory = new Default();


                  public TEquipmentType GetEquipment<TEquipmentType>(string entityID) {
                        return default;
                  }

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


                  // Get Factory.
                  public IEquipmentFactory Get<TFactory>() where TFactory : IEquipmentFactory {
                        var factoryName = typeof(TFactory).Name;
                        if (_factories.ContainsKey(factoryName)) return _factories[factoryName];
                        Debug.LogWarning($"Equipment Factory [{factoryName}] is not registered with Eden.");
                        return Default.Factory;
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