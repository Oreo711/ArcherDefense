using System;
using UnityEngine;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay.Features
{
    public class Mole : MonoBehaviour, ITickable
    {
        [SerializeField] private Transform _mole;

        private const float InHeight  = -0.75f;
        private const float OutHeight = -0.25f;
        private       bool  _isOut;
        private       float _outTime;

        public  bool  IsOut => _isOut;

        public event Action Entered;

        public void LeaveHole (float outTime)
        {
            _isOut = true;
            _outTime = outTime;
            _mole.localPosition = new Vector3(0, OutHeight, 0);
        }

        public void Tick ()
        {
            if (!_isOut)
            {
                return;
            }

            _outTime -= Time.deltaTime;

            if (_outTime <= 0)
            {
                EnterHole();
                Entered?.Invoke();
            }
        }

        public void EnterHole ()
        {
            _isOut = false;
            _mole.localPosition = new Vector3(0, InHeight, 0);
        }
    }
}
