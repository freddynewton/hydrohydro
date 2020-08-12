using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputAiEnum
{
    Health,
    RangeToTargetNormalized
}

public class UtilityAIHandler : MonoBehaviour
{
    [Header("Settings")]
    public SettingsAI settings;

    [HideInInspector] public GameObject target;
    [HideInInspector] public Rigidbody2D rb;

    private Unit unit;
    private List<float> utilitiesArr;
    private ActionAI currentAction;
    
    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Unit>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void calcUtility()
    {
        foreach (var action in settings.actionList)
        {
           float ut = 1;

           foreach (var setting in action.settingList)
            {
                ut *= setting.curve.Evaluate(getEnumInputValue(setting.input));
            }

            utilitiesArr[settings.actionList.IndexOf(action)] = ut;
        }
    }

    private void chooseHighestScoreUtility()
    {
        int index = 0;
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
        }

        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
