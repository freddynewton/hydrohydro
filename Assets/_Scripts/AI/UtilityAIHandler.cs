using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputAiEnum
{
    Health,
    RangeToTargetNormalized,
    TargetHealth
}

public class UtilityAIHandler : MonoBehaviour
{
    [Header("Settings")]
    public SettingsAI settings;

    [HideInInspector] public GameObject target;
    [HideInInspector] public Unit targetUnit;
    [HideInInspector] public Rigidbody2D rb;

    private Unit unit;
    private List<float> utilitiesArr;
    private int currentAction;
    
    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Unit>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void calcUtility()
    {
        foreach (var action in settings.actionSettingList)
        {
           float ut = 1;

           foreach (var setting in action.settingList)
            {
                ut *= setting.curve.Evaluate(getEnumInputValue(setting.input));
            }

            utilitiesArr[settings.actionSettingList.IndexOf(action)] = ut;
        }

        chooseHighestScoreUtility();
    }

    private void getTarget()
    {
        target = Playercontroller.Instance.gameObject;
        targetUnit = Playercontroller.Instance.unit;
    }

    private void chooseHighestScoreUtility()
    {
        int index = 0;
        float highestScore = 0;

        foreach (float ut in utilitiesArr)
        {
            if (ut > highestScore)
            {
                highestScore = ut;
                index = utilitiesArr.IndexOf(ut);
            }
        }

        currentAction = index;
    }

    private float getEnumInputValue(InputAiEnum input)
    {
        switch (input)
        {
            case InputAiEnum.Health:
                return unit.currentHealth / unit.stats.health;
            case InputAiEnum.RangeToTargetNormalized:
                {
                    if (target != null)
                    {
                        float tmp = Vector2.Distance(gameObject.transform.position, target.transform.position) / unit.stats.maxRange;
                        return tmp > 1 ? 1 : tmp;
                    } 
                    return 0;
                }
            case InputAiEnum.TargetHealth:
                return targetUnit.currentHealth / targetUnit.stats.health;
            
        }

        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ActionAI action in settings.actionSettingList[currentAction].actionList)
        {
            action.use(this);
        }
    }

    private void LateUpdate()
    {
        calcUtility();
    }
}
