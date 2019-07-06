using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{ 

    public Animator animator;
    private int sceneToLoad;
    
    private static GameObject MonoSingletionRoot;
    private static SceneChanger instance;
    public static SceneChanger Instance
    {
        get
        {
            if (MonoSingletionRoot==null)
            {
                MonoSingletionRoot = GameObject.Find("SceneChanger");
                if (MonoSingletionRoot==null)
                {
                    MonoSingletionRoot = new GameObject();
                    MonoSingletionRoot.name = "SceneChanger";
                    DontDestroyOnLoad(MonoSingletionRoot);
                }
            }

            if (instance == null)
            {
                instance = MonoSingletionRoot.GetComponent<SceneChanger>();
                if (instance == null)
                {
                    instance = MonoSingletionRoot.AddComponent<SceneChanger>();
                }
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void FadeToNextScene()
    {
        FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //触发fade动画
    public void FadeToScene(int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    //加载
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
