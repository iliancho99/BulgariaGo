using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class CoinController : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            PlayerPrefs.SetInt("FirstHintUnlocked", 0);
            PlayerPrefs.SetInt("SecondHintUnlocked", 0);
            PlayerPrefs.SetInt("IsBuildingRecognized", 1);
            SceneManager.LoadSceneAsync("Quiz");
        }
    }
}