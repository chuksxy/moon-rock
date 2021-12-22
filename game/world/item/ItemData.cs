using UnityEngine;

namespace game.world.item {

      public static partial class Item {


            public class THat : ScriptableObject { }

            public struct Hat : IHaveWeight, ICanStack {


                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   Name      { get; set; }
                  public int      Health    { get; set; }
                  public float    Weight    { get; set; }

                  public bool CanStack { get; }

            }

            public class TBaseLayer : ScriptableObject { }

            public struct BaseLayer : IHaveWeight, ICanStack {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }
                  public float    Weight { get; set; }

                  public bool CanStack { get; }

            }

            public class TOuterWear : ScriptableObject { }

            public struct OuterWear : IHaveWeight, ICanStack {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }
                  public float    Weight { get; set; }

                  public bool CanStack { get; }

            }

            public class TShoes : ScriptableObject { }

            public struct Shoes : IHaveWeight, ICanStack {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }
                  public float    Weight { get; set; }

                  public bool CanStack { get; }

            }

            public class TSleeve : MonoBehaviour { }

            public struct Sleeve : IHaveWeight, ICanStack {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }
                  public float    Weight { get; set; }

                  public bool CanStack { get; }

            }

      }

}