namespace Runtime.Player
{
    public interface IPlayerStatusEvent
    {
        public void RegisterListener(PlayerEventListener listener);
        public void UnregisterListener(PlayerEventListener listener);
    }
}