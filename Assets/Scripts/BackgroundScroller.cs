﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.2f;
    Material bgMaterial;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        bgMaterial = gameObject.GetComponent<MeshRenderer>().material;
        offset = new Vector2(0f, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        bgMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
