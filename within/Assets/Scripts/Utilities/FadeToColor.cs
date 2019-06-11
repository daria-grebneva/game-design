using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToColor : MonoBehaviour
{
    public Image MainImage;
    public Color MainColor;
    public Color FadeColor;
    public float TimerToFade = 0.75f;
    
    private bool _nowFade;

    // Start is called before the first frame update
    void Start()
    {
        MainImage = GetComponent<Image>();
        MainColor = MainImage.color;
    }

    public void InvokeRed()
    {
        MainColor = MainImage.color;
        FadeColor = new Color(0.98f,0.2f,0.1f);
        InvokeFadeOut();
    }
    
    public void InvokeGreen()
    {
        MainColor = MainImage.color;
        FadeColor = new Color(0.35f,0.98f,0.1f);
        InvokeFadeOut();
    }
    
    public void InvokeGreenPulse()
    {
        MainColor = MainImage.color;
        FadeColor = new Color(0.35f,0.98f,0.1f);
        
        StartCoroutine(FadeCycle());
        
    }

    [ContextMenu("Исчезни!")]
    public void InvokeFadeOut()
    {
        StartCoroutine(FadeOut());
        _nowFade = true;
    }
    
    IEnumerator FadeOut()
    {
        float mainTimer = 0;
        
        while (mainTimer <= TimerToFade)
        {
            MainImage.color = Color.Lerp(MainColor,FadeColor,mainTimer/TimerToFade);
            mainTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    
    IEnumerator FadeCycle()
    {
        float mainTimer = 0;
        
        while (!_nowFade)
        {
            MainImage.color = Color.Lerp(MainColor,FadeColor, mainTimer);
            if (mainTimer > 1)
            {
                TimerToFade = -TimerToFade;
            }else if(mainTimer < 0)
            {
                TimerToFade = -TimerToFade;
            }
            mainTimer += Time.deltaTime/(TimerToFade/4f);
            
            yield return new WaitForEndOfFrame();
        }
    }
}
