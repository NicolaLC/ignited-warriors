using System.Collections;
using UnityEngine;

namespace Effects
{
    public class MatFinalPowerModifier: ShaderModifier
    {
        private float originalFinalPower;
        
        private static readonly int FinalPower = Shader.PropertyToID("_FinalPower");

        protected void Awake()
        {
            originalFinalPower = targetGraphics.material.GetFloat(FinalPower);
        }

        protected override IEnumerator _Apply()
        {
            float t = 0;
            while (t < 1f)
            {
                t += Time.deltaTime;
                float normalizedTime = t / 1f;

                // Change the material color over time
                targetGraphics.material.SetFloat(FinalPower, Mathf.Lerp(100f, originalFinalPower, normalizedTime));;

                yield return null;
            }

            // Reset the material color to its original value
            targetGraphics.material.SetFloat(FinalPower, originalFinalPower);
        }
    }
}