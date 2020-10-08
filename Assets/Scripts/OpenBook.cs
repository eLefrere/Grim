using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author : Veli-Matti Vuoti
/// This class contains functions to Open/Close book by adjusting blendshape variable from skinnedmeshrenderer and syncs it through network
/// </summary>
public class OpenBook : MonoBehaviour
{    
    IEnumerator enumerator;
    public float timeToOpen;
    public float timeToClose;
    public SkinnedMeshRenderer rend;
    public float currentBlendShapeVal;
    float previousBlendShapeVal = 0;

    public Material highlightMaterial;
    public Material originalMaterial;

    float t = 0;
    public float timeTick = 0.2f;

    bool attachedToHand = false;

    private void Awake()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
        currentBlendShapeVal = rend.GetBlendShapeWeight(0);
        previousBlendShapeVal = currentBlendShapeVal;
    }

    public bool IsPickedUp()
    {
        return attachedToHand;
    }

    public void SetHighlightMaterial()
    {
        rend.material = highlightMaterial;
    }

    public void SetOriginalMaterial()
    {
        rend.material = originalMaterial;
    }

    public void Open()
    {
        attachedToHand = true;
        enumerator = OpensBook(timeToOpen);
        StartCoroutine(enumerator);
    }

    public void Close()
    {
        attachedToHand = false;
        enumerator = ClosesBook(timeToClose);
        StartCoroutine(enumerator);
    }

    IEnumerator OpensBook(float time)
    {
        float elapsedTime = 0;
        float startPos = currentBlendShapeVal;

        while (elapsedTime < time)
        {
            currentBlendShapeVal = Mathf.Lerp(startPos, 100, elapsedTime / time);
            rend.SetBlendShapeWeight(0, currentBlendShapeVal);        
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ClosesBook(float time)
    {
        float elapsedTime = 0;
        float startPos = currentBlendShapeVal;

        while (elapsedTime < time)
        {
            currentBlendShapeVal = Mathf.Lerp(startPos, 0, elapsedTime / time);
            rend.SetBlendShapeWeight(0, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentBlendShapeVal = 0;
    }

}
