using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource hitAudio;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Hit3");

        if (other?.gameObject?.tag == "Obstacle")
        {
            //Debug.Log("Hit4");
            hitAudio.Play();
            Destroy(other.gameObject);
        }
    }
}