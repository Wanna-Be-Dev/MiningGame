using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private SpriteRenderer spriteR;
    [SerializeField]
    private GameObject FinishMenu;

    [SerializeField]
    private GameObject RayCastOrigin;

    bool canFinish = false;

    Ray2D ray;
    void Awake()
    {
        EventManager.onTriggerEnter.AddListener(OnClose);
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.color = new Color(1f, 1f, 1f, 0f);
        CheckRayCast();
    }
    void OnClose(float distance)
    {
        spriteR.color = new Color(1f, 1f, 1f, distance);
        canFinish = (distance > 0) ? canFinish = true : canFinish = false;

    }
    void FinishGame() 
    {
        if(CheckRayCast())
        {
            StartCoroutine(DoubleCheck());
            if (canFinish)
                FinishMenu.SetActive(true);
                FinishMenu.gameObject.GetComponent<EndScreen>().OpenMenu();
        }
    }
    IEnumerator DoubleCheck()
    {
        int counter = 5;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        canFinish = CheckRayCast();
    }
    bool CheckRayCast()
    {
        bool fin = false;
        Debug.DrawRay(RayCastOrigin.transform.position, Vector2.left * 5, Color.red);
        RaycastHit2D hitGem = Physics2D.Raycast(RayCastOrigin.transform.position, Vector2.left);
        if ((hitGem.collider.gameObject.GetComponent<Ores>() != null) && !hitGem.collider.gameObject.GetComponent<Ores>().IsGemNew())
        {
            fin = true;
        }
        return fin;
    }

    private void Update()
    {
        FinishGame();
    }

}
