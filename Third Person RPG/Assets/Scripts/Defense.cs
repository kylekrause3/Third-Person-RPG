using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Defense : MonoBehaviour //EnemyDefense
{
    private NavMeshAgent agent;
    //TODO: On attacked, do defensive style until unfocused; add method like amIfocused? amIbeingAttacked?
    public enum DefensiveStyle
    {
        Passive,
        Aggressive,
        Scared
    }

    public DefensiveStyle defenseStyle;

    private bool startUpdate = false;

    public delegate void DoDefense();
    public static event DoDefense doDefense;

    Attack attack;
    private Transform attacker = null;

    private Patrol patrol;

    [HideInInspector]
    public Inventory inventory;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        attack = gameObject.GetComponent<Attack>();
        patrol = gameObject.GetComponent<Patrol>();
        inventory = gameObject.GetComponent<Inventory>();

        doDefense = () => Debug.Log("doDefense has not been Assigned");
    }

    private void Update()
    {
        if (startUpdate)
        {
            doDefense?.Invoke();
        }
    }

    public bool getUpdate()
    {
        return startUpdate;
    }

    public void onDefense()
    {

    }
    public void onDeDefense()
    {
        if (agent.isActiveAndEnabled)
        {
            switch (defenseStyle)
            {
                case DefensiveStyle.Passive:
                    agent.speed *= 2f;
                    break;
                case DefensiveStyle.Aggressive:
                    attack?.endAttack();
                    agent.stoppingDistance = 0;
                    patrol?.GotoNextPoint();
                    break;
                case DefensiveStyle.Scared:
                    break;
                default:
                    break;
            }
            doDefense = () => Debug.Log("doDefense has not been Assigned");
            startUpdate = false;
        }

    }

    public void startDefense()
    {
        startUpdate = true;
        switch (defenseStyle)
        {
            case DefensiveStyle.Passive:
                agent.speed *= .5f;
                break;
            case DefensiveStyle.Aggressive:
                doDefense = null;
                aggressiveDefense();
                break;
            case DefensiveStyle.Scared:
                break;
            default:
                break;
        }
    }

    public void SetAttacker(Transform attacker)
    {
        this.attacker = attacker;
    }

    public void aggressiveDefense()
    {
        if (this.GetComponent<Attack>() == null)
            return;
        FaceTarget(attacker);
        attack.startAttack(attacker);
        agent.stoppingDistance = inventory.getWeaponRange() * 0.9f;
        agent.SetDestination(attacker.position);
    }

    public Transform getAttacker()
    {
        return attacker;
    }

    void FaceTarget(Transform focus)
    {
        Vector3 direction = (focus.transform.position - agent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
