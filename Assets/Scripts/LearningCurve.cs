using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    private int currentAge = 30;
    public int addedAge = 1;

    public float pi = 3.14f;
    public string firstName = "Harrison";
    public bool isAuthor = true;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(30 + 1);

        //Debug.Log(currentAge + 1);
        
        computeAge();

        // 文字列内で変数を展開する
        Debug.Log($"文字列には{firstName}のような変数を挿入できる");

        // 型変換
        Debug.Log((int)3.14);

        Debug.Log(GameObject.Find("Directional Light").GetComponent<Transform>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void computeAge()
    {
        Debug.Log(currentAge + addedAge);
    }
}
