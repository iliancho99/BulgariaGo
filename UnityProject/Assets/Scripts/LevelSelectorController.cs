using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelSelectorController : MonoBehaviour
    {

        public GameObject Content;
        public GameObject Building;
        public GameObject DefaultBuildingText;
        public Sprite[] BuildingsSprites;

        void Start()
        {
            LoadBuildings();
        }

        public void LoadBuildings()
        {
            GameObject building;
            var currentBuilding = PlayerPrefs.GetInt("MaxLevel");
            if (currentBuilding >= Data.BuildingIdToName.Count)
            {
                currentBuilding -= 1;
            }

            for (int i = 0; i <= currentBuilding; i++)
            {
                building = (GameObject)Instantiate(this.Building);
                var buildingImage = building.GetComponentsInChildren<Image>()[1];
                buildingImage.sprite = BuildingsSprites[i];
                var texts = building.GetComponentsInChildren<Text>();
                texts[0].text = Data.BuildingIdToName[i];
                building.transform.parent = this.Content.transform;
                building.name = "Building" + i;
                var correctAnswers = PlayerPrefs.GetInt("Building" + i);
                if (i != currentBuilding || PlayerPrefs.GetInt("MaxLevel") + 1 > Data.BuildingIdToName.Count)
                {
                    texts[1].text = string.Format("Верни отговори: {0} \\ {1}", correctAnswers,
                    Data.BuildingIdToQuestions[i].Count);
                }
                else
                {
                    texts[1].text = "Не е отговорено";
                }

                var buildingLayoutElement = building.GetComponent<LayoutElement>();
                buildingLayoutElement.minHeight = Camera.main.pixelHeight / 5;

            }            
        }

        public void HomeButtonClicked()
        {
            SceneManager.LoadSceneAsync("Menu");
        }
    }
}
