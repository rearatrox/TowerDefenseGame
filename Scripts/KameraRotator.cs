using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KameraRotator : MonoBehaviour
{
    Vector3 FirstPoint;
    Vector3 SecondPoint;
   public float xAngle;
   public float yAngle;
   public float xAngleTemp;
    public float yAngleTemp;

    void Start()
    {
        xAngle = 0;
        yAngle = 0;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    }

    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                FirstPoint = Input.GetTouch(0).position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                SecondPoint = Input.GetTouch(0).position;
                xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                xAngle = Mathf.Clamp(xAngle, -90, 90);
               // yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
            }
        }

    }

}
