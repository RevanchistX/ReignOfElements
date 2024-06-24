using System;
using UnityEngine;

namespace Spellcasting
{
    public class ColiderDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other);
        }
    }
}