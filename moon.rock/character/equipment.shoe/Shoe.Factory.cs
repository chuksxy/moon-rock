using eden.equipment;

namespace moon.rock.character.equipment.shoe {

      public partial class Shoe {

            public class Factory : Equipment.IEquipmentFactory {

                  public TEquipmentType GetEquipment<TEquipmentType>(string entityID) {
                        throw new System.NotImplementedException();
                  }

            }

      }

}