using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test");

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {

        if (other?.body?.gameObject?.tag == "Obstacle")
        {
            Debug.Log("Hit");
        }
    }
}
