using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    public BodyPart bp;
    public enum BodyPart
    {
        RightArm,
        LeftArm,
        Torso,
        RightLeg,
        LeftLeg
    }
    // Start is called before the first frame update
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.z -= this.speed * Time.deltaTime;
        this.transform.position = pos;
    }
}
