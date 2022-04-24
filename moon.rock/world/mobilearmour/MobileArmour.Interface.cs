using moon.rock.world.item;
using UnityEngine;

/*
 * Mobile Armour interface with the Unity Engine.
 */
namespace moon.rock.world.mobilearmour {

      public partial class MobileArmour {

            public class Controller : MonoBehaviour {

                  private string _id;


                  internal static class Anim {

                        internal static class Param {

                              internal static readonly int DirectionX = Animator.StringToHash("DirectionX");
                              internal static readonly int DirectionZ = Animator.StringToHash("DirectionZ");
                              internal static readonly int Boost      = Animator.StringToHash("Boost");

                        }

                  }


                  internal string GetID() {
                        return _id ?? "id_not_present";
                  }


                  internal Animator GetAnimator() {
                        return null;
                  }


                  public void Init(string id) {
                        _id = id;
                  }


                  public void Move(Vector3 direction) {
                        var a = GetAnimator();
                        if (a == null) return;
                        a.SetFloat(Anim.Param.DirectionX, direction.x);
                        a.SetFloat(Anim.Param.DirectionZ, direction.z);
                  }


                  // Pick up weapons.
                  // Enter and Exit Cover.
                  public void Interact(Vector3 direction) { }
                  public void Use()                       { }


                  public void UsePrimary() {
                        Item.GetPrimary(GetID())?.Use(this);
                  }


                  public void UseSecondary() {
                        Item.GetSecondary(GetID())?.Use(this);
                  }


                  public void Guard() {
                        Item.GetMelee(GetID())?.Use(this);
                  }


                  public void Melee() {
                        Item.GetMelee(GetID())?.Use(this);
                  }


                  public void ToggleBoost(bool value) {
                        var a = GetAnimator();
                        if (a == null) return;
                        a.SetBool(Anim.Param.Boost, value);
                  }

            }

            private static class Item {

                  public static item.Item GetMelee(string id) {
                        return null;
                  }


                  public static item.Item GetPrimary(string id) {
                        return null;
                  }


                  public static item.Item GetSecondary(string id) {
                        return null;
                  }

            }

      }

}