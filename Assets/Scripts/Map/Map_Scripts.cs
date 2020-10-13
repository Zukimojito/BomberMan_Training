using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using GameManagers;
using UnityEngine;




namespace Bomberboy.Map
{
    public class Map_Scripts : MonoBehaviour
    {
        
        [SerializeField] [OddRange(3,30)] private int _width;
        public int width => _width;

        [SerializeField] [OddRange(3, 30)] private int _height;
        public int height => _height;

        [SerializeField] private GameObject _SolidWall;
        [SerializeField] private GameObject _SolidWallCorner;


        private GameObject[ , ] _MapData;

        private void Awake()
        {
            _MapData = new GameObject[_width+2,_height+2];
            
            SetGOAtPos(-1,-1,Instantiate(_SolidWallCorner,Vector3.zero,Quaternion.Euler(0,0,0),transform));
            SetGOAtPos(-1,_height,Instantiate(_SolidWallCorner,Vector3.zero, Quaternion.Euler(0,0,0),transform));
            SetGOAtPos(_width,-1,Instantiate(_SolidWallCorner,Vector3.zero,Quaternion.Euler(0,0,0),transform));
            SetGOAtPos(_width,_height,Instantiate(_SolidWallCorner,Vector3.zero, Quaternion.Euler(0,0,0),transform));
        }

        private GameObject GetGoAtPos(int x, int y)
        {
            return _MapData[x + 1, y + 1];
        }

        private void SetGOAtPos(int x, int y, GameObject Go)
        {
            GameObject currentGo = GetGoAtPos(x, y);
            if (!ReferenceEquals(currentGo, null))
            {
                Destroy(currentGo);
            }
            
            Go.transform.position = new Vector3(x,0,y);
            _MapData[x + 1, y + 1] = Go;
        }
        
        
        private void HorsLimiteOrNot(int x, int y, bool Limite)
        {
            if (Limite)
            {
                if (!between(x, -1, _width) || !between(y,-1,_height))
                {
                    throw new InvalidCastException("Error, The Limit of your range has been exceeded");
                }
            }
            else
            {
                if (!between(x, _width, -1) || !between(y, _height, -1))
                {
                    throw new InvalidCastException("Error, The Limit of your range has been exceeded");
                }
            }
            
        }
        
        public static bool between(int value, int min, int max)
        {
            return value >= min && value <= max;
        }
        
        
    }
}