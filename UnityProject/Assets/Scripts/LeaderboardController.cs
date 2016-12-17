using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LeaderboardController : MonoBehaviour
    {
        private readonly string secretKey = "BulgariaGo";
        public string AddUserURL = "http://www.iliyankordev.bplaced.net/adduser.php?";
        public string HighscoreURL = "http://www.iliyankordev.bplaced.net/display.php";
        public string UpdateScoreURL = "http://www.iliyankordev.bplaced.net/updatescore.php?";
        public string Username;

        public GameObject Content;
        public GameObject Continue;
        public GameObject InputField;
        public GameObject LoadingBar;
        public GameObject Notification;

        public GameObject Player;



        private void Start()
        {
            if (PlayerPrefs.HasKey("Username"))
            {
                this.Username = PlayerPrefs.GetString("Username");
                StartCoroutine(UpdataScores(PlayerPrefs.GetString("Username"), PlayerPrefs.GetInt("Score")));
            }
            else
            {
                this.InputField.SetActive(true);
                this.Continue.SetActive(true);
                this.LoadingBar.SetActive(false);
            }
        }

        private IEnumerator AddUser(string name, int score = 0)
        {
            var hash = Utilities.Md5Sum(name + score + secretKey);

            var userExistURL = AddUserURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

            var userExist = new WWW(userExistURL);
            yield return userExist;

            if (userExist.text == "The username exist")
            {
                this.Notification.GetComponentInChildren<Text>().text =
                   "Въведеното потребителско име съществува \n (чукни за да продължиш)";
                this.Notification.SetActive(true);
                this.LoadingBar.SetActive(false);
            }
            else
            {
                var addUserURL = this.AddUserURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" +
                                 hash;
                var addUser = new WWW(addUserURL);
                yield return addUser;

                if (addUser.error != null)
                {
                    this.Notification.GetComponentInChildren<Text>().text = "Възникнала е грешка при връзката със сървъра!";
                    this.Notification.SetActive(true);
                }
                else
                {
                    PlayerPrefs.SetString("Username", Username);
                    this.InputField.SetActive(false);
                    this.Continue.SetActive(false);
                    this.LoadingBar.SetActive(true);
                    this.StartCoroutine(GetScores());
                }
            }
        }


        private IEnumerator UpdataScores(string name, int score)
        {
            var hash = Utilities.Md5Sum(name + score + secretKey);

            var post_url = UpdateScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

            var hs_post = new WWW(post_url);
            yield return hs_post;

            if (hs_post.error != null)
            {
                this.Notification.GetComponentInChildren<Text>().text = "Възникнала е грешка при връзката със сървъра!";
                this.Notification.SetActive(true);
                this.LoadingBar.SetActive(false);
            }
            else
            {
                this.InputField.SetActive(false);
                this.Continue.SetActive(false);
                this.LoadingBar.SetActive(true);
                StartCoroutine(GetScores());
            }
        }


        private IEnumerator GetScores()
        {
            var hs_get = new WWW(this.HighscoreURL);
            yield return hs_get;

            if (hs_get.error != null)
            {
                this.Notification.GetComponentInChildren<Text>().text = "Възникнала е грешка при връзката със сървъра!";
                this.Notification.SetActive(true);
                this.LoadingBar.SetActive(false);
            }
            else
            {

                var headerScrollBar = Instantiate(Player);
                headerScrollBar.transform.parent = Content.transform;
                var headerScrollBarSize = headerScrollBar.GetComponent<LayoutElement>();

                var headerLayoutElement = headerScrollBar.GetComponent<LayoutElement>();
                headerLayoutElement.minHeight = Camera.main.pixelHeight / 6;

                var response = hs_get.text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var position = 1;
                for (var i = 1; i < response.Length; i += 2, position++)
                {
                    var currentPlayer = Instantiate(Player);
                    var textComponents = currentPlayer.GetComponentsInChildren<Text>();
                    if (response[i - 1] == PlayerPrefs.GetString("Username"))
                    {
                        currentPlayer.GetComponent<Image>().color = Color.green;
                    }

                    textComponents[0].text = position.ToString();
                    textComponents[1].text = response[i - 1];
                    textComponents[2].text = response[i];
                    currentPlayer.transform.parent = Content.transform;

                    var playerLayoutElement = currentPlayer.GetComponent<LayoutElement>();
                    playerLayoutElement.minHeight = Camera.main.pixelHeight / 8;

                }

            }

            this.LoadingBar.SetActive(false);
        }

        public void RegisterButtonClicked()
        {
            var username = InputField.GetComponentsInChildren<Text>()[1].text.Trim();
            if (username.Length < 6)
            {
                this.Notification.GetComponentInChildren<Text>().text = "Потребителското е с минимална дължина 6 символа!";
                this.Notification.SetActive(true);
                this.LoadingBar.SetActive(false);
                return;
            }

            this.Username = username;

            StartCoroutine(this.AddUser(Username, PlayerPrefs.GetInt("Score")));
        }

        public void HomeButtonClicked()
        {
            SceneManager.LoadSceneAsync("Menu");
        }

        public void NotificationButtonClicked()
        {
            SceneManager.LoadSceneAsync("Leaderboard");
        }
    }
}