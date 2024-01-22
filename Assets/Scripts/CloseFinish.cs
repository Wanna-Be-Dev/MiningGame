using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseFinish : MonoBehaviour
{
    float distance = -1f;
    [SerializeField]
    float EndLine = 2.5f;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ores>()!=null)
        {
            if (!col.gameObject.GetComponent<Ores>().IsGemNew())
            {
                distance = 1 - (EndLine - col.gameObject.transform.position.y) + (col.gameObject.GetComponent<Ores>().GetRad() * 5);
                EventManager.CloseFinish(distance);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ores>() != null)
        {
            if (!col.gameObject.GetComponent<Ores>().IsGemNew())
            {
                distance = 1 - (EndLine - col.gameObject.transform.position.y) + (col.gameObject.GetComponent<Ores>().GetRad() * 5);
                EventManager.CloseFinish(distance);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ores>() != null)
        {
            if (!col.gameObject.GetComponent<Ores>().IsGemNew())
            {
                distance = -1f;
                EventManager.CloseFinish(distance);
            }
        }
    }


}
