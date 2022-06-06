using System;
using UnityEngine;
using us_dead_kids.avatar;
using us_dead_kids.lib.animation;

namespace us_dead_kids.game {

      public class GameManager : MonoBehaviour {

            private static AvatarRegistry    _avatarRegistry;
            private static AvatarService     _avatarService;
            private static AnimationRegistry _animationRegistry;


            public void OnEnable() {
                  _avatarRegistry    = gameObject.AddComponent<AvatarRegistry>();
                  _animationRegistry = gameObject.AddComponent<AnimationRegistry>();
                  _avatarService     = gameObject.AddComponent<AvatarService>();

                  _animationRegistry.LoadAll();
                  _animationRegistry.Init();
            }


            public static void ExecOnAvatarRegistry(Action<AvatarRegistry> callBack) {
                  if (_avatarRegistry == null) return;
                  callBack.Invoke(_avatarRegistry);
            }


            public static void ExecOnAvatarService(Action<AvatarService> callBack) {
                  if (_avatarService == null) return;
                  callBack.Invoke(_avatarService);
            }


            public static bool CheckAvatarRegistryCondition(Func<AvatarRegistry, bool> predicate) {
                  return _avatarRegistry != null && predicate.Invoke(_avatarRegistry);
            }


      }

}