using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuSystem : MonoBehaviour
{
    public GameObject MenuObjectPrefab;
    public Transform MenuObjectPrefabParent;
    public float LocalTimer;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MenuObjectPrefabParent.Rotate(Vector3.forward*4*Time.deltaTime);
        LocalTimer += Time.deltaTime;
        if (LocalTimer > 1.1f)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(MenuObjectPrefab, MenuObjectPrefabParent);
            }

            LocalTimer = 0;
        }
    }
}
