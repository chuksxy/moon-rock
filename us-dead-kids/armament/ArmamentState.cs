using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace us_dead_kids.armament {

      [System.Serializable]
      public class ArmamentState {

            public string                               ID          { get; set; }
            public string                               AvatarID    { get; set; }
            public string                               Name        { get; set; }
            public string                               Description { get; set; }
            public int                                  Hand        { get; set; }
            public SerializedDictionary<string, object> Modifiers   { get; set; }

            public float Range  { get; set; }
            public int   Damage { get; set; }

            public int MaxHealth     { get; set; }
            public int CurrentHealth { get; set; }

            public int AmmoCount { get; set; }
            public int ClipCount { get; set; }
            public int ClipSize  { get; set; }


            public bool IsReady() {
                  return !IsBroken() && AmmoCount > 0;
            }


            public bool IsBroken() {
                  return CurrentHealth <= 0;
            }


            public int AdjustHealth(int amount) {
                  CurrentHealth = Mathf.Min(MaxHealth, Mathf.Max(0, CurrentHealth + amount));
                  return CurrentHealth;
            }


            public int AdjustAmmoCount(int amount) {
                  AmmoCount = Mathf.Min(ClipCount, Mathf.Max(0, AmmoCount + amount));
                  return AmmoCount;
            }


            public void Reload() {
                  AmmoCount = ClipSize;
                  ClipCount--;
            }

      }

}