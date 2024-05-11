using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    
    void Start()
    {
        foreach(Transform child in transform)
        {
            child.GetChild(0).Find("ReplayButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });
        }
    }

    void Update()
    {
        
    }
    /// <summary>
    /// 打开结束面板
    /// 0=猫赢，1=狗赢，2=平局
    /// </summary>
    /// <param name="index"></param>
    public void OpenPanel(int index)
    {
        transform.GetChild(index).gameObject.SetActive(true);
    }
}
