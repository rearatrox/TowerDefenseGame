using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lebensleiste : MonoBehaviour
{

    public int maxLeben = 100;
    public float aktuellesLeben = 100;
    private float originalScale;


    // Start is called before the first frame update
    void Start()
    {
        originalScale = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempScale = this.transform.localScale;
        tempScale.x = aktuellesLeben / maxLeben * originalScale;
        gameObject.transform.localScale = tempScale;
    }
}
