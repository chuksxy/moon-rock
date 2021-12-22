using game.world.item;
using UnityEngine;

namespace game.world.character {

      public static partial class Character {

            public class Template : ScriptableObject {

                  public string characterName;
                  public int    health;
                  public float  energy;

                  public SkinnedMeshRenderer skeleton;

                  public Item.THat[]       head;
                  public Item.TBaseLayer[] body;
                  public Item.TSleeve[]    leftSleeve;
                  public Item.TSleeve[]    rightSleeve;
                  public Item.TOuterWear[] outerWear;
                  public Item.TShoes[]     shoes;

            }

            public struct Data {

                  public string   Name      { get; set; }
                  public int      Health    { get; set; }
                  public float    Energy    { get; set; }
                  public string[] Modifiers { get; set; }

                  public string Base { get; set; }


                  public Item.Hat[]       Hats        { get; set; }
                  public Item.BaseLayer[] BaseLayer   { get; set; }
                  public Item.Sleeve[]    LeftSleeve  { get; set; }
                  public Item.Sleeve[]    RightSleeve { get; set; }
                  public Item.OuterWear[] OuterWear   { get; set; }
                  public Item.Shoes[]     Shoes       { get; set; }


                  public Data(
                        string           name,
                        int              health,
                        float            energy,
                        string[]         modifiers,
                        string           @base,
                        Item.Hat[]       hats,
                        Item.BaseLayer[] baseLayer,
                        Item.Sleeve[]    leftSleeve,
                        Item.Sleeve[]    rightSleeve,
                        Item.OuterWear[] outerWear,
                        Item.Shoes[]     shoes) {
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

            }

            public class Builder {

                  private Item.BaseLayer[] _baseLayer;
                  private float            _energy;
                  private Item.Hat[]       _hats;
                  private int              _health;
                  private Item.Sleeve[]    _leftSleeves;
                  private string[]         _modifiers;
                  private string           _name;
                  private Item.OuterWear[] _outerWear;
                  private Item.Sleeve[]    _rightSleeves;
                  private Item.Shoes[]     _shoes;
                  private string           _skeleton;


                  public static Builder Make() {
                        return new Builder();
                  }


                  public Builder AddName(string name) {
                        _name = name;
                        return this;
                  }


                  public Builder AddHealth(int health) {
                        _health = health;
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
                              _name,
                              _health,
                              _energy,
                              _modifiers,
                              _skeleton,
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