﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    #region Активные параметры

    public float GameTimer;
    public int MainLevel;
    public int PointLevel;
    public int GlobalLevel;
    
    public Vector2 MinMaxSize;
    public float TrueSize;
    
    public float[] LevelsSizeOffset;
    
    private Coroutine _corTimer;

    public bool BlockControll;

    private GameElement _trueVariantOfElement;
    
    public List<GameElement> _pressedVariants = new List<GameElement>();

    public List<Vector3> _positionsOfCircles;

    #endregion
    
    #region Инспектор
    
    [SerializeField] private Transform _timerLine;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _levelText;
    [SerializeField] private FadeOutText _fancyText;
    [SerializeField] private FadeOutText _resultText;
    [SerializeField] private Image _fadeOnScene;
    [SerializeField] private SizeOutInFade[] _points;
    
    [SerializeField] private GameObject _gameElementPrefab;
    [SerializeField] private GameObject _gameAreaPrefab;
    
    [SerializeField] private Transform _gameElementContainerBody;
    [SerializeField] private Transform _gameElementContainerLine;
    [SerializeField] private Transform _gameElementContainerBodyChild;
     public Transform _gameElementContainerLineChild;
    [SerializeField] private Transform _gameAreaContainer;
     public Transform _gameAreaContainerChild;
    
    
    #endregion

    private void Start()
    {
        _corTimer = StartCoroutine(TimerCor());
        GenerateNewLevel();
        foreach (var point in _points)
        {
            point.InvokeFadeOut();
        }
    }

    public void ToClearScene()
    {
        _positionsOfCircles.Clear();
        
        GameObject GamesElementsContainer = _gameElementContainerBody.childCount > 0 ? _gameElementContainerBody.GetChild(0).gameObject : null;
        if (GamesElementsContainer != null)
        {
            Destroy(GamesElementsContainer);
        }
        
        GamesElementsContainer = _gameElementContainerLine.childCount > 0 ? _gameElementContainerLine.GetChild(0).gameObject : null;
        if (GamesElementsContainer != null)
        {
            Destroy(GamesElementsContainer);
        }

        GamesElementsContainer = _gameAreaContainer.childCount > 0 ? _gameAreaContainer.GetChild(0).gameObject : null;
        if (GamesElementsContainer != null)
        {
            Destroy(GamesElementsContainer);
        }
    }

    public void CreateClearScene()
    {
        ToClearScene();

        _gameElementContainerBodyChild = new GameObject("BlockLevelBody_"+MainLevel+"_"+PointLevel).transform;
        _gameElementContainerBodyChild.parent = _gameElementContainerBody;
        _gameElementContainerBodyChild.localPosition = Vector3.zero;
        _gameElementContainerBodyChild.localScale = Vector3.one;
        
        _gameElementContainerLineChild = new GameObject("BlockLevelLine_"+MainLevel+"_"+PointLevel).transform;
        _gameElementContainerLineChild.parent = _gameElementContainerLine;
        _gameElementContainerLineChild.localPosition = Vector3.zero;
        _gameElementContainerLineChild.localScale = Vector3.one;
        
        _gameAreaContainerChild = new GameObject("BlockLevelArea_"+MainLevel+"_"+PointLevel).transform;
        _gameAreaContainerChild.parent = _gameAreaContainer;
        _gameAreaContainerChild.localPosition = Vector3.zero;
        _gameAreaContainerChild.localScale = Vector3.one;
    }

    [ContextMenu("Generate")]
    public void GenerateNewLevel()
    {
        CreateClearScene();
        
        TrueSize = Random.Range(MinMaxSize.x, MinMaxSize.y);

        int colOfFigures = 5 + (int)(PointLevel * 1.5f);
        int idOfTrueFigure = Random.Range(0, colOfFigures);
        for (int i = 0; i < colOfFigures; i++)
        {
            
            GameObject newFigure = Instantiate(_gameElementPrefab,_gameElementContainerBodyChild);
           
            newFigure.name = "figure_" + i;
            GameElement FigureGameElement = newFigure.GetComponent<GameElement>();
            if (idOfTrueFigure == i)
            {
                FigureGameElement.Generate(this,TrueSize,true);
                _trueVariantOfElement = FigureGameElement;
            }
            else
            {
                if (Random.Range(0.0f,1.0f) >= 0.5f)
                {
                    float sizeFigure = Mathf.Clamp(TrueSize+Random.Range(1,colOfFigures+1)*LevelsSizeOffset[MainLevel],MinMaxSize.x,1.5f*MinMaxSize.y);
                    FigureGameElement.Generate(this,sizeFigure);
                }
                else
                {
                    float sizeFigure = Mathf.Clamp(TrueSize-Random.Range(1,colOfFigures+1)*LevelsSizeOffset[MainLevel],0.5f*MinMaxSize.x,MinMaxSize.y);
                    FigureGameElement.Generate(this,sizeFigure);
                }
            }
        }
        
        colOfFigures = Random.Range(0,3);
        for (int i = 0; i < colOfFigures; i++)
        {
            GameObject newFigure = Instantiate(_gameElementPrefab,_gameElementContainerBodyChild);
            newFigure.name = "figure_FAKE_" + i;
            GameElement FigureGameElement = newFigure.GetComponent<GameElement>();
            
            float sizeFigure = Mathf.Clamp(TrueSize-Random.Range(1,colOfFigures+1)*LevelsSizeOffset[MainLevel],0.25f*MinMaxSize.x,MinMaxSize.y);
            FigureGameElement.Generate(this,sizeFigure);
         
        }
        
        GameObject newArea = Instantiate(_gameAreaPrefab,_gameAreaContainerChild);
        newArea.transform.localPosition = Vector3.zero;
        newArea.transform.localScale = Vector3.one * TrueSize;

        _trueVariantOfElement.gameObject.transform.SetAsLastSibling();
    }
    
    IEnumerator TimerCor()
    {
        while (GameTimer > 0)
        {
            _timerText.text = ""+((int) GameTimer);
            _timerLine.localScale = new Vector3(GameTimer/(35.0f-(GlobalLevel*3)),1,1);
            GameTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        ///Время кончилось

        GameTimer = 0;
        
        BlockControll = true;
     
        _trueVariantOfElement.MarkeredLine();
        
        _resultText.InvokeFade("Время вышло!");

        GlobalLevel = 0;
        MainLevel = 0;
        PointLevel = 0;
            
        foreach (var point in _points)
        {
            point.InvokeFadeOut();
        }
        
        StartCoroutine(StartNewLevel());
        
    }
    
    IEnumerator StartNewLevel()
    {
        float timerLocal = 1.85f;
        
        while (timerLocal > 0)
        {
            timerLocal -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        
        
        timerLocal = 0;
        while (timerLocal < 0.6f)
        {
            _fadeOnScene.color = new Color(
                _fadeOnScene.color.r,
                _fadeOnScene.color.g,
                _fadeOnScene.color.b,
                timerLocal/0.6f);
            timerLocal += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        _fadeOnScene.color = new Color(
            _fadeOnScene.color.r,
            _fadeOnScene.color.g,
            _fadeOnScene.color.b,
            1);
        
        GenerateNewLevel();
        
        _levelText.text = "LEVEL " +(MainLevel+1);
        
        if (PointLevel == 0)
        {
            foreach (var point in _points)
            {
                point.InvokeFadeOut();
            }
        }
        
        timerLocal = 0.6f;
        while (timerLocal > 0)
        {
            _fadeOnScene.color = new Color(
                _fadeOnScene.color.r,
                _fadeOnScene.color.g,
                _fadeOnScene.color.b,
                timerLocal/0.6f);
            
            timerLocal -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        _fadeOnScene.color = new Color(
            _fadeOnScene.color.r,
            _fadeOnScene.color.g,
            _fadeOnScene.color.b,
            0);

        BlockControll = false;
        GameTimer = (35.0f-(GlobalLevel*3));
        _corTimer = StartCoroutine(TimerCor());
    }

    private void LateUpdate()
    {
        if (_pressedVariants.Count > 0)
        {
            if (BlockControll)
            {
                bool isTrue = false;
                foreach (var figure in _pressedVariants)
                {
                    if (figure.TrueVariant)
                    {
                        isTrue = true;

                        TriggerFigure(true, figure.Size);
                        figure.MakeLikeSelect();

                    }
                }

                if (!isTrue)
                {
                    TriggerFigure(false,_pressedVariants[0].Size);
                    _pressedVariants[0].MakeLikeSelect();
                }
                
                _pressedVariants.Clear();
            }

            BlockControll = true;
        }
    }

    public void AddToListOfSlected(GameElement variable)
    {
        _pressedVariants.Add(variable);
    }

    public void TriggerFigure(bool trueVar,float sizeFigure)
    {
        //BlockControll = true;
     
        _trueVariantOfElement.MarkeredLine();
        
        StopCoroutine(_corTimer);

        if (trueVar)
        {
            ///Выбран правильный вариант
            _resultText.InvokeFade("То что нужно!");
            if (GameTimer >= 13)
            {
                if (GameTimer >= 18)
                {
                    if (GameTimer >= 23)
                    {
                        if (GameTimer >= 27)
                        {
                            _fancyText.InvokeFade("Браво!");
                        }
                        else
                        {
                            _fancyText.InvokeFade("Прекрасно!");
                        }
                    }
                    else
                    {
                        _fancyText.InvokeFade("Хорошо!");
                    }
                }
                else
                {
                    _fancyText.InvokeFade("Неплохо");
                }
            }

            PointLevel++;
            _points[PointLevel-1].InvokeFadeIn();
            if (PointLevel > 2)
            {
                PointLevel = 0;
                MainLevel++;
                if (MainLevel > 2)
                {
                    MainLevel = 0;
                    GlobalLevel++;
                    if (GlobalLevel > 10)
                    {
                        GlobalLevel = 10;
                    }
                }
            }
            
        }
        else
        {
            ///Выбран неправильный вариант
            if (sizeFigure > TrueSize)
            {
                _resultText.InvokeFade("Слишком велик");
            }
            else
            {
                _resultText.InvokeFade("Слишком мал");
            }

            PointLevel++;
            _points[PointLevel-1].InvokeFadeIn();
            
            GlobalLevel = 0;
            MainLevel = 0;
            PointLevel = 0;
            
            foreach (var point in _points)
            {
                point.InvokeFadeOut();
            }
        }

        StartCoroutine(StartNewLevel());
    }

}
