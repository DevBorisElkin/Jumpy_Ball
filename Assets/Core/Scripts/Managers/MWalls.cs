using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class MWalls : MonoBehaviour
{
    public float wallsMovementSpeed = -7f;
    public float outOfScreenDist = 25f;
    public Vector2 randomWallsDistance = new Vector2(2f, 6f);
    public Vector2 randomWallXScale = new Vector2(4f, 10f);

    private List<Wall> controlledWalls;

    private CompositeDisposable _disposables = new CompositeDisposable();
    private ReactiveCommand<Wall> WallDeactivated = new ReactiveCommand<Wall>();
    
    private void Start()
    {
        controlledWalls = FindObjectsOfType<Wall>().Where(a => a.isActiveAndEnabled).ToList();
        WallDeactivated.Subscribe(OnWallDeactivated).AddTo(_disposables);
    }
    
    private void FixedUpdate() => ControllWallsMovement();

    void ControllWallsMovement()
    {
        foreach (var a in controlledWalls)
        {
            Vector2 translation = new Vector2(wallsMovementSpeed * Time.deltaTime, 0);
            a.transform.Translate(translation);
            
            if (a.transform.position.x <= -outOfScreenDist)
            {
                a.gameObject.SetActive(false);
                OnWallDeactivated(a);
            }
        }
    }

    void OnWallDeactivated(Wall wall)
    {
        var furthestWall = controlledWalls
            .Where(a => a.upper == wall.upper)
            .OrderByDescending(a => a.transform.position.x)
            .First();

        float _randomDist = UnityEngine.Random.Range(randomWallsDistance.x, randomWallsDistance.y);
        float _randomWallXScale = UnityEngine.Random.Range(randomWallXScale.x, randomWallXScale.y);

        var tr = wall.transform;
        tr.localScale = new Vector2(_randomWallXScale, tr.localScale.y);
        
        tr.position = new Vector2
        (
            furthestWall.transform.position.x + furthestWall.transform.localScale.x / 2 + _randomDist + _randomWallXScale / 2,
            tr.position.y
        );
        wall.worldComponent.scale = tr.localScale;
        wall.gameObject.SetActive(true);
    }

    private void OnDestroy() =>  _disposables.Clear();
}
