using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts

{
    public class RecognitionController : MonoBehaviour
    {
        public int BuildingId;
        public GameObject[] buildingPrefabs;
        public Sprite[] BuildingSprites;

        public GameObject Camera;
        public bool IsBuildingButtonClicked;
        public bool IsTargetButtonClicked;
        public Image Panel;
        public Sprite[] TargetSprites;
        public Canvas Canvas;
        public GameObject WinnerButton;

        

        private void Start()
        {
            this.BuildingId = PlayerPrefs.GetInt("Level");
            if (this.BuildingId >= Data.BuildingIdToName.Count)
            {
                var buttons = this.Canvas.GetComponentsInChildren<Button>();
                foreach (var button in buttons)
                {
                    button.gameObject.SetActive(false);
                }
                
                this.WinnerButton.SetActive(true);

                this.Camera.SetActive(true);
                
            }
            else
            {
                Instantiate(buildingPrefabs[BuildingId], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                this.WinnerButton.SetActive(false);
            }

            this.Camera.SetActive(true);


        }

        public void BuildingButtonClicked()
        {
            if (this.IsBuildingButtonClicked)
            {
                this.Panel.gameObject.SetActive(false);
                this.IsBuildingButtonClicked = false;
            }
            else
            {
                this.Panel.sprite = BuildingSprites[BuildingId];
                this.Panel.gameObject.SetActive(true);
                this.IsBuildingButtonClicked = true;
                this.IsTargetButtonClicked = false;
            }
        }

        public void TargetButtonClicked()
        {
            if (this.IsTargetButtonClicked)
            {
                this.Panel.gameObject.SetActive(false);
                this.IsTargetButtonClicked = false;
            }
            else
            {
                this.Panel.sprite = TargetSprites[BuildingId];
                this.Panel.gameObject.SetActive(true);
                this.IsTargetButtonClicked = true;
                this.IsBuildingButtonClicked = false;
            }
        }

        public void HomeButtonClicked()
        {
            SceneManager.LoadSceneAsync("Menu");
        }
    }
}