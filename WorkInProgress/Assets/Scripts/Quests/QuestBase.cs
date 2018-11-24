using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase : MonoBehaviour
{
    protected bool active = false;
    protected bool complete = false;

    virtual public void Activate()
    {

    }

    public virtual bool IsActive()
    {
        return active;
    }

    public virtual int GetEnemiesLeft()
    {
        return 0;
    }

    public bool IsComplete()
    {
        return complete;
    }
}
