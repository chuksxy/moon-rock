using System.Collections.Generic;
using game.table;
using game.world.character;
using game.world.item;
using UnityEngine.Rendering;

namespace game.world {

      public static partial class World {

            private static readonly Registry                Main             = Registry.LoadMain();
            private static readonly Table<string, Registry> CachedRegistries = new Table<string, Registry>();

            public class Registry {

                  // `Character Tables` for characters located in the world.
                  private readonly Table<string, Character.Data>  _characters;
                  private readonly Table<string, string>          _characterIDsToZoneIDs;
                  private readonly Table<string, HashSet<string>> _zoneIDsToCharactersIDs;

                  // `Item Tables` for items located in the world.
                  private readonly Table<string, string>          _itemIDsToZoneIDs;
                  private readonly Table<string, HashSet<string>> _zoneIDsToItemIDs;


                  private Registry(
                        Table<string, Character.Data>  characters,
                        Table<string, string>          characterIDsToZoneIDs,
                        Table<string, HashSet<string>> zoneIDsToCharactersIDs,
                        Table<string, string>          itemIDsToZoneIDs,
                        Table<string, HashSet<string>> zoneIDsToItemIDs
                  ) {
                        _characterIDsToZoneIDs  = characterIDsToZoneIDs;
                        _characters             = characters;
                        _zoneIDsToCharactersIDs = zoneIDsToCharactersIDs;
                        _itemIDsToZoneIDs       = itemIDsToZoneIDs;
                        _zoneIDsToItemIDs       = zoneIDsToItemIDs;
                  }


                  // Get Registry that has already loaded else fallback and load it or use the main one.
                  public static Registry Get(string registryID) {
                        if ("main.registry".Equals(registryID)) return Main;

                        if (CachedRegistries.ContainsKey(registryID)) return CachedRegistries[registryID];

                        var loadedRegistry = Load(registryID);
                        if (loadedRegistry != null) CachedRegistries.Add(registryID, loadedRegistry);
                        return loadedRegistry ?? Main;
                  }


                  // Load Main registry from disk.
                  public static Registry LoadMain() {
                        return Load("main.registry");
                  }


                  // Load registry by ID from disk.
                  public static Registry Load(string registryID) {
                        var characterIDs           = new Table<string, Character.Data>();
                        var characterIDsToZoneIDs  = new Table<string, string>();
                        var zoneIDsToCharactersIDs = new Table<string, HashSet<string>>();
                        var itemIDsToZoneIDs       = new Table<string, string>();
                        var zoneIDsToItemIDs       = new Table<string, HashSet<string>>();
                        return new Registry(
                              characterIDs, characterIDsToZoneIDs, zoneIDsToCharactersIDs, itemIDsToZoneIDs, zoneIDsToItemIDs);
                  }


                  // Register Character by ID in a Zone.
                  public void RegisterCharacter(string characterID, string zoneID, Character.Data data) {
                        if (_characters.ContainsKey(characterID)) return;

                        _characters.Add(characterID, data);
                        EnterZone(zoneID, characterID);
                  }


                  // De-Register Character by ID.
                  public void DeRegisterCharacter(string characterID) {
                        if (!_characters.ContainsKey(characterID)) return;

                        _characters.Remove(characterID);
                        LeaveZone(GetZoneID(characterID), characterID);
                  }


                  public Character.Data GetCharacterData(string characterID) {
                        return !_characters.ContainsKey(characterID) ? new Character.Data() : _characters[characterID];
                  }


                  public string GetZoneID(string characterID) {
                        return !_characterIDsToZoneIDs.ContainsKey(characterID)
                              ? "no.zone.ID"
                              : _characterIDsToZoneIDs[characterID];
                  }


                  public HashSet<string> GetAllCharactersInZone(string zoneID) {
                        return !_zoneIDsToCharactersIDs.ContainsKey(zoneID)
                              ? new HashSet<string>()
                              : _zoneIDsToCharactersIDs[zoneID];
                  }


                  public void EnterZone(string zoneID, string characterID) {
                        if (_characterIDsToZoneIDs.ContainsKey(characterID)) return;

                        _characterIDsToZoneIDs.Add(characterID, zoneID);
                        _zoneIDsToCharactersIDs[zoneID].Add(characterID);
                  }


                  public void LeaveZone(string zoneId, string characterID) {
                        if (!_characterIDsToZoneIDs.ContainsKey(characterID)) return;

                        _characterIDsToZoneIDs.Remove(characterID);
                        _zoneIDsToCharactersIDs[zoneId].Remove(characterID);
                  }


                  public void Persist() { }

            }

      }

}