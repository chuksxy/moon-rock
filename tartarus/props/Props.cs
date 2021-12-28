using System.Collections.Generic;
using tartarus.graph;
using UnityEngine.Rendering;

/*
 * Node Property creation DSL.
 */
namespace tartarus.props {

      public class Props {

            private SerializedDictionary<string, float>  Floats   { get; set; }
            private SerializedDictionary<string, bool>   Booleans { get; set; }
            private SerializedDictionary<string, int>    Ints     { get; set; }
            private SerializedDictionary<string, string> Strings  { get; set; }


            public static Props Empty() {
                  return new Props {
                        Floats   = new SerializedDictionary<string, float>(),
                        Booleans = new SerializedDictionary<string, bool>(),
                        Ints     = new SerializedDictionary<string, int>(),
                        Strings  = new SerializedDictionary<string, string>()
                  };
            }


            // TODO:: Implement!
            // Clone property values. Could have side effects!
            public Props Clone() {
                  return new Props {
                        Floats   = new SerializedDictionary<string, float>(),
                        Booleans = new SerializedDictionary<string, bool>(),
                        Ints     = new SerializedDictionary<string, int>(),
                        Strings  = new SerializedDictionary<string, string>()
                  };
            }


            public class Builder {

                  private Dictionary<string, float>  AllFloats   { get; set; }
                  private Dictionary<string, bool>   AllBooleans { get; set; }
                  private Dictionary<string, int>    AllInts     { get; set; }
                  private Dictionary<string, string> AllStrings  { get; set; }


                  public Builder(
                        Dictionary<string, float>  allFloats   = null,
                        Dictionary<string, bool>   allBooleans = null,
                        Dictionary<string, int>    allInts     = null,
                        Dictionary<string, string> allStrings  = null
                  ) {
                        AllFloats   = allFloats;
                        AllBooleans = allBooleans;
                        AllInts     = allInts;
                        AllStrings  = allStrings;
                  }


                  public static Builder NewGroup(string name) {
                        return new Builder();
                  }


                  public static Props NewProperty(string propertyID, int value) {
                        return new Builder(allInts: new Dictionary<string, int> {
                              {propertyID, value}
                        }).Build();
                  }


                  public static Props NewProperty(string propertyID, float value) {
                        return new Builder(allFloats: new Dictionary<string, float>() {
                              {propertyID, value}
                        }).Build();
                  }


                  public static Props NewProperty(string propertyID, string value) {
                        return new Builder(allStrings: new Dictionary<string, string>() {
                              {propertyID, value}
                        }).Build();
                  }


                  public static Props NewProperty(string propertyID, bool value) {
                        return new Builder(allBooleans: new Dictionary<string, bool>() {
                              {propertyID, value}
                        }).Build();
                  }


                  public Builder AddProperty(string propertyID, int value) {
                        return this;
                  }


                  public Builder AddProperty(string propertyID, float value) {
                        return this;
                  }


                  public Builder AddProperty(string propertyID, string value) {
                        return this;
                  }


                  public Builder AddProperty(string propertyID, bool value) {
                        return this;
                  }


                  public Props Build() {
                        return new Props();
                  }

            }

      }

}