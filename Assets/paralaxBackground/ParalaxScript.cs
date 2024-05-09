using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    private float startPos, length;
    public List<Transform> backgrounds;
    public float parallaxEffect;
    private Vector3 lastCameraPosition;
    public float smoothness = 5f; // Adjust this value for desired smoothness
    float timer = 0;
    void Start()
    {
        startPos = Camera.main.transform.position.x;
        length = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        lastCameraPosition = Camera.main.transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime * 0.05f;
        //Move cameras x direction based on sin wave
        Camera.main.transform.position = new Vector3((Mathf.Sin(timer) * 45), Camera.main.transform.position.y, Camera.main.transform.position.z);
        float cameraDelta = Camera.main.transform.position.x - lastCameraPosition.x;
        float dist = cameraDelta * parallaxEffect;

        for (int i = 0; i < backgrounds.Count; i++)
        {
            float parallax = dist * (i + 1); // Adjust the parallax calculation
            float backgroundTargetPosX = backgrounds[i].position.x - parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Smoothly transition to the target position
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, Time.deltaTime * smoothness);

            // Check if the background is off screen
            if (backgrounds[i].position.x + length / 2 < startPos - length * (backgrounds.Count - 1))
            {
                // Move the background to the right
                backgrounds[i].position += new Vector3(length * backgrounds.Count, 0, 0);
            }
            else if (backgrounds[i].position.x - length / 2 > startPos + length * (backgrounds.Count - 1))
            {
                // Move the background to the left
                backgrounds[i].position -= new Vector3(length * backgrounds.Count, 0, 0);
            }
        }

        startPos = Camera.main.transform.position.x;
        lastCameraPosition = Camera.main.transform.position;
    }
}
