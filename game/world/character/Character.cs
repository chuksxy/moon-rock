using System;
using System.Linq;
using game.world.item;
using game.world.property;
using UnityEngine;

/*
 * Character Management and Behaviour.
 */
namespace game.world.character {

      public static partial class Character {

            // Generate ID for a character.
            public static string GenerateID() {
                  return $"character_{Guid.NewGuid().ToString().ToLower()}";
            }


            // Reset Character's health and energy to the max.
            public static void ResetCharacter(Interface characterInterface) {
                  var registry = World.Registry.Get(characterInterface.GetRegistryID());
                  var data     = registry.GetCharacterData(characterInterface.GetCharacterID());

                  if (data.IsBlank()) return;

                  data.Health.Current = data.Health.Max;
                  data.Energy.Current = data.Energy.Max;
            }


            // Create a character from a `Character Template` ScriptableObject. Give the character an ID and register 
            // the character within the world.
            public static Interface Create(Template template, string characterID, string registryID, string zoneID) {
                  var data = ConvertToData(template, characterID);
                  return RawLoad(data, registryID, zoneID);
            }


            // Assemble a character from a `Character Template` ScriptableObject, with items/gear pre-populated.
            public static Interface Assemble(Template template) {
                  var data = ConvertToData(template, "no.character.ID");
                  return Assemble(data, "main.registry");
            }


            // Assemble a character from `Character Data` with items/gear pre-populated.
            public static Interface Assemble(Data data, string registryID) {
                  var character          = RawLoad(data.Base);
                  var characterInterface = character.AddComponent<Interface>().Init(data, registryID);

                  Item.Equip(characterInterface, Item.Slot.Head, Item.ExtractNames(data.Hats));
                  Item.Equip(characterInterface, Item.Slot.Body, Item.ExtractNames(data.BaseLayer));
                  Item.Equip(characterInterface, Item.Slot.LeftSleeve, Item.ExtractNames(data.LeftSleeve));
                  Item.Equip(characterInterface, Item.Slot.RightSleeve, Item.ExtractNames(data.RightSleeve));
                  Item.Equip(characterInterface, Item.Slot.OuterWear, Item.ExtractNames(data.OuterWear));
                  Item.Equip(characterInterface, Item.Slot.Feet, Item.ExtractNames(data.Shoes));

                  return characterInterface;
            }


            // Raw Load a character from `Character Data`(Persisted). Assign the ID and register the character within the world.
            public static Interface RawLoad(Data data, string registryID, string zoneID) {
                  return Assemble(data, registryID).Init(data, registryID, zoneID);
            }


            // Raw Load the Character's base fbx file from the `Resources` folder by name.
            public static GameObject RawLoad(string name) {
                  var item = Resources.Load<GameObject>($"/Character/{name}.prefab");
                  return item == null ? Resources.Load<GameObject>($"/Character/{name}.fbx") : item;
            }


            // Set World Position of the character.
            public static void SetWorldPosition(Interface characterInterface, Vector3 position) {
                  var registry = World.Registry.Get(characterInterface.GetRegistryID());
                  var data     = registry.GetCharacterData(characterInterface.GetCharacterID());

                  if (data.IsBlank()) return;

                  characterInterface.gameObject.transform.position = position;
                  data.WorldPosition                               = position;
            }


            // Move the character in a specific direction using the CharacterController. Also apply any speed modifier
            // that is present.
            public static void Move(Interface characterInterface, Vector3 direction, float modifier) {
                  var registry = World.Registry.Get(characterInterface.GetRegistryID());
                  var data     = registry.GetCharacterData(characterInterface.GetCharacterID());

                  if (data.IsBlank()) return;

                  var speed      = Mathf.Min(World.MAX_MOVEMENT_SPEED_AFTER_MODIFIERS, EvaluateMovementSpeed(data) * modifier);
                  var controller = characterInterface.GetController();
                  controller.Move(direction * modifier * speed * Time.deltaTime);

                  data.WorldPosition = controller.gameObject.transform.position;

                  var animator = characterInterface.GetAnimator();
                  animator.SetFloat(Animation.Horizontal, direction.x * speed);
                  animator.SetFloat(Animation.Vertical, direction.y * speed);
            }


