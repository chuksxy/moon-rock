namespace us_dead_kids.environment {

      public enum Env {

            STAGING,
            PRODUCTION

      }

      public static class Environment {

            public static Env GetCurrent() {
                  return Env.STAGING;
            }

      }

}