using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class PhysicsEngine : MonoBehaviour
{
    public float gravityForce = 9.81f;
    public float maxClampedFallSpeed = 9.81f;

    public bool checkForCollisions = true;

    List<PhysicsComponent> physicsComponents;
    List<WorldComponent> obstacles;
    
    public float maxCollisionCheckDistance = 5f;
    
    private void Start()
    {
        physicsComponents = new List<PhysicsComponent>();
        physicsComponents.AddRange(FindObjectsOfType<PhysicsComponent>());
        obstacles = new List<WorldComponent>();
        obstacles.AddRange(FindObjectsOfType<WorldComponent>());
    }

    private void FixedUpdate()
    {
        CheckGravityForComponents();
        CheckCollisionsForPhysicsComponents();
    }

    void CheckGravityForComponents()
    {
        foreach (PhysicsComponent a in physicsComponents) 
        {
            if(!a.hasBottomSupport)
                CalculateGravityFixedUpdate(a);
        }
    }

    void CalculateGravityFixedUpdate(PhysicsComponent physComp)
    {
        if (physComp.clampMaxFallingSpeed)
        {
            physComp.vecocity.y = Mathf.Clamp(physComp.vecocity.y + gravityForce * Time.deltaTime,
                -Mathf.Abs(maxClampedFallSpeed), Mathf.Abs(maxClampedFallSpeed));
        }
        else physComp.vecocity.y += gravityForce * Time.deltaTime;
        
        physComp.transform.Translate(physComp.vecocity * Time.deltaTime);
    }

    void CheckCollisionsForPhysicsComponents()
    {
        if (!checkForCollisions) return;
        //CheckCollisionsForPhysComponent();
        CheckCollisionsWithOtherPhysComp();
    }

    //void CheckCollisionsForPhysComponent()
    //{
    //    foreach (PhysicsComponent a in physicsComponents)
    //    {
    //        foreach (WorldComponent b in GetClosestComponents(a, maxCollisionCheckDistance))
    //        {
    //            if (WorldComponent.DoComponentsIntersect(a, b))
    //            {
    //                a.hasBottomSupport = true;
    //                a.vecocity = new Vector3(0, 0, 0);
    //                WorldComponent.FixYPosition(a, b);
    //                break;
    //            }
    //        }
    //        a.hasBottomSupport = false;
    //    }
    //    
    //}

    void CheckCollisionsWithOtherPhysComp()
    {
        foreach(PhysicsComponent physComp in physicsComponents)
        {
            foreach (WorldComponent a in GetClosestComponents(physComp, maxCollisionCheckDistance))
            {
                if (physComp == a) continue;
                if (WorldComponent.DoComponentsIntersect(physComp, a))
                {
                    if (gravityForce < 0)
                    {
                        if(physComp.transform.position.y < a.transform.position.y) continue;
                        
                        if (physComp.transform.position.y > a.transform.position.y)
                        {
                            physComp.Normalize(a);
                            break;
                        }
                    }
                    else if (gravityForce > 0)
                    {
                        if (physComp.transform.position.y > a.transform.position.y) continue;
                        
                        if (physComp.transform.position.y < a.transform.position.y)
                        {
                            physComp.Normalize(a);
                            break;
                        }
                    }
                }
                physComp.hasBottomSupport = false;
            }
        }
    }
    

    List<WorldComponent> GetClosestComponents(WorldComponent initial, float maxDistance)
    {
        List<WorldComponent> closestComponents = new List<WorldComponent>();
        foreach(WorldComponent a in obstacles)
        {
            if (a == initial) continue;

            if (Vector2.Distance(initial.transform.position, a.transform.position) > maxDistance) continue;
            //if ((initial.transform.position - a.transform.position).sqrMagnitude > maxDistance) continue;
            closestComponents.Add(a);
        }
        foreach(WorldComponent b in physicsComponents)
        {
            if (b == initial) continue;
            if (Vector2.Distance(initial.transform.position, b.transform.position) > maxDistance) continue;
            //if ((initial.transform.position - b.transform.position).sqrMagnitude > maxDistance) continue;
            closestComponents.Add(b);
        }
        return closestComponents;
    }


    float mapSizeX = 100;
    float mapSizeZ = 100;
    int checkBy = 10;
    void CheckMapByBatches()
    {
        for (int i = 0; i < checkBy; i++)
        {
            for (int j = 0; j < checkBy; j++)
            {

            }
        }
        // start check 10 times by 10x10


    }
}
