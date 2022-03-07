using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SkillSlotPresenter : MonoBehaviour
{
    [SerializeField]
    private SkillSlotView _skillSlotView;
    private MiniLol.Unit.ISkillSlotCtrl _skillSlotCtrl;

    public void Init(MiniLol.Unit.ISkillSlotCtrl skillSlotCtrl)
    {
        _skillSlotCtrl = skillSlotCtrl;

        _skillSlotCtrl.GetSkillslot(0).curCoolTime
            .Subscribe(x => _skillSlotView.SetSkillSlotCoolTimeValue(0, (float)x / _skillSlotCtrl.GetSkillslot(0).skillDataBase.skillCoolTime))
            .AddTo(this);

        _skillSlotCtrl.GetSkillslot(1).curCoolTime
    .Subscribe(x => _skillSlotView.SetSkillSlotCoolTimeValue(1, (float)x / _skillSlotCtrl.GetSkillslot(1).skillDataBase.skillCoolTime))
    .AddTo(this);

        _skillSlotCtrl.GetSkillslot(2).curCoolTime
 .Subscribe(x => _skillSlotView.SetSkillSlotCoolTimeValue(2, (float)x / _skillSlotCtrl.GetSkillslot(2).skillDataBase.skillCoolTime))
 .AddTo(this);

        _skillSlotCtrl.GetSkillslot(3).curCoolTime
    .Subscribe(x => _skillSlotView.SetSkillSlotCoolTimeValue(3, (float)x / _skillSlotCtrl.GetSkillslot(3).skillDataBase.skillCoolTime))
    .AddTo(this);
    }
}
