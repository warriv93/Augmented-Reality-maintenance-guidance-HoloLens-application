using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Kallbert
{
    public static class ExtensionMethods
    {
        public static List<T> FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag) where T : Component
        {
            List<T> list = new List<T>();
            Transform t = parent.transform;
            foreach (Transform tr in t)
            {
                if (tr.tag == tag)
                {
                    list.Add(tr.GetComponent<T>());
                }
            }
            return list;
        }
    }
}