            // Jump in the direction specified and apply any jump speed modifier if present.
            public static void Jump(Interface characterInterface, Vector3 direction, float modifier) {
                  var registry = World.Registry.Get(characterInterface.GetRegistryID());
                  var data     = registry.GetCharacterData(characterInterface.GetCharacterID());

                  if (data.IsBlank()) return;

                  var speed      = Mathf.Min(World.MAX_JUMP_SPEED_AFTER_MODIFIERS, EvaluateJumpSpeed(data) * modifier);
                  var controller = characterInterface.GetController();
                  controller.Move(direction * modifier * speed);

                  data.WorldPosition = controller.gameObject.transform.position;

                  var animator = characterInterface.GetAnimator();
                  animator.SetTrigger(Animation.Jump);
            }


            // Evaluate `Jump Speed` based off the character's current equipment and items in possession.
            public static float EvaluateJumpSpeed(Data data) {
                  return Mathf.Min(World.MAX_JUMP_SPEED, EvaluateMovementSpeed(data) / 2.0f);
            }


            // Evaluate `Movement Speed` based off the character's current equipment and items in possession.
            public static float EvaluateMovementSpeed(Data data) {
                  return (1 - data.Weight / World.MAX_WEIGHT) * World.MAX_MOVEMENT_SPEED;
            }


            // Convert `Character Template` ScriptableObject to a Data object.
            public static Data ConvertToData(Template t, string characterID) {
                  var data = new Data {
                        ID   = characterID,
                        Name = t.characterName,
                        Health = new Property.Health
                              {Current = t.maxHealth, Max = t.maxHealth, Modifiers = Array.Empty<string>()},
                        Energy = new Property.Energy
                              {Current = t.maxEnergy, Max = t.maxEnergy, Modifiers = Array.Empty<string>()},
                        Base        = t.skeleton.name,
                        BaseLayer   = t.body.Select(b => b.ToData()).ToArray(),
                        Hats        = t.head.Select(h => h.ToData()).ToArray(),
                        LeftSleeve  = t.leftSleeve.Select(l => l.ToData()).ToArray(),
                        RightSleeve = t.rightSleeve.Select(r => r.ToData()).ToArray(),
                        Modifiers   = t.modifiers,
                        Tags        = t.tags
                  };

                  return data;
            }


            public static Item.Hat SwapHat(Interface characterInterface, int index, Item.Hat hat) {
                  var data = characterInterface.GetData();
                  var prev = data.Hats[index];

                  Item.Equip(characterInterface, Item.Slot.Head, index, hat.Name);
                  data.Hats[index] = hat;

                  return prev;
            }


            private static Item.BaseLayer SwapBaseLayer(Interface characterInterface, int index, Item.BaseLayer baseLayer) {
                  var data = characterInterface.GetData();
                  var prev = data.BaseLayer[index];

                  Item.Equip(characterInterface, Item.Slot.Body, index, baseLayer.Name);
                  data.BaseLayer[index] = baseLayer;

                  return prev;
            }


            private static Item.Sleeve SwapLeftSleeve(Interface characterInterface, int index, Item.Sleeve sleeve) {
                  var data = characterInterface.GetData();
                  var prev = data.LeftSleeve[index];

                  Item.Equip(characterInterface, Item.Slot.LeftSleeve, index, sleeve.Name);
                  data.LeftSleeve[index] = sleeve;

                  return prev;
            }


            private static Item.Sleeve SwapRightSleeve(Interface characterInterface, int index, Item.Sleeve sleeve) {
                  var data = characterInterface.GetData();
                  var prev = data.RightSleeve[index];

                  Item.Equip(characterInterface, Item.Slot.RightSleeve, index, sleeve.Name);
                  data.RightSleeve[index] = sleeve;

                  return prev;
            }


            private static Item.OuterWear SwapOuterWear(Interface characterInterface, int index, Item.OuterWear outerWear) {
                  var data = characterInterface.GetData();
                  var prev = data.OuterWear[index];

                  Item.Equip(characterInterface, Item.Slot.OuterWear, index, outerWear.Name);
                  data.OuterWear[index] = outerWear;

                  return prev;
            }


            private static Item.Shoes SwapShoes(Interface characterInterface, int index, Item.Shoes shoes) {
                  var data = characterInterface.GetData();
                  var prev = data.Shoes[index];

                  Item.Equip(characterInterface, Item.Slot.Feet, index, shoes.Name);
                  data.Shoes[index] = shoes;

                  return prev;
            }

      }

}