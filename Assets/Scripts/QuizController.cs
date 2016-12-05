using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class QuizController : MonoBehaviour
    {
        public bool ContinueToNextLevel;
        public int CorrectAnswerId;
        public int CurrentBuildingId;
        public int CurrentQuestionId;
        public int IncorrectAnswersCount;
        public bool IsAnswered;
        public int UserAnswerId;
        public Color DefaultButtonColor;

        public Text ButtonAText;
        public Text ButtonBText;
        public Text ButtonCText;
        public Text ButtonDText;
        public GameObject LoadingBar;
        public Button Notification;
        public Text QuestionText;
        public GameObject[] Bubbles;


        private void Start()
        {
            this.IncorrectAnswersCount = PlayerPrefs.GetInt("IncorrectAnswersCount");
            this.CurrentQuestionId = PlayerPrefs.GetInt("QuestionId");
            this.CorrectAnswerId = 1;
            this.UserAnswerId = 1;
            this.CurrentBuildingId = PlayerPrefs.GetInt("Level");
            this.LoadDataInQuiz();

            if (PlayerPrefs.GetInt("ShowBubbles") == 1)
            {
                for (int i = 0; i < this.Bubbles.Length; i++)
                {
                    this.Bubbles[i].SetActive(true);
                }
            }
        }

        private void LoadDataInQuiz()
        {
            if (this.CorrectAnswerId != this.UserAnswerId)
            {
                this.ChangeButtonColor(UserAnswerId, DefaultButtonColor);
            }

            this.ChangeButtonColor(CorrectAnswerId, DefaultButtonColor);


            var question = Data.BuildingIdToQuestions[CurrentBuildingId][CurrentQuestionId];
            this.QuestionText.text = question.QuestionText;
            this.ButtonAText.text = "А)" + question.Answers[0];
            this.ButtonBText.text = "Б)" + question.Answers[1];
            this.ButtonCText.text = "В)" + question.Answers[2];
            this.ButtonDText.text = "Г)" + question.Answers[3];
            this.CorrectAnswerId = question.CorrectAnswerId;
        }

        public void AnswerButtonClicked(int buttonId)
        {
            if (this.IsAnswered)
            {
                return;
            }

            this.IsAnswered = true;
            this.UserAnswerId = buttonId;

            if (this.CorrectAnswerId == buttonId)
            {
                this.ChangeButtonColor(buttonId, Color.green);
            }
            else
            {
                this.ChangeButtonColor(buttonId, Color.red);
                this.ChangeButtonColor(CorrectAnswerId, Color.green);
                this.IncorrectAnswersCount++;
                PlayerPrefs.SetInt("IncorrectAnswersCount", this.IncorrectAnswersCount);

            }

            StartCoroutine(Wait(1));
        }

        public Button IdToButton(int buttonId)
        {
            Button button;
            switch (buttonId)
            {
                case 1:
                    button = this.ButtonAText.gameObject.GetComponentInParent<Button>();
                    break;
                case 2:
                    button = this.ButtonBText.gameObject.GetComponentInParent<Button>();
                    break;
                case 3:
                    button = this.ButtonCText.gameObject.GetComponentInParent<Button>();
                    break;
                case 4:
                    button = this.ButtonDText.gameObject.GetComponentInParent<Button>();
                    break;
                default:
                    throw new ArgumentException("The button Id is invalid!" + buttonId);
            }
            return button;
        }

        public void ChangeButtonColor(int id, Color color)
        {
            var button = IdToButton(id);
            var imageButton = button.GetComponent<Image>();
            imageButton.color = color;
        }

        private IEnumerator Wait(float seconds)
        {
            yield return new WaitForSeconds(seconds);


            if (this.CurrentQuestionId == Data.BuildingIdToQuestions[this.CurrentBuildingId].Count - 1)
            {
                if (this.IncorrectAnswersCount <= (this.CurrentQuestionId + 1)/3)
                {
                    this.ShowNotification("Браво!\n(чукни за да продължиш)", Color.green);
                    this.ContinueToNextLevel = true;
                }
                else
                {
                    this.ShowNotification("Твърде много грешки! \nОпитай пак!\n(чукни за да продължиш)", Color.red);
                    this.ContinueToNextLevel = false;
                    this.IncorrectAnswersCount = 0;
                    PlayerPrefs.SetInt("IncorrectAnswersCount", 0);
                    PlayerPrefs.SetInt("QuestionId", 0);
                }

                this.CurrentQuestionId = 0;
            }
            else
            {
                this.CurrentQuestionId++;
                PlayerPrefs.SetInt("QuestionId", this.CurrentQuestionId);
                this.IsAnswered = false;
                this.LoadDataInQuiz();
            }
        }

        private void ShowNotification(string text, Color color)
        {
            var notificationText = Notification.GetComponentInChildren<Text>();
            notificationText.text = text;
            this.Notification.GetComponent<Image>().color = color;
            this.Notification.gameObject.SetActive(true);
        }

        public void NotificationButtonClicked()
        {
            if (this.ContinueToNextLevel)
            {
                this.Notification.gameObject.SetActive(false);
                this.LoadingBar.SetActive(true);

                var playerScore = PlayerPrefs.GetInt("Score");
                var scoreToGive = Data.BuildingIdToQuestions[this.CurrentBuildingId].Count - this.IncorrectAnswersCount;

                

                if (this.CurrentBuildingId + 1 > PlayerPrefs.GetInt("MaxLevel"))
                {
                    PlayerPrefs.SetInt("MaxLevel", this.CurrentBuildingId + 1);
                }

                var scoreToSave = Math.Max(scoreToGive, PlayerPrefs.GetInt("Building" + this.CurrentBuildingId));

                if (this.CurrentBuildingId < PlayerPrefs.GetInt("MaxLevel"))
                {
                    scoreToGive -= PlayerPrefs.GetInt("Building" + this.CurrentBuildingId);
                }

                PlayerPrefs.SetInt("Building" + this.CurrentBuildingId, scoreToSave);

                

                PlayerPrefs.SetInt("Score", playerScore + scoreToGive);
                PlayerPrefs.SetInt("Level", this.CurrentBuildingId + 1);
                PlayerPrefs.SetInt("IsBuildingRecognized", 0);
                PlayerPrefs.SetInt("IncorrectAnswersCount", 0);
                PlayerPrefs.SetInt("QuestionId", 0);
                PlayerPrefs.SetInt("ShowBubbles", 0);
                
                SceneManager.LoadSceneAsync("Recognition");
            }
            else
            {
                this.LoadDataInQuiz();
                this.Notification.gameObject.SetActive(false);
                this.UserAnswerId = 1;
                this.IncorrectAnswersCount = 0;
                this.IsAnswered = false;
            }
        }

        public void HomeButtonClicked()
        {
            SceneManager.LoadSceneAsync("Menu");
        }


        public void HelpButtonClicked()
        {
            Application.OpenURL(Data.BuildingURL[this.CurrentBuildingId]);
        }
    }
}