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

                  public string   ID        { get; set; }
                  public string   Name      { get; set; }
                  public Health   Health    { get; set; }
                  public Energy   Energy    { get; set; }
                  public string[] Modifiers { get; set; }

                  public string Base { get; set; }


                  public Item.Hat[]       Hats        { get; set; }
                  public Item.BaseLayer[] BaseLayer   { get; set; }
                  public Item.Sleeve[]    LeftSleeve  { get; set; }
                  public Item.Sleeve[]    RightSleeve { get; set; }
                  public Item.OuterWear[] OuterWear   { get; set; }
                  public Item.Shoes[]     Shoes       { get; set; }


                  public Data(
                        string           id,
                        string           name,
                        Health           health,
                        Energy           energy,
                        string[]         modifiers,
                        string           @base,
                        Item.Hat[]       hats,
                        Item.BaseLayer[] baseLayer,
                        Item.Sleeve[]    leftSleeve,
                        Item.Sleeve[]    rightSleeve,
                        Item.OuterWear[] outerWear,
                        Item.Shoes[]     shoes
                  ) {
                        ID          = id;
                        Name        = name;
                        Health      = health;
                        Energy      = energy;
                        Modifiers   = modifiers;
                        Base        = @base;
                        Hats        = hats;
                        BaseLayer   = baseLayer;
                        LeftSleeve  = leftSleeve;
                        RightSleeve = rightSleeve;
                        OuterWear   = outerWear;
                        Shoes       = shoes;
                  }


                  public float Weight => Item.Weigh(Hats)
                                       + Item.Weigh(BaseLayer)
                                       + Item.Weigh(LeftSleeve)
                                       + Item.Weigh(RightSleeve)
                                       + Item.Weigh(OuterWear)
                                       + Item.Weigh(Shoes);

            }

            public class Builder {

                  private string           _ID;
                  private string           _name;
                  private Health           _health;
                  private Energy           _energy;
                  private Item.BaseLayer[] _baseLayer;
                  private Item.Hat[]       _hats;
                  private Item.Sleeve[]    _leftSleeves;
                  private string[]         _modifiers;
                  private Item.OuterWear[] _outerWear;
                  private Item.Sleeve[]    _rightSleeves;
                  private Item.Shoes[]     _shoes;
                  private string           _base;


                  public static Builder Make() {
                        return new Builder();
                  }


                  public Builder AddID(string @ID) {
                        _ID = @ID;
                        return this;
                  }


                  public Builder AddName(string name) {
                        _name = name;
                        return this;
                  }


                  public Builder AddHealth(Health health) {
                        _health = health;
                        return this;
                  }


                  public Builder AddEnergy(Energy energy) {
                        _energy = energy;
                        return this;
                  }


                  public Builder AddBase(string @base) {
                        _base = @base;
                        return this;
                  }


                  public Builder AddModifiers(string[] modifiers) {
                        _modifiers = modifiers;
                        return this;
                  }


                  public Builder AddHats(Item.Hat[] hats) {
                        _hats = hats;
                        return this;
                  }


                  public Builder AddBaseLayer(Item.BaseLayer[] baseLayer) {
                        _baseLayer = baseLayer;
                        return this;
                  }


                  public Builder AddLeftSleeves(Item.Sleeve[] sleeves) {
                        _leftSleeves = sleeves;
                        return this;
                  }


                  public Builder AddRightSleeves(Item.Sleeve[] sleeves) {
                        _rightSleeves = sleeves;
                        return this;
                  }


                  public Builder AddOuterWear(Item.OuterWear[] outerWear) {
                        _outerWear = outerWear;
                        return this;
                  }


                  public Builder AddShoes(Item.Shoes[] shoes) {
                        _shoes = shoes;
                        return this;
                  }


                  public Data Build() {
                        return new Data(
                              _ID,
                              _name,
                              _health,
                              _energy,
                              _modifiers,
                              _base,
                              _hats,
                              _baseLayer,
                              _leftSleeves,
                              _rightSleeves,
                              _outerWear,
                              _shoes);
                  }

            }


      }

}