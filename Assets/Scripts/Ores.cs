using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ores : MonoBehaviour
{
    /*
     * Gem types
        Jade        1    0.03
        Peridot     2    0.06
        Amethyst    3    0.10
        Citrine     4    0.13
        Tourmaline  5    0.17
        Aquamarine  6    0.21
        Emerald     7    0.24
        Alexandrite 8    0.28
        Ruby        9    0.31
        Sapphire    10   0.33
        Diamond     11   0.34
    */
    Dictionary<int, double> size = new Dictionary<int, double>()
    {
    { 0, 0.03},
    { 1, 0.06},
    { 2, 0.10},
    { 3, 0.13},
    { 4, 0.17},
    { 5, 0.21},
    { 6, 0.24},
    { 7, 0.28},
    { 8, 0.31},
    { 9, 0.33},
    { 10, 0.34},
    };

    public int spawnedId = 0;
    public int GemType = 0;

    public Sprite[] newSprite;

    public SpriteRenderer spriteRenderer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ores>() != null)
        {
            Ores other = collision.gameObject.GetComponent<Ores>();
            if (other.GetGemType() == GemType)
                if(spawnedId > other.GetId())
                {
                    Destroy(collision.gameObject);
                    ChangeGemType();
                }      
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ores>() != null)
        {
            Ores other = collision.gameObject.GetComponent<Ores>();
            if (other.GetGemType() == GemType)
                if (spawnedId > other.GetId())
                {
                    Destroy(collision.gameObject);
                    ChangeGemType();
                }
        }
    }
    void ChangeGemType()
    {
        EventManager.SendScore((GemType + 1) * 2);
        GemType++;
        spriteRenderer.sprite = newSprite[GemType];
        gameObject.transform.localScale = new Vector3((float)size[GemType], (float)size[GemType], (float)size[GemType]);
        GetComponent<Rigidbody2D>().mass = (float)size[GemType]*100;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        StartCoroutine(Slowdown());
    }
    IEnumerator Slowdown()
    {
        GetComponent<Rigidbody2D>().drag = 5f;
        int counter = 3;
        while (counter > 0)
        {
            GetComponent<Rigidbody2D>().drag -= counter;
            yield return new WaitForSeconds(1);
            counter--;
        }
        GetComponent<Rigidbody2D>().drag = 1f;
    }
    public int GetGemType()
    {
        return GemType;
    } 
    public int GetId()
    {
        return spawnedId;
    }
    public int SetGemType(int num) => GemType = num;
    public int SetID(int num) => spawnedId = num;
}
