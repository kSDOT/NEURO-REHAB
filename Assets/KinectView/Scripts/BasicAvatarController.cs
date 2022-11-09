using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using System;
using UnityEditor.XR.LegacyInputHelpers;

public class BasicAvatarController : MonoBehaviour
{


    // root transformation, used to determine the initial rotation of the complete model
    public Transform RootTransform;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    // avatar of the motion capturing data
    public BasicAvatarModel MoCapAvatar;

    // dict of all the joint-transforms that are available in the model
    protected Dictionary<JointType, Transform> knownJoints = new Dictionary<JointType, Transform>();
    protected Dictionary<JointType, Transform> lastMoved;


    // initial joint rotations of the model (the rotations are "local rotations" relative to the RootTransform rotation; see Start function for further details)
    protected Dictionary<JointType, Quaternion> initialModelJointRotations = new Dictionary<JointType, Quaternion>();

    public Vector3 offsetRoot;

    public Transform offsetVR;
    public Vector3 offsetAdd;
    public Transform headTransform;


    // called by derived class (e.g. UnityChanController) at the end of its own Start function after setting the available joints
    public virtual void Start()
    {
        // check which joints were set
        foreach (JointType jt in Enum.GetValues(typeof(JointType)))
        {
            GameObject UnityJoint = GameObject.Find(jt.ToString());
            if (UnityJoint != null) knownJoints[jt] = UnityJoint.transform;
            lastMoved = new Dictionary<JointType, Transform>(knownJoints);
        }

        // compute initial rotation of the joints of the model
        // Note: because we want the rotation to be relative to Quaternion.identity (no rotation), we compute a "local rotation" relative to the RootTransform rotation of the model
        foreach (JointType jt in knownJoints.Keys)
        {
            initialModelJointRotations[jt] = Quaternion.Inverse(RootTransform.rotation) * knownJoints[jt].rotation;
        }

    }
    // Update rotation of all known joints
    public virtual void Update()
    {

        foreach (JointType jt in knownJoints.Keys)
        {

            Quaternion localRotTowardsRootTransform = MoCapAvatar.applyRelativeRotationChange(jt, initialModelJointRotations[jt]);

            // ...therefore we have to multiply it with the RootTransform Rotation to get the global rotation of the joint
            var temp = RootTransform.rotation * localRotTowardsRootTransform;
            var difference = lastMoved[jt].rotation.eulerAngles - temp.eulerAngles;

            if ((difference.magnitude <= 30 * Time.deltaTime || difference.magnitude >= 330 * Time.deltaTime) || Time.timeSinceLevelLoad < 1f)
            {
                lastMoved[jt].rotation = temp;
            }
            if ((lastMoved[jt].rotation.eulerAngles - knownJoints[jt].rotation.eulerAngles).magnitude >= 30 * Time.deltaTime)
            {
                knownJoints[jt].rotation = Quaternion.Slerp(knownJoints[jt].rotation, lastMoved[jt].rotation, 1f);      

            }

        }
        
        RootTransform.position = MoCapAvatar.getRawWorldPosition(JointType.SpineBase)/10.0f + offsetRoot;
        offsetVR.position = headTransform.position + offsetAdd; 
    }


}
