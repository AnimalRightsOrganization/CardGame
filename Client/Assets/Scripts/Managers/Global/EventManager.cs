using UnityEngine.Events;

public class MyEvent<T0> : UnityEvent<T0> { }
public class MyEvent<T0, T1> : UnityEvent<T0, T1> { }
public class MyEvent<T0, T1, T2> : UnityEvent<T0, T1, T2> { }

public class NetStateManager
{
    private static MyEvent<int> eventList = new MyEvent<int>();
    public static void RegisterEvent(UnityAction<int> action)
    {
        eventList.AddListener(action);
    }
    public static void UnRegisterEvent(UnityAction<int> action)
    {
        eventList.RemoveListener(action);
    }
    public static void Trigger(int peer)
    {
        eventList.Invoke(peer);
    }
}

public class NetPacketManager
{
    private static MyEvent<NetPacket> eventList = new MyEvent<NetPacket>();
    public static void RegisterEvent(UnityAction<NetPacket> action)
    {
        eventList.AddListener(action);
    }
    public static void UnRegisterEvent(UnityAction<NetPacket> action)
    {
        eventList.RemoveListener(action);
    }
    public static void Trigger(NetPacket peer)
    {
        eventList.Invoke(peer);
    }
}