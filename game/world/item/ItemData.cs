using UnityEngine;

namespace game.world.item {

      public static partial class Item {


            public class THat : ScriptableObject { }

            public struct Hat : IHaveWeight {


                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   Name      { get; set; }
                  public int      Health    { get; set; }
                  public float    Weight    { get; set; }

            }

            public class TBaseLayer : ScriptableObject { }

            public struct BaseLayer : IHaveWeight {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }
                  public float    Weight { get; set; }

            }

            public class TOuterWear : ScriptableObject { }

            public struct OuterWear : IHaveWeight {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }
                  public float    Weight { get; set; }

            }

            public class TShoes : ScriptableObject { }

            public struct Shoes : IHaveWeight {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }
                  public float    Weight { get; set; }

            }

            public class TSleeve : MonoBehaviour { }

            public struct Sleeve : IHaveWeight {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }
                  public float    Weight { get; set; }

            }

      }

}