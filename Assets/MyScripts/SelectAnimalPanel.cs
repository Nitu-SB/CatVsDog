using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            GameManager.instance.StartGame();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
