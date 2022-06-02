using System;
using System.Collections.Generic;
using UnityEngine;

namespace us_dead_kids.avatar {


      public class AvatarRegistry : MonoBehaviour {

            private readonly Dictionary<string, AvatarState> _stateByID = new Dictionary<string, AvatarState>();

            private static AvatarRegistry _registry;


            private void OnEnable() {
                  if (_registry != null) {
                        Debug.LogWarning("avatar registry already initialized.");
                        return;
                  }

                  _registry = this;
                  Debug.Log($"avatar registry initialised on [{gameObject.name}]");
            }


            public static AvatarState Read(string avatarID) {
                  if (_registry == null) {
                        return null;
                  }

                  if (_registry._stateByID.ContainsKey(avatarID)) {
                        return _registry._stateByID[avatarID];
                  }

                  Debug.LogWarning($"Attempting to access null avatar with ID [{avatarID}].");
                  return null;
            }


            public static void Put(AvatarState state) {
                  if (_registry == null) {
                        return;
                  }

                  if (_registry._stateByID.ContainsKey(state.ID)) {
                        Debug.LogWarning($"An avatar with ID [{state.ID}] already exists.");
                        return;
                  }

                  _registry._stateByID.Add(state.ID, state);
            }

      }

}