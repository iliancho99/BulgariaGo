using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HelpController : MonoBehaviour
    {
        public int BuildingId;

        public bool FirstHintUnlocked;
        public int PlayerScore;
        public bool SecondHintUnlocked;
        public Color TextBoxColor;

        public GameObject FirstHintButton;
        public GameObject SecondHintButton;
        public GameObject TextBox;
        public Text TextBoxText;
        public GameObject[] Bubbles;
        public GameObject HelpHintBuble;
        public bool ShowHintBubble;

        private void Start()
        {
            this.BuildingId = PlayerPrefs.GetInt("Level");
            this.PlayerScore = PlayerPrefs.GetInt("Score");
            this.TextBoxColor = TextBox.GetComponent<Image>().color;
            this.TextBoxText.text = "Текущи точки: " + PlayerScore;
            this.ShowHintBubble = PlayerPrefs.GetInt("ShowBubbles") == 1;
            if (this.BuildingId < Data.BuildingIdToName.Count)
            {
                this.LoadDataInButtons();

            }
            if (PlayerPrefs.GetInt("ShowBubbles") == 1)
            {
                for (int i = 0; i < this.Bubbles.Length; i++)
                {
                    this.Bubbles[i].SetActive(true);
                }
            }

        }

        private void LoadDataInButtons()
        {
            if (PlayerPrefs.GetInt("FirstHintUnlocked") == 1)
            {
                this.FirstHintButton.GetComponentInChildren<Text>().text = Data.BuildingIdToHints[this.BuildingId][0].Text;
                this.FirstHintUnlocked = true;
            }
            else
            {
                this.FirstHintButton.GetComponentInChildren<Text>().text = "Град - Точки:" +
                                                                      Data.BuildingIdToHints[this.BuildingId][0].Price;
            }

            if (PlayerPrefs.GetInt("SecondHintUnlocked") == 1)
            {
                this.SecondHintButton.GetComponentInChildren<Text>().text = Data.BuildingIdToHints[this.BuildingId][1].Text;
                this.SecondHintUnlocked = true;

            }
            else
            {
                this.SecondHintButton.GetComponentInChildren<Text>().text = "Име - Точки:" +
                                                                       Data.BuildingIdToHints[this.BuildingId][1].Price;
            }
        }

        public void HelpButtonClicked()
        {
            if (this.TextBox.activeSelf)
            {
                this.TextBox.SetActive(false);
                this.FirstHintButton.SetActive(false);
                this.SecondHintButton.SetActive(false);
                if (this.ShowHintBubble)
                {
                    this.HelpHintBuble.SetActive(false);
                }
            }
            else
            {
                this.TextBox.SetActive(true);
                this.FirstHintButton.SetActive(true);
                this.SecondHintButton.SetActive(true);
                if (this.ShowHintBubble)
                {
                    this.HelpHintBuble.SetActive(true);
                }
            }
        }

        public void HintButtonClicked(int buttonId)
        {
            if ((this.FirstHintUnlocked && 0 == buttonId) ||
                (this.SecondHintUnlocked && 1 == buttonId))
            {
                return;
            }

            var hint = Data.BuildingIdToHints[BuildingId][buttonId];
            if (this.PlayerScore < hint.Price)
            {
                StartCoroutine(ShowMessage(1f, "Нямате достатъчно точки", Color.red));
                return;
            }

            this.PlayerScore -= hint.Price;
            PlayerPrefs.SetInt("Score", this.PlayerScore);
            TextBoxText.text = "Текущи точки: " + this.PlayerScore;

            if (buttonId == 0)
            {
                this.FirstHintButton.GetComponentInChildren<Text>().text = hint.Text;
                this.FirstHintUnlocked = true;
                PlayerPrefs.SetInt("FirstHintUnlocked", 1);
            }
            else
            {
                this.SecondHintButton.GetComponentInChildren<Text>().text = hint.Text;
                this.SecondHintUnlocked = true;
                PlayerPrefs.SetInt("SecondHintUnlocked", 1);
            }
        }

        private IEnumerator ShowMessage(float seconds, string message, Color color)
        {
            this.TextBox.GetComponent<Image>().color = color;
            this.TextBoxText.text = message;

            yield return new WaitForSeconds(seconds);

            this.TextBoxText.text = "Текущи точки: " + PlayerScore;
            this.TextBox.GetComponent<Image>().color = TextBoxColor;
        }

        public void ChangeShowButton(bool newState)
        {
            this.ShowHintBubble = newState;
        }
    }
}