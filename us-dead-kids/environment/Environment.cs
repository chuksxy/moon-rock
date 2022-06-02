namespace us_dead_kids.environment {

      public enum Env {

            STAGING,
            PRODUCTION

      }

      public static class Environment {

            public static readonly Env Current = Env.STAGING;


            public static string Path() {
                  return $"us-dead-kids/{Env.STAGING.ToString().ToLower()}";
            }

      }

}