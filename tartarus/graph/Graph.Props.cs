using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

/*
 * Node Property creation DSL.
 */
namespace tartarus.graph {

      public partial class Graph {

            public partial class Props {

                  private HashSet<string>                      Groups  { get; }
                  private SerializedDictionary<string, object> Entries { get; }


                  internal Props(HashSet<string> groups, SerializedDictionary<string, object> entries) {
                        Groups  = groups;
                        Entries = entries;
                  }


                  public static Props Empty() {
                        return new Props(new HashSet<string>(), new SerializedDictionary<string, object>());
                  }


                  // Get property by type.
                  public Ref<T> Get<T>(string name) {
                        if (!Entries.ContainsKey(name)) return Ref.NoProp() as Ref<T>;
                        return new Ref<T>(name, (T) Entries[name]);
                  }


                  // Get `Property` By [group] [name].
                  public Ref<T> GetByGroup<T>(string group, string name) {
                        var key = $"{group}.{name}";
                        if (!Entries.ContainsKey(key)) return Ref.NoProp() as Ref<T>;
                        return new Ref<T>(key, (T) Entries[key]);
                  }


                  // Get `Property Group`.
                  public Ref.Group GetGroup(string groupName) {
                        if (!Groups.Contains(groupName)) return new Ref.Group("", new Dictionary<string, object>());

                        var properties = new Dictionary<string, object>();
                        Entries.Where(entry => entry.Key.StartsWith(groupName))
                               .ToList()
                               .ForEach(entry => properties.Add(entry.Key, entry.Value));

                        return new Ref.Group(groupName, properties);
                  }


                  // Deep Clone property values.
                  public Props DeepClone() {
                        var clone = new SerializedDictionary<string, object>();
                        Entries.ToList().ForEach(entry => clone.Add(entry.Key, entry.Value));
                        return new Props(new HashSet<string>(Groups), clone);
                  }


                  // TODO:: Implement!
                  public Props Merge(Props props) {
                        return this;
                  }


                  public class Builder {

                        private readonly HashSet<string>            _groups;
                        private readonly Dictionary<string, object> _properties;


                        private Builder(HashSet<string> groups, Dictionary<string, object> properties) {
                              _groups     = groups;
                              _properties = properties;
                        }


                        public static Builder New() {
                              return new Builder(new HashSet<string>(), new Dictionary<string, object>());
                        }


                        public Group NewGroup(string name) {
                              _groups.Add(name);
                              return new Group(this, name, new Dictionary<string, object>());
                        }


                        private Builder AddGroup(Group group) {
                              group.Properties.ToList().ForEach(prop => _properties.Add($"{group.Name}.{prop.Key}", prop.Value));
                              return this;
                        }


                        public Builder NewProperty(string propertyID, int value) {
                              _properties.Add(propertyID, value);
                              return this;
                        }


                        public Builder NewProperty(string propertyID, float value) {
                              _properties.Add(propertyID, value);
                              return this;
                        }


                        public Builder NewProperty(string propertyID, string value) {
                              _properties.Add(propertyID, value);
                              return this;
                        }


                        public Builder NewProperty(string propertyID, bool value) {
                              _properties.Add(propertyID, value);
                              return this;
                        }


                        public Props Build() {
                              var props = new SerializedDictionary<string, object>();
                              _properties.ToList().ForEach(prop => props.Add(prop.Key, prop.Value));
                              return new Props(_groups, props);
                        }


                        public class Group {

                              private readonly Builder _builder;

                              internal string                     Name       { get; }
                              internal Dictionary<string, object> Properties { get; }


                              internal Group(Builder builder, string name, Dictionary<string, object> properties) {
                                    _builder   = builder;
                                    Name       = name;
                                    Properties = properties;
                              }


                              public Group AddProperty(string propertyID, int value) {
                                    Properties.Add(propertyID, value);
                                    return this;
                              }


                              public Group AddProperty(string propertyID, float value) {
                                    Properties.Add(propertyID, value);
                                    return this;
                              }


                              public Group AddProperty(string propertyID, string value) {
                                    Properties.Add(propertyID, value);
                                    return this;
                              }


                              public Group AddProperty(string propertyID, bool value) {
                                    Properties.Add(propertyID, value);
                                    return this;
                              }


                              public Builder Next() {
                                    return _builder.AddGroup(this);
                              }

                        }

                  }

            }

      }

}