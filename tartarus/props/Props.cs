using System.Collections.Generic;

namespace tartarus.props {

      public class Props {

            public HashSet<Float>   Floats   { get; set; }
            public HashSet<Boolean> Booleans { get; set; }
            public HashSet<Int>     Ints     { get; set; }
            public HashSet<String>  Strings  { get; set; }


            public static Props Empty() {
                  return new Props {
                        Floats   = new HashSet<Float>(),
                        Booleans = new HashSet<Boolean>(),
                        Ints     = new HashSet<Int>(),
                        Strings  = new HashSet<String>()
                  };
            }


            public struct Boolean {

                  public string ID    { get; set; }
                  public bool   Value { get; set; }

            }

            public struct Int {

                  public string ID    { get; set; }
                  public int    Value { get; set; }

            }

            public struct Float {

                  public string ID    { get; set; }
                  public float  Value { get; set; }

            }

            public struct String {

                  public string ID    { get; set; }
                  public string Value { get; set; }

            }

      }

}