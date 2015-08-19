using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Motor : MonoBehaviour {
    public float enginePower = 20f;
    public float turnPower = 20f;

    public Wheel[] wheel;

    void FixedUpdate()
    {
        float torque = Input.GetAxis("Vertical") * 20f;
        float turnSpeed = Input.GetAxis("Horizontal") * 20f;


        // front wheel drive
        wheel[0].Move(torque);
        wheel[1].Move(torque);

        // rear wheel drive
        //      wheel[3].Move(torque);
        //      wheel[4].Move(torque);

        // front wheel steering
        wheel[0].Turn(turnSpeed);
        wheel[1].Turn(turnSpeed);

        // rear wheel steering
    //    wheel[3].Turn(turnSpeed);
      //  wheel[4].Turn(turnSpeed);
    }
}
