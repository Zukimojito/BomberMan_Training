using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using GameManagers;
using UnityEngine;
using Random = System.Random;


namespace Bomberboy.Map
{
    public class Map_Scripts : MonoBehaviour
    {
        
        [SerializeField]
        [OddRange(3, 30)]
        private int _width;
        public int width => _width;
        
        [SerializeField] 
        [OddRange(3, 30)] 
        private int _height;
        public int height => _height;

        [SerializeField] private GameObject _SolidWallBorner;
        [SerializeField] private GameObject _SolidWallCorner;
        [SerializeField] private GameObject _SolidWall;
        [SerializeField] private GameObject _floorPrefab;
        [SerializeField] private GameObject _BreakableWallPrefab;

        [SerializeField] 
        [Range(0.0f,1.0f)]
        private float _breakableWallProbability;

        public float BreakableWallProbability => _breakableWallProbability;
        

        private GameObject[ , ] _mapData;
        private Random _random;

        private void Awake()
        {
            _mapData = new GameObject[_width+2,_height+2];
            
            SetGOAtPos(-1,-1,Instantiate(_SolidWallCorner,Vector3.zero,Quaternion.Euler(0,0,0),transform));
            SetGOAtPos(-1,_height,Instantiate(_SolidWallCorner,Vector3.zero, Quaternion.Euler(0,90,0),transform));
            SetGOAtPos(_width,_height,Instantiate(_SolidWallCorner,Vector3.zero, Quaternion.Euler(0,180,0),transform));
            SetGOAtPos(_width,-1,Instantiate(_SolidWallCorner,Vector3.zero,Quaternion.Euler(0,270,0),transform));
            

            for (int x = 0; x < _width; x++)
            {
                SetGOAtPos(x,-1,Instantiate(_SolidWallBorner,Vector3.zero, Quaternion.Euler(0,0,0),transform));        //WALL BOT AXE x
                SetGOAtPos(x,_height,Instantiate(_SolidWallBorner,Vector3.zero,Quaternion.Euler(0,180,0),transform));    //WALL TOP AXE x
            }
            for (int y = 0; y < _height; y++)
            {
                SetGOAtPos(-1,y,Instantiate(_SolidWallBorner,Vector3.zero,Quaternion.Euler(0,90,0),transform));    //WALL LEFT AXE Y
                SetGOAtPos(_width,y,Instantiate(_SolidWallBorner,Vector3.zero, Quaternion.Euler(0,270,0),transform));    //WALL RIGHT AXE Y
            }

            _random = new Random((int)DateTime.Now.Ticks);
            
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if ((x & 1) == 1 && (y & 1) == 1)
                    {
                        SetGOAtPos(x,y,Instantiate(_SolidWall));
                        continue;
                    }
                    
                    if (Between(x, 0, 1) && Between(y, 0, 1) ||
                        Between(x, _width - 2, _width - 1) && Between(y, 0, 1) ||
                        Between(x, 0, 1) && Between(y, _height - 2, _height - 1) ||
                        Between(x, _width - 2, _width - 1) && Between(y, _height - 2, _height - 1))
                    {
                        SetGOAtPos(x, y, Instantiate(_floorPrefab, transform));
                        continue;
                    }
                    
                    if (_random.NextDouble() < _breakableWallProbability)
                    {
                        SetGOAtPos(x, y, Instantiate(_BreakableWallPrefab, transform));
                    }
                    else
                    {
                        SetGOAtPos(x, y, Instantiate(_floorPrefab, transform));
                    }
                    
                    
                }
            }
            
            
            
            
            
            
            
        }

        private GameObject GetGoAtPos(int x, int y)
        {
            HorsLimiteOrNot(x,y,true);
            return _mapData[x + 1, y + 1];
        }

        private void SetGOAtPos(int x, int y, GameObject Go)
        {
            HorsLimiteOrNot(x, y, true);
            GameObject currentGo = GetGoAtPos(x, y);
            if (!ReferenceEquals(currentGo, null))
            {
                Destroy(currentGo);
            }
            
            Go.transform.position = new Vector3(x,0,y);
            _mapData[x + 1, y + 1] = Go;
        }
        
        
        private void HorsLimiteOrNot(int x, int y, bool Limite)
        {
            if (Limite)
            {
                if (!Between(x, -1, _width) || !Between(y,-1,_height))
                {
                    throw new InvalidCastException("Error, The Limit of your range has been exceeded");
                }
            }
            else
            {
                if (!Between(x, _width, -1) || !Between(y, _height, -1))
                {
                    throw new InvalidCastException("Error, The Limit of your range has been exceeded");
                }
            }
            
        }
        
        public static bool Between(int value, int min, int max)
        {
            return value >= min && value <= max;
        }
        
        
    }
}