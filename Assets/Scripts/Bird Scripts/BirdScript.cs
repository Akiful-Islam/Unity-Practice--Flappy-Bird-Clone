using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    public static BirdScript instance;

    [SerializeField] private Rigidbody2D _birdBody;
    [SerializeField] private Animator _birdAnimator;
    private Button _flapButton;

    private float _forwardSpeed = 3f;
    private float _bounceSpeed = 4f;

    private bool _didFlap;

    public bool isDead;
    private void Awake()
    {
        MakeSingleton();
        _flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        _flapButton.onClick.AddListener(() => FlapBird());
        isDead = false;
        SetCameraX();
    }
    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void FixedUpdate()
    {
        BirdMovement();
    }

    private void BirdMovement()
    {
        if (!isDead)
        {
            Vector3 temp = transform.position;
            temp.x += _forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if (_didFlap)
            {
                _didFlap = false;
                _birdBody.velocity = new Vector2(0, _bounceSpeed);
                _birdAnimator.SetTrigger("Flap");
            }

            if (_birdBody.velocity.y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -_birdBody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
    private void SetCameraX()
    {
        CameraScript.offSetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }
    public void FlapBird()
    {
        _didFlap = true;
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }


}
