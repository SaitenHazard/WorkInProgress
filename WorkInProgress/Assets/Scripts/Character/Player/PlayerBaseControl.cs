using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseControl : CharacterBaseControl {
    
    override public void OnActionPressed()
    {
        m_movementModel.DoAction();
    }
}