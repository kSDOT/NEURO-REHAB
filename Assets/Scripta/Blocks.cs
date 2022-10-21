using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{

    #region Fields
    public float currentSpeed;
    public float minSpeed;
    public float maxSpeed;
    public GameObject itemPrefab;

    private float x, y, z;
    private float outOfScreenScale = 1.3f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetPositionAndSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMove = currentSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * amtToMove, Space.World);

        if (transform.position.z >= 1.5)
        {
            SetPositionAndSpeed();
        }
    }

    public void SetPositionAndSpeed()
    {

        currentSpeed = Random.Range(minSpeed, maxSpeed);

        int lr = Random.Range(1, 6);

        if (lr % 2 == 0)
        {
            x = Random.Range(-0.6f, -0.2f);
        }
        else
        {
            x = Random.Range(0.2f, 0.6f);
        }
        y = Random.Range(1f, 1.5f);
        z = -3f;
        transform.position = new Vector3(x, y, z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Playerhit");
            SetPositionAndSpeed();
        }
    }
}
