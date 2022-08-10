# Rush Hour

> Simple Unity rush hour puzzle game

## Development

_TODO_

### Assets

There are several assets that must be installed to work with the project!

- `External/`
  - ALINE 
  - Message Dispatched (ooti)
- `Plugins/`
  - Odin Inspector (Sirenix)

## Caveats

### Unity Serialization

Unity does not serialize public properties or private attributes by default, which can lead to some interesting/unexpected scenarios. Public attributes will be properly serialized and displayed in the editor.

Public properties can be displayed in the editor **but** will not be serialized. Instead, use a serialized private property along with a public accessor to both display and serialize the data.

Private fields can be serialized and displayed in the editor with the `[SerializeField]` attribute. However, without this attribute private fields will not be serialized (stored)! If wanting to serialize a private field **but** not display it, add both the `[SerializeField]` and `[HideInInspector]` attributes.

### Message Dispatcher

Due to the way Unity's "Fast Play" mode works, the "Domain Reload" option conflicts with the Message Dispatcher static fields. As [Unity Manual](https://docs.unity3d.com/Manual/DomainReloading.html) on this topic, some additional work is needed in the `MessageDispatcher` class. Without these changes (including Editor tags!), the `DontDestroyOnLoad` object used to track events will not be present!

```c#
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
static void Init()
{
    Debug.Log("Message Dispatcher - Init");

    sStub = (new GameObject("MessageDispatcherStub")).AddComponent<MessageDispatcherStub>();
    mMessages = new List<IMessage>();
    mMessageHandlers = new Dictionary<string, Dictionary<string, MessageHandler>>();
    mListenerAdds = new List<MessageListenerDefinition>();
    mListenerRemoves = new List<MessageListenerDefinition>();
}
```

