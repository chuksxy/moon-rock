using System;
using System.Collections.Generic;
using System.Linq;
using adam.character;
using adam.props;
using moon.rock.world;
using UnityEngine;
using Object = UnityEngine.Object;

/*
 * Item management and behaviours.
 */
namespace adam.item {

      public static partial class Item {

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


            // Generate ID for item by the slot on the character they occupy.
            public static string GenerateID(Slot slotID) {
                  return $"{slotID.ToString().ToLower()}_{Guid.NewGuid().ToString().ToLower()}";
            }


            // Equip items in an assigned slot, allowing multiple items in the same slot if they can be stacked.
            public static void Equip(Character.Interface characterInterface, Slot slotID, IEnumerable<string> names) {
                  var slot = characterInterface.gameObject.transform.Find(slotID.ToString()).gameObject;

                  RemoveAll(slot);

                  var loadedSkinnedMeshes =
                        names.Select(name => RawLoad(slotID, name).GetComponentInChildren<SkinnedMeshRenderer>()).ToArray();

                  for (var index = 0; index < loadedSkinnedMeshes.Length; index++) {
                        var index_skinnedMesh = slot.AddComponent<SkinnedMeshRenderer>();
                        var index_interface   = slot.AddComponent<Interface>();

                        if (index > 0 && !CanStack(characterInterface.GetData(), slotID, index)) continue;

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
                  var loadedSkinnedMesh = RawLoad(slotID, name).GetComponentInChildren<SkinnedMeshRenderer>();

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


            // Raw Load an item's `.fbx` or `.prefab` file from the `Resources` folder.
            public static GameObject RawLoad(Slot slot, string name) {
                  var item = Resources.Load<GameObject>($"{slot}/{name}.prefab");
                  return item == null ? Resources.Load<GameObject>($"{slot}/{name}.fbx") : item;
            }


            // Load an item from an existing pre-made template.
            public static Interface Load(
                  Slot slotID, string template, string registryID = "main.registry", string zoneID = "no.zone.ID") {
                  var path = $"{slotID}/{template}";

                  GameObject        prefab  = null;
                  Props.IAmAnObject @object = null;

                  switch (slotID) {
                        case Slot.Head:
                              var tHat = Resources.Load<THat>(path);
                              @object = tHat.ToData();
                              prefab  = Object.Instantiate(tHat.prefab);
                              break;
                        case Slot.Body:
                              var tBaseLayer = Resources.Load<TBaseLayer>(path);
                              @object = tBaseLayer.ToData();
                              prefab  = Object.Instantiate(tBaseLayer.prefab);
                              break;
                        case Slot.Feet:
                              var tShoes = Resources.Load<TShoes>(path);
                              @object = tShoes.ToData();
                              prefab  = Object.Instantiate(tShoes.prefab);
                              break;
                        case Slot.LeftSleeve:
                              var tLSleeve = Resources.Load<TSleeve>(path);
                              @object = tLSleeve.ToData();
                              prefab  = Object.Instantiate(tLSleeve.prefab);
                              break;
                        case Slot.RightSleeve:
                              var tRSleeve = Resources.Load<TSleeve>(path);
                              @object = tRSleeve.ToData();
                              prefab  = Object.Instantiate(tRSleeve.prefab);
                              break;
                        case Slot.OuterWear:
                              var tOuterWear = Resources.Load<TOuterWear>(path);
                              @object = tOuterWear.ToData();
                              prefab  = Object.Instantiate(tOuterWear.prefab);
                              break;
                        case Slot.None:
                        default:
                              break;
                  }

                  var objectID = World.GenerateID();
                  var itemID   = GenerateID(slotID);

                  return prefab == null
                        ? new GameObject().AddComponent<Interface>()
                        : prefab.AddComponent<Interface>().Init(slotID, objectID, itemID, registryID, zoneID, @object);
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


            public static float Weigh(Props.IHaveWeight[] weights) {
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


            // Address for locating item in `Character Data` via it's Slot (Head, Body, etc.) and Index in the stack.
            public struct Address {

                  public Slot SlotID { get; set; }
                  public int  Index  { get; set; }

            }

      }

}