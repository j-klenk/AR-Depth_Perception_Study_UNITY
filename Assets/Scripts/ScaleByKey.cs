using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Allows an object to scale up or down in controlled steps using keyboard input.
/// Intended for controlled size manipulation during experiments/stimuli presentation.
/// </summary>
public class ScaleByKey : MonoBehaviour
{
    [Header("Scaling Parameters")]
    [Tooltip("How much to scale per step (e.g., 0.01 = ±1%)")]
    [SerializeField] float scalingFactor = 0.01f;

    [Tooltip("Key to increase scale")]
    [SerializeField] KeyCode increaseKey = KeyCode.UpArrow;

    [Tooltip("Key to decrease scale")]
    [SerializeField] KeyCode decreaseKey = KeyCode.DownArrow;

    [Tooltip("If true, scale changes are multiplicative (1% of current size). If false, additive.")]
    [SerializeField] bool useMultiplicativeScaling = true;

    [Tooltip("If true, forces uniform scaling on all axes.")]
    [SerializeField] bool lockUniformScale = true;

    void Update()
    {
        HandleScaling();
    }

    /// <summary>
    /// Reads key presses and decides whether to scale up or down.
    /// Uses GetKeyDown(), so scaling only happens once per key press (not continuous while held).
    /// </summary>
    void HandleScaling()
    {
        if (Input.GetKeyDown(increaseKey))
            AdjustScale(1);
        else if (Input.GetKeyDown(decreaseKey))
            AdjustScale(-1);
    }

    /// <summary>
    /// Applies the actual scale change to the object.
    /// direction should be +1 (up) or -1 (down).
    /// </summary>
    /// <param name="direction">+1 to increase scale, -1 to decrease scale.</param>
    void AdjustScale(int direction)
    {
        Vector3 newScale = transform.localScale;

        if (useMultiplicativeScaling)
        {
            float factor = 1f + (scalingFactor * direction);
            newScale *= factor;
        }
        else
        {
            newScale += Vector3.one * scalingFactor * direction;
        }

        if (lockUniformScale)
        {
            float uniform = newScale.x;
            newScale = new Vector3(uniform, uniform, uniform);
        }

        transform.localScale = newScale;

        Debug.Log($"[{Time.time:F2}s] Scale adjusted to {transform.localScale.x:F4}");
    }
}
