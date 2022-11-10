using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Linq;
using TMPro;
public class Spawner : MonoBehaviour
{

    #region Fields
    private GameObject Player;
    public GameObject Model;

    public Vector3 Range = new Vector3(5, 0f, 5f);
    public float Depth=5f;

    public AudioSource success;
    public int InstantSpawn = 0;
    public List<Obstacle> Spawned;
    private List<Obstacle.BodyPart> keyList;
    public bool debugPositions = false;
    static public int Score = 0;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.FindGameObjectWithTag("Player");



        keyList = MainMenu.parts.Where(kvp => kvp.Value == true).Select(kvp => kvp.Key).ToList();
        if (keyList.Count == 0)
        {

            MainMenu.parts[Obstacle.BodyPart.RightArm] = true;
            MainMenu.parts[Obstacle.BodyPart.LeftArm] = true;
            MainMenu.parts[Obstacle.BodyPart.Torso] = true;
            MainMenu.parts[Obstacle.BodyPart.RightLeg] = true;
            MainMenu.parts[Obstacle.BodyPart.LeftLeg] = true;
            keyList = MainMenu.parts.Where(kvp => kvp.Value == true).Select(kvp => kvp.Key).ToList();
        }

        for (int i = 0; i < this.InstantSpawn; i++)
        {
            InstantiateObstacle();
        }
        StartCoroutine(this.SpawnNext());
    }

    private (Vector3, Obstacle.BodyPart) RandomInitialize()
    {
        Obstacle.BodyPart part = keyList[UnityEngine.Random.Range(0, keyList.Count)];
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

            MinValue = GameObject.Find("SpineBase").transform.position;
            MaxValue = 
              (GameObject.Find("SpineBase").transform.position + GameObject.Find("SpineShoulder").transform.position) * 3/5;
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
        if (debugPositions)
        {

            var temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //temp.transform.parent = this.Player.transform;
            temp.transform.position = MinValue;
            temp.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            var temp2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //temp.transform.parent = this.Player.transform;
            temp2.transform.position = MaxValue;
            temp2.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            temp2.gameObject.GetComponent<Renderer>().material.color = Color.red;
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
        GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: " + Score.ToString();
        for (int index= 0; index < this.Spawned.Count; index++)
        {
            Obstacle obstacle = this.Spawned[index];
            if (obstacle != null) {      
                if (obstacle.transform.position.z <= this.Player.transform.position.z - 0.5f)
                {
                     success.Play();
                     Destroy(obstacle.gameObject);
                     this.Spawned.Remove(obstacle);
                     Score++;
                }
            }
       
        }

    }

}
