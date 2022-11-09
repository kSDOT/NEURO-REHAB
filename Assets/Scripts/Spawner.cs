using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{

    #region Fields
    private GameObject Player;
    public Obstacle Model;

    public Vector3 Range = new Vector3(5, 0f, 5f);
    public float SafeDistance = 3f;
    

    public int InstantSpawn = 0;
    public List<Obstacle> Spawned;


    enum BodyPart { 
        RightArm,
        LeftArm,
        Torso,
        RightLeg,
        LeftLeg
    }
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
        return (new Vector3(UnityEngine.Random.Range(-this.Range.x / 2, this.Range.x / 2),
                                UnityEngine.Random.Range(SafeDistance, this.Range.y),
                                UnityEngine.Random.Range(SafeDistance, this.Range.z)

                    ), Obstacle.BodyPart.RightArm);
    }

    
    private void InitObject(Obstacle obs)
    {
        (obs.transform.position, obs.bp) = this.RandomInitialize();
        obs.SetSpeed(MainMenu.speed);
    }
    
    private void InstantiateObstacle()
    {
        Obstacle obs = Instantiate(this.Model) as Obstacle;
        this.InitObject(obs);
        this.Spawned.Add(obs);
    }

    IEnumerator SpawnNext()
    {
        yield return new WaitForSeconds(MainMenu.spawnInterval);
        InstantiateObstacle();
    }

    void Update()
    {
        for (int index= 0; index < this.Spawned.Count; index++)
        {
            Obstacle obstacle = this.Spawned[index];
            if (obstacle.transform.position.z <= this.Player.transform.position.z - 0.3f)
            {
                Destroy(obstacle);
                this.Spawned.Remove(obstacle);
            }
        }
    }

}
