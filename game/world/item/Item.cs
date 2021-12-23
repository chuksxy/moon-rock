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

                  var skinnedMeshes =
                        names.Select(name => Load(slotID, name).GetComponentInChildren<SkinnedMeshRenderer>()).ToArray();

                  for (var index = 0; index < skinnedMeshes.Length; index++) {
                        var index_skinnedMesh = slot.AddComponent<SkinnedMeshRenderer>();
                        var index_interface   = slot.AddComponent<Interface>();

                        if (index > 0 && !Item.CanStack(characterInterface.GetData(), slotID, index)) {
                              continue;
                        }

                        index_interface.Init(
                              slotID, index, characterInterface.GetCharacterID(), characterInterface.GetRegistryID());

                        index_skinnedMesh.sharedMesh          = skinnedMeshes[index].sharedMesh;
                        index_skinnedMesh.material            = skinnedMeshes[index].material;
                        index_skinnedMesh.sharedMaterial      = skinnedMeshes[index].sharedMaterial;
                        index_skinnedMesh.quality             = skinnedMeshes[index].quality;
                        index_skinnedMesh.updateWhenOffscreen = skinnedMeshes[index].updateWhenOffscreen;
                        index_skinnedMesh.materials           = skinnedMeshes[index].materials;
                        index_skinnedMesh.sortingOrder        = index;

                        // Apply Modifiers here.
                  }
            }


            // Remove All items equipped in slot.
            private static void RemoveAll(GameObject slot) {
                  slot.GetComponentsInChildren<SkinnedMeshRenderer>().ToList().ForEach(Object.Destroy);
                  slot.GetComponentsInChildren<Interface>().ToList().ForEach(Object.Destroy);
            }


            // Load an item's `.fbx` or `.prefab` file from the `Resources` folder.
            private static GameObject Load(Slot part, string name) {
                  var item = Resources.Load<GameObject>($"{part}/{name}.prefab");
                  return item == null ? Resources.Load<GameObject>($"{part}/{name}.fbx") : item;
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
                              return data.Hats[index].CanStack;
                        case Slot.Body:
                              return data.BaseLayer[index].CanStack;
                        case Slot.Feet:
                              return data.Shoes[index].CanStack;
                        case Slot.LeftSleeve:
                              return data.LeftSleeve[index].CanStack;
                        case Slot.RightSleeve:
                              return data.RightSleeve[index].CanStack;
                        case Slot.OuterWear:
                              return data.OuterWear[index].CanStack;
                        case Slot.None:
                        default:
                              return false;
                  }
            }

      }

}