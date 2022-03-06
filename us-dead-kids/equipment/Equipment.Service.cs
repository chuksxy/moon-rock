using UnityEngine;
using UnityEngine.Rendering;

namespace us_dead_kids.equipment {

      public partial class Equipment {


            // TODO:: [Redo this comment] Equipment Table of a specific type.
            public interface IEquipmentTable<out E> {

                  E Read(string entityID);

            }

            // Equipment Table for all sub-equipments in the world.
            public interface IEquipmentTable : IEquipmentTable<object> {

                  new object Read(string entityID);

            }

            public class Service {

                  private readonly SerializedDictionary<string, IEquipmentTable> _allTables;


                  // Private Constructor, do not expose.
                  private Service(SerializedDictionary<string, IEquipmentTable> allTables) {
                        _allTables = allTables;
                  }


                  // New Equipment Service.
                  public static Service New() {
                        return new Service(new SerializedDictionary<string, IEquipmentTable>());
                  }


                  // TODO:: Redo comment. "GetTable for a specific equipment type."
                  public IEquipmentTable<E> GetTable<T, E>()
                        where T : IEquipmentTable<E> {
                        var tableName = typeof(T).Name;
                        if (_allTables.ContainsKey(tableName))
                              return _allTables[tableName] as IEquipmentTable<E>;

                        Debug.LogWarning($"Equipment Table [{tableName}] is not registered with Eden.");
                        return default;
                  }


                  // Register an Equipment Table of a specific equipment type.
                  public void Register<E>(E type) where E : IEquipmentTable {
                        var tableName = typeof(E).Name;
                        if (_allTables.ContainsKey(tableName)) return;

                        _allTables.Add(tableName, type);
                  }

            }

      }

}