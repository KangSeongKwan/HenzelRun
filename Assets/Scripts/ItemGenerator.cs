using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 아이템을 생성하는 제너레이터 스크립트 */
public class ItemGenerator : MonoBehaviour
{
    public GameObject[] candyPrefabs;
    float span = 3.0f;
    float delta = 0;

    void Start()
    {
        candyPrefabs = GameObject.FindGameObjectsWithTag("Item");
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 오버 상태에서는 동작하지 않는다.
        if (GameDirector.instance.isGameover)
        {
            return;
        }

        // 3초마다 아이템 생성
        this.delta += Time.deltaTime;
        if (this.delta > this.span) {
            this.delta = 0;
            
            // 0부터 25 사이의 수를 랜덤하게 생성
            int i = Random.Range(0, 26);

            // i번째 아이템 생성
            GameObject item = Instantiate(candyPrefabs[i]) as GameObject;

             // 아이템의 X 좌표는 7부터 14 사이의 수를 랜덤하게 생성
            int x = Random.Range(7, 15);

            // 아이템의 Y 좌표는 0부터 4 사이의 수를 랜덤하게 생성
            int y = Random.Range(0, 5);
            item.transform.position = new Vector3(x, y, 0);
        }
    }
}