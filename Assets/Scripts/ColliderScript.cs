using UnityEngine;

public class Collider : MonoBehaviour
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");

        if (other?.gameObject?.tag == "Obstacle")
        {
            Debug.Log("Hit");
        }
    }
}