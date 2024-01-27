using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Weapon weapon;
    private void Start()
    {
        
    }

    public bool hasWeapon()
    {
        return weapon != null;
    }
    public void setWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
    public int getWeaponDamage()
    {
        return weapon.damage;
    }
    public float getWeaponFireRate()
    {
        return weapon.attacksPerSecond;
    }
    public float getWeaponRange()
    {
        return weapon.range;
    }
    public Weapon.WeaponType getWeaponType()
    {
        return weapon.weaponType;
    }
    public XPParent.XPType weaponTypeToXP()
    {
        switch (weapon.weaponType)
        {
            case Weapon.WeaponType.Melee:
                return XPParent.XPType.Attack;
            case Weapon.WeaponType.Ranged:
                return XPParent.XPType.Ranged;
            case Weapon.WeaponType.Magic:
                return XPParent.XPType.Magic;
            default:
                return XPParent.XPType.Attack;
        } //don't need break in this switch statement because return
    }

    
}
