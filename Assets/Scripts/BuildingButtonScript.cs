using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class BuildingButtonScript : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            var name = this.gameObject.name;
            var id = int.Parse(name.Substring(8));
            PlayerPrefs.SetInt("Level", id);
            PlayerPrefs.SetInt("IncorrectAnswersCount", 0);
            PlayerPrefs.SetInt("QuestionId", 0);
            if (id < PlayerPrefs.GetInt("MaxLevel"))
            {
                PlayerPrefs.SetInt("IsBuildingRecognized", 1);
                SceneManager.LoadSceneAsync("Quiz");

            }
            else
            {
                PlayerPrefs.SetInt("IsBuildingRecognized", 0);
                SceneManager.LoadSceneAsync("Recognition");
            }
        }
    }
}
