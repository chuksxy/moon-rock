using System.Collections.Generic;
using game.world.character;
using game.world.item;

namespace game.world {

      public static partial class World {

            public class Registry {

                  // Character Tables for characters located in the world.
                  private readonly Dictionary<string, Character.Data>  _characters;
                  private readonly Dictionary<string, string>          _characterIDsToZoneIDs;
                  private readonly Dictionary<string, HashSet<string>> _zoneIDsToCharactersIDs;

                  // Item Tables for items located in the world.
                  private readonly Dictionary<string, string>          _itemIDsToZoneIDs;
                  private readonly Dictionary<string, HashSet<string>> _zoneIDsToItemIDs;


                  private Registry(
                        Dictionary<string, Character.Data>  characters,
                        Dictionary<string, string>          characterIDsToZoneIDs,
                        Dictionary<string, HashSet<string>> zoneIDsToCharactersIDs,
                        Dictionary<string, string>          itemIDsToZoneIDs,
                        Dictionary<string, HashSet<string>> zoneIDsToItemIDs
                  ) {
                        _characterIDsToZoneIDs  = characterIDsToZoneIDs;
                        _characters             = characters;
                        _zoneIDsToCharactersIDs = zoneIDsToCharactersIDs;
                        _itemIDsToZoneIDs       = itemIDsToZoneIDs;
                        _zoneIDsToItemIDs       = zoneIDsToItemIDs;
                  }


                  // Get Registry by ID from Disk.
                  public static Registry Get(string registryID) {
                        var characterIDs           = new Dictionary<string, Character.Data>();
                        var characterIDsToZoneIDs  = new Dictionary<string, string>();
                        var zoneIDsToCharactersIDs = new Dictionary<string, HashSet<string>>();
                        var itemIDsToZoneIDs       = new Dictionary<string, string>();
                        var zoneIDsToItemIDs       = new Dictionary<string, HashSet<string>>();
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

            }

      }

}