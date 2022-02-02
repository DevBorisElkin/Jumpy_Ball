using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
//using System.Linq;

public class PhysicsEngine : MonoBehaviour
{
    public ReactiveProperty<float> gravityForce = new ReactiveProperty<float>();
    public float maxClampedFallSpeed = 9.81f;

    public bool checkForCollisions = true;

    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    List<PhysicsComponent> physicsComponents;
    List<WorldComponent> obstacles;
    
    public float maxCollisionCheckDistance = 25f;
    
    private void Start()
    {
        physicsComponents = new List<PhysicsComponent>();
        physicsComponents.AddRange(FindObjectsOfType<PhysicsComponent>());
        obstacles = new List<WorldComponent>();
        obstacles.AddRange(FindObjectsOfType<WorldComponent>());

        gravityForce.Subscribe(_ => NotifyPhysicsComponents()).AddTo(_compositeDisposable);
    }

    void NotifyPhysicsComponents()
    {
        foreach (var physComponent in physicsComponents)
            physComponent.ResetSupportsData();
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
            if(gravityForce.Value < 0 && !a.hasBottomSupport || gravityForce.Value > 0 && !a.hasUpperSupport)
                CalculateGravityFixedUpdate(a);
        }
    }

    void CalculateGravityFixedUpdate(PhysicsComponent physComp)
    {
        if (physComp.clampMaxFallingSpeed)
        {
            physComp.vecocity.y = Mathf.Clamp(physComp.vecocity.y + gravityForce.Value * Time.deltaTime,
                -Mathf.Abs(maxClampedFallSpeed), Mathf.Abs(maxClampedFallSpeed));
        }
        else physComp.vecocity.y += gravityForce.Value * Time.deltaTime;
        
        physComp.transform.Translate(physComp.vecocity * Time.deltaTime);
    }

    void CheckCollisionsForPhysicsComponents()
    {
        if (!checkForCollisions) return;
        //CheckCollisionsForPhysComponent();
        CheckCollisionsWithOtherPhysComp();
    }
    
    void CheckCollisionsWithOtherPhysComp()
    {
        foreach(PhysicsComponent physComp in physicsComponents)
        {
            foreach (WorldComponent a in GetClosestComponents(physComp, maxCollisionCheckDistance))
            {
                if (physComp == a) continue;
                if (WorldComponent.DoComponentsIntersect(physComp, a))
                {
                    if (gravityForce.Value < 0)
                    {
                        if(physComp.transform.position.y < a.transform.position.y) continue;
                        if (physComp.transform.position.y > a.transform.position.y)
                        {
                            physComp.Normalize(a);
                            break;
                        }
                    }
                    else if (gravityForce.Value > 0)
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
                physComp.hasUpperSupport = false;
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

    private void OnDestroy()
    {
        _compositeDisposable.Clear();
    }
}
