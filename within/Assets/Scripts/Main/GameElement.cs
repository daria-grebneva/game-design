using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameElement : MonoBehaviour
{
    public float Size;
    public Transform Line;
    public SizeOutInFade LineFade;
    public SizeOutInFade MainFade;
    public Image MainImage;
    public bool TrueVariant;

    private GameSystem _mainSystem;

    public void ActivateLikeButton()
    {
        float distationToMouse = Vector2.Distance(Input.mousePosition,transform.position);
        if (_mainSystem.BlockControll || distationToMouse > Size*50)
        {
            return;
        }
        
        _mainSystem.AddToListOfSlected(this);


    }

    public void MakeLikeSelect()
    {
        GameObject newFigure = Instantiate(gameObject,_mainSystem._gameAreaContainerChild);
        newFigure.transform.localPosition = Vector3.zero;
        newFigure.transform.localScale = Vector3.zero;
        newFigure.GetComponent<SizeOutInFade>().InvokeFadeIn();
        
        newFigure = Instantiate(Line.gameObject,_mainSystem._gameAreaContainerChild);
        newFigure.transform.localPosition = Vector3.zero;
        
        Destroy(newFigure.GetComponent<GameElement>());
        newFigure.transform.localScale = transform.localScale;
        
        
        MainFade.InvokeFadeOut();
    }

    public void MarkeredLine()
    {
        Line.GetComponent<Image>().color = new Color(1, 0.629f, 0, 1);
    }

    public void Generate(GameSystem mainSys, float pSize, bool trueFigure = false)
    {
        Size = pSize;
        _mainSystem=mainSys;
        transform.localScale = Vector3.one * Size;

        TrueVariant = trueFigure;
        
        Vector2 minMaxPos =  new Vector2(Screen.width*0.35f-50.0f*Size,Screen.height*0.33f-50.0f*Size);
        transform.localPosition = new Vector3(Random.Range(-minMaxPos.x,minMaxPos.x),Random.Range(-minMaxPos.y,minMaxPos.y),0);

        if (mainSys._positionsOfCircles.Count == 0)
        {
            mainSys._positionsOfCircles.Add(new Vector3(transform.position.x,transform.position.y,Size*50));
        }
        else
        {
            foreach (var circleOld in mainSys._positionsOfCircles)
            {
                float distanceBw = Vector2.Distance((Vector2)circleOld,transform.position);
                if (distanceBw < Mathf.Max(circleOld.z,Size*50))
                {
                    transform.position += (Vector3)(((Vector2)transform.position-(Vector2)circleOld).normalized*Mathf.Max(circleOld.z,Size*50));
                }
            }
        }
        
        transform.localPosition = new Vector3(Mathf.Min(transform.localPosition.x,minMaxPos.x),Mathf.Min(transform.localPosition.y,minMaxPos.y),0);
        
        LineFade.SetData();
        Line.parent = mainSys._gameElementContainerLineChild;
        MainFade.SetData();
        
        MainImage.color = Random.Range(0.0f,1.0f) > 0.4f ? Color.Lerp(Random.ColorHSV(),Color.white, 0.5f) : Color.white;
        
        MainFade.InvokeFadeIn();
    }
    
    public void TryInstance(Transform newParent)
    {
        
    }
}
