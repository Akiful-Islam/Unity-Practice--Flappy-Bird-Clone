using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour
{
    private GameObject[] _backgrounds;
    private GameObject[] _grounds;

    private float _lastBGX;
    private float _lastGroundX;

    private void Awake()
    {
        GetValues();
        ReassignX();

    }

    private void GetValues()
    {
        _backgrounds = GameObject.FindGameObjectsWithTag("Background");
        _grounds = GameObject.FindGameObjectsWithTag("Ground");

        _lastBGX = _backgrounds[0].transform.position.x;
        _lastGroundX = _grounds[0].transform.position.x;
    }

    private void ReassignX()
    {
        for (int i = 1; i < _backgrounds.Length; i++)
        {
            if (_lastBGX < _backgrounds[i].transform.position.x)
            {
                _lastBGX = _backgrounds[i].transform.position.x;
            }
        }

        for (int i = 1; i < _grounds.Length; i++)
        {
            if (_lastGroundX < _grounds[i].transform.position.x)
            {
                _lastGroundX = _grounds[i].transform.position.x;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Background")
        {
            _lastBGX = SwapAndGetLastX(other, _lastBGX);
        }
        else if (other.tag == "Ground")
        {
            _lastGroundX = SwapAndGetLastX(other, _lastGroundX);
        }
    }

    private float SwapAndGetLastX(Collider2D other, float lastX)
    {
        Vector3 temp = other.transform.position;
        float width = ((BoxCollider2D)other).size.x;

        temp.x = lastX + width;

        other.transform.position = temp;

        lastX = temp.x;

        return lastX;
    }
}
