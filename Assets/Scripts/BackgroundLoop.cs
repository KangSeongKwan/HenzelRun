using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour {
    private MeshRenderer render;
    private float offset;
    public float speed;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(!GameDirector.instance.isGameover)
        {
            offset += Time.deltaTime * speed;
            render.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}
