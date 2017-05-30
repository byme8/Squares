using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Coroutines;
using Squares;
using Squares.Game;
using Tweens;
using UniRx;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public GridController GridController;
    public Transform PanelTransform;
    public GameObject SquareTemplate;
    private Vector3 Scale = new Vector3(50, 50, 1);
    private Queue<Transform> squaresTransforms = new Queue<Transform>();
    private WaitForSeconds wait = new WaitForSeconds(0.5f);

    private void Awake()
    {
        ColorsManager.Instance.NewColors.Subscribe(colors =>
        {
            this.NewColorsCoroutine(colors).StartCoroutine();
        });

        this.GridController.CellSelection.Subscribe(_ =>
        {
            if (!this.squaresTransforms.Any())
                return;

            this.squaresTransforms.Dequeue().Scale(Vector3.zero, 0.5f).StartCoroutine();
        });
    }

    private IEnumerator NewColorsCoroutine(IEnumerable<Color> colors)
    {
        if (colors == null)
            yield break;

        yield return this.wait;

        foreach (Transform square in this.PanelTransform)
            GameObject.Destroy(square.gameObject);

        var delay = 0.1f;
        foreach (var color in colors)
        {
            var square = this.SquareTemplate.Clone();
            square.transform.SetParent(this.PanelTransform);
            square.transform.localScale = Vector3.zero;
            square.transform.Scale(this.Scale, 1, delay, Curves.ElasticOut).StartCoroutine();
            square.GetComponent<MeshRenderer>().material.color = color;

            this.squaresTransforms.Enqueue(square.transform);

            delay += 0.1f;
        }
    }
}