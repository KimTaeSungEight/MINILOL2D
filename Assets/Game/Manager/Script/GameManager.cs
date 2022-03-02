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

            var testGo = GameObject.Instantiate(_test, Vector3.zero, Quaternion.identity);
            var testUM = testGo.GetComponent<Unit.IUnitModerator>() as Unit.ChampionModerator;
            testUM.IsControllChampion = true;
            testUM.Init();
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