using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    // every frameS
    Touch touch;
    public GameObject prefab;

    int count = 0;

    bool Cooldown = false;

    [SerializeField]
    public int CooldownTimer = 1;
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
            newPos.x = Mathf.Clamp(worldPos.x,-2,2);
            // apply new position
            transform.position = newPos;

        } 
        else if((Input.GetMouseButtonUp(0) || (touch.phase == TouchPhase.Ended))&& !Cooldown)
        {
            GameObject go = Instantiate(prefab, transform.position, transform.rotation);
            go.GetComponent<Ores>().SetID(count);
            count++;
            Cooldown = true;
            StartCoroutine(Countdown());
        }
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
