using PawBab.DesignPatterns.FSM;
using UnityEngine;

namespace PawBab.DesignPatterns
{
    public interface IDoor
    {
        Vector3 ClosedEuler { get; }
        Vector3 OpenEuler { get; }

        Vector3 LocalEulerAngles { get; set; }
    }

    public class Door : MonoBehaviour, IDoor
    {
        [SerializeField]
        private Vector3 closedEuler;

        [SerializeField]
        private Vector3 openEuler;

        [SerializeField]
        private float openSpeed = 5f;

        [SerializeField]
        private float closedSpeed = 5f;

        private StateMachine<IDoor> _fsm;

        public Vector3 ClosedEuler => closedEuler;
        public Vector3 OpenEuler => openEuler;

        public Vector3 LocalEulerAngles
        {
            get => transform.localEulerAngles;
            set => transform.localEulerAngles = value;
        }

        private void Awake()
        {
            _fsm = new(this);
            _fsm.AddStates(
                new ClosedDoor(),
                new OpeningDoor(openSpeed),
                new OpenDoor(),
                new ClosingDoor(closedSpeed)
            );
        }

        private void Start() => _fsm.ChangeState<ClosedDoor>();

        private void Update() => _fsm.Tick(Time.deltaTime);

        public void Interact() => _fsm.Interact();

        private class ClosedDoor : State<IDoor>
        {
            public override void OnEnter()
            {
                Owner.LocalEulerAngles = Owner.ClosedEuler;
            }

            public override void Interact()
            {
                Machine.ChangeState<OpeningDoor>();
            }
        }

        private class OpeningDoor : State<IDoor>
        {
            private readonly float _openSpeed;

            public OpeningDoor(float openSpeed)
            {
                _openSpeed = openSpeed;
            }

            public override void Tick(float deltaTime)
            {
                var fromEuler = Owner.LocalEulerAngles;
                var toEuler = Owner.OpenEuler;

                Owner.LocalEulerAngles = Vector3.Slerp(fromEuler, toEuler, deltaTime * _openSpeed);

                var angle = Quaternion.Angle(Quaternion.Euler(Owner.LocalEulerAngles), Quaternion.Euler(toEuler));

                if (angle < 1f)
                    Machine.ChangeState<OpenDoor>();
            }
        }

        private class OpenDoor : State<IDoor>
        {
            public override void OnEnter()
            {
                Owner.LocalEulerAngles = Owner.OpenEuler;
            }

            public override void Interact()
            {
                Machine.ChangeState<ClosingDoor>();
            }
        }

        private class ClosingDoor : State<IDoor>
        {
            private readonly float _closeSpeed;

            public ClosingDoor(float closeSpeed)
            {
                _closeSpeed = closeSpeed;
            }

            public override void Tick(float deltaTime)
            {
                var fromEuler = Owner.LocalEulerAngles;
                var toEuler = Owner.ClosedEuler;

                Owner.LocalEulerAngles = Vector3.Slerp(fromEuler, toEuler, deltaTime * _closeSpeed);

                var angle = Quaternion.Angle(Quaternion.Euler(Owner.LocalEulerAngles), Quaternion.Euler(toEuler));

                if (angle < 1f)
                    Machine.ChangeState<ClosedDoor>();
            }
        }
    }
}