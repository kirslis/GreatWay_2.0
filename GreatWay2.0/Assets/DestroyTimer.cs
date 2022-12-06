using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    private float DestroyTime = -1;
    private float SpentTime = 0;

    public float destroyTime { set { DestroyTime = value; } }

void Update()
    {
        if(DestroyTime > 0)
        {
            if(SpentTime > DestroyTime)
                Destroy(gameObject);

            SpentTime += Time.deltaTime;
        }        
    }
}
