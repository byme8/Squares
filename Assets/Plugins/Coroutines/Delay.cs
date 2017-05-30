using System.Collections;
using CoroutinesEx;
using CoroutinesEx.Abstractions;
using UnityEngine;

public static class Delay
{
    public static IEnumerator Create(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
