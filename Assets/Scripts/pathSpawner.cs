using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathSpawner : MonoBehaviour
{
    ObjectPooler pool;

    [SerializeField] Transform player;

    [SerializeField, Range(0, 2f)] float tileSize;
    [SerializeField, Range(0, 20)] int minLength_min, maxLength_min, minLength_max, maxLength_max;
    [SerializeField, Range(0f, 2f)] float timeBTWspawns;

    int count, side = -1, minLength, maxLength;
    float lastTime = 0;
    GameObject spawnedTile;
    Vector3 spawnPos;

    void Start()
    {
        pool = ObjectPooler.instance;
        spawnPos = Vector3.zero;
        spawnPos.z = -tileSize * 5;

        if(player != null) player.position = new Vector3(0, player.position.y, spawnPos.z);

        if(pool != null)
        {
            for (int i = 0; i < 5; i++)
            {
                spawnedTile = pool.GetObject(0);
                spawnedTile.transform.position = spawnPos;
                spawnedTile.SetActive(true);
                spawnedTile.transform.localScale = new Vector3(tileSize, 0.2f, tileSize);

                spawnPos.z += tileSize;
            }
        }
    }

    void Update()
    {
        if (count <= 0)
        {
            minLength = Random.Range(minLength_min, minLength_max);
            maxLength = Random.Range(maxLength_min, maxLength_max);
            count = Random.Range(minLength, maxLength);
            if(side == -1) side = Random.Range(0, 2);
            else side = (side == 0) ? 1 : 0;
        }

        if (Time.time - lastTime > timeBTWspawns)
        {
            lastTime = Time.time;

            if (pool != null)
            {
                spawnedTile = pool.GetObject(0);
                spawnedTile.transform.position = spawnPos;

                spawnPos.z += tileSize / 1.41f;

                if (side == 0) spawnPos.x += tileSize / 1.41f;
                else if (side == 1) spawnPos.x -= tileSize / 1.41f;

                spawnedTile.SetActive(true);

                spawnedTile.transform.localScale = new Vector3(tileSize, 0.2f, tileSize);
                spawnedTile.transform.eulerAngles = new Vector3(0, -45, 0);

                count--;
            }
        }
    }
}
