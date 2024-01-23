using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseFinish : MonoBehaviour
{
    //float distance = -1f;
    [SerializeField]
    float EndLine = 2.5f;

    [SerializeField]
    private GameObject RayCastOrigin;
    /*    void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<Ores>() != null)
            {
                if (!col.gameObject.GetComponent<Ores>().IsGemNew())
                {
                    distance = 1 - (EndLine - col.gameObject.transform.position.y) + (col.gameObject.GetComponent<Ores>().GetRad() * 5);
                    EventManager.CloseFinish(distance);
                }
            }
        }*/
    void CheckRayCast()
    {
        Debug.DrawRay(RayCastOrigin.transform.position, Vector2.left * 5, Color.blue);
        RaycastHit2D hitGem = Physics2D.Raycast(RayCastOrigin.transform.position, Vector2.left);
        if ((hitGem.collider.gameObject.GetComponent<Ores>() != null) && !hitGem.collider.gameObject.GetComponent<Ores>().IsGemNew())
        {
            EventManager.CloseFinish(1);
        }else
        {
            EventManager.CloseFinish(-1);
        }
    }

    private void Update()
    {
        CheckRayCast();
    }


}
