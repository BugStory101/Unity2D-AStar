using System.Collections;
using UnityEngine;

public class CourutinTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Test");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // 코루틴 중지
            StopCoroutine(Test());
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {            
            StartCoroutine("Test");
        }
    }


    IEnumerator Test()
    {
        int i = 0;
        while(true)
        {
            ++i;
            Debug.Log(i + "초");
            yield return new WaitForSeconds(1.0f);
        }

        // 코루틴 종료
        yield break;
    }
}
