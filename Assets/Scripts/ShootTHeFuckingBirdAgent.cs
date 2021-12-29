using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class ShootTHeFuckingBirdAgent : Agent{

    public Level L;
    public Slingshot S;
    public override void OnActionReceived(ActionBuffers actions)
    {
        float x = actions.ContinuousActions[0]*2 + 2;
        float y = actions.ContinuousActions[1]*4;
        //Debug.Log(x + "   " + y);
        S.AIShootsBird(x, y);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(3-S.launched);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
    }

}
