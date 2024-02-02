using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected bool walk;
    public void toggleWalk()
    {
        walk = !walk;
    }

    public void toggleWalk(bool x)
    {
        walk = x;
    }
    public virtual void teleport(Vector3 position)
    {
        transform.position = position + new Vector3(0, 1, 0);
    }
}
