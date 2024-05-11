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
    /// �򿪽������
    /// 0=èӮ��1=��Ӯ��2=ƽ��
    /// </summary>
    /// <param name="index"></param>
    public void OpenPanel(int index)
    {
        transform.GetChild(index).gameObject.SetActive(true);
    }
}
