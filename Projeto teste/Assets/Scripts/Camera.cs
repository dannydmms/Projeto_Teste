using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform target;
    [SerializeField]float distanceX=1;
    [SerializeField]float distanceY=-1;
    [SerializeField]float distanceZ=6;
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(target.position.x- distanceX, target.position.y- distanceY, target.position.z - distanceZ);
    }

}
