using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoving : MonoBehaviour
{
    //[Order("Cam1")]
    [ShowOnly] public float frame = 0;
    [Space(10)]
    [Header("Camera 1 Settings")]
    [SerializeField] GameObject Cam1;
    [Range(0, 5000)]
    [SerializeField] int MaxMovementFrame1;
    [SerializeField] float Cam1_VectorX;
    [SerializeField] float Cam1_VectorZ;
    [Space(10)]
    [Header("Camera 2 Settings")]
    [SerializeField] GameObject Cam2;
    [Range(0,10000)]
    [SerializeField] int MaxMovementFrame2;
    [SerializeField] float Cam2_VectorX;
    [SerializeField] float Cam2_VectorZ;
    [Space(10)]
    [Header("Camera 3 Settings")]
    [SerializeField] GameObject Cam3;
    [Range(0,15000)]
    [SerializeField] int MaxMovementFrame3;
    [SerializeField] float Cam3_VectorX;
    [SerializeField] float Cam3_VectorZ;
    [Space(10)]
    [Header("Camera 4 Settings")]
    [SerializeField] GameObject Cam4;
    [Range(0, 15000)]
    [SerializeField] int MaxMovementFrame4;
    [SerializeField] float Cam4_VectorX;
    [SerializeField] float Cam4_VectorZ;
    //[Space(10)]


    void Start()
    {
        
    }


    void FixedUpdate()
    {
        frame++;
        CamTimer();
    }
    void CamTimer()
    {
        Cam1.GetComponent<Transform>().position = Cam1.GetComponent<Transform>().position + new Vector3(Cam1_VectorX * -0.001f, 0 , -0.001f * Cam1_VectorZ);
        if (frame == MaxMovementFrame1)
        {
            Cam1.SetActive(false);
            Cam2.SetActive(true);
        }

        Cam2.GetComponent<Transform>().position = Cam2.GetComponent<Transform>().position + new Vector3(Cam2_VectorX * -0.001f, 0, -0.001f * Cam2_VectorZ);    
        if (frame == MaxMovementFrame2)
        {
            Cam2.SetActive(false);
            Cam3.SetActive(true);
        }
        Cam3.GetComponent<Transform>().position = Cam3.GetComponent<Transform>().position + new Vector3(Cam3_VectorX * -0.001f, 0, -0.001f * Cam3_VectorZ);
        if (frame == MaxMovementFrame3)
        {
            Cam3.SetActive(false);
            Cam4.SetActive(true);
        }
        Cam4.GetComponent<Transform>().position = Cam4.GetComponent<Transform>().position + new Vector3(Cam4_VectorX * -0.001f, 0, -0.001f * Cam4_VectorZ);
        if (frame == MaxMovementFrame4)
        {
            Cam4.SetActive(false);
            Cam1.SetActive(true);
            frame = 0;
        }



    }
}


