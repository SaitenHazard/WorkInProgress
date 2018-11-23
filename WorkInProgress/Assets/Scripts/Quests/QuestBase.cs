using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase : MonoBehaviour
{
    protected bool active = false;

    virtual public void Activate()
    {

    }

    public bool IsActive()
    {
        return active;
    }
}
