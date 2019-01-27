using UnityEngine;
using System.Collections;

public class MoveUp : MonoBehaviour 
{
    
    private BoxCollider2D groundCollider;       //This stores a reference to the collider attached to the Ground.
    private float groundHorizontalLength;       //A float to store the x-axis length of the collider2D attached to the Ground GameObject.
    public float timer;
    private float timerTwo;
    
    RectTransform m_RectTransform;

    //Awake is called before Start.
    private void Awake ()
    {
        //Get and store a reference to the collider2D attached to Ground.
        groundCollider = GetComponent<BoxCollider2D> ();
        //Store the size of the collider along the x axis (its length in units).
        groundHorizontalLength = groundCollider.size.y;
        m_RectTransform = GetComponent<RectTransform>();
        timerTwo = timer;
    }

    //Update runs once per frame
    private void Update()
    {
    transform.Translate(-1, 1, 0);
    timer -= Time.deltaTime;
    if (timer < 0)
    {
    m_RectTransform.anchoredPosition = new Vector3(-554, -1796, 0);
    timer = timerTwo;
    }
        //Check if the difference along the x axis between the main Camera and the position of the object this is attached to is greater than groundHorizontalLength.
        if (transform.position.y < -groundHorizontalLength)
        {
            //If true, this means this object is no longer visible and we can safely move it forward to be re-used.
            RepositionBackground ();
        }
    }

    //Moves the object this script is attached to right in order to create our looping background effect.
    private void RepositionBackground()
    {
        //This is how far to the right we will move our background object, in this case, twice its length. This will position it directly to the right of the currently visible background object.
        Vector2 groundOffSet = new Vector2(groundHorizontalLength * 2f, 0);

        //Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
        transform.position = (Vector2) transform.position + groundOffSet;
    }
}