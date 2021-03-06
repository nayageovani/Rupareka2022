using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TechnomediaLabs
{
	public static class MyExtensions
	{
		/// <summary>
		/// Swap two elements in array
		/// </summary>
		public static void Swap<T>(this T[] array, int a, int b)
		{
			T x = array[a];
			array[a] = array[b];
			array[b] = x;
		}


		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			var toGet = gameObject.GetComponent<T>();
			if (toGet != null) return toGet;
			return gameObject.AddComponent<T>();
		}

		public static T GetOrAddComponent<T>(this Component component) where T : Component
		{
			var toGet = component.gameObject.GetComponent<T>();
			if (toGet != null) return toGet;
			return component.gameObject.AddComponent<T>();
		}

		public static bool HasComponent<T>(this GameObject gameObject) where T : Component
		{
			return gameObject.GetComponent<T>() != null;
		}
		
		
		/// <summary>
		/// Get all components of specified Layer in childs
		/// </summary>
		public static List<Transform> GetObjectsOfLayerInChilds(this GameObject gameObject, int layer)
		{
			List<Transform> list = new List<Transform>();
			CheckChildsOfLayer(gameObject.transform, layer, list);
			return list;
		}

		/// <summary>
		/// Get all components of specified Layer in childs
		/// </summary>
		public static List<Transform> GetObjectsOfLayerInChilds(this Component component, int layer)
		{
			return component.gameObject.GetObjectsOfLayerInChilds(layer);
		}

		private static void CheckChildsOfLayer(Transform transform, int layer, List<Transform> childsCache)
		{
			foreach (Transform t in transform)
			{
				CheckChildsOfLayer(t, layer, childsCache);

				if (t.gameObject.layer != layer) continue;
				childsCache.Add(t);
			}
		}


		/// <summary>
		/// Swap Rigidbody IsKinematic and DetectCollisions
		/// </summary>
		/// <param name="body"></param>
		/// <param name="state"></param>
		public static void SetBodyState(this Rigidbody body, bool state)
		{
			body.isKinematic = !state;
			body.detectCollisions = state;
		}
		
		
		/// <summary>
		/// Find all Components of specified interface
		/// </summary>
		public static I[] FindObjectsOfInterface<I>() where I : class
		{
			var monoBehaviours = Object.FindObjectsOfType<Transform>();

			return monoBehaviours.Select(behaviour => behaviour.GetComponent(typeof(I))).OfType<I>().ToArray();
		}

		/// <summary>
		/// Find all Components of specified interface along with Component itself
		/// </summary>
		public static ComponentOfInterface<I>[] FindObjectsOfInterfaceAsComponents<I>() where I : class
		{
			return Object.FindObjectsOfType<Component>()
				.Where(c => c is I)
				.Select(c => new ComponentOfInterface<I>(c, c as I)).ToArray();
		}

		public struct ComponentOfInterface<I>
		{
			public readonly Component Component;
			public readonly I Interface;

			public ComponentOfInterface(Component component, I @interface)
			{
				Component = component;
				Interface = @interface;
			}
		}
		

		#region One Per Instance

		/// <summary>
		/// Get components with unique Instance ID
		/// </summary>
		public static T[] OnePerInstance<T>(this T[] components) where T : Component
		{
			if (components == null || components.Length == 0) return null;
			return components.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToArray();
		}

		/// <summary>
		/// Get hits with unique owner Instance ID
		/// </summary>
		public static RaycastHit2D[] OneHitPerInstance(this RaycastHit2D[] hits)
		{
			if (hits == null || hits.Length == 0) return null;
			return hits.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToArray();
		}

		/// <summary>
		/// Get colliders with unique owner Instance ID
		/// </summary>
		public static Collider2D[] OneHitPerInstance(this Collider2D[] hits)
		{
			if (hits == null || hits.Length == 0) return null;
			return hits.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToArray();
		}

		/// <summary>
		/// Get colliders with unique owner Instance ID
		/// </summary>
		public static List<Collider2D> OneHitPerInstanceList(this Collider2D[] hits)
		{
			if (hits == null || hits.Length == 0) return null;
			return hits.GroupBy(h => h.transform.GetInstanceID()).Select(g => g.First()).ToList();
		}

		#endregion
	}
}