using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class ScoreDataController : MonoBehaviour {

    private string gameDataProjectFilePath = "/StreamingAssets/scores.json";

    
    private List<GameScore> scores;

    public Text list;

    public Text scoreHeader;

    // Use this for initialization
    void Start () {
        LoadGameData();
        if (list != null)
        {
            showScores();
        }
        if(scoreHeader != null)
        {
            setScoreHeader();
        }
	}

    // Update is called once per frame
    void Update () {
		
	}

    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            ListContainer container = JsonUtility.FromJson<ListContainer>(dataAsJson);
            if (container.dataList != null)
            {
                scores = container.dataList;
                scores.Sort(delegate (GameScore x, GameScore y)
                {
                    return y.score.CompareTo(x.score);
                });
            } else
                scores = new List<GameScore>();
        }
        else
        {
            scores = new List<GameScore>();
        }
    }

    private void SaveGameData()
    {
        ListContainer container = new ListContainer(scores);

        string dataAsJson = JsonUtility.ToJson(container);
        
        //string dataAsJson = JsonUtility.ToJson(scores);

        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);

    }

    public void AddScore(GameScore score)
    {
        scores.Add(score);
        SaveGameData();
    }

    private void showScores()
    {
        string texto = "";
        int i = 1;
        foreach (GameScore s in scores)
        {
            texto += i + ". " + s.name + "\n" + s.score + "\n";
            i++;
        }

        list.text = texto;
    }

    public struct ListContainer
    {
        public List<GameScore> dataList;

        public ListContainer(List<GameScore> _dataList)
        {
            dataList = _dataList;
        }
    }

    private void setScoreHeader()
    {
        scoreHeader.text = ""+PlayerPrefs.GetInt("score");
    }
}
