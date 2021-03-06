using System;
using Uniject.Impl;
using Uniject;


//TODO:: re-add extensions for conversion and backing wrappers as needed
namespace Uniject.Util
{
	public static class ToUnijectExtensions
	{
		public static Vector3 ToUniject(this UnityEngine.Vector3 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}
		
		public static Quaternion ToUniject(this UnityEngine.Quaternion q)
		{
			return new Quaternion(q.x, q.y, q.z, q.w);
		}
		
		public static UnityTransform ToUniject(this UnityEngine.Transform t)
		{
			if (t == null)
			{
				return null;
			}
			return new UnityTransform(t.gameObject);
		}
		
//		public static IAccelerationEvent ToUniject(this UnityEngine.AccelerationEvent e)
//		{
//			return new UnityAccelerationEvent(e);
//		}
//		
//		public static ITouch ToUniject(this UnityEngine.Touch t)
//		{
//			return new UnityTouch(t);
//		}
//		
//		public static Color ToUniject(this UnityEngine.Color c)
//		{
//			return new Color(c.r, c.g, c.b, c.a);
//		}
//		
//		public static Vector2 ToUniject(this UnityEngine.Vector2 v)
//		{
//			return new Vector2(v.x, v.y);
//		}
		
//		public static ICollider ToUniject(this UnityEngine.Collider c)
//		{
//			return new UnityCollider(c);
//		}
		
//		public static IAudioClip ToUniject(this UnityEngine.AudioClip c)
//		{
//			return new UnityAudioClip(c);
//		}
//		
//		public static IMaterial ToUniject(this UnityEngine.Material m)
//		{
//			return new UnityMaterial(m);
//		}
//		
//		public static Resolution ToUniject(this UnityEngine.Resolution r)
//		{
//			return new Resolution(r.width, r.height, r.refreshRate);
//		}
		
		public static IGameObject ToUniject(this UnityEngine.GameObject g)
		{
			var bridge = g.GetComponent<UnityBridgeComponent>();
			if (bridge != null)
			{
				return bridge.GameObject;
			}
			return new UnityGameObject(g);
		}
	}
}