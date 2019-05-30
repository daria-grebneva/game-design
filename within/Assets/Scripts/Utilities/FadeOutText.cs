using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutText : MonoBehaviour
{
    [Tooltip("Исходный цвет")] public Color TextMainColor;
    [Tooltip("Время ожидания (сек)")] public float timeWaitToFade;
    [Tooltip("Время исчезновения (сек)")] public float timeFullFade;

    private Text _mainText;
    private Coroutine _nowFade;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainText = GetComponent<Text>();
        if (_mainText == null)
        {
            Destroy(this);
        }
        else
        {
            TextMainColor = _mainText.color;
        }
        
    }

    [ContextMenu("Исчезни!")]
    public void InvokeFade()
    {
        if (_nowFade != null)
        {
            StopCoroutine(_nowFade);
        }
        
        _nowFade = StartCoroutine(FadeCor());
    }
    
    public void InvokeFade(string SetText)
    {
        _mainText.text = SetText;
        InvokeFade();
    }

    IEnumerator FadeCor()
    {
        float mainTimer = timeWaitToFade + timeFullFade;
        
        _mainText.color = new Color(
            TextMainColor.r,
            TextMainColor.g,
            TextMainColor.b,
            TextMainColor.a);
        
        while (mainTimer > 0)
        {
            if (mainTimer < timeFullFade)
            {
                _mainText.color = new Color(
                    TextMainColor.r,
                    TextMainColor.g,
                    TextMainColor.b,
                    TextMainColor.a*(mainTimer/timeFullFade));
            }
            
            mainTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        _mainText.color = new Color(
            TextMainColor.r,
            TextMainColor.g,
            TextMainColor.b,
            0);
    }

    [ContextMenu("Вернуть как было!")]
    public void ClearFade()
    {
        if (_nowFade != null)
        {
            StopCoroutine(_nowFade);
        }

        _mainText.color = TextMainColor;
    }
    
}
