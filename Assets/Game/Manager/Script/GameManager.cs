using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniLol.Manager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        [SerializeField]
        private StatDataBank _statDataBank;
        public StatDataBank StatDataBank => _statDataBank;

        [SerializeField]
        private GameObject _test;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }


        }

        public void CrateChampion(int championId)
        {
            var testGo = GameObject.Instantiate(_test, new Vector3(0, 0, -0.6f), Quaternion.identity);
            var testUM = testGo.GetComponent<Unit.IUnitModerator>() as Unit.ChampionModerator;
            testUM.Init(championId, true);
        }


        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    return null;
                }
                else
                {
                    return instance;
                }
            }
        }
    }
}