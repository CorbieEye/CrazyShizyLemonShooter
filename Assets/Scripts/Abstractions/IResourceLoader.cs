using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceLoader
{
    //
    public T Load<T>(string name);
    public CharacterSO[] GetPlayableCharacters();
}
