﻿using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BeatGamba.SlotMachine.SMLogic;

public class Slot : MonoBehaviour
{
    public int LastResult;
    private float speedMultiplier = 5f;
    public bool IsRolling { get; private set; } = false;
    public enum Result
    {
        Skull,
        Badcut,
        Miss,
        Perfect
    }
    private Transform _slotTransform = null!;
    
    public void Awake()
    {
        _slotTransform = GetComponent<Transform>();
    }

    public IEnumerator Roll(Result result)
    {
        IsRolling = true;
        LastResult = (int)result;
        yield return StartCoroutine("RollCoroutine");
        ShowResult(result);
        IsRolling = false;
    }
    
    IEnumerator RollCoroutine()
    {
        float totalRotation = 0;
        float targetRotation = Random.Range(700 * speedMultiplier, 800 * speedMultiplier);

        while (totalRotation < targetRotation)
        {
            float rotateAmount = 360 * Time.deltaTime * speedMultiplier;
            _slotTransform.Rotate(0, 0, rotateAmount);
            totalRotation += rotateAmount;
            yield return null;
        }
    }
    
    public void ShowResult(Result result)
    {
        switch (result)
        {
            case Result.Skull:
                _slotTransform.localRotation = Quaternion.Euler(0,90,-31);
                break;
            case Result.Perfect:
                _slotTransform.localRotation = Quaternion.Euler(0,90,-123);
                break;
            case Result.Miss:
                _slotTransform.localRotation = Quaternion.Euler(0,90,-213);
                break;
            case Result.Badcut:
                _slotTransform.localRotation = Quaternion.Euler(0,90,-300);
                break;
        }
    }
}
