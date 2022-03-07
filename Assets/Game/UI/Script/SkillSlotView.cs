using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotView : MonoBehaviour
{
    [SerializeField]
    private Image[] skillSlotCoolTimeImageArr;
    
    public void SetSkillSlotCoolTimeValue(int index, float value)
    {
        skillSlotCoolTimeImageArr[index].fillAmount = value;
    }
}
