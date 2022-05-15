namespace us_dead_kids.skill {

      public class SkillState {

            public SkillState(string id, int skillHash) {
                  ID        = id;
                  SkillHash = skillHash;
            }


            public string ID          { get; }
            public int    SkillHash   { get; }
            public bool   IsCancelled { get; set; }

      }

}