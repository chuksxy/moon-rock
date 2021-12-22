using game.world.item;
using UnityEngine;

namespace game.world.character {

      public class TCharacter : ScriptableObject {

            public string characterName;
            public int    health;
            public float  energy;

            public SkinnedMeshRenderer skeleton;

            public THat[]       head;
            public TBaseLayer[] body;
            public TSleeve[]    leftSleeve;
            public TSleeve[]    rightSleeve;
            public TOuterWear[] outerWear;
            public TShoes[]     shoes;

      }

}