using System;
using System.Collections.Generic;
using UnityEngine;

namespace us_dead_kids.avatar {

      public partial class Avatar {

            public class Registry : MonoBehaviour {

                  private readonly Dictionary<string, State> _stateByID = new Dictionary<string, State>();

                  private static Registry _registry;


                  private void Start() {
                        if (_registry != null) {
                              return;
                        }

                        _registry = this;
                  }


                  public static State Read(string avatarID) {
                        if (_registry == null) {
                              return null;
                        }

                        if (_registry._stateByID.ContainsKey(avatarID)) {
                              return _registry._stateByID[avatarID];
                        }

                        Debug.LogWarning($"Attempting to access null avatar with ID [{avatarID}].");
                        return null;
                  }


                  public static void Put(State state) {
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

}