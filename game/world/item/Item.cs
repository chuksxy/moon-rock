using System;
using System.Collections.Generic;
using System.Linq;
using game.world.character;
using UnityEngine;
using Object = UnityEngine.Object;

/*
 * Item management and behaviours.
 */
namespace game.world.item {

      public static partial class Item {

            // Address for locating item in `Character Data` via it's Slot (Head, Body, etc.) and Index in the stack.
            public struct Address {

                  public Slot SlotID { get; set; }
                  public int  Index  { get; set; }

            }

            // Slots for equipping body parts and gear.
            public enum Slot {

                  None,
                  Head,
                  Body,
                  Feet,
                  LeftSleeve,
                  RightSleeve,
                  OuterWear

            }


            // Equip items in an assigned slot, allowing multiple items in the same slot if they can be stacked.
            public static void Equip(Character.Interface characterInterface, Slot slotID, IEnumerable<string> names) {
                  var slot = characterInterface.gameObject.transform.Find(slotID.ToString()).gameObject;

                  RemoveAll(slot);

                  var loadedSkinnedMeshes =
                        names.Select(name => Load(slotID, name).GetComponentInChildren<SkinnedMeshRenderer>()).ToArray();

                  for (var index = 0; index < loadedSkinnedMeshes.Length; index++) {
                        var index_skinnedMesh = slot.AddComponent<SkinnedMeshRenderer>();
                        var index_interface   = slot.AddComponent<Interface>();

                        if (index > 0 && !CanStack(characterInterface.GetData(), slotID, index)) {
                              continue;
                        }

                        var loadedSkinnedMesh = loadedSkinnedMeshes[index];

                        index_interface.Init(
                              slotID, index, characterInterface.GetCharacterID(), characterInterface.GetRegistryID());

                        AssignMesh(index_skinnedMesh, loadedSkinnedMesh, index);

                        // Apply Modifiers here.
                  }
            }


            // Equip item in slot at position in stack.
            public static void Equip(Character.Interface characterInterface, Slot slotID, int index, string name) {
                  var slot = characterInterface.gameObject.transform.Find(slotID.ToString()).gameObject;

                  RemoveAll(slot);

                  var targetSkinnedMesh = slot.AddComponent<SkinnedMeshRenderer>();
                  var loadedSkinnedMesh = Load(slotID, name).GetComponentInChildren<SkinnedMeshRenderer>();

                  AssignMesh(targetSkinnedMesh, loadedSkinnedMesh, index);
            }


            // Assign Mesh Properties such as materials, skinning data, geometry etc...
            private static void AssignMesh(SkinnedMeshRenderer target, SkinnedMeshRenderer loaded, int index) {
                  target.sharedMesh          = loaded.sharedMesh;
                  target.material            = loaded.material;
                  target.sharedMaterial      = loaded.sharedMaterial;
                  target.quality             = loaded.quality;
                  target.updateWhenOffscreen = loaded.updateWhenOffscreen;
                  target.materials           = loaded.materials;
                  target.sortingOrder        = index;
            }


            // Remove All items equipped in slot.
            public static void RemoveAll(GameObject slot) {
                  slot.GetComponentsInChildren<SkinnedMeshRenderer>().ToList().ForEach(Object.Destroy);
                  slot.GetComponentsInChildren<Interface>().ToList().ForEach(Object.Destroy);
            }


            // Load an item's `.fbx` or `.prefab` file from the `Resources` folder.
            public static GameObject Load(Slot slot, string name) {
                  var item = Resources.Load<GameObject>($"{slot}/{name}.prefab");
                  return item == null ? Resources.Load<GameObject>($"{slot}/{name}.fbx") : item;
            }


