using System.Collections.Generic;
using UnityEngine;

public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	[SerializeField, HideInInspector]
	private List<TKey> keyData = new List<TKey>();
	
	[SerializeField, HideInInspector]
	private List<TValue> valueData = new List<TValue>();

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
		this.Clear();
		for (int i = 0; i < keyData.Count && i < valueData.Count; i++)
		{
			this[keyData[i]] = valueData[i];
		}
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
		keyData.Clear();
		valueData.Clear();

		foreach (var item in this)
		{
			keyData.Add(item.Key);
			valueData.Add(item.Value);
		}
    }
}

