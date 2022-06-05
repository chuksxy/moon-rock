using UnityEngine;

namespace us_dead_kids.lib.animation {

      [System.Serializable]
      public class AnimationIntParamSO {

            [SerializeField] private string name;
            [SerializeField] private int    value;
            [SerializeField] private bool   reset;


            public string Name() {
                  return name;
            }


            public int Value() {
                  return value;
            }


            public bool Reset() {
                  return reset;
            }

      }

}