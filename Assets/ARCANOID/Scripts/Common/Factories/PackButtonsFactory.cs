using Zenject;

public class PackButtonsFactory : PlaceholderFactory<PackButton> { }

public class CustomPackButtonsFactory : IFactory<PackButton>
{
    private readonly DiContainer _container;
    private readonly DefaultPackButtonVisualParams _defaultParams;
    private readonly LevelsMapUIController _uiController;
    private readonly PackButton _prefab;
    
    public CustomPackButtonsFactory(DiContainer container, DefaultPackButtonVisualParams defaultParams, LevelsMapUIController uiController, PackButton prefab)
    {
        _container = container;
        _defaultParams = defaultParams;
        _uiController = uiController;
        _prefab = prefab;
    }   
    
    public PackButton Create()
    {
        var packButton = _container.InstantiatePrefabForComponent<PackButton>(_prefab);
        packButton.Init(_defaultParams, _uiController);
        return packButton;
    }
}