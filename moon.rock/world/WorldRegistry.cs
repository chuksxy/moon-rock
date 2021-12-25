using System.Collections.Generic;
using moon.rock.table;
using moon.rock.world.property;
using Character = moon.rock.world.character.Character;

namespace moon.rock.world {

      public static partial class World {

            private static readonly Registry                Main             = Registry.LoadMain();
            private static readonly Table<string, Registry> CachedRegistries = new Table<string, Registry>();

            public class Registry {

                  // `Character Tables` for characters located in the world.
                  private readonly Table<string, Character.Data>  _characters;
                  private readonly Table<string, string>          _characterIDsToZoneIDs;
                  private readonly Table<string, HashSet<string>> _zoneIDsToCharactersIDs;

                  // `Item Tables` for items located in the world.
                  private readonly Table<string, Property.IAmAnObject> _objects;
                  private readonly Table<string, HashSet<string>>      _zoneIDsToObjectsIDs;


                  private Registry(
                        Table<string, Character.Data>       characters,
                        Table<string, string>               characterIDsToZoneIDs,
                        Table<string, HashSet<string>>      zoneIDsToCharactersIDs,
                        Table<string, Property.IAmAnObject> objects,
                        Table<string, HashSet<string>>      zoneIDsToObjectsIDs
                  ) {
                        _characterIDsToZoneIDs  = characterIDsToZoneIDs;
                        _characters             = characters;
                        _zoneIDsToCharactersIDs = zoneIDsToCharactersIDs;
                        _objects                = objects;
                        _zoneIDsToObjectsIDs    = zoneIDsToObjectsIDs;
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
                        var characters             = new Table<string, Character.Data>();
                        var characterIDsToZoneIDs  = new Table<string, string>();
                        var zoneIDsToCharactersIDs = new Table<string, HashSet<string>>();
                        var objects                = new Table<string, Property.IAmAnObject>();
                        var zoneIDsToObjectIDs     = new Table<string, HashSet<string>>();
                        return new Registry(
                              characters,
                              characterIDsToZoneIDs,
                              zoneIDsToCharactersIDs,
                              objects,
                              zoneIDsToObjectIDs
                        );
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

                        ReadyZoneForCharacters(zoneID);
                        _zoneIDsToCharactersIDs[zoneID].Add(characterID);
                  }


                  public void LeaveZone(string zoneID, string characterID) {
                        if (!_characterIDsToZoneIDs.ContainsKey(characterID)) return;

                        _characterIDsToZoneIDs.Remove(characterID);

                        ReadyZoneForCharacters(zoneID);
                        _zoneIDsToCharactersIDs[zoneID].Remove(characterID);
                  }


                  public void RegisterObject(string objectID, string zoneID, Property.IAmAnObject @object) {
                        if (_objects.ContainsKey(objectID)) return;

                        _objects.Add(objectID, @object);

                        ReadyZoneForObjects(zoneID);
                        _zoneIDsToObjectsIDs[zoneID].Add(objectID);
                  }


                  // Ready Zone specified by `ID` to store characters.
                  private void ReadyZoneForCharacters(string zoneID) {
                        if (_zoneIDsToCharactersIDs[zoneID] == null) {
                              _zoneIDsToCharactersIDs.Add(zoneID, new HashSet<string>());
                        }
                  }


                  // Ready the Zone specified by `ID` to store Objects.
                  private void ReadyZoneForObjects(string zoneID) {
                        if (_zoneIDsToObjectsIDs[zoneID] == null) {
                              _zoneIDsToObjectsIDs.Add(zoneID, new HashSet<string>());
                        }
                  }


                  public void Persist(string registryID) {
                        if (!CachedRegistries.ContainsKey(registryID)) return;
                        // Save the Registry to Disk using ES3 Save.
                  }

            }

      }

}