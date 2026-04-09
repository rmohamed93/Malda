using UnityEngine;

public class Parallax_BG : MonoBehaviour
{
    private float length, startpos;
    public GameObject camera;
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (camera.transform.position.x * (1 - parallaxEffect));
        float distance = (camera.transform.position.x * parallaxEffect);
        
        transform.position = 
        new Vector3(startpos + distance, transform.position.y, transform.position.z);
        
        if (temp > startpos + length) startpos += length * 2;
        else if (temp < startpos - length) startpos -= length * 2;
    }
}
