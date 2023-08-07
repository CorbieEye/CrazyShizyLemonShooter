using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablesResourceLoader : IResourceLoader
{
    #region GetPlayableCharacters
    public CharacterSO[] GetPlayableCharacters()
    {
        var loadHandle = Addressables.LoadAssetsAsync<CharacterSO>(
                new List<string>() { Constants.PLAYER_CHARACTER_ADDRESSABLE_LABEL },
                addressable =>
                {

                }, Addressables.MergeMode.Union, 
                false);
        var endedOperation = loadHandle.WaitForCompletion();

        CharacterSO[] characters = new CharacterSO[endedOperation.Count];
        for (int i = 0; i < endedOperation.Count; i++) {
            characters[i] = endedOperation[i];
        }

        return characters;
    }
    #endregion

    #region Load
    public T Load<T>(string name)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
