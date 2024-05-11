using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameManager()
    {
        instance = this;
    }
    public Transform catSpawnPos, dogSpawnPos;

    public List<GameObject> cats = new List<GameObject>();
    public List<GameObject> dogs = new List<GameObject>();

    public int catNum=0,dogNum=0;
    //public int curCatNum,curDogNum;
    public Text catText,dogText;

    public bool trackMode=false;

    public EndPanel endPanel;
    // Start is called before the first frame update
    void Start()
    {
        catNum = 0;
        dogNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAnimal(BelongCamp belongCamp,int index)
    {
        if(belongCamp == BelongCamp.Cat)
        {
            catNum++;
            if (catNum > 10)
            {
                catNum = 10;
                return;
            }
            catText.text = catNum + "/10";
            GameObject animal = Instantiate(cats[index], catSpawnPos);
            animal.transform.position = catSpawnPos.position;
        }

        if(belongCamp == BelongCamp.Dog)
        {
            dogNum++;
            if (dogNum > 10)
            {
                dogNum = 10;
                return;
            }
            dogText.text = dogNum + "/10";
            GameObject animal = Instantiate(dogs[index], catSpawnPos);
            animal.transform.position = dogSpawnPos.position;
        }
    }
    public void AnimalDie(BelongCamp belongCamp)
    {
        if (belongCamp == BelongCamp.Cat)
        {
            catNum--;
        }
        if (belongCamp == BelongCamp.Dog)
        {
            dogNum--;
        }
        if(catNum <=1 || dogNum <= 1)
        {
            trackMode = true;
        }
        if(catNum ==0 || dogNum == 0)
        {
            DOVirtual.DelayedCall(1, () => { 
                if(catNum==0 && dogNum == 0)
                {
                    endPanel.OpenPanel(2);
                }
                if(catNum==0&& dogNum > 0)
                {
                    endPanel.OpenPanel(1);
                }
                if (catNum > 0 && dogNum == 0)
                {
                    endPanel.OpenPanel(0);
                }
            });
        }
    }
    public void StartGame()
    {
        
        foreach(Transform child in catSpawnPos)
        {
            child.GetComponent<AnimalBase>().StartGame();
        }

        foreach (Transform child in dogSpawnPos)
        {
            child.GetComponent<AnimalBase>().StartGame();
        }

        if(dogNum ==1 || catNum == 1)
        {
            trackMode = true;
        }
    }
}
