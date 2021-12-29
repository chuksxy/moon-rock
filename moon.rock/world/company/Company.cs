using tartarus.graph;

/*
 * All Major Companies operating within the world of Moon Rock.
 */
namespace moon.rock.world.company {

      public static class Company {

            public static string GetNameFromTag(string tag) {
                  return tag.Split(':')[1];
            }


            // Ogun Motors Electric Motors Company.
            public static Graph.Node OgunMotors() {
                  const string name = "ogun-motors";
                  return Graph.Node.New(name)
                              .Tag($"company:{name}")
                              .Tag($"compatibility:{name}")
                              .Tag("electric")
                              .Tag("motor")
                              .Tag("hardware");
            }


            // Peter And Lawanson Incorporated.
            public static Graph.Node PeterAndLawansonInc() {
                  const string name = "peter-and-lawanson";
                  return Graph.Node.New(name)
                              .Tag($"company:{name}")
                              .Tag($"compatibility:{name}")
                              .Tag("electric")
                              .Tag("motor")
                              .Tag("hardware")
                              .Tag("middleware");
            }


            // House of Maalpertuus Institute.
            public static Graph.Node HouseOfMaalpertuus() {
                  const string name = "house.of.maalpertuus";
                  return Graph.Node.New(name)
                              .Tag($"company:{name}")
                              .Tag($"compatibility:{name}")
                              .Tag("compatibility:all")
                              .Tag("electric")
                              .Tag("motor")
                              .Tag("hardware")
                              .Tag("institute")
                              .Tag("research")
                              .Tag("software");
            }

      }

}