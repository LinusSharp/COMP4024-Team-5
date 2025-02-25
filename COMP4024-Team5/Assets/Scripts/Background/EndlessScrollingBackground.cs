using UnityEngine;

public class EndlessScrollingBackground : MonoBehaviour
{
    // References to the two background transforms.
    public Transform background1;
    public Transform background2;
    
    // The width of your background image (in world units).
    public float backgroundWidth;
    
    // The speed at which the background scrolls.
    // This should be tied to the player's speed.
    public float scrollSpeed;

    void Update()
    {
        // Calculate movement for this frame.
        float movement = scrollSpeed * Time.deltaTime;
        
        // Move both backgrounds to the left (assuming player moves right).
        background1.position += Vector3.left * movement;
        background2.position += Vector3.left * movement;
        
        // Check if background1 is completely off-screen to the left.
        // This check assumes the background's pivot is at the left edge.
        if (background1.position.x <= -backgroundWidth)
        {
            // Reposition background1 to the immediate right of background2.
            background1.position = new Vector3(background2.position.x + backgroundWidth, 
                                                 background1.position.y, 
                                                 background1.position.z);
        }
        
        // Similarly, check for background2.
        if (background2.position.x <= -backgroundWidth)
        {
            background2.position = new Vector3(background1.position.x + backgroundWidth, 
                                                 background2.position.y, 
                                                 background2.position.z);
        }
    }
}
