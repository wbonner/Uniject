using System;
using System.Reflection;
using Uniject.Impl;
using UnityEngine;
using System.Collections;

namespace Uniject
{
	public class UnityBridgeComponent : MonoBehaviour, IUnityBridgeComponent
	{
		public void Awake()
		{
		}
		
		public void OnDestroy() {
//			this._gameObject
			_gameObject.Destroy();
		}
		
		public void Update() {
			_gameObject.Update();
		}
		
		public void OnGUI()
		{
			_gameObject.OnGUI();
		}
		
		public void CollisionEnter (ICollision collision)
		{
		}
		
		public void StartCoroutine (object _this, string coroutine, object[] args)
		{
			base.StartCoroutine("CoroutineBridge", new object[] { _this, coroutine, args });
		}
		
		void IComponent.StartCoroutine(string coroutine, params object[] args)
		{
			throw new NotImplementedException();
		}
		
		void IComponent.StartCoroutine (IEnumerator coroutine)
		{
			base.StartCoroutine("CoroutineBridgeEnumerator", coroutine);
		}
		
		void IComponent.StartCoroutine (string coroutine)
		{
			throw new NotImplementedException();
		}
		
		void IComponent.StopCoroutines()
		{
			base.StopAllCoroutines();
		}
		
		void IComponent.StopCoroutine(string coroutine)
		{
			base.StopCoroutine(coroutine);
		}
		
		public void CollisionEnter(UnityEngine.Collision c) {
			UnityBridgeComponent other = c.gameObject.GetComponent<UnityBridgeComponent>();
			if (null != other) {
//				Collision testableCollision = new Collision(c.relativeVelocity.ToUniject(),
//				                                            other.GameObject.Transform,
//				                                            other.GameObject,
//				                                            c.contacts);
//				_gameObject.CollisionEnter(c);
			}
		}
		
		public IEnumerator CoroutineBridge (object[] packedArgs)
		{
			var _this = packedArgs[0];
			var type = _this.GetType();
			var coroutineName = packedArgs[1] as string;
			var args = packedArgs[2] as object[];
			var method = type.GetMethod(coroutineName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new Exception(String.Format ("Method {0} not found on type {1}", coroutineName, type));
			}
			return method.Invoke(_this, args) as IEnumerator;
		}
		
		public IEnumerator CoroutineBridgeEnumerator (IEnumerator enumerator)
		{
			while (enumerator.MoveNext())
			{
				yield return enumerator.Current;
			}
		}
		
		private UnityGameObject _gameObject;
		public IGameObject GameObject {
			get { return _gameObject; }
			set { _gameObject = value as UnityGameObject; }
		}
	}
}

