using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
   
    [Header("로딩 연출")]
    public Slider progressBar;
    public float fakeLoadSpeed = 3f;// 조절 가능한 가짜 로딩 속도
    
    [Header("캐릭터 연출")]
    public GameObject[] objects;
    int objectSelect;
    
    void Start()
    {
        objectSelect = Random.Range(0, 3);
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false); // 모든 캐릭터 비활성화
        }
        
            StartCoroutine(LoadNextScene());
        
    }
    IEnumerator LoadNextScene()
    {

        objects[objectSelect].SetActive(true); //캐릭터 3개중 랜덤 출현

        AsyncOperation op = SceneManager.LoadSceneAsync(SceneLoader.nextScene);

        op.allowSceneActivation = false;
        float fakeProgress = 0.0f;

        // 실제 로딩이 진행되는 동안
        while (op.progress < 0.9f)
        {
            fakeProgress = Mathf.MoveTowards(fakeProgress, op.progress, Time.deltaTime * fakeLoadSpeed);
            progressBar.value = fakeProgress;
            yield return null;
        }

        // 로딩 완료됐지만 아직 전환하지 않음
        while (fakeProgress < 1.0f)
        {
            fakeProgress = Mathf.MoveTowards(fakeProgress, 1.0f, Time.deltaTime * fakeLoadSpeed);
            progressBar.value = fakeProgress;
            //Debug.Log($"[로드 진행도] op.progress: {op.progress}, fake: {fakeProgress}");
            yield return null;
        }

        // 약간의 지연 후 씬 전환
        yield return new WaitForSeconds(0.3f);
        op.allowSceneActivation = true;
    }
}
