using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawner : MonoBehaviour
{
    public GameObject scorePrefab; // ������ ���ع��� ���� ������
    float span = 4.0f;
    float delta = 0;

    // Update is called once per frame
    void Update()
    {
        // ���� ���� ���¿����� �������� �ʴ´�.
        if (GameDirector.instance.isGameover)
        {
            return;
        }

        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject po = Instantiate(scorePrefab);
            int px = Random.Range(7, 15);
            int py = Random.Range(-2, 3);
            po.transform.position = new Vector3(px, py, 0);
        }
    }
}
