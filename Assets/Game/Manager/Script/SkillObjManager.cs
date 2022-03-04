using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniLol.Unit.Skill;

namespace MiniLol.Manager
{
    public class SkillObjManager : MonoBehaviour
    {
        private Queue<SkillObj> _skillObjQueue = new Queue<SkillObj>();

        private bool ReserveQueue(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var skillObj = CreateSkillObject();
                if (skillObj == null)
                    continue;

                skillObj.transform.SetParent(this.transform);
                skillObj.gameObject.SetActive(false);

                _skillObjQueue.Enqueue(skillObj);
            }

            return true;
        }

        private SkillObj CreateSkillObject()
        {
            GameObject obj = new GameObject("skillObject");
            var skillObject = obj.AddComponent<SkillObj>();

            return skillObject;
        }

        public SkillObj GetSkillObject()
        {
            if (_skillObjQueue.Count <= 0)
            {
                if (ReserveQueue(5) == false)
                    return null;
            }

            var skillObject = _skillObjQueue.Dequeue();
            skillObject.gameObject.SetActive(true);
            return skillObject;
        }

        public void EnqueueSkillObject(SkillObj skillObject)
        {
            skillObject.gameObject.SetActive(false);
            _skillObjQueue.Enqueue(skillObject);
        }
    }
}