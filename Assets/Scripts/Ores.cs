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
    //Radius of each gem
    Dictionary<int, float> size = new Dictionary<int, float>()
    {
    { 0, 0.03f},
    { 1, 0.06f},
    { 2, 0.09f},
    { 3, 0.12f},
    { 4, 0.15f},
    { 5, 0.18f},
    { 6, 0.21f},
    { 7, 0.24f},
    { 8, 0.27f},
    { 9, 0.30f},
    };

    public int spawnedId = 0;
    public int GemType = 0;

    public bool isNewGem = true;

    public Sprite[] newSprite;

    public SpriteRenderer spriteRenderer;
    private void Start()
    {
        //
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ores>() != null)
        {
            Ores other = collision.gameObject.GetComponent<Ores>();
            if (other.GetGemType() == GemType)
            {
                if (spawnedId > other.GetId())
                {
                    Destroy(collision.gameObject);
                    ChangeGemType();
                }
            }
        }
        StartCoroutine(JustSpawned());
        EventManager.HasTouched();
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
        if(GemType<8)
        { 
            GemType++;
            spriteRenderer.sprite = newSprite[GemType];
            gameObject.transform.localScale = new Vector3(size[GemType], size[GemType], size[GemType]);
            GetComponent<Rigidbody2D>().mass = (float)size[GemType]*100;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            StartCoroutine(Slowdown());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// To Delay the finnish line trigger
    /// </summary>
    /// <returns>changes a bool state</returns>
    IEnumerator JustSpawned()
    {

        int counter = 1;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        isNewGem = false;
    }
    IEnumerator Slowdown()
    {
        //GetComponent<Rigidbody2D>().drag = 4f;
        int counter = 2;
        while (counter > 0)
        {
            GetComponent<Rigidbody2D>().angularVelocity -= 1;
            //GetComponent<Rigidbody2D>().drag -= 0.5f;
            yield return new WaitForSeconds(1);
            counter--;
        }
        GetComponent<Rigidbody2D>().drag = 1f;
    }
    public bool IsGemNew()
    {
        return isNewGem;
    }
    public int GetGemType()
    {
        return GemType;
    } 
    public int GetId()
    {
        return spawnedId;
    }
    public float GetRad()
    {
        return size[GemType];
    }
    public int SetID(int num) => spawnedId = num;
    public void SetGemType(int num)
    {
        GemType = num;
        spriteRenderer.sprite = newSprite[GemType];
        gameObject.transform.localScale = new Vector3(size[GemType], size[GemType], size[GemType]);
        GetComponent<Rigidbody2D>().mass = (float)size[GemType] * 100;
    }


}
