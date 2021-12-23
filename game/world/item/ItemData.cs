using System;
using System.Collections.Generic;
using UnityEngine;

namespace game.world.item {

      public static partial class Item {


            public class THat : ScriptableObject {

                  public string     itemName;
                  public int        health;
                  public float      weight;
                  public string[]   modifiers;
                  public GameObject prefab;
                  public Texture2D  icon;


                  internal Hat ToData() {
                        return new Hat() {
                              Name      = itemName,
                              Health    = new Health() {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct Hat : IHaveWeight, ICanStack, IAmAnObject {


                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   ID        { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public float    Weight    { get; set; }
                  public bool     CanStack  { get; }

            }

            public class TBaseLayer : ScriptableObject {

                  public string     itemName;
                  public int        health;
                  public float      weight;
                  public string[]   modifiers;
                  public GameObject prefab;
                  public Texture2D  icon;


                  internal BaseLayer ToData() {
                        return new BaseLayer {
                              Name      = itemName,
                              Health    = new Health() {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct BaseLayer : IHaveWeight, ICanStack, IAmAnObject {

                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   ID        { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public float    Weight    { get; set; }
                  public bool     CanStack  { get; }

            }

            public class TOuterWear : ScriptableObject {

                  public string     itemName;
                  public int        health;
                  public float      weight;
                  public string[]   modifiers;
                  public GameObject prefab;
                  public Texture2D  icon;


                  internal OuterWear ToData() {
                        return new OuterWear {
                              Name      = itemName,
                              Health    = new Health() {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct OuterWear : IHaveWeight, ICanStack, IAmAnObject {

                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   ID        { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public float    Weight    { get; set; }
                  public bool     CanStack  { get; }

            }

            public class TShoes : ScriptableObject {

                  public string     itemName;
                  public int        health;
                  public float      weight;
                  public string[]   modifiers;
                  public GameObject prefab;
                  public Texture2D  icon;


                  internal Shoes ToData() {
                        return new Shoes() {
                              Name      = itemName,
                              Health    = new Health() {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct Shoes : IHaveWeight, ICanStack, IAmAnObject {

                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   ID        { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public float    Weight    { get; set; }
                  public bool     CanStack  { get; }

            }

            public class TSleeve : MonoBehaviour {

                  public string     itemName;
                  public int        health;
                  public float      weight;
                  public string[]   modifiers;
                  public GameObject prefab;
                  public Texture2D  icon;


                  internal Sleeve ToData() {
                        return new Sleeve {
                              Name      = itemName,
                              Health    = new Health() {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct Sleeve : IHaveWeight, ICanStack, IAmAnObject {

                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   ID        { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public float    Weight    { get; set; }
                  public bool     CanStack  { get; }

            }

      }

}