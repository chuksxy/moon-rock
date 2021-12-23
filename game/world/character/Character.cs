using System;
using System.Linq;
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
                  var item = Resources.Load<GameObject>($"/Character/{name}.prefab");
                  return item == null ? Resources.Load<GameObject>($"/Character/{name}.fbx") : item;
            }


            // Move the character in a specific direction using the CharacterController. Also apply any speed modifier
            // that is present.
            private static void Move(Interface characterInterface, Vector3 direction, float modifier) {
                  var registry = WorldRegistry.GetRegistry(characterInterface.GetRegistryID());
                  var data     = registry.GetCharacterData(characterInterface.GetCharacterID());
                  var speed    = Mathf.Min(World.MAX_MOVEMENT_SPEED_AFTER_MODIFIERS, EvaluateMovementSpeed(data) * modifier);

                  var controller = characterInterface.GetController();
                  controller.Move(direction * modifier * speed * Time.deltaTime);

                  var animator = characterInterface.GetAnimator();
                  animator.SetFloat(Animation.Horizontal, direction.x * speed);
                  animator.SetFloat(Animation.Vertical, direction.y * speed);
            }


            // Jump in the direction specified and apply any jump speed modifier if present.
            private static void Jump(Interface characterInterface, Vector3 direction, float modifier) {
                  var registry = WorldRegistry.GetRegistry(characterInterface.GetRegistryID());
                  var data     = registry.GetCharacterData(characterInterface.GetCharacterID());
                  var speed    = Mathf.Min(World.MAX_JUMP_SPEED_AFTER_MODIFIERS, EvaluateJumpSpeed(data) * modifier);

                  var controller = characterInterface.GetController();
                  controller.Move(direction * modifier * speed);

                  var animator = characterInterface.GetAnimator();
                  animator.SetTrigger(Animation.Jump);
            }


            // Evaluate `Jump Speed` based off the character's current equipment and items in possession.
            private static float EvaluateJumpSpeed(Data data) {
                  return EvaluateMovementSpeed(data) / 2.0f;
            }


            // Evaluate `Movement Speed` based off the character's current equipment and items in possession.
            private static float EvaluateMovementSpeed(Data data) {
                  return (1 - data.Weight / World.MAX_WEIGHT) * World.MAX_MOVEMENT_SPEED;
            }


            // Convert `Character Template` ScriptableObject to a Data object.
            private static Data ConvertToData(Template t, string characterID) {
                  var data = new Data {
                        ID          = characterID,
                        Name        = t.characterName,
                        Health      = new Health {Current = t.maxHealth, Max = t.maxHealth, Modifiers = Array.Empty<string>()},
                        Energy      = new Energy {Current = t.maxEnergy, Max = t.maxEnergy, Modifiers = Array.Empty<string>()},
                        Base        = t.skeleton.name,
                        BaseLayer   = t.body.Select(b => b.ToData()).ToArray(),
                        Hats        = t.head.Select(h => h.ToData()).ToArray(),
                        LeftSleeve  = t.leftSleeve.Select(l => l.ToData()).ToArray(),
                        RightSleeve = t.rightSleeve.Select(r => r.ToData()).ToArray(),
                        Modifiers   = t.modifiers
                  };

                  return data;
            }

      }

}