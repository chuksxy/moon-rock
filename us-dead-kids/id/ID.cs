using System;

namespace us_dead_kids.id {

      public static class ID {

            public static string GenerateFlake(string prefix) {
                  var id = System.Guid.NewGuid().ToString();
                  return string.IsNullOrEmpty(prefix) ? id : $"{prefix}_id";
            }

      }

}