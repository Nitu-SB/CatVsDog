using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAnimal(BelongCamp belongCamp,int index)
    {
        if(belongCamp == BelongCamp.Cat)
        {
            GameObject animal = Instantiate(cats[index], catSpawnPos);
            animal.transform.position = catSpawnPos.position;
        }

        if(belongCamp == BelongCamp.Dog)
        {
            GameObject animal = Instantiate(dogs[index], catSpawnPos);
            animal.transform.position = dogSpawnPos.position;
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
