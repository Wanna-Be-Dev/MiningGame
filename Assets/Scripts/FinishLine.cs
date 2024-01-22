using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private SpriteRenderer spriteR;
    [SerializeField]
    private GameObject FinishMenu;

    bool canFinish = false;
    void Awake()
    {
        EventManager.onTriggerEnter.AddListener(OnClose);
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.color = new Color(1f, 1f, 1f, 0f);
    }
    void OnClose(float distance)
    {
        spriteR.color = new Color(1f, 1f, 1f, distance);
        canFinish = (distance > 0) ? canFinish = true : canFinish=false;

    }
    void OnTriggerStay2D(Collider2D col)
    {
        if ((col.gameObject.GetComponent<Ores>() != null) && canFinish && !(col.gameObject.GetComponent<Ores>().IsGemNew()))
        {
            FinishMenu.SetActive(true);
        }
    }
}
