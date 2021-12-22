using game.world.item;
using UnityEngine;

namespace game.world.character {
      public static class Character {
            public static CharacterInterface Create(
                  TCharacter template, string characterID, string registryID, string zoneID) {
                  return Create(ToData(template), characterID, registryID, zoneID);
            }

            public static CharacterInterface Create(
                  CharacterData data, string characterID, string registryID, string zoneID) {
                  CharacterRegistry.GetRegistry(registryID).AddCharacter(characterID, zoneID, data);
                  return Assemble(data);
            }

            public static CharacterInterface Assemble(TCharacter template) {
                  return Assemble(ToData(template));
            }

            public static CharacterInterface Assemble(CharacterData characterData) {
                  var character = Load(characterData.Base);

                  Item.Equip(character, Item.Slot.Head, Item.ExtractNames(characterData.Hats));
                  Item.Equip(character, Item.Slot.Body, Item.ExtractNames(characterData.BaseLayer));
                  Item.Equip(character, Item.Slot.LeftSleeve, Item.ExtractNames(characterData.LeftSleeve));
                  Item.Equip(character, Item.Slot.RightSleeve, Item.ExtractNames(characterData.RightSleeve));
                  Item.Equip(character, Item.Slot.OuterWear, Item.ExtractNames(characterData.OuterWear));
                  Item.Equip(character, Item.Slot.Feet, Item.ExtractNames(characterData.Shoes));

                  return character.AddComponent<CharacterInterface>();
            }

            public static GameObject Load(string name) {
                  return Resources.Load<GameObject>($"/Character/{name}.fbx");
            }

            public static float EvaluateMovementSpeed(CharacterData characterData) {
                  return 0.0f;
            }

            public static float EvaluateJumpSpeed(CharacterData characterData) {
                  return 0.0f;
            }

            private static CharacterData ToData(TCharacter template) {
                  return new CharacterData();
            }
      }
}