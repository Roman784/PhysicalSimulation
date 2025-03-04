using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Lab3
{
    public class BirdLab3 : MonoBehaviour
    {
        public static BirdLab3 Instance;

        private float _t1;
        private float _t2;
        private float _t3;
        
        private float _a;
        private float _b;

        private float _c;
        private float _d;

        private Vector2 _initialVelocity;

        private Vector2 _initialPosition;
        private float _time = 0f;
        private bool _canMove = false;

        private Coroutine _moveRoutine;

        public UnityEvent<float> OnTimeChanged = new();
        public UnityEvent<float> OnPathChanged = new();
        public UnityEvent<Vector2> OnVelocityChanged = new();
        public UnityEvent<Vector2> OnAccelerationChanged = new();

        private void Awake()
        {
            Instance = this;

            _initialPosition = transform.position;
            _canMove = false;
        }

        private Vector2 InitialAcceleration => new Vector2(_a, _c);
        private Vector2 Jerk => new Vector2(_b, _d);
        private Vector2 Velocity(float time) => Formulas.Velocity(_initialVelocity, InitialAcceleration, Jerk, time);
        private Vector2 Acceleration(float time) => Formulas.Acceleration(InitialAcceleration, Jerk, time);
        private float Path(float time) => Formulas.Path(_initialPosition, _initialVelocity, InitialAcceleration, Jerk, time);

        public void SetTimePoints(Vector3 points)
        {
            _t1 = points.x;
            _t2 = points.y;
            _t3 = points.z;
            UpdateExitData();
        }
        public void SetInitialVelocity(Vector2 velocity)
        {
            _initialVelocity = velocity;
            UpdateExitData();
        }

        public void SetA(float a)
        {
            _a = a;
            UpdateExitData();
        }

        public void SetB(float b)
        {
            _b = b;
            UpdateExitData();
        }

        public void SetC(float c)
        {
            _c = c;
            UpdateExitData();
        }

        public void SetD(float d)
        {
            _d = d;
            UpdateExitData();
        }

        public void ResetTime()
        {
            _time = 0;
            UpdateExitData();

            if (_moveRoutine != null)
                StopCoroutine( _moveRoutine);
            _moveRoutine = StartCoroutine(ToT1());
        }

        public void SetCanMove(bool canMove)
        {
            _canMove = canMove;

            if (_canMove && _moveRoutine == null)
                _moveRoutine = StartCoroutine(ToT1());
        }

        private void UpdateExitData()
        {
            OnTimeChanged?.Invoke(_time);
            OnPathChanged?.Invoke(Path(_t2));
            OnVelocityChanged?.Invoke(Velocity(_t3));
            OnAccelerationChanged?.Invoke(Acceleration(_t3));
        }

        private void Update()
        {
            IncreaseTime();
        }

        private IEnumerator ToT1()
        {
            while (_time < _t1)
            {
                transform.position = Formulas.Position(_initialPosition, _initialVelocity, Vector2.zero, Vector2.zero, _time);
                yield return null;
            }

            _moveRoutine = StartCoroutine(ToT2());
        }

        private IEnumerator ToT2()
        {
            while (_time < _t2)
            {
                transform.position = Formulas.Position(_initialPosition, _initialVelocity, InitialAcceleration, Jerk, _time);
                yield return null;
            }

            _moveRoutine = StartCoroutine(ToT3());
        }

        private IEnumerator ToT3()
        {
            while (_time < _t3)
            {
                transform.position = Formulas.Position(_initialPosition, _initialVelocity, InitialAcceleration, Jerk, _time);
                yield return null;
            }

            _canMove = false;
        }

        private void IncreaseTime()
        {
            if (!_canMove) return;

            _time += Time.deltaTime;
            OnTimeChanged?.Invoke(_time);
        }
    }
}
