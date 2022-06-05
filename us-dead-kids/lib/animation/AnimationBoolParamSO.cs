using UnityEngine;

namespace us_dead_kids.lib.animation {

      [System.Serializable]
      public class AnimationBoolParamSO {


            [SerializeField] private string name;
            [SerializeField] private bool   value;
            [SerializeField] private bool   reset;


            public string Name() {
                  return name;
            }


            public bool Value() {
                  return value;
            }


            public bool Reset() {
                  return reset;
            }

      }

}