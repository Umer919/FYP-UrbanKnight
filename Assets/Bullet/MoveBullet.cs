using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public int _Speed;
    public bool isMove;


    #region UnityMethod

    private void OnEnable()
    {
        FncDestroy();
    }

    void Awake()
    {
        isMove = true;
    }

    void Update()
    {
        if (isMove)
            transform.position += transform.forward * Time.deltaTime * _Speed;
    }

    void FncDestroy()
    {
        Destroy(this.gameObject,1.0f);
    }
    #endregion

}
