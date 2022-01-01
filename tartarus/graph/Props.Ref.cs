using System.Collections.Generic;

namespace tartarus.graph {

      public partial class Graph {

            public partial class Props {


                  public class Ref : Ref<object> {

                        private Ref(string key, object value) : base(key, value) { }


                        public static Ref NoProp() {
                              return new Ref("", new object());
                        }


                        public class Group {

                              private readonly string                     _name;
                              private readonly Dictionary<string, object> _properties;


                              internal Group(string name, Dictionary<string, object> properties) {
                                    _name       = name;
                                    _properties = properties;
                              }


                              public T Get<T>(string name) where T : class {
                                    return _properties[$"{_name}.{name}"] as T;
                              }

                        }

                  }

                  public class Ref<T> {

                        public string Key   { get; }
                        public T      Value { get; }


                        internal Ref(string key, T value) {
                              Key   = key;
                              Value = value;
                        }

                  }

            }


      }

}