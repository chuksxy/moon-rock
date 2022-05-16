using UnityEngine;
using UnityEngine.Events;

namespace us_dead_kids.skill {

      [System.Serializable]
      public struct SkillEvent {

            public float       invokeTime;
            public SkillAction action;


            public SkillTrigger ToTrigger() {
                  return new SkillTrigger {
                        TimeMarker = invokeTime,
                        Action     = action,
                  };
            }

      }

}