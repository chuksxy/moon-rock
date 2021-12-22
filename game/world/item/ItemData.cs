using UnityEngine;

namespace game.world.item {

      public static partial class Item {

            public class THat : ScriptableObject { }

            public struct Hat {


                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }
                  public string   Name      { get; set; }
                  public int      Health    { get; set; }

            }

            public class TBaseLayer : ScriptableObject { }

            public struct BaseLayer {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }

            }

            public class TOuterWear : ScriptableObject { }

            public class OuterWear {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }

            }

            public class TShoes : ScriptableObject { }

            public struct Shoes {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }

            }

            public class TSleeve : MonoBehaviour { }

            public struct Sleeve {

                  public string[] Tags   { get; set; }
                  public string   Name   { get; set; }
                  public int      Health { get; set; }

            }

      }

}