using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public float CamDistance;
    public float moveSpeed = 1f;
    public float shakePower;
    public Vector3 offset;
    public CamShake _camShake;
    public Animator camAnimator;
    Vector3 targetPos;
    float viewBefore;
    float viewAfter;

    protected static CameraController _instance;
    public static CameraController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraController>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<CameraController>();
                }
            }
            return _instance;
        }
    }


    void Start()
    {
        //CamDistance = transform.position.z;
    }

    void FixedUpdate()
    {
        

        //targetPos = FlockManager.Instance.flockCenter;
        
        //transform.position = Vector3.Slerp(transform.position,
        //                                   new Vector3(targetPos.x + offset.x, targetPos.y + offset.y, CamDistance), 
        //                                   (moveSpeed + GameManager.Instance.IncreaseSpeed) * Time.deltaTime);
    }

    //通过CamShake脚本开放的启动函数开启震动
    public void CamShake()
    {
        _camShake.Shake(shakePower);
    }

    IEnumerator  CamZoomIncrease()
    {
        yield return null;
        viewAfter += 0.1F;
        Camera.main.fieldOfView = viewAfter;
        if (viewAfter - viewBefore >= 1f)
        {
            StopCoroutine(CamZoomIncrease());
        }
        else StartCoroutine(CamZoomIncrease());
        
    }

    IEnumerator CamZoomDecrease()
    {
        yield return null;
        viewAfter -= 0.1F;
        Camera.main.fieldOfView = viewAfter;
        if (viewBefore - viewAfter >= 1f)
        {
            StopCoroutine(CamZoomDecrease());
        }
        else StartCoroutine(CamZoomDecrease());
       
    }

    public void CamZoomIncreaseStart()
    {
        viewBefore = Camera.main.fieldOfView;
        viewAfter = viewBefore;
        StartCoroutine(CamZoomIncrease());
    }


    public void CamZoomDecreaseStart()
    {
        viewBefore = Camera.main.fieldOfView;
        viewAfter = viewBefore;
        StartCoroutine(CamZoomDecrease());
    }
}
