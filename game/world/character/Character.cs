using game.world.item;
using UnityEngine;

/*
 * Character Management and Behaviour.
 */
namespace game.world.character {

      public static partial class Character {

            // Create a character from a `Character Template` ScriptableObject. Give the character an ID and register 
            // the character within the world.
            public static Interface Create(Template template, string characterID, string registryID, string zoneID) {
                  var data = ConvertToData(template, characterID);
                  return Load(data, registryID, zoneID);
            }


            // Load a character from `Character Data`(Persisted). Assign the ID and register the character within the world.
            public static Interface Load(Data data, string registryID, string zoneID) {
                  return Assemble(data, registryID).Init(data, registryID, zoneID);
            }


            // Assemble a character from a `Character Template` ScriptableObject.
            public static Interface Assemble(Template template) {
                  var data = ConvertToData(template, "no.character.id");
                  return Assemble(data, "main.registry");
            }


            // Assemble a character from `Character Data`.
            public static Interface Assemble(Data data, string registryID) {
                  var character          = Load(data.Base);
                  var characterInterface = character.AddComponent<Interface>().Init(data, registryID);

                  Item.Equip(characterInterface, Item.Slot.Head, Item.ExtractNames(data.Hats));
                  Item.Equip(characterInterface, Item.Slot.Body, Item.ExtractNames(data.BaseLayer));
                  Item.Equip(characterInterface, Item.Slot.LeftSleeve, Item.ExtractNames(data.LeftSleeve));
                  Item.Equip(characterInterface, Item.Slot.RightSleeve, Item.ExtractNames(data.RightSleeve));
                  Item.Equip(characterInterface, Item.Slot.OuterWear, Item.ExtractNames(data.OuterWear));
                  Item.Equip(characterInterface, Item.Slot.Feet, Item.ExtractNames(data.Shoes));

                  return characterInterface;
            }


            // Load the Character's base fbx file from the `Resources` folder by name.
            public static GameObject Load(string name) {
                  return Resources.Load<GameObject>($"/Character/{name}.fbx");
            }


            // Move the character in a specific direction using the CharacterController. Also apply any speed modifier
            // that is present.
            internal static void Move(Interface characterInterface, Vector3 direction, float modifier) {
                  var registry   = WorldRegistry.GetRegistry(characterInterface.GetRegistryID());
                  var data       = registry.GetCharacterData(characterInterface.GetCharacterID());
                  var speed      = EvaluateMovementSpeed(data);
                  var controller = characterInterface.GetController();

                  controller.Move(direction * modifier * speed);
            }


            // Jump in the direction specified and apply any jump speed modifier if present.
            internal static void Jump(Interface characterInterface, Vector3 direction, float modifier) {
                  var registry   = WorldRegistry.GetRegistry(characterInterface.GetRegistryID());
                  var data       = registry.GetCharacterData(characterInterface.GetCharacterID());
                  var speed      = EvaluateJumpSpeed(data);
                  var controller = characterInterface.GetController();

                  controller.Move(direction * modifier * speed);
            }


            // Evaluate `Movement Speed` based off the character's current equipment and items in possession.
            private static float EvaluateMovementSpeed(Data data) {
                  return 12.0f;
            }


            // Evaluate `Jump Speed` based off the character's current equipment and items in possession.
            private static float EvaluateJumpSpeed(Data data) {
                  return 8.0f;
            }


            // Convert `Character Template` ScriptableObject to a Data object.
            private static Data ConvertToData(Template template, string characterID) {
                  return new Data();
            }

      }

}