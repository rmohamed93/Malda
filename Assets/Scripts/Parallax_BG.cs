using UnityEngine;

public class Parallax_BG : MonoBehaviour
{
    private float length, startpos;
    // R.M. - Adjusted GameObject "camera" to "cam" due to inherrited Unity confilcts.
    public GameObject cam;
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    // This essentially is the copy and past method for the layers.
    // It also moves each layer at desired speeds based on the settings.
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);
        
        transform.position = 
        new Vector3(startpos + distance, transform.position.y, transform.position.z);
        
        if (temp > startpos + length) startpos += length * 2;
        else if (temp < startpos - length) startpos -= length * 2;
    }
}
