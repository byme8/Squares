using System.Collections;
using Coroutines;
using Coroutines.Abstractions;
using UnityEngine;

public static class Delay
{
    public static IEnumerator Create(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
