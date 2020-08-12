using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "AI/Settings")]
public class SettingsAI : ScriptableObject
{

    public List<actionSetting> actionList;
    
    [Serializable]
    public struct actionSetting
    {
        public ActionAI action;
        public List<setting> settingList;
    }

    [Serializable]
    public struct setting
    {
        public AnimationCurve curve;
        public InputAiEnum input;
    }


}
