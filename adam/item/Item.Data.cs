using adam.props;
using UnityEngine;

namespace adam.item {

      public static partial class Item {


            public class THat : ScriptableObject {

                  public string     itemName;
                  public int        health;
                  public float      weight;
                  public string[]   modifiers;
                  public GameObject prefab;
                  public Texture2D  icon;


                  internal Hat ToData() {
                        return new Hat {
                              Name      = itemName,
                              Health    = new Props.Health {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct Hat : Props.IHaveWeight, Props.ICanStack, Props.IAmAnObject {


                  public string[]        Tags          { get; set; }
                  public string[]        Modifiers     { get; set; }
                  public string          ID            { get; set; }
                  public Vector3         WorldPosition { get; set; }
                  public Vector3         WorldRotation { get; set; }
                  public string          Name          { get; set; }
                  public Props.Health Health        { get; set; }
                  public float           Weight        { get; set; }
                  public bool            CanStack      { get; }

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
                              Health    = new Props.Health {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct BaseLayer : Props.IHaveWeight, Props.ICanStack, Props.IAmAnObject {

                  public string[]        Tags          { get; set; }
                  public string[]        Modifiers     { get; set; }
                  public string          ID            { get; set; }
                  public Vector3         WorldPosition { get; set; }
                  public Vector3         WorldRotation { get; set; }
                  public string          Name          { get; set; }
                  public Props.Health Health        { get; set; }
                  public float           Weight        { get; set; }
                  public bool            CanStack      { get; }

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
                              Health    = new Props.Health {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct OuterWear : Props.IHaveWeight, Props.ICanStack, Props.IAmAnObject {

                  public string[]        Tags          { get; set; }
                  public string[]        Modifiers     { get; set; }
                  public string          ID            { get; set; }
                  public Vector3         WorldPosition { get; set; }
                  public Vector3         WorldRotation { get; set; }
                  public string          Name          { get; set; }
                  public Props.Health Health        { get; set; }
                  public float           Weight        { get; set; }
                  public bool            CanStack      { get; }

            }

            public class TShoes : ScriptableObject {

                  public string     itemName;
                  public int        health;
                  public float      weight;
                  public string[]   modifiers;
                  public GameObject prefab;
                  public Texture2D  icon;


                  internal Shoes ToData() {
                        return new Shoes {
                              Name      = itemName,
                              Health    = new Props.Health {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct Shoes : Props.IHaveWeight, Props.ICanStack, Props.IAmAnObject {

                  public string[]        Tags          { get; set; }
                  public string[]        Modifiers     { get; set; }
                  public string          ID            { get; set; }
                  public Vector3         WorldPosition { get; set; }
                  public Vector3         WorldRotation { get; set; }
                  public string          Name          { get; set; }
                  public Props.Health Health        { get; set; }
                  public float           Weight        { get; set; }
                  public bool            CanStack      { get; }

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
                              Health    = new Props.Health {Current = health, Max = health},
                              Weight    = weight,
                              Modifiers = modifiers
                        };
                  }

            }

            public struct Sleeve : Props.IHaveWeight, Props.ICanStack, Props.IAmAnObject {

                  public string[]        Tags          { get; set; }
                  public string[]        Modifiers     { get; set; }
                  public string          ID            { get; set; }
                  public Vector3         WorldPosition { get; set; }
                  public Vector3         WorldRotation { get; set; }
                  public string          Name          { get; set; }
                  public Props.Health Health        { get; set; }
                  public float           Weight        { get; set; }
                  public bool            CanStack      { get; }

            }

      }

}