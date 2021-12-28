using UnityEngine;

/*
 * Mobile Armour interface with the Unity Engine.
 */
namespace moon.rock.world.actor.mobilearmour {

      public partial class MobileArmour {

            public class Interface : MonoBehaviour {

                  private string _nodeID = "no.node.ID";
                  public  void   Move(Vector3     direction) { }
                  public  void   Interact(Vector3 direction) { }
                  public  void   Use()                       { }
                  public  void   UsePrimary()                { }
                  public  void   UseSecondary()              { }
                  public  void   ToggleBoost()               { }

            }

      }

}