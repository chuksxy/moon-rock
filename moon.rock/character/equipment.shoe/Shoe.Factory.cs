using eden.equipment;

namespace moon.rock.character.equipment.shoe {

      public partial class Shoe {

            public class Factory : Equipment.IEquipmentFactory<Shoe> {

                  public Shoe GetEquipment(string entityID) {
                        throw new System.NotImplementedException();
                  }

            }

      }

}