            // Load an item from an existing pre-made template.
            public static Interface LoadFromTemplate(Slot slotID, string templateName) {
                  var path = $"{slotID}/{templateName}";

                  GameObject prefab = null;
                  switch (slotID) {
                        case Slot.Head:
                              prefab = Object.Instantiate(Resources.Load<THat>(path).prefab);
                              break;
                        case Slot.Body:
                              prefab = Object.Instantiate(Resources.Load<TBaseLayer>(path).prefab);
                              break;
                        case Slot.Feet:
                              prefab = Object.Instantiate(Resources.Load<TShoes>(path).prefab);
                              break;
                        case Slot.LeftSleeve:
                              prefab = Object.Instantiate(Resources.Load<TSleeve>(path).prefab);
                              break;
                        case Slot.RightSleeve:
                              prefab = Object.Instantiate(Resources.Load<TSleeve>(path).prefab);
                              break;
                        case Slot.OuterWear:
                              prefab = Object.Instantiate(Resources.Load<TOuterWear>(path).prefab);
                              break;
                        case Slot.None:
                        default:
                              break;
                  }

                  return prefab == null
                        ? new GameObject().AddComponent<Interface>()
                        : prefab.AddComponent<Interface>().Init(slotID, 0, "no.character.id", "main.registry");
            }


            // Extract Item names from hats.
            public static IEnumerable<string> ExtractNames(IEnumerable<Hat> hats) {
                  return hats.Select(hat => hat.Name);
            }


            // Extract Item names from base layers.
            public static IEnumerable<string> ExtractNames(IEnumerable<BaseLayer> baseLayers) {
                  return baseLayers.Select(layer => layer.Name);
            }


            // Extract Item names from sleeves.
            public static IEnumerable<string> ExtractNames(IEnumerable<Sleeve> sleeves) {
                  return sleeves.Select(sleeve => sleeve.Name);
            }


            // Extract Item names from outer wears.
            public static IEnumerable<string> ExtractNames(IEnumerable<OuterWear> outerWears) {
                  return outerWears.Select(outerWear => outerWear.Name);
            }


            // Extract Item names from shoes.
            public static IEnumerable<string> ExtractNames(IEnumerable<Shoes> shoes) {
                  return shoes.Select(shoe => shoe.Name);
            }


            public static float Weigh(IHaveWeight[] weights) {
                  return weights.Select(hasWeight => hasWeight.Weight).Sum();
            }


            public static float Weigh(IEnumerable<Hat> hats) {
                  return hats.Select(hat => hat.Weight).Sum();
            }


            public static float Weigh(IEnumerable<BaseLayer> baseLayers) {
                  return baseLayers.Select(baseLayer => baseLayer.Weight).Sum();
            }


            public static float Weigh(IEnumerable<Sleeve> sleeves) {
                  return sleeves.Select(sleeve => sleeve.Weight).Sum();
            }


            public static float Weigh(IEnumerable<OuterWear> outerWears) {
                  return outerWears.Select(outerWear => outerWear.Weight).Sum();
            }


            public static float Weigh(IEnumerable<Shoes> allShoes) {
                  return allShoes.Select(shoes => shoes.Weight).Sum();
            }


            public static bool CanStack(Character.Data data, Address address) {
                  return CanStack(data, address.SlotID, address.Index);
            }


            public static bool CanStack(Character.Data data, Slot slotId, int index) {
                  switch (slotId) {
                        case Slot.Head:
                              return index < data.Hats.Length && data.Hats[index].CanStack;
                        case Slot.Body:
                              return index < data.BaseLayer.Length && data.BaseLayer[index].CanStack;
                        case Slot.Feet:
                              return index < data.Shoes.Length && data.Shoes[index].CanStack;
                        case Slot.LeftSleeve:
                              return index < data.LeftSleeve.Length && data.LeftSleeve[index].CanStack;
                        case Slot.RightSleeve:
                              return index < data.RightSleeve.Length && data.RightSleeve[index].CanStack;
                        case Slot.OuterWear:
                              return index < data.OuterWear.Length && data.OuterWear[index].CanStack;
                        case Slot.None:
                        default:
                              return false;
                  }
            }

      }

}