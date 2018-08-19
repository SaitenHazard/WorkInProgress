using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyAIWalkControl : MonoBehaviour
{
    private CharacterBaseControl m_BaseControl;
    private CharacterAttributes m_Attributes;
    public float steps;

    private void Awake()
    {
        m_BaseControl = GetComponent<CharacterBaseControl>();
        m_Attributes = GetComponent<CharacterAttributes>();
    }

    private void Start()
    {
        //StartCoroutine(WalKCycle());
    }

    private void Update()
    {
        UpdateHit();
        //Debug.Log("in");
    }

    private void UpdateHit()
    {
        if(m_Attributes.IsHitState() == true)
        {
            StopCoroutine(WalKCycle());
        }

        return;
    }

    IEnumerator WalKCycle()
    {
        for (float time = Time.time; (Time.time - time) <= steps;)
        {
            m_BaseControl.UpdateDirection(Directions.Right);
            yield return null;
        }

        for (float time = Time.time; (Time.time - time) <= steps;)
        {
            m_BaseControl.UpdateDirection(Directions.Down);
            yield return null;
        }

        for (float time = Time.time; (Time.time - time) <= steps;)
        {
            m_BaseControl.UpdateDirection(Directions.Left);
            yield return null;
        }

        for (float time = Time.time; (Time.time - time) <= steps;)
        {
            m_BaseControl.UpdateDirection(Directions.Up);
            yield return null;
        }

        StartCoroutine(WalKCycle());
    }

    //void UpdateAction()
    //{
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        OnActionPressed();
    //    }
    //}
}
