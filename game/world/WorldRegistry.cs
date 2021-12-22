using System.Collections.Generic;

namespace game.world.character {

      public class WorldRegistry {

            private readonly Dictionary<string, string>          _characterIDsToZoneIDs;
            private readonly Dictionary<string, Character.Data>  _characters;
            private readonly Dictionary<string, HashSet<string>> _zoneIDsToCharactersIDs;


            private WorldRegistry(Dictionary<string, Character.Data>  characters,
                                  Dictionary<string, string>          characterIDsToZoneIDs,
                                  Dictionary<string, HashSet<string>> zoneIDsToCharactersIDs) {
                  _characters             = characters;
                  _characterIDsToZoneIDs  = characterIDsToZoneIDs;
                  _zoneIDsToCharactersIDs = zoneIDsToCharactersIDs;
            }


            public static WorldRegistry GetRegistry(string registryID) {
                  var characterIDs           = new Dictionary<string, Character.Data>();
                  var characterIDsToZoneIDs  = new Dictionary<string, string>();
                  var zoneIDsToCharactersIDs = new Dictionary<string, HashSet<string>>();
                  return new WorldRegistry(characterIDs, characterIDsToZoneIDs, zoneIDsToCharactersIDs);
            }


            public void RegisterCharacter(string characterID, string zoneID, Character.Data data) {
                  _characters.Add(characterID, data);
                  EnterZone(zoneID, characterID);
            }


            public void DeRegisterCharacter(string characterID) {
                  _characters.Remove(characterID);
                  LeaveZone(GetZoneID(characterID), characterID);
            }


            public Character.Data GetCharacterData(string characterID) {
                  return _characters[characterID];
            }


            public string GetZoneID(string characterID) {
                  return _characterIDsToZoneIDs[characterID];
            }


            public HashSet<string> GetAllCharactersInZone(string zoneID) {
                  return _zoneIDsToCharactersIDs[zoneID];
            }


            public void EnterZone(string zoneID, string characterID) {
                  _characterIDsToZoneIDs.Add(characterID, zoneID);
                  _zoneIDsToCharactersIDs[zoneID].Add(characterID);
            }


            public void LeaveZone(string zoneId, string characterID) {
                  _characterIDsToZoneIDs.Remove(characterID);
                  _zoneIDsToCharactersIDs[zoneId].Remove(characterID);
            }

      }

}