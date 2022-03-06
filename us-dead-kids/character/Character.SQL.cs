namespace us_dead_kids.character {

      public partial class Character {

            public static class SQL {

                  public static string ReadCharacter() {
                        return "select * " +
                               "from character " +
                               "where character.ID = ?";
                  }

            }

      }

}