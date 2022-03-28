using System;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPlatformSettings", menuName = "GameObjectsConfiguration/PlayerPlatform/PlayerPlatformSettings")]
public class PlayerPlatformSettings : ScriptableObject
{
     [SerializeField] private float initialSize = 1;
     [SerializeField] private float initialSpeed = 10;
     [SerializeField, Range(0, 1), Tooltip("less is more accurate")]
     private float targetPositionAccuracy = 0.1f;

     [SerializeField, Space(10)] private BackToInitPositionSettings backToInitPositionSettings;

     public float InitialSize => initialSize;
     public float InitialSpeed => initialSpeed;
     public float TargetPositionAccuracy => targetPositionAccuracy;
     public BackToInitPositionSettings BackToInitPositionSettings => backToInitPositionSettings;
}

[Serializable] 
public struct BackToInitPositionSettings
{
     [Min(0.1f)] public float duration;
     public Ease ease;
}