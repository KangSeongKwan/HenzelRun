using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpawner : MonoBehaviour
{
    public GameObject poisonPrefab; // 생성할 독극물의 원본 프리팹
    float span = 4.0f;
    float delta = 0;

    // Update is called once per frame
    void Update()
    {
        // 게임 오버 상태에서는 동작하지 않는다.
        if (GameDirector.instance.isGameover)
        {
            return;
        }

        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject po = Instantiate(poisonPrefab);
            int px = Random.Range(7, 15);
            int py = Random.Range(-2, 3);
            po.transform.position = new Vector3(px, py, 0);
        }
    }
}
