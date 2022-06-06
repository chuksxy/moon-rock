using System.Collections.Generic;
using UnityEngine;
using us_dead_kids.game;

namespace us_dead_kids.avatar {

      public class AvatarService : MonoBehaviour {

            public bool IsAlive(string avatarID) {
                  return GameManager.CheckAvatarRegistryCondition(r => {
                        var avatar = r.Read(avatarID);
                        if (avatar == null) return false;

                        return avatar.CurrentHealth > 0;
                  });
            }


            public void MutateHealth(string avatarID, int value) {
                  GameManager.ExecOnAvatarRegistry(r => {
                        var avatar = r.Read(avatarID);
                        if (avatar != null) {
                              avatar.CurrentHealth = Mathf.Max(0, Mathf.Min(avatar.MaxHealth, avatar.CurrentHealth + value));
                        }
                  });
            }


            public void MutateStamina(string avatarID, int value) {
                  GameManager.ExecOnAvatarRegistry(r => {
                        var avatar = r.Read(avatarID);
                        if (avatar != null) {
                              avatar.CurrentStamina = Mathf.Max(0, Mathf.Min(avatar.MaxStamina, avatar.CurrentStamina + value));
                        }
                  });
            }


            public void AddEquipment(string    equipmentID, string name, int amount = 1, bool stack = false) { }
            public void RemoveEquipment(string equipmentID, string name, int amount = 1, bool stack = false) { }

            public void AddItem(string    itemID, string name, int amount = 1, bool stack = false) { }
            public void RemoveItem(string itemID, string name, int amount = 1, bool stack = false) { }

      }

}