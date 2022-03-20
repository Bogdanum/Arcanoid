using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentSetOfPacksConfig", menuName = "Data/LevelPacks/Packs Map Configuration", order = 0)]
public class CurrentSetOfPacksConfig : ScriptableObject
{
     public GameMode gameMode;
     [SerializeField] private SetOfPacks currentSetOfPacks;
     
     public string StartPackID => currentSetOfPacks.Set[0].PackID;

     public Dictionary<string, LevelPack> GetPacks()
     {
          var packs = new Dictionary<string, LevelPack>();
          foreach (var levelPack in currentSetOfPacks.Set)
          {
               packs.Add(levelPack.PackID, levelPack);
          }
          return packs;
     }
     
     public enum GameMode
     {
          Normal,
          Debug
     }
}
