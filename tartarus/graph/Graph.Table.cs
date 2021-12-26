using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

namespace tartarus.graph {

      public partial class Graph {

            public class Table<TK, TV> : SerializedDictionary<TK, TV> {

                  public static Table<TK, TV> From(Dictionary<TK, TV> dictionary) {
                        var table = new Table<TK, TV>();
                        dictionary.ToList().ForEach(entry => table.Add(entry.Key, entry.Value));
                        return table;
                  }


                  public static Table<TK, TV> MergeRight(Table<TK, TV> source, Table<TK, TV> target) {
                        var table = new Table<TK, TV>();
                        source.Where(e => !table.ContainsKey(e.Key)).ToList().ForEach(entry => table.Add(entry.Key, entry.Value));
                        target.Where(e => !table.ContainsKey(e.Key)).ToList().ForEach(entry => table.Add(entry.Key, entry.Value));


                        return table;
                  }

            }

      }

}