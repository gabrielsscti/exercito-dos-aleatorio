using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : Component
{
	protected static T _instance;
	protected bool _enabled;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<T>();
				DontDestroyOnLoad(_instance.gameObject);
				if (_instance == null)
				{
					GameObject obj = new GameObject();
					_instance = obj.AddComponent<T>();
				}
			}
			return _instance;
		}
	}

	protected virtual void Awake()
	{
		if (!Application.isPlaying)
		{
			return;
		}

		if (_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this as T;
			DontDestroyOnLoad(transform.gameObject);
			_enabled = true;
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if (this != _instance)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
