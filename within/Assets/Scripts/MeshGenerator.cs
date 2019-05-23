using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class MeshGenerator : MonoBehaviour {

    public GameObject circle;
    public GameObject square;
    private Vector3  MousePos;

    private List<GameObject> circles;
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
    
    void Update () 
    {
        if (Input.GetKeyDown (KeyCode.Mouse0)) {
            MousePos = Input.mousePosition;
            print(MousePos.x);
            CanvasSize canvas = new CanvasSize();
            print("canvas x: " + canvas.xMin / canvas.scale);
            print("canvas width: " + canvas.width / canvas.scale);
            print("square x: " + square.transform.position.x / canvas.scale);
            print("square width: " + square.transform.localScale.x / canvas.scale);
        }
    }
    
    
    
    void Start()
    {

        GenerateCircles(10);
        GenerateSquare();
        Update();

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
        circles = new List<GameObject>();
        for (int i = 0; i < circlesNumber; i++)
        {
            GameObject go = Instantiate(circle);
            CanvasSize canvas = new CanvasSize();

            float radius = Random.Range(10, 20); //getRandomScaleRadiusInRange
            float posX = canvas.xMin + Random.Range(radius * 4, canvas.width * 0.7f  - radius * 4); //getRandomPositionXInRange
            float posY = canvas.yMin + Random.Range(radius * 4    , canvas.height * 0.9f - radius * 4); //getRandomPositionYInRange
            Color color = GetRandomColor(); //getRandomColor
            
            go.transform.position = new Vector3(posX * canvas.scale, posY * canvas.scale, 0);
            go.transform.localScale = new Vector3(radius * 2* canvas.scale, radius * 2* canvas.scale, 0);
            Renderer rend = go.GetComponent<Renderer>();
            rend.material.color = color;
            
            
            CircleCollider2D myCollider =  go.GetComponent<CircleCollider2D>();
            circles.Add(go);
        }

    }

    GameObject GenerateSquare()
    {
        int index = Random.Range(0, circles.Count);
        GameObject go = Instantiate(square);
        CanvasSize canvas = new CanvasSize();


        GameObject circle = circles[index]; 
        Renderer rendSquare = circle.GetComponent<Renderer>();
        go.transform.localScale = circle.transform.localScale;
        float posX = canvas.xMin + canvas.width * 0.85f;//  - go.transform.localScale.x; //getRandomPositionXInRange
        float posY = canvas.yMin + canvas.height * 0.5f; //getRandomPositionYInRange
        go.transform.position = new Vector3(posX * canvas.scale, posY * canvas.scale, 0);
        Renderer rend = go.GetComponent<Renderer>();
        
        
        return go;
    }
}