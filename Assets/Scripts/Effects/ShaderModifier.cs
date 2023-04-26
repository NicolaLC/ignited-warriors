using System.Collections;
using UnityEngine;

namespace Effects
{
    public abstract class ShaderModifier: MonoBehaviour
    {
        public MeshRenderer targetGraphics;

        public virtual void Apply()
        {
            StartCoroutine(_Apply());
        }
        protected virtual IEnumerator _Apply()
        {
            yield return null;
        }
    }
}