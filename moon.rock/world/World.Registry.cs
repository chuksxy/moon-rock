using System.Collections.Generic;
using adam.character;
using adam.props;
using moon.rock.table;

namespace moon.rock.world {

      public static partial class World {

            private static readonly Registry                 Main             = Registry.LoadMain();
            private static readonly STable<string, Registry> CachedRegistries = new STable<string, Registry>();

            public class Registry {

                  private readonly STable<string, string> _characterIDsToZoneIDs;

                  // `Character Tables` for characters located in the world.
                  private readonly STable<string, Character.Data> _characters;

                  // `Item Tables` for items located in the world.
                  private readonly STable<string, Props.IAmAnObject> _objects;
                  private readonly STable<string, HashSet<string>>   _zoneIDsToCharactersIDs;
                  private readonly STable<string, HashSet<string>>   _zoneIDsToObjectsIDs;

                  
                  private Registry(
                        STable<string, Character.Data>    characters,
                        STable<string, string>            characterIDsToZoneIDs,
                        STable<string, HashSet<string>>   zoneIDsToCharactersIDs,
                        STable<string, Props.IAmAnObject> objects,
                        STable<string, HashSet<string>>   zoneIDsToObjectsIDs
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
                        var characters             = new STable<string, Character.Data>();
                        var characterIDsToZoneIDs  = new STable<string, string>();
                        var zoneIDsToCharactersIDs = new STable<string, HashSet<string>>();
                        var objects                = new STable<string, Props.IAmAnObject>();
                        var zoneIDsToObjectIDs     = new STable<string, HashSet<string>>();
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


                  public void RegisterObject(string objectID, string zoneID, Props.IAmAnObject @object) {
                        if (_objects.ContainsKey(objectID)) return;

                        _objects.Add(objectID, @object);

                        ReadyZoneForObjects(zoneID);
                        _zoneIDsToObjectsIDs[zoneID].Add(objectID);
                  }


                  // Ready Zone specified by `ID` to store characters.
                  private void ReadyZoneForCharacters(string zoneID) {
                        if (_zoneIDsToCharactersIDs[zoneID] == null) _zoneIDsToCharactersIDs.Add(zoneID, new HashSet<string>());
                  }


                  // Ready the Zone specified by `ID` to store Objects.
                  private void ReadyZoneForObjects(string zoneID) {
                        if (_zoneIDsToObjectsIDs[zoneID] == null) _zoneIDsToObjectsIDs.Add(zoneID, new HashSet<string>());
                  }


                  public void Persist(string registryID) {
                        if (!CachedRegistries.ContainsKey(registryID)) return;
                        // Save the Registry to Disk using ES3 Save.
                  }

            }

      }

}