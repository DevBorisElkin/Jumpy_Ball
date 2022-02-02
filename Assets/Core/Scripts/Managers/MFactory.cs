using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MFactory : Singleton<MFactory>
    {
        private List<Wall> _wallsPool = new List<Wall>();

        public void InitializeManager()
        {
            
        }

        public Wall GetWall(Vector2 scale)
        {
            var wall =
                _wallsPool.FirstOrDefault(inactiveBullet => !inactiveBullet.gameObject.activeInHierarchy);

            if (wall != null)
            {
                wall.ConfigureWall(scale);
                return wall;
            }

            wall = CreateWall();
            wall.ConfigureWall(scale);
            return wall;
        }

        private Wall CreateWall()
        {
            var wall = Instantiate(MPrefabs.Instance.WallBrefab, transform);
            wall.gameObject.SetActive(false);
            _wallsPool.Add(wall);
            return wall;
        }
    }
