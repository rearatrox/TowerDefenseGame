using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GegnerMoveHomescreen : MonoBehaviour
{

   // public GameObject[] Mage;
    // Start is called before the first frame update
    public float Speed;
    public float ChangeAngle;
    public Vector3 RotVec;
    Vector3 StartVec;
    void Awake()
    {
        ChangeAngle = 181f;
        StartVec = transform.forward;
    }

    void Update()
    {

       
            if (Vector3.Angle(StartVec, transform.forward) >= ChangeAngle)
            {
                StartVec = transform.forward;
                RotVec = -RotVec;
            }

            transform.Rotate(RotVec * Speed * Time.deltaTime);
        
        
    }
}
