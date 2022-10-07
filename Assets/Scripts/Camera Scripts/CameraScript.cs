using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static float offSetX;

    private void Update()
    {
        if (BirdScript.instance != null)
        {
            if (BirdScript.instance.isDead == false)
            {
                MoveCamera();
            }
        }
    }

    private void MoveCamera()
    {
        Vector3 temp = transform.position;
        temp.x = BirdScript.instance.GetPositionX() + offSetX;
        transform.position = temp;
    }
}
