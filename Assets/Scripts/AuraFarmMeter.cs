using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class AuraFarmMeter : MonoBehaviour
{   
    private static AuraFarmMeter instance;

    [Header("UI References")]
    [SerializeField] private Slider auraMeter;

    [Header("Variables")]
    [SerializeField] private float maxAura;
    [SerializeField] private float fillSpeed;

    private float currentAura = 0f;
    private Coroutine fillCoroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {   
        //Listens to scene changes to find new ui slider
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSliderInScene();
    }

    private void Start()
    {   
        FindSliderInScene();
    }

    private void FindSliderInScene()
    {
        if (auraMeter == null)
        {
            auraMeter = FindAnyObjectByType<Slider>();
        }

        if (auraMeter != null)
        {
            auraMeter.maxValue = maxAura;
            auraMeter.value = currentAura;
        }

    }

    //Yarncommands can be custom made to fit projects
    //The text inside the quotes are the syntax for the command within your yarnscript
    [YarnCommand("add_aura")]
    public void AddAura(float auraToAdd)
    {
        currentAura = Mathf.Clamp(currentAura + auraToAdd, 0f, maxAura);
        
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }
        fillCoroutine = StartCoroutine(AnimateMeter());
    }

    private IEnumerator AnimateMeter()
    {
        while (!Mathf.Approximately(auraMeter.value, currentAura))
        {
            auraMeter.value = Mathf.MoveTowards(auraMeter.value, currentAura, fillSpeed * Time.deltaTime * 50f);
            yield return null;
        }
    }
}
