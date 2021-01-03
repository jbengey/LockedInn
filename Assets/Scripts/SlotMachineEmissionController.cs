using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineEmissionController : MonoBehaviour
{
    Material myMaterial;

    private void Awake()
    {
        myMaterial = GetComponent<MeshRenderer>().material;
        StartCoroutine(ChangeEmission(0.0f, 0.5f, 4.0f));
        
    }


    //Function to smooth the change in emission
    private IEnumerator ChangeEmission(float v_start, float v_end, float duration)
    {
        // for some reason, the desired intensity value (set in the UI slider) needs to be modified slightly for proper internal consumption
        float adjustedIntensityEnd = v_end - (0.4169F);
        float adjustedIntensityStart = v_start - (0.4169F);
        Color color = myMaterial.GetColor("_Color");

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float weightedIntensity = Mathf.Lerp(adjustedIntensityStart, adjustedIntensityEnd, elapsed / duration);
            color *= Mathf.Pow(2.0F, weightedIntensity);
            myMaterial.SetColor("_EmissionColor", color);

            elapsed += Time.deltaTime;
            yield return null;
        }

        color *= Mathf.Pow(2.0F, adjustedIntensityEnd);
        myMaterial.SetColor("_EmissionColor", color);

    }





}
