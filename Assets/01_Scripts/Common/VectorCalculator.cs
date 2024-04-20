using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calculator
{
    
    public class VectorCalculator
    {
        public static Vector2[] DirectionsFromCenter(int directionAmount)
        {
            float angleStep = 360f / directionAmount; // 각도 단계 계산
            Vector2[] result = new Vector2[directionAmount];
            for (int i = 0; i < directionAmount; i++)
            {
                // 각도 계산
                float angle = i * angleStep;
                float radians = angle * Mathf.Deg2Rad;
                result[i] = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            }

            return result;
        }

    }
}
