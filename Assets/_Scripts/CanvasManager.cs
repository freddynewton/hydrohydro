using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    [Header("HUD")]
    public Slider HealthSlider;
    public Slider WaterSlider;
    public WeaponUi WeaponUi;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.fillRect.GetComponent<Image>().color = Color.Lerp(new Color32(174, 35, 52, 255), new Color32(30, 188, 115, 255), HealthSlider.normalizedValue);
        WaterSlider.fillRect.GetComponent<Image>().color = Color.Lerp(new Color32(77, 101, 180, 255), new Color32(143, 211, 255, 255), WaterSlider.normalizedValue);
    }

    public void SetHealthSlider(int health, int maxHealth)
    {
        HealthSlider.maxValue = maxHealth;

        LeanTween.value(HealthSlider.value, health, 0.5f).setEase(LeanTweenType.easeOutBounce).setOnUpdate((float val) =>
        {
            HealthSlider.value = val;
        });
    }

    public void SetWaterSlider(int water, int maxWater)
    {
        WaterSlider.maxValue = maxWater;

        

        LeanTween.value(WaterSlider.value, water, 0.5f).setEase(LeanTweenType.easeOutExpo).setOnUpdate((float val) =>
        {
            WaterSlider.value = val;
        });
    }

    public void StartReloadAnim(float reloadTime, int ClipSize)
    {

    }

    private void Init()
    {
        StartCoroutine(InitSliders());
    }

    private IEnumerator InitSliders()
    {
        yield return new WaitForEndOfFrame();
        HealthSlider.maxValue = Playercontroller.Instance.unit.stats.health;
        HealthSlider.value = Playercontroller.Instance.unit.currentHealth;
        WaterSlider.maxValue = Playercontroller.Instance.unit.MaxWater;
        WaterSlider.value = Playercontroller.Instance.unit.water;
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
