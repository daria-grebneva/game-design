using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeOutInFade : MonoBehaviour
{
    [Tooltip("Изначальный размер")] public Vector3 startSize;
    [Tooltip("Время появления (сек)")] public float timeFadeIn;
    [Tooltip("Время исчезновения (сек)")] public float timeFadeOut;

    private Coroutine _nowFade;
    
    // Start is called before the first frame update
//    void Start()
//    {
//        SetData();
//    }

    public void SetData()
    {
        startSize = transform.localScale;
    }

    [ContextMenu("Исчезни!")]
    public void InvokeFadeOut()
    {
        if (transform.localScale == Vector3.zero)
        {
            return;
        }
        
        if (_nowFade != null)
        {
            StopCoroutine(_nowFade);
        }
        
        _nowFade = StartCoroutine(FadeOut());
    }
    
    [ContextMenu("Появись!")]
    public void InvokeFadeIn()
    {
        if (transform.localScale == startSize)
        {
            return;
        }
        
        if (_nowFade != null)
        {
            StopCoroutine(_nowFade);
        }
        
        _nowFade = StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float mainTimer = 0;
        
        while (mainTimer < timeFadeIn)
        {
            transform.localScale = startSize * (mainTimer/timeFadeIn);
            mainTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        transform.localScale = startSize;
    }
    
    IEnumerator FadeOut()
    {
        float mainTimer = timeFadeOut;
        
        while (mainTimer > 0)
        {   
            transform.localScale = startSize * (mainTimer/timeFadeOut);
            mainTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        transform.localScale = Vector3.zero;
    }
}
