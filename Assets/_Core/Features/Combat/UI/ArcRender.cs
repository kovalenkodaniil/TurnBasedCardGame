using System;
using System.Collections.Generic;
using _Core.Features.MainMenu;
using UnityEngine;

namespace _Core.Features.Combat.UI
{
    public class ArcRender : MonoBehaviour
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private int pollSize = 50;
        [SerializeField] private float spacing = 50f;
        [SerializeField] private float arrowAngleAdjustment = 0;
        [SerializeField] private int dotsToSkip = 1;
        [SerializeField] private Transform poolContainer;
        
        private List<GameObject> dotPool = new List<GameObject>();
        private GameObject arrowInstance;
        private Vector3 arrowDirrection;

        public void Init()
        {
            if (arrowInstance != null) return;
            
            arrowInstance = Instantiate(arrowPrefab, poolContainer);
            arrowInstance.transform.localPosition = Vector3.zero;
            InitializeDotPoll(pollSize);
            
            poolContainer.gameObject.SetActive(false);
        }

        public void DrawArc(Vector3 startPosition)
        {
            if (!poolContainer.gameObject.activeSelf)
                EnableArc();
            
            Vector3 mousePos = GlobalCamera.Camera.ScreenToWorldPoint(Input.mousePosition);

            mousePos.z = 0;
            startPosition.z = 0;

            Vector3 startPos = startPosition;
            Vector3 midPoint = CalculateMidPoint(startPos, mousePos);

            UpdateArc(startPos, midPoint, mousePos);
            PositionAndRotateArrow(mousePos);
        }

        public void DisableArc()
        {
            poolContainer.gameObject.SetActive(false);
        }

        public void EnableArc()
        {
            poolContainer.gameObject.SetActive(true);
        }

        private void UpdateArc(Vector3 startPos, Vector3 midPoint, Vector3 endPos)
        {
            int numDots = Mathf.CeilToInt(Vector3.Distance(startPos, endPos) / spacing);

            for (int i = 0; i < numDots && i <dotPool.Count; i++)
            {
                float t = i / (float) numDots;
                t = Mathf.Clamp(t, 0f, 1f);

                Vector3 position = QuadraticBezierPoint(startPos, midPoint, endPos, t);

                if (i != numDots - dotsToSkip)
                {
                    dotPool[i].transform.position = position;
                    dotPool[i].SetActive(true);
                }

                if (i == numDots - (dotsToSkip + 1) && i - dotsToSkip + 1 >= 0)
                {
                    arrowDirrection = dotPool[i].transform.position;
                }
            }

            for (int i = numDots - dotsToSkip; i < dotPool.Count; i++)
            {
                if (i > 0)
                {
                    dotPool[i].SetActive(false);
                }
            }
        }

        private Vector3 CalculateMidPoint(Vector3 startPos, Vector3 endPos)
        {
            Vector3 midPoint = (startPos + endPos) / 2;
            float arcHeight = Vector3.Distance(startPos, endPos) / 90f;
            midPoint.y = arcHeight;
            midPoint.x = arcHeight;
            
            return midPoint;
        }
        
        private void PositionAndRotateArrow(Vector3 position)
        {
            arrowInstance.transform.position = position;
            Vector3 direction = arrowDirrection - position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += arrowAngleAdjustment;
            arrowInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        private Vector3 QuadraticBezierPoint(Vector3 startPos, Vector3 midPoint, Vector3 endPos, float t)
        {
            float u = 1 - t;
            float sqrT = t * t;
            float sqrU = u * u;

            Vector3 point = sqrU * startPos;
            point += 2 * u * t * midPoint;
            point += sqrT * endPos;
            return point;
        }

        private void InitializeDotPoll(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject dot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity, poolContainer);
                dot.SetActive(false);
                dotPool.Add(dot);
            }
        }
    }
}