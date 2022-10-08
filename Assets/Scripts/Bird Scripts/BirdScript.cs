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
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _flapClip, _pointClip, _deathClip;
    private Button _flapButton;

    private float _forwardSpeed = 3f;
    private float _bounceSpeed = 4f;

    private bool _didFlap;

    [HideInInspector] public bool isDead;

    [HideInInspector] public int score;
    private void Awake()
    {
        MakeSingleton();
        InitializeButton();
        SetCameraX();
        isDead = false;
        score = 0;
    }

    private void FixedUpdate()
    {
        BirdMovement();
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

    private void InitializeButton()
    {
        _flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        _flapButton.onClick.AddListener(() => FlapBird());
    }


    private void SetCameraX()
    {
        CameraScript.offSetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
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
                _audioSource.PlayOneShot(_flapClip);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PipeHolder")
        {
            _audioSource.PlayOneShot(_pointClip);
            score++;
            GameplayController.instance.SetScore(score);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Pipe")
        {
            if (!isDead)
            {
                isDead = true;
                _birdAnimator.SetTrigger("Dead");
                _audioSource.PlayOneShot(_deathClip);
                GameplayController.instance.StopGameOnDeath(score);
            }
        }
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
