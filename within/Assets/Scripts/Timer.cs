using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private Dictionary<string, string> results = GameResult.GetParameters();
    
    public float timeLeft;

    public bool isGameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SampleScene2")
        {
            CanvasGroup resultScreen = GameObject.Find("CanvasGroup").GetComponent<CanvasGroup>();
            resultScreen.alpha = 0f;
            resultScreen.blocksRaycasts = false;
            SpriteRenderer blur = GameObject.Find("Blur").GetComponent<SpriteRenderer>();
            blur.color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Slider").GetComponent<Slider>().value = timeLeft;
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else if(timeLeft > -100 && timeLeft < 0)
        {
            timeLeft = 0;
        }

        if(Math.Abs(timeLeft) == 0.0f)
        {
            GameOver();
            timeLeft -= 200;
        }

    }

    void GameOver()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SampleScene2")
        {
            Console.WriteLine("GameOver");
            GameObject emptyObject = GameObject.Find("EmptyObject");
            List<String> visitedDots = emptyObject.GetComponent<EmptyObject>().GetVisitedDots();
            
            List<String> requiredDots = LevelGenerator.GetGeneratedListStr();
            List<bool> isContains = new List<bool>(requiredDots.Count);
            foreach (var requiredDot in requiredDots)
            {
                isContains.Add(visitedDots.Contains(requiredDot) ? true : false);
            }
            
            bool win = false;
            if (requiredDots.Count == visitedDots.Count)
            {
                foreach (var isContain in isContains)
                {
                    if (!isContain)
                    {
                        win = false;
                        break;
                    }

                    win = true;
                }
            }

            if (win)
            {
                int resultWins = Int32.Parse(results["WINS"]);
                ++resultWins;
                results["WINS"] = resultWins.ToString();
            }
            else
            {
                int resultLoses = Int32.Parse(results["LOSES"]);
                ++resultLoses;
                results["LOSES"] = resultLoses.ToString();
            }
            File.WriteAllLines("game.result",
                results.Select(element => element.Key + "=" + element.Value).ToArray());
            
            CanvasGroup resultScreen = GameObject.Find("CanvasGroup").GetComponent<CanvasGroup>();
            SpriteRenderer blur = GameObject.Find("Blur").GetComponent<SpriteRenderer>();
            Text resultText = GameObject.Find("Text").GetComponent<Text>();
            resultScreen.alpha = 1f;
            resultScreen.blocksRaycasts = true;
            if (win)
            {
                blur.color = new Color(0.0f, 255.0f, 0.0f, 0.2f);
                resultText.text = "Win!";
            }
            else
            {
                blur.color = new Color(255.0f, 0.0f, 0.0f, 0.2f);
                resultText.text = "Lose!";
            }

            isGameOver = true;
            return;
        }
        Application.LoadLevel("SampleScene2");

    }
}
