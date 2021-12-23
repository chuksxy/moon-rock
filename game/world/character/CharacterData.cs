using System;
using game.world.item;
using UnityEngine;

namespace game.world.character {

      public static partial class Character {

            public class Template : ScriptableObject {

                  public string   characterName;
                  public int      maxHealth;
                  public float    maxEnergy;
                  public string[] tags;
                  public string[] modifiers;

                  public SkinnedMeshRenderer skeleton;

                  public Item.THat[]       head;
                  public Item.TBaseLayer[] body;
                  public Item.TSleeve[]    leftSleeve;
                  public Item.TSleeve[]    rightSleeve;
                  public Item.TOuterWear[] outerWear;
                  public Item.TShoes[]     shoes;

            }

            public struct Data : IHaveWeight {

                  public string ID     { get; set; }
                  public string Name   { get; set; }
                  public Health Health { get; set; }
                  public Energy Energy { get; set; }

                  public string[] Tags      { get; set; }
                  public string[] Modifiers { get; set; }

                  public string Base { get; set; }

                  public Item.Hat[]       Hats        { get; set; }
                  public Item.BaseLayer[] BaseLayer   { get; set; }
                  public Item.Sleeve[]    LeftSleeve  { get; set; }
                  public Item.Sleeve[]    RightSleeve { get; set; }
                  public Item.OuterWear[] OuterWear   { get; set; }
                  public Item.Shoes[]     Shoes       { get; set; }

                  public float Weight => Item.Weigh(Hats)
                                       + Item.Weigh(BaseLayer)
                                       + Item.Weigh(LeftSleeve)
                                       + Item.Weigh(RightSleeve)
                                       + Item.Weigh(OuterWear)
                                       + Item.Weigh(Shoes);

            }

      }

}