using UnityEngine;
using UnityEngine.Events;

namespace us_dead_kids.skill {

      [System.Serializable]
      public struct SkillEvent {

            public float beginTime;
            public float endTime;


            public SkillTrigger ToTrigger() {
                  var e = new UnityEvent<Skill, Animator>();
                  e.AddListener(Test);

                  return new SkillTrigger {
                        BeingTime  = beginTime,
                        EndTime    = endTime,
                        BeginEvent = e,
                        EndEvent   = e
                  };
            }


            public void Test(Skill s, Animator a) {
                  Debug.Log($"Skill [{s.ID}] invoked by [{a.name}] at [{a.GetCurrentAnimatorStateInfo(0).normalizedTime}].");
            }

      }

}