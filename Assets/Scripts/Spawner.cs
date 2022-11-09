using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Linq;

public class Spawner : MonoBehaviour
{

    #region Fields
    private GameObject Player;
    public GameObject Model;

    public Vector3 Range = new Vector3(5, 0f, 5f);
    public float Depth=5f;
    

    public int InstantSpawn = 0;
    public List<Obstacle> Spawned;



    #endregion

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.FindGameObjectWithTag("Player");
       

        for (int i = 0; i < this.InstantSpawn; i++)
        {
            InstantiateObstacle();
        }
        StartCoroutine(this.SpawnNext());
    }

    private (Vector3, Obstacle.BodyPart) RandomInitialize()
    {
        List<Obstacle.BodyPart> keyList =  MainMenu.parts.Where(kvp => kvp.Value == true).Select(kvp=> kvp.Key).ToList();
        if (keyList.Count == 0)
        {
            throw new Exception("Need to select at least 1 part");
        }
        Obstacle.BodyPart part = keyList[UnityEngine.Random.Range(0, keyList.Count-1)];
        Vector3 MinValue = Vector3.zero;
        Vector3 MaxValue = Vector3.zero;
        if (part == Obstacle.BodyPart.RightArm)
        {
            MinValue = GameObject.Find("WristRight").transform.position;
            MaxValue = 
              (GameObject.Find("ElbowRight").transform.position + GameObject.Find("ShoulderRight").transform.position)/2;

        }
        else if (part == Obstacle.BodyPart.LeftArm)
        {

            MinValue = GameObject.Find("WristLeft").transform.position;
            MaxValue = 
              (GameObject.Find("ElbowLeft").transform.position + GameObject.Find("ShoulderLeft").transform.position) / 2;
        }
        else if (part == Obstacle.BodyPart.Torso)
        {

            MinValue = GameObject.Find("SpineShoulder").transform.position;
            MaxValue = 
              (GameObject.Find("SpineBase").transform.position + GameObject.Find("SpineShoulder").transform.position) *2/ 3;
        }
        else if (part == Obstacle.BodyPart.RightLeg)
        {

            MinValue = GameObject.Find("AnkleRight").transform.position;
            MaxValue =
              (GameObject.Find("KneeRight").transform.position + GameObject.Find("HipRight").transform.position) / 2;
        }
        else if (part == Obstacle.BodyPart.LeftLeg)
        {

            MinValue = GameObject.Find("AnkleLeft").transform.position;
            MaxValue =
              (GameObject.Find("KneeLeft").transform.position + GameObject.Find("HipLeft").transform.position) / 2;
        }

        return (new Vector3(    UnityEngine.Random.Range(MinValue.x, MaxValue.x),
                                UnityEngine.Random.Range(MinValue.y, MaxValue.y),
                                UnityEngine.Random.Range(Depth, Depth)

                    ), part);
    }

    
    private void InitObject(Obstacle obs)
    {
        (obs.transform.position, obs.bp) = this.RandomInitialize();
        obs.SetSpeed(MainMenu.speed);
    }
    
    private void InstantiateObstacle()
    {
        Obstacle obs = Instantiate(this.Model).GetComponent<Obstacle>();
        this.InitObject(obs);
        this.Spawned.Add(obs);
    }

    IEnumerator SpawnNext()
    {
        yield return new WaitForSeconds(MainMenu.spawnInterval);
        InstantiateObstacle();
        StartCoroutine(SpawnNext());
    }

    void Update()
    {
        for (int index= 0; index < this.Spawned.Count; index++)
        {
            Obstacle obstacle = this.Spawned[index];
            if (obstacle.transform.position.z <= this.Player.transform.position.z - 0.3f)
            {
                Destroy(obstacle.gameObject);
                this.Spawned.Remove(obstacle);
            }
        }

    }

}
