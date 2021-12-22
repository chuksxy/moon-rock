using game.world.item;
using UnityEngine;

/*
 * This contains all necessary functions related to character management and behaviour.
 */
namespace game.world.character {

      public static class Character {

            // Create a character from a `Character Template` ScriptableObject. Give the character an ID and register 
            // the character within the world.
            public static CharacterInterface Create(
                  TCharacter template,
                  string     characterID,
                  string     registryID,
                  string     zoneID
            ) {
                  var data = ConvertToData(template);
                  return Load(data, characterID, registryID, zoneID);
            }


            // Load a character from `Character Data`(Persisted). Assign the ID and register the character within the 
            // world.
            public static CharacterInterface Load(
                  CharacterData data,
                  string        characterID,
                  string        registryID,
                  string        zoneID
            ) {
                  return Assemble(data).Init(data, characterID, registryID, zoneID);
            }


            // Assemble a character from a `Character Template` ScriptableObject.
            public static CharacterInterface Assemble(TCharacter template) {
                  var data = ConvertToData(template);
                  return Assemble(data);
            }


            // Assemble a character from `Character Data`.
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


            // Load the Character's base fbx file from the `Resources` folder by name.
            public static GameObject Load(string name) {
                  return Resources.Load<GameObject>($"/Character/{name}.fbx");
            }


            // Move the character in a specific direction using the CharacterController. Also apply any speed modifier 
            // that is present.
            internal static void Move(
                  CharacterInterface @interface,
                  Vector3            direction,
                  float              modifier
            ) {
                  var data = WorldRegistry.GetRegistry(@interface.GetRegistryID())
                                          .GetCharacterData(@interface.GetCharacterID());

                  var speed      = EvaluateMovementSpeed(data);
                  var controller = @interface.GetController();

                  controller.Move(direction * modifier * speed);
            }


            // Jump in the direction specified and apply any jump speed modifier if present.
            internal static void Jump(
                  CharacterInterface @interface,
                  Vector3            direction,
                  float              modifier
            ) {
                  var data = WorldRegistry.GetRegistry(@interface.GetRegistryID())
                                          .GetCharacterData(@interface.GetCharacterID());

                  var speed      = EvaluateJumpSpeed(data);
                  var controller = @interface.GetController();

                  controller.Move(direction * modifier * speed);
            }


            // Evaluate `Movement Speed` based off the character's current equipment and items in possession.
            private static float EvaluateMovementSpeed(CharacterData characterData) {
                  return 12.0f;
            }


            // Evaluate `Jump Speed` based off the character's current equipment and items in possession.
            private static float EvaluateJumpSpeed(CharacterData characterData) {
                  return 8.0f;
            }


            // Convert `Character Template` ScriptableObject to a Data object.
            private static CharacterData ConvertToData(TCharacter template) {
                  return new CharacterData();
            }
      }

}