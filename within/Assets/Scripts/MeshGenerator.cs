using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshGenerator : MonoBehaviour {


    float width = 1;
    float height = 1;
    public GameObject myPrefab;
    // Use this for initialization
    
    public class CanvasSize
    {
        static Canvas canvas = FindObjectOfType<Canvas>();
        public float scale = canvas.GetComponent<RectTransform>().localScale.x;
        public float width = canvas.GetComponent<RectTransform>().rect.width;
        public float xMin = canvas.GetComponent<RectTransform>().rect.xMin;
        public float height = canvas.GetComponent<RectTransform>().rect.height;
        public float yMin = canvas.GetComponent<RectTransform>().rect.yMin;
    }
    
    void Start()
    {

        GenerateCircles(10);
    }

    Color GetRandomColor()
    {
        return new Color(
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f)
        );
    }
    
    void GenerateCircles(int circlesNumber)
    {
        for (int i = 0; i < circlesNumber; i++)
        {
//            Instantiate(myPrefab, new Vector3(10 + i*10, 10 + i*10, 0), Quaternion.identity);
            GameObject go = Instantiate(myPrefab);
            CanvasSize canvas = new CanvasSize();



            float radius = Random.Range(10, 60); //getRandomScaleRadiusInRange
            float posX = canvas.xMin + Random.Range(radius * 2, canvas.width - radius * 2); //getRandomPositionXInRange
            float posY = canvas.yMin + Random.Range(radius * 2, canvas.height - radius * 2); //getRandomPositionYInRange
            Color color = GetRandomColor(); //getRandomColor
            
            go.transform.position = new Vector3(posX * canvas.scale, posY * canvas.scale, 0);
            go.transform.localScale = new Vector3(radius * 2, radius * 2, 0);
            Renderer rend = go.GetComponent<Renderer>();
            rend.material.color = color;
            
            
            SphereCollider myCollider =  go.GetComponent<SphereCollider>();
        }
    }
}