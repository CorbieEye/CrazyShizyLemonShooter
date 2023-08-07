using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Entity
{
    //
    private string _uid;
    public string Uid { get { return _uid; } }

    [Inject]
    public void Construct(IUniqueIdService uniqueIdService) {
        _uid = uniqueIdService.GetNewId();
    }
}
