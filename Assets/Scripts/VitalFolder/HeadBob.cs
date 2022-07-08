using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{

    public float BPM;
    void Start()
    {
        StartCoroutine(MoveHead());
    }

    private IEnumerator MoveHead()
    {
        transform.DOLocalMove(new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.01f, this.transform.localPosition.z), 60/BPM).SetLoops(-1, LoopType.Yoyo);
        yield return null;
    }
}
