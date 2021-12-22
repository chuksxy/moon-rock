using System.Collections.Generic;
using System.Linq;
using game.world.character;
using UnityEngine;

/*
 * Item manages and behaviours.
 */
namespace game.world.item {

      public static partial class Item {

            public enum Slot {

                  Head,
                  Body,
                  Feet,
                  LeftSleeve,
                  RightSleeve,
                  OuterWear

            }


            // Equip items in an assigned slot, allowing multiple items in the same slot if they can be stacked.
            public static void Equip(GameObject character, string characterID, Slot slot, IEnumerable<string> names) {
                  var items = names.Select(name => Load(slot, name)).ToArray();

                  for (var index = 0; index < items.Length; index++) {
                        var gameObject = character.transform.Find(slot.ToString()).gameObject;
                        var renderer   = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
                        var item       = gameObject.GetComponentInChildren<Interface>();

                        item.Init(characterID);

                        renderer.sharedMesh          = items[index].sharedMesh;
                        renderer.material            = items[index].material;
                        renderer.sharedMaterial      = items[index].sharedMaterial;
                        renderer.quality             = items[index].quality;
                        renderer.updateWhenOffscreen = items[index].updateWhenOffscreen;
                        renderer.materials           = items[index].materials;
                        renderer.sortingOrder        = index;
                  }
            }


            // Load an item's fbx file from the `Resources` folder ane extract the SkinMeshRenderer.
            private static SkinnedMeshRenderer Load(Slot part, string item) {
                  return Resources.Load<GameObject>($"{part}/{item}.fbx").GetComponentInChildren<SkinnedMeshRenderer>();
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

      }

}