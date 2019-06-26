using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAxis : MonoBehaviour {

    Vector3 startPoint, currentPoint;
    bool dragging = false;
    public float amplifier = 1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //
        if (dragging)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool result = Physics.Raycast(ray, out hitInfo, 1000);
            if (!result)
            {
                dragging = false;
            }
            else
            {

                currentPoint = hitInfo.point;

                //两个向量
                Vector3 u = (currentPoint - transform.position).normalized;
                Vector3 v = (startPoint - transform.position).normalized;

                //目标旋转轴
                Vector3 axis = Vector3.Cross(u, v);

                //旋转角度
                float angle = -Vector3.Dot(u, v) * amplifier;

                //一切俱备，jut rotate
                transform.RotateAround(transform.position, axis, angle);

            }
        }

        //
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 1000))
            {
                dragging = true;
                startPoint = hitInfo.point;
            }
        }

        //
        if (Input.GetMouseButtonUp(0))
        {

            dragging = false;
        }

    }
}
