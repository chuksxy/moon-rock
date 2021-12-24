namespace game.world.property {

      public static partial class Property {

            public interface IHaveWeight {

                  float Weight { get; }

            }

            public interface ICanStack {

                  bool CanStack { get; }

            }

            public interface IAmAnObject { }

            public interface IModify {

                  string ID { get; }

            }

            public class Health {

                  public int      Max       { get; set; }
                  public int      Current   { get; set; }
                  public string[] Modifiers { get; set; }

            }

            public class Energy {

                  public float    Max       { get; set; }
                  public float    Current   { get; set; }
                  public string[] Modifiers { get; set; }

            }

      }

}