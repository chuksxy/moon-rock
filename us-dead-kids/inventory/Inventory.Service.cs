using System.Linq;
using UnityEngine;

namespace us_dead_kids.inventory {

      public partial class Inventory {

            private const string FIND_INVENTORY_BY_CHARACTER_ID =
                  "select * " +
                  "from inventory i " +
                  "where i.characterID = ?";

            public static class Service {

                  public static string GetPrimaryWeaponID(string characterID) {
                        var db = UsDeadKids.DB.Get();
                        if (db == null) {
                              Debug.LogWarning($"cannot get character's [{characterID}] primary weapon, DB is null.");
                              return "";
                        }

                        var inventories = db.Query<Inventory>(FIND_INVENTORY_BY_CHARACTER_ID, characterID);
                        if (inventories == null || inventories.Count == 0) {
                              Debug.LogWarning($"inventory for character [{characterID}] not present.");
                              return "";
                        }

                        return inventories.Select(i => i.PrimaryWeaponID).FirstOrDefault();
                  }

            }

      }

}