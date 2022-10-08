using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour
{
    private GameObject[] _pipeHolders;
    private float _distance = 7.172364f;
    private float _lastPipeX;
    private float _pipeMin = -1.75f;
    private float _pipeMax = 2.5f;

    private void Awake()
    {
        InitiatePipeHolders();
    }

    private void InitiatePipeHolders()
    {
        _pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

        for (int i = 0; i < _pipeHolders.Length; i++)
        {
            Vector3 temp = _pipeHolders[i].transform.position;
            temp.y = UnityEngine.Random.Range(_pipeMin, _pipeMax);
            _pipeHolders[i].transform.position = temp;
        }

        _lastPipeX = _pipeHolders[0].transform.position.x;

        for (int i = 1; i < _pipeHolders.Length; i++)
        {
            if (_lastPipeX < _pipeHolders[i].transform.position.x)
            {
                _lastPipeX = _pipeHolders[i].transform.position.x;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PipeHolder")
        {
            Vector3 temp = other.transform.position;

            temp.x = _lastPipeX + _distance;
            temp.y = UnityEngine.Random.Range(_pipeMin, _pipeMax);

            other.transform.position = temp;

            _lastPipeX = temp.x;
        }
    }
}