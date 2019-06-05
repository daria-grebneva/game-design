using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuObjects : MonoBehaviour
{
    public float Size;
    public float TransSize;
    public float LocalTimer;
    public Image MainImage;
    

    private void Start()
    {
        
        TransSize = Random.Range(0.5f, 6f);
        Size = Random.Range(0.5f, 2.5f);
        MainImage.color = Random.ColorHSV();
        transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f)*Mathf.Max(Screen.width,Screen.height),Random.Range(-0.5f, 0.5f)*Mathf.Max(Screen.width,Screen.height),0);
        MainImage.color = new Color(MainImage.color.r,MainImage.color.g,MainImage.color.b,1);
    }

    // Update is called once per frame
    void Update()
    {
        LocalTimer += Time.deltaTime*(1.0f/Size);
        transform.localScale = Vector3.one*TransSize*LocalTimer;
        MainImage.color = new Color(MainImage.color.r,MainImage.color.g,MainImage.color.b,1-LocalTimer);

        if (LocalTimer > 1)
        {
            Destroy(gameObject);
        }
    }
}
