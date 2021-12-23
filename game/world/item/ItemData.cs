using System;
using System.Collections.Generic;
using UnityEngine;

namespace game.world.item {

      public static partial class Item {


            public class THat : ScriptableObject {

                  [SerializeField] private string   itemName;
                  [SerializeField] private int      health;
                  [SerializeField] private float    weight;
                  [SerializeField] private string[] modifiers;


                  internal Hat ToData() {
                        return new Hat() {
                              Name      = itemName,
                              Health    = new Health() {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct Hat : IHaveWeight, ICanStack {


                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public float    Weight    { get; set; }
                  public bool     CanStack  { get; }

            }

            public class TBaseLayer : ScriptableObject {

                  [SerializeField] private string   itemName;
                  [SerializeField] private int      health;
                  [SerializeField] private float    weight;
                  [SerializeField] private string[] modifiers;


                  internal BaseLayer ToData() {
                        return new BaseLayer {
                              Name      = itemName,
                              Health    = new Health() {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct BaseLayer : IHaveWeight, ICanStack {

                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public float    Weight    { get; set; }
                  public bool     CanStack  { get; }

            }

            public class TOuterWear : ScriptableObject {

                  [SerializeField] private string   itemName;
                  [SerializeField] private int      health;
                  [SerializeField] private float    weight;
                  [SerializeField] private string[] modifiers;


                  internal OuterWear ToData() {
                        return new OuterWear {
                              Name      = itemName,
                              Health    = new Health() {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct OuterWear : IHaveWeight, ICanStack {

                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public float    Weight    { get; set; }
                  public bool     CanStack  { get; }

            }

            public class TShoes : ScriptableObject {

                  [SerializeField] private string   itemName;
                  [SerializeField] private int      health;
                  [SerializeField] private float    weight;
                  [SerializeField] private string[] modifiers;


                  internal Shoes ToData() {
                        return new Shoes() {
                              Name      = itemName,
                              Health    = new Health() {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct Shoes : IHaveWeight, ICanStack {

                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public float    Weight    { get; set; }
                  public bool     CanStack  { get; }

            }

            public class TSleeve : MonoBehaviour {

                  [SerializeField] private string   itemName;
                  [SerializeField] private int      health;
                  [SerializeField] private float    weight;
                  [SerializeField] private string[] modifiers;


                  internal Sleeve ToData() {
                        return new Sleeve {
                              Name      = itemName,
                              Health    = new Health() {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct Sleeve : IHaveWeight, ICanStack {

                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public float    Weight    { get; set; }
                  public bool     CanStack  { get; }

            }

      }

}