using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SelectAnimalPanel : MonoBehaviour
{
    public Transform catBtns,dogBtns;

    public Button startBtn;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in catBtns)
        {
            child.GetComponent<Button>().onClick.RemoveAllListeners();
            child.GetComponent<Button>().onClick.AddListener(() => {
                GameManager.instance.SpawnAnimal(BelongCamp.Cat, child.GetSiblingIndex());
            });
        }

        foreach (Transform child in dogBtns)
        {
            child.GetComponent<Button>().onClick.RemoveAllListeners();
            child.GetComponent<Button>().onClick.AddListener(() => {
                GameManager.instance.SpawnAnimal(BelongCamp.Dog, child.GetSiblingIndex());
            });
        }

        startBtn.onClick.RemoveAllListeners();
        startBtn.onClick.AddListener(() => {

            if (GameManager.instance.catNum == 0 || GameManager.instance.dogNum == 0)
            {
                Debug.Log("存在数量为0的队伍");
                return;
            }

            transform.GetChild(0).DOScale(0, 0.3f).SetEase(Ease.OutQuart);
            transform.GetChild(1).DOScale(0, 0.3f).SetEase(Ease.OutQuart);
            transform.GetChild(2).DOScale(1,0.4f).SetEase(Ease.OutBack).OnComplete(() => {
                
                DOVirtual.DelayedCall(0.8f, () => {
                    GameManager.instance.StartGame();
                    transform.GetChild(2).DOScale(0, 0.4f).SetEase(Ease.OutQuart).OnComplete(() => {
                        gameObject.SetActive(false);
                    });
           
                });
                
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
