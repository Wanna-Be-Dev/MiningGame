using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cart : MonoBehaviour
{
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
    { 10, 0.32f},
    };

    [Header("Next Object")]
    [SerializeField]
    public GameObject nextObject;
    [SerializeField]
    public GameObject nextafterObject;

    public Sprite[] Sprites;

    //for phone
    bool hasTouched = false;

    public GameObject prefab;

    //Gem count
    int count = 0;
    //tapping cooldown
    bool Cooldown = false;

    int [] NextGemType = new int[3];

    [SerializeField]
    public int CooldownTimer = 1;

    [Header("Left Right Stopper")]
    [SerializeField]
    private float RightStop = 2; //2.85
    [SerializeField]
    private float LeftStop = -2;//-2.65

    public void Start()
    {
        NextGemType[0] = 0;
        NextGemType[1] = 0;
        NextGemType[2] = 0;
        DisplayNextPiece();
        EventManager.ObjectHasTouched += DisplayNextPiece;
    }
    void Update()
    {
        // if left-mouse-button is being held OR there is at least one touch
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            //finger touch
            //touch = Input.GetTouch(0);
            // get mouse position in screen space
            // (if touch, gets average of all touches)
            Vector3 screenPos = Input.mousePosition;
            // set a distance from the camera
            screenPos.z = 10.0f;
            // convert mouse position to world space
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            // get current position of this GameObject
            Vector3 newPos = transform.position;
            // set x position to mouse world-space x position
            newPos.x = Mathf.Clamp(worldPos.x, LeftStop, RightStop);
            // apply new position
            transform.position = newPos;
            hasTouched = true;
        } 
        else if((Input.GetMouseButtonUp(0) || (Input.touchCount == 0 && hasTouched)) && !Cooldown)
        {
            GameObject go = Instantiate(prefab, transform.position, transform.rotation);
            go.GetComponent<Ores>().SetID(count);
            go.GetComponent<Ores>().SetGemType(RandGemType());
            count++;

            nextObject.GetComponent<SpriteRenderer>().sprite = null;
            //for debug unlimited spawn
            StartCoroutine(Countdown());
            Cooldown = true;
            hasTouched = false;
            SetOffsets();
        }
    }
    void DisplayNextPiece()
    {
        nextObject.GetComponent<SpriteRenderer>().sprite = Sprites[NextGemType[1]];
        nextObject.transform.localScale = new Vector3(size[NextGemType[1]], size[NextGemType[1]], size[NextGemType[1]]);
        nextafterObject.GetComponent<Image>().sprite = Sprites[NextGemType[2]];
    }
    private int RandGemType()
    {
        switch (count)
        {
            case 0:
                NextType(count);
                break;
            case 1:
                NextType(Random.Range(0, 2));
                break;
            case 2:
                NextType(Random.Range(0, 3));
                break;
            case 3:
                NextType(Random.Range(0, 4));
                break;
            default:
                NextType(Random.Range(0, 6));
                break;
        }
        return NextGemType[0];
    }
    void SetOffsets()
    {
        float radius = 5 * (size[NextGemType[1]]);
        RightStop = 2.6f - radius;
        LeftStop = -2.4f + radius;
    }

    void NextType(int nextValue)
    {

        NextGemType[0] = NextGemType[1];
        NextGemType[1] = NextGemType[2];
        NextGemType[2] = nextValue;
        //StartCoroutine(DisplayNextPiece());
        //DisplayNextPiece();
    }
    IEnumerator Countdown()
    {
        int counter = CooldownTimer;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        Cooldown = false;
    }
}