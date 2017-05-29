using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        public static void Enable(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(true);
        }

        public static void Disable(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
        }

        public static void Enable(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public static void Disable(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public static GameObject Clone(this GameObject gameObject)
        {
            return GameObject.Instantiate(gameObject);
        }

        public static TComponent Clone<TComponent>(this GameObject gameObject)
        {
            return GameObject.Instantiate(gameObject).GetComponent<TComponent>();
        }
    }
}
