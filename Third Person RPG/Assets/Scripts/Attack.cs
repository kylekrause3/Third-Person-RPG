using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Attack : MonoBehaviour
{
    int damage;
    float fireRate;
    float range;

    private Inventory inventory;
    private PlayerXP xp;
    private Transform target;
    private bool startUpdate = false;

    private float waitTime;
    private float nextFireTime = 0f;

    private Camera mainCamera;
    private Transform hitSplatParent;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
    private void Start()
    {
        int randomRadius = 2;
        damage = Random.Range(inventory.getWeaponDamage() - randomRadius, inventory.getWeaponDamage() + (randomRadius + 1));
        fireRate = inventory.getWeaponFireRate();
        range = inventory.getWeaponRange();
        waitTime = 1 / fireRate;
        xp = GetComponent<PlayerXP>();

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        hitSplatParent = GameObject.Find("hitSplats").GetComponent<Transform>();
    }
    private void Update()
    {
        if (startUpdate && target.GetComponent<Health>().getHealth() > 0)
        {

            damage = inventory.getWeaponDamage();
            fireRate = inventory.getWeaponFireRate();
            range = inventory.getWeaponRange();
            if (nextFireTime <= Time.time)
            {
                doAttack();
                nextFireTime = Time.time + waitTime;
            }
        }
    }

    public void startAttack(Transform Enemy)
    {
        nextFireTime = Time.time + waitTime;
        startUpdate = true;
        target = Enemy;
    }

    public void endAttack()
    {
        target = null;
        startUpdate = false;
    }

    private void doAttack()
    {
        float accuracy;

        if(xp == null)
        {
            accuracy = 0.3f;
        }
        else
        {
            int level = (int)xp.getLevel(inventory.weaponTypeToXP());
            accuracy = xp.accuracyPerLevel[level];
        }
        Defense d = target.GetComponent<Defense>();
        if (d != null)
        {
            d.SetAttacker(this.transform);
        }

        
            

        float random = Random.Range(0f, 1f);
        if(random <= accuracy)
        {
            target.GetComponent<Health>().attack(damage);
            //Debug.Log("Hit! "+ target.GetComponent<Health>().getHealth() + " " + ("" + target).Split(' ')[0]);
            
            hitSplat.Create(mainCamera, hitSplatParent, target, damage);
        }
        else
        {
            target.GetComponent<Health>().attack(0);
            //Debug.Log("Miss! " + target.GetComponent<Health>().getHealth() + " " + ("" + target).Split(' ')[0]);

            //hitSplat.Create(mainCamera, hitSplatParent, target, 0);

        }
        
        
        
    }
}
