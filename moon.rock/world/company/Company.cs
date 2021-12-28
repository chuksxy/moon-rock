using tartarus.graph;

namespace moon.rock.world.company {

      public static class Company {

            public static Graph.Node OgunMotors() {
                  return Graph.Node.New("ogun-motors.unassigned")
                              .Tag("electric")
                              .Tag("motor")
                              .Tag("hardware");
            }


            public static Graph.Node PeterAndLawansonInc() {
                  return Graph.Node.New("peter-and-lawanson.unassigned")
                              .Tag("electric")
                              .Tag("motor")
                              .Tag("hardware")
                              .Tag("middleware");
            }


            public static Graph.Node HouseOfMaalpertuus() {
                  return Graph.Node.New("house.of.maalpertuus.unassigned")
                              .Tag("electric")
                              .Tag("motor")
                              .Tag("hardware")
                              .Tag("institute")
                              .Tag("research")
                              .Tag("software");
            }

      }

}