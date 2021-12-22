using game.world.item;

namespace game.world.character {
      public struct CharacterData {
            public string   Name      { get; set; }
            public int      Health    { get; set; }
            public float    Energy    { get; set; }
            public string[] Modifiers { get; set; }

            public string Base { get; set; }


            public Hat[]       Hats        { get; set; }
            public BaseLayer[] BaseLayer   { get; set; }
            public Sleeve[]    LeftSleeve  { get; set; }
            public Sleeve[]    RightSleeve { get; set; }
            public OuterWear[] OuterWear   { get; set; }
            public Shoes[]     Shoes       { get; set; }


            public CharacterData(
                  string      name,
                  int         health,
                  float       energy,
                  string[]    modifiers,
                  string      @base,
                  Hat[]       hats,
                  BaseLayer[] baseLayer,
                  Sleeve[]    leftSleeve,
                  Sleeve[]    rightSleeve,
                  OuterWear[] outerWear,
                  Shoes[]     shoes) {
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

      public class CharacterBuilder {
            private string      _name;
            private int         _health;
            private float       _energy;
            private string[]    _modifiers;
            private string      _skeleton;
            private Hat[]       _hats;
            private BaseLayer[] _baseLayer;
            private Sleeve[]    _leftSleeves;
            private Sleeve[]    _rightSleeves;
            private OuterWear[] _outerWear;
            private Shoes[]     _shoes;

            public static CharacterBuilder Make() {
                  return new CharacterBuilder();
            }

            public CharacterBuilder AddName(string name) {
                  _name = name;
                  return this;
            }

            public CharacterBuilder AddHealth(int health) {
                  _health = health;
                  return this;
            }

            public CharacterBuilder AddModifiers(string[] modifiers) {
                  _modifiers = modifiers;
                  return this;
            }

            public CharacterBuilder AddHats(Hat[] hats) {
                  _hats = hats;
                  return this;
            }

            public CharacterBuilder AddBaseLayer(BaseLayer[] baseLayer) {
                  _baseLayer = baseLayer;
                  return this;
            }

            public CharacterBuilder AddLeftSleeves(Sleeve[] sleeves) {
                  _leftSleeves = sleeves;
                  return this;
            }

            public CharacterBuilder AddRightSleeves(Sleeve[] sleeves) {
                  _rightSleeves = sleeves;
                  return this;
            }

            public CharacterBuilder AddOuterWear(OuterWear[] outerWear) {
                  _outerWear = outerWear;
                  return this;
            }

            public CharacterBuilder AddShoes(Shoes[] shoes) {
                  _shoes = shoes;
                  return this;
            }

            public CharacterData Build() {
                  return new CharacterData(
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