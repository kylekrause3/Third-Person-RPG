using UnityEngine;
using UnityEngine.AI;
//float distance = Vector3.Distance (object1.transform.position, object2.transform.position);
public class PointAndClick : Movement
{
    public Camera cam;
    private NavMeshAgent player;
    public float playerSpeed;
    
    //public Animator playerAnimator;
    //public GameObject targetDest;

    Interactable focus;

    private void Awake()
    {
        player = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && walk)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitpoint;

            if(Physics.Raycast(ray, out hitpoint))
            {
                //targetDest.transform.position = hitpoint.point;
                

                Interactable interactable = hitpoint.transform.GetComponent<Interactable>();
                //Debug.Log(interactable);
                if (interactable != null)
                {
                    SetFocus(interactable);
                    
                }
                else if (interactable == null)
                {
                    RemoveFocus();
                    player.SetDestination(hitpoint.point);
                }
            }
        }

        /*if (player.velocity != Vector3.zero)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else if (player.velocity == Vector3.zero)
        {
            playerAnimator.SetBool("isWalking", false);
        }*/

        if (focus != null)
        {
            player.SetDestination(focus.interactionTransform.position);
            FaceTarget();
        }
    }

    public void StartNav()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public void StopNav()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }

    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
                focus.OnDefocused();

            focus = newFocus;

            //navmesh stuff
            float radius;
            if(focus.primaryAction == Interactable.PrimaryAction.Attack)
            {
                radius = GetComponent<Inventory>().getWeaponRange();
                
            }
            else
            {
                radius = focus.radius;
            }
            player.stoppingDistance = radius * .9f;
            player.updateRotation = false;
        }

        focus.OnFocused(transform);
    }

    public void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;

        //navmesh stuff
        player.stoppingDistance = 0;
        player.updateRotation = true;
    }

    void FaceTarget()
    {
        Vector3 direction = (focus.transform.position - player.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    public void toggleSprint(float x)
    {
        if (player.speed > playerSpeed)
            player.speed = playerSpeed;
        else
            player.speed = playerSpeed * x;
    }

    public override void teleport(Vector3 position)
    {
        player.Warp(position);
    }

    //player.stoppingDistance = attack range
}
