using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [Serializable]
    public class ScrollData
    {
        [SerializeField] private string itemName;
        [SerializeField] private CollectibleType type;
        private int _maxCount;
        
        public string ItemName
        {
            get => itemName;
            private set => itemName = value;
        }
        
        public CollectibleType Type
        {
            get => type;
            private set => type = value;
        }
        
        public int MaxCount
        {
            get => _maxCount;
            set => _maxCount = value;
        }
    }
    
    public class Scroll : MonoBehaviour
    {
        [SerializeField] private ScrollData[] scrollData;
        [SerializeField] private GameObject scrollOverlay;
        [SerializeField] private GameObject scrollItem;
        [SerializeField] private GameObject scrollPanel;
        private CollectionState[] _collectionStates = new CollectionState[2];

        private void Start()
        {
            foreach (ScrollData data in scrollData)
            {
                data.MaxCount = GameStats.Instance.NumberOfCollectibles(data.Type);
            }
            
            _collectionStates[0] = CollectionState.InPlayerInventory;
            _collectionStates[1] = CollectionState.Returned;
        }

        private void OnScroll(InputValue inputValue)
        {
            bool scrollState = !scrollOverlay.activeSelf;

            if (scrollState)
            {
                RectTransform panelTransform = scrollPanel.GetComponent<RectTransform>();
                int children = panelTransform.childCount;
                for (int i = children - 1; i >= 0; i--)
                {
                    Destroy(panelTransform.GetChild(i).gameObject);
                }
                
                BuildScroll();
            }
            
            scrollOverlay.SetActive(scrollState);
            AkSoundEngine.PostEvent("Play_paper", gameObject);
            GameStats.Instance.IsPaused = scrollState;
        }

        private void BuildScroll()
        {
            for(int i = 0; i < scrollData.Length; i++)
            {
                int collected = GameStats.Instance.NumberOfCollectibles(scrollData[i].Type, _collectionStates);
                int maxCount = scrollData[i].MaxCount;
                GameObject scrollEntryInstance = Instantiate(scrollItem, scrollPanel.transform);
                TMP_Text scrollEntryText = scrollEntryInstance.GetComponent<TMP_Text>();
                
                scrollEntryText.text = $"{collected}/{maxCount} {scrollData[i].ItemName}";
                
                if (collected == maxCount)
                {
                    scrollEntryText.fontStyle = FontStyles.Strikethrough;
                }
            }
        }
    }
}