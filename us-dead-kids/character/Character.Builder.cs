using System;
using System.Collections.Generic;
using UnityEngine;
using us_dead_kids.attribute.health;

namespace us_dead_kids.character {

      public partial class Character {

            public class Build {

                  public string          Name       { get; set; }
                  public int             Priority   { get; set; }
                  public int             MaxHealth  { get; set; }
                  public float           MaxStamina { get; set; }
                  public float           MaxSpeed   { get; set; }
                  public HashSet<string> Tags       { get; set; }
                  public List<string>    Equipment  { get; set; }

            }

            public class Builder {

                  // Create a new character in the world.
                  public static Character Create(Build b) {
                        var c = new Character() {
                              ID       = id.ID.GenerateFlake("character"),
                              Name     = b.Name,
                              Priority = b.Priority
                        };


                        try {
                              // Make it transactional
                              // Persist in health table
                              // Persist in stamina table
                              // persist in speed table
                              us_dead_kids.attribute.health.Health.Service.Persist(new Health() {
                                    ID       = id.ID.GenerateFlake("health"),
                                    EntityID = c.ID,
                                    Current  = b.MaxHealth,
                                    Max      = b.MaxHealth
                              });
                              Service.Persist(c);
                        }
                        catch (Exception e) {
                              Debug.LogError($"exception persisting character [{b.Name}].\n {e}");
                              return null;
                        }

                        return c;
                  }

            }

      }

}