using System.Collections;
using System.Collections.Generic;
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
        
        
        
        
        
        
    }
}