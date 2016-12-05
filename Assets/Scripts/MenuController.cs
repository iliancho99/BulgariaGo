using System.Runtime.CompilerServices;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject LoadingBar;
    public Text ScoreText;
    public Text BuildingText;
    public GameObject[] Bubbles;
    public GameObject NotHelpImage;

    private void Start()
    {
        
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("IsBuildingRecognized", 0);
            PlayerPrefs.SetInt("IncorrectAnswersCount", 0);
            PlayerPrefs.SetInt("QuestionId", 0);
            PlayerPrefs.SetInt("FirstHintUnlocked", 0);
            PlayerPrefs.SetInt("SecondHintUnlocked", 0);
            PlayerPrefs.SetInt("ShowBubbles", 1);
        }

        this.ScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        this.BuildingText.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
        if (PlayerPrefs.GetInt("ShowBubbles") == 1)
        {
            for (int i = 0; i < this.Bubbles.Length; i++)
            {
                this.Bubbles[i].SetActive(true);
            }

            this.NotHelpImage.SetActive(true);
        }
    }

    public void StartButtonClicked()
    {
        this.LoadingBar.SetActive(true);
        if (PlayerPrefs.GetInt("IsBuildingRecognized") == 1)
        {
            SceneManager.LoadSceneAsync("Quiz");
        }
        else
        {
            SceneManager.LoadSceneAsync("Recognition");
        }
    }

    public void ResetBottonClicked()
    {
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("MaxLevel", 0);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("IsBuildingRecognized", 0);
        PlayerPrefs.SetInt("IncorrectAnswersCount", 0);
        PlayerPrefs.SetInt("QuestionId", 0);
        PlayerPrefs.SetInt("FirstHintUnlocked", 0);
        PlayerPrefs.SetInt("SecondHintUnlocked", 0);

        this.ScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        this.BuildingText.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void NextTargetButtonClicked()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        this.BuildingText.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
    }

    public void LeaderboardButtonClicked()
    {
        this.LoadingBar.SetActive(true);
        SceneManager.LoadSceneAsync("Leaderboard");
    }

    public void LevelSelectorButtonClicked()
    {
        this.LoadingBar.SetActive(true);
        SceneManager.LoadSceneAsync("LevelSelector");
    }

    public void HelpButtonClicked()
    {
        if (PlayerPrefs.GetInt("ShowBubbles") == 0)
        {
            PlayerPrefs.SetInt("ShowBubbles", 1);
            for (int i = 0; i < this.Bubbles.Length; i++)
            {
                this.Bubbles[i].SetActive(true);
            }

            this.NotHelpImage.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("ShowBubbles", 0);
            for (int i = 0; i < this.Bubbles.Length; i++)
            {
                this.Bubbles[i].SetActive(false);
            }

            this.NotHelpImage.SetActive(false);

        }
    }
}