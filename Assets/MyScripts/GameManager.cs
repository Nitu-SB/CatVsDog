using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int curCatNum,curDogNum;
    public Text catText,dogText;

    public bool trackMode=false;
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
            curCatNum = catNum;
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
            curDogNum = dogNum;
            GameObject animal = Instantiate(dogs[index], catSpawnPos);
            animal.transform.position = dogSpawnPos.position;
        }
    }
    public void AnimalDie(BelongCamp belongCamp)
    {
        if (belongCamp == BelongCamp.Cat)
        {
            curCatNum--;
        }
        if (belongCamp == BelongCamp.Dog)
        {
            curDogNum--;
        }
        if(curCatNum <=1 || curDogNum <= 1)
        {
            trackMode = true;
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
    }
}
