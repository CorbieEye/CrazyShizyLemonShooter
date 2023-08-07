using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class BootstrapInstaller : MonoInstaller, IInitializable
{
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private Test _test;


    public override void InstallBindings()
    {
        AddDiContainer();
        AddUniqueIdService();
        AddResourceLoader();
        AddGameManager();
    }

    #region AddDiContainer
    private void AddDiContainer() {
        Container
            .BindInterfacesAndSelfTo<DiContainer>()
            .FromInstance(Container)
            .AsSingle()
            .NonLazy();

        
    }
    #endregion

    #region AddUniqueIdService
    private void AddUniqueIdService() {
        Container
            .Bind<IUniqueIdService>()
            .To<GuidIdService>()
            .AsTransient();
    }
    #endregion

    #region AddResourceLoader
    private void AddResourceLoader() {
        Container
            .Bind<IResourceLoader>()
            .To<AddressablesResourceLoader>()
            .AsSingle()
            .NonLazy();
    }
    #endregion

    #region AddGameManager
    private void AddGameManager() {
        Container
            .BindInterfacesAndSelfTo<GameManager>()
            .FromInstance(_gameManager)
            .AsSingle()
            .NonLazy();
        Container.BindInterfacesTo<SignalDeclarationAsyncInitializer>().AsSingle();
    }

    public void Initialize()
    {
        Container.Resolve<GameManager>().SetState(new MainMenuGameState(Container));
    }
    #endregion

}
