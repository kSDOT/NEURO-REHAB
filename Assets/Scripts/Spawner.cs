using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hello World!

public class Spawner : MonoBehaviour
{

    #region Fields
    public float currentSpeed;
    public float minSpeed;
    public float maxSpeed;
    public GameObject itemPrefab;

    public GameObject Model;
    public GameObject Player;

    public Vector3 Range = new Vector3(5, 0f, 5f);
    public float SafeDistance = 3f;

    public int InstantSpawn = 6;
    public int MaxSpawn = 30;
    public float SpawnInterval = 2f;

    public GameObject[] Spawned;

    private float x, y, z;
    private float outOfScreenScale = 1.3f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        this.Spawned = new GameObject[MaxSpawn];
        this.Player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < this.InstantSpawn; i++)
        {
            this.Spawned[i] = this.InstantiateObject();

        }
        StartCoroutine(this.SpawnNext());
        //SetPositionAndSpeed();
    }

    private Vector3 RandomVector()
    {
        return new Vector3(UnityEngine.Random.Range(-this.Range.x / 2, this.Range.x / 2),
                                UnityEngine.Random.Range(SafeDistance, this.Range.y),
                                UnityEngine.Random.Range(SafeDistance, this.Range.z)

                    );
    }

    private GameObject InstantiateObject()
    {
        GameObject obj = Instantiate(this.Model);
        obj.transform.position = this.RandomVector();
        return obj;
    }

    public IEnumerator Respawn(int Index)
    {
        GameObject obj = this.Spawned[Index];
        obj.SetActive(false);
        yield return new WaitForSeconds(this.SpawnInterval);
        obj.transform.position = this.RandomVector();
        obj.SetActive(true);
    }

    IEnumerator SpawnNext()
    {
        for (int i = this.InstantSpawn; i < this.MaxSpawn; i++)
        {
            yield return new WaitForSeconds(this.SpawnInterval);
            this.Spawned[i] = this.InstantiateObject();
        }
    }

    void Update()
    {
        foreach (GameObject obstacle in this.Spawned)
        {
            if (obstacle != null)
            {

                Vector3 pos = obstacle.transform.position;
                pos.z -= this.currentSpeed * Time.deltaTime;
                obstacle.transform.position = pos;
            }
        }
    }

    /*
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
    */

}
