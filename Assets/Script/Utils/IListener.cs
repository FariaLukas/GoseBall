
public interface IListener
{
    void RegisterAsListener();
    void UnregisterAsListener();
    public void OnEventRaised(CustomEvent customEvent, object param);
}
