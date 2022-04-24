using UnityEngine;

namespace Runtime.Player
{
    public class PlayerStatusContainer : MonoBehaviour
    {
        [SerializeField] private PlayerStatus _baseStatus;
        private PlayerStatus _status;
        public PlayerStatus Status => _status ??= Instantiate(_baseStatus);
        public IPlayerStatusEvent StatusEvent => _status ??= Instantiate(_baseStatus);
        
        // public PlayerStatus Status => _status ??= _baseStatus;
        // public IPlayerStatusEvent StatusEvent => _status ??= _baseStatus;
        
    }
}