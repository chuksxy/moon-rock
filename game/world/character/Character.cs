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
                  return Assemble(data).Init(data, characterID, registryID, zoneID);
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

            internal static void Move(CharacterInterface @interface, Vector3 direction, float modifier) {
                  var data = WorldRegistry.GetRegistry(@interface.GetRegistryID())
                                          .GetCharacterData(@interface.GetCharacterID());
                  var speed = EvaluateMovementSpeed(data);

                  @interface.GetController().Move(direction * modifier * speed);
            }

            internal static void Jump(CharacterInterface @interface, Vector3 direction, float modifier) {
                  var data = WorldRegistry.GetRegistry(@interface.GetRegistryID())
                                          .GetCharacterData(@interface.GetCharacterID());
                  var speed = EvaluateJumpSpeed(data);

                  @interface.GetController().Move(direction * modifier * speed);
            }

            private static float EvaluateMovementSpeed(CharacterData characterData) {
                  return 12.0f;
            }

            private static float EvaluateJumpSpeed(CharacterData characterData) {
                  return 8.0f;
            }

            private static CharacterData ToData(TCharacter template) {
                  return new CharacterData();
            }
      }
}