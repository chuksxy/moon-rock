using System;
using System.Collections.Generic;
using UnityEngine;

namespace us_dead_kids.avatar {


      public class AvatarRegistry : MonoBehaviour {

            private readonly Dictionary<string, AvatarState> _stateByID = new Dictionary<string, AvatarState>();


            public AvatarState Read(string avatarID) {
                  if (_stateByID.ContainsKey(avatarID)) {
                        return _stateByID[avatarID];
                  }

                  Debug.LogWarning($"Attempting to access null avatar with ID [{avatarID}].");
                  return null;
            }


            public void Put(AvatarState state) {
                  if (_stateByID.ContainsKey(state.ID)) {
                        Debug.LogWarning($"An avatar with ID [{state.ID}] already exists.");
                        return;
                  }

                  _stateByID.Add(state.ID, state);
            }

      }

}