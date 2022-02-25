using UnityEngine;

public class TestButton : MonoBehaviour
{
     [SerializeField] private LanguagesEnums.Language language;

     public void ChangeLanguage()
     {
          LocalizationManager.Instance.SetCurrentLanguage(language);
     }
}
