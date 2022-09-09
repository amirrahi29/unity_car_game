using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_spawner : MonoBehaviour
{

    public GameObject platform;
    public Transform lastPlatform;
    Vector3 lastPos;
    Vector3 newPos;
    public bool stop;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = lastPlatform.position;
        StartCoroutine(spawnPlatform());
    }

    void generatePos(){
        newPos = lastPos;
        int rand = Random.Range(0, 2);
        if(rand>0){
            newPos.x += 2f;
        }
        else
        {
            newPos.z += 2f;
        }
    }

    IEnumerator spawnPlatform(){
        while (!stop)
        {
            generatePos();
            Instantiate(platform,newPos,Quaternion.identity);
            lastPos = newPos;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
