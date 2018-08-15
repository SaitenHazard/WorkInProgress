using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseControl : CharacterBaseControl {
    
    protected void OnActionPressed()
    {
        m_movementModel.DoAction();
    }
}
