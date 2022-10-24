using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject bomber;
    public GameObject bomberInstance;
    public GameObject obstacles;

    public bool drop = false;
    // Start is called before the first frame update
    void Start()
    {
        bomberInstance = Instantiate(bomber, new Vector3(11.15f, 2.9f, 0.0f), transform.rotation);
        Instantiate(obstacles, new Vector3(8f, -3.2f, 0f), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (bomberInstance.transform.position.x < - 11.15f)
        {
            Destroy(this.bomberInstance);
            bomberInstance = Instantiate(bomber, new Vector3(11.15f, 2.9f, 0.0f), transform.rotation);
        }
    }
}
