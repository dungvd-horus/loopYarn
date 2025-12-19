var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i996 = root || request.c( 'UnityEngine.JointSpring' )
  var i997 = data
  i996.spring = i997[0]
  i996.damper = i997[1]
  i996.targetPosition = i997[2]
  return i996
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i998 = root || request.c( 'UnityEngine.JointMotor' )
  var i999 = data
  i998.m_TargetVelocity = i999[0]
  i998.m_Force = i999[1]
  i998.m_FreeSpin = i999[2]
  return i998
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i1000 = root || request.c( 'UnityEngine.JointLimits' )
  var i1001 = data
  i1000.m_Min = i1001[0]
  i1000.m_Max = i1001[1]
  i1000.m_Bounciness = i1001[2]
  i1000.m_BounceMinVelocity = i1001[3]
  i1000.m_ContactDistance = i1001[4]
  i1000.minBounce = i1001[5]
  i1000.maxBounce = i1001[6]
  return i1000
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i1002 = root || request.c( 'UnityEngine.JointDrive' )
  var i1003 = data
  i1002.m_PositionSpring = i1003[0]
  i1002.m_PositionDamper = i1003[1]
  i1002.m_MaximumForce = i1003[2]
  i1002.m_UseAcceleration = i1003[3]
  return i1002
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i1004 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i1005 = data
  i1004.m_Spring = i1005[0]
  i1004.m_Damper = i1005[1]
  return i1004
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i1006 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i1007 = data
  i1006.m_Limit = i1007[0]
  i1006.m_Bounciness = i1007[1]
  i1006.m_ContactDistance = i1007[2]
  return i1006
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i1008 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i1009 = data
  i1008.m_ExtremumSlip = i1009[0]
  i1008.m_ExtremumValue = i1009[1]
  i1008.m_AsymptoteSlip = i1009[2]
  i1008.m_AsymptoteValue = i1009[3]
  i1008.m_Stiffness = i1009[4]
  return i1008
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i1010 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i1011 = data
  i1010.m_LowerAngle = i1011[0]
  i1010.m_UpperAngle = i1011[1]
  return i1010
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i1012 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i1013 = data
  i1012.m_MotorSpeed = i1013[0]
  i1012.m_MaximumMotorTorque = i1013[1]
  return i1012
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i1014 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i1015 = data
  i1014.m_DampingRatio = i1015[0]
  i1014.m_Frequency = i1015[1]
  i1014.m_Angle = i1015[2]
  return i1014
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i1016 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i1017 = data
  i1016.m_LowerTranslation = i1017[0]
  i1016.m_UpperTranslation = i1017[1]
  return i1016
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.RectTransform"] = function (request, data, root) {
  var i1018 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.RectTransform' )
  var i1019 = data
  i1018.pivot = new pc.Vec2( i1019[0], i1019[1] )
  i1018.anchorMin = new pc.Vec2( i1019[2], i1019[3] )
  i1018.anchorMax = new pc.Vec2( i1019[4], i1019[5] )
  i1018.sizeDelta = new pc.Vec2( i1019[6], i1019[7] )
  i1018.anchoredPosition3D = new pc.Vec3( i1019[8], i1019[9], i1019[10] )
  i1018.rotation = new pc.Quat(i1019[11], i1019[12], i1019[13], i1019[14])
  i1018.scale = new pc.Vec3( i1019[15], i1019[16], i1019[17] )
  return i1018
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Canvas"] = function (request, data, root) {
  var i1020 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Canvas' )
  var i1021 = data
  i1020.planeDistance = i1021[0]
  i1020.referencePixelsPerUnit = i1021[1]
  i1020.isFallbackOverlay = !!i1021[2]
  i1020.renderMode = i1021[3]
  i1020.renderOrder = i1021[4]
  i1020.sortingLayerName = i1021[5]
  i1020.sortingOrder = i1021[6]
  i1020.scaleFactor = i1021[7]
  request.r(i1021[8], i1021[9], 0, i1020, 'worldCamera')
  i1020.overrideSorting = !!i1021[10]
  i1020.pixelPerfect = !!i1021[11]
  i1020.targetDisplay = i1021[12]
  i1020.overridePixelPerfect = !!i1021[13]
  i1020.enabled = !!i1021[14]
  return i1020
}

Deserializers["UnityEngine.UI.CanvasScaler"] = function (request, data, root) {
  var i1022 = root || request.c( 'UnityEngine.UI.CanvasScaler' )
  var i1023 = data
  i1022.m_UiScaleMode = i1023[0]
  i1022.m_ReferencePixelsPerUnit = i1023[1]
  i1022.m_ScaleFactor = i1023[2]
  i1022.m_ReferenceResolution = new pc.Vec2( i1023[3], i1023[4] )
  i1022.m_ScreenMatchMode = i1023[5]
  i1022.m_MatchWidthOrHeight = i1023[6]
  i1022.m_PhysicalUnit = i1023[7]
  i1022.m_FallbackScreenDPI = i1023[8]
  i1022.m_DefaultSpriteDPI = i1023[9]
  i1022.m_DynamicPixelsPerUnit = i1023[10]
  i1022.m_PresetInfoIsWorld = !!i1023[11]
  return i1022
}

Deserializers["UnityEngine.UI.GraphicRaycaster"] = function (request, data, root) {
  var i1024 = root || request.c( 'UnityEngine.UI.GraphicRaycaster' )
  var i1025 = data
  i1024.m_IgnoreReversedGraphics = !!i1025[0]
  i1024.m_BlockingObjects = i1025[1]
  i1024.m_BlockingMask = UnityEngine.LayerMask.FromIntegerValue( i1025[2] )
  return i1024
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasGroup"] = function (request, data, root) {
  var i1026 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasGroup' )
  var i1027 = data
  i1026.m_Alpha = i1027[0]
  i1026.m_Interactable = !!i1027[1]
  i1026.m_BlocksRaycasts = !!i1027[2]
  i1026.m_IgnoreParentGroups = !!i1027[3]
  i1026.enabled = !!i1027[4]
  return i1026
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer"] = function (request, data, root) {
  var i1028 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer' )
  var i1029 = data
  i1028.cullTransparentMesh = !!i1029[0]
  return i1028
}

Deserializers["UnityEngine.UI.Image"] = function (request, data, root) {
  var i1030 = root || request.c( 'UnityEngine.UI.Image' )
  var i1031 = data
  request.r(i1031[0], i1031[1], 0, i1030, 'm_Sprite')
  i1030.m_Type = i1031[2]
  i1030.m_PreserveAspect = !!i1031[3]
  i1030.m_FillCenter = !!i1031[4]
  i1030.m_FillMethod = i1031[5]
  i1030.m_FillAmount = i1031[6]
  i1030.m_FillClockwise = !!i1031[7]
  i1030.m_FillOrigin = i1031[8]
  i1030.m_UseSpriteMesh = !!i1031[9]
  i1030.m_PixelsPerUnitMultiplier = i1031[10]
  request.r(i1031[11], i1031[12], 0, i1030, 'm_Material')
  i1030.m_Maskable = !!i1031[13]
  i1030.m_Color = new pc.Color(i1031[14], i1031[15], i1031[16], i1031[17])
  i1030.m_RaycastTarget = !!i1031[18]
  i1030.m_RaycastPadding = new pc.Vec4( i1031[19], i1031[20], i1031[21], i1031[22] )
  return i1030
}

Deserializers["UnityEngine.UI.Button"] = function (request, data, root) {
  var i1032 = root || request.c( 'UnityEngine.UI.Button' )
  var i1033 = data
  i1032.m_OnClick = request.d('UnityEngine.UI.Button+ButtonClickedEvent', i1033[0], i1032.m_OnClick)
  i1032.m_Navigation = request.d('UnityEngine.UI.Navigation', i1033[1], i1032.m_Navigation)
  i1032.m_Transition = i1033[2]
  i1032.m_Colors = request.d('UnityEngine.UI.ColorBlock', i1033[3], i1032.m_Colors)
  i1032.m_SpriteState = request.d('UnityEngine.UI.SpriteState', i1033[4], i1032.m_SpriteState)
  i1032.m_AnimationTriggers = request.d('UnityEngine.UI.AnimationTriggers', i1033[5], i1032.m_AnimationTriggers)
  i1032.m_Interactable = !!i1033[6]
  request.r(i1033[7], i1033[8], 0, i1032, 'm_TargetGraphic')
  return i1032
}

Deserializers["UnityEngine.UI.Button+ButtonClickedEvent"] = function (request, data, root) {
  var i1034 = root || request.c( 'UnityEngine.UI.Button+ButtonClickedEvent' )
  var i1035 = data
  i1034.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i1035[0], i1034.m_PersistentCalls)
  return i1034
}

Deserializers["UnityEngine.Events.PersistentCallGroup"] = function (request, data, root) {
  var i1036 = root || request.c( 'UnityEngine.Events.PersistentCallGroup' )
  var i1037 = data
  var i1039 = i1037[0]
  var i1038 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Events.PersistentCall')))
  for(var i = 0; i < i1039.length; i += 1) {
    i1038.add(request.d('UnityEngine.Events.PersistentCall', i1039[i + 0]));
  }
  i1036.m_Calls = i1038
  return i1036
}

Deserializers["UnityEngine.Events.PersistentCall"] = function (request, data, root) {
  var i1042 = root || request.c( 'UnityEngine.Events.PersistentCall' )
  var i1043 = data
  request.r(i1043[0], i1043[1], 0, i1042, 'm_Target')
  i1042.m_TargetAssemblyTypeName = i1043[2]
  i1042.m_MethodName = i1043[3]
  i1042.m_Mode = i1043[4]
  i1042.m_Arguments = request.d('UnityEngine.Events.ArgumentCache', i1043[5], i1042.m_Arguments)
  i1042.m_CallState = i1043[6]
  return i1042
}

Deserializers["UnityEngine.UI.Navigation"] = function (request, data, root) {
  var i1044 = root || request.c( 'UnityEngine.UI.Navigation' )
  var i1045 = data
  i1044.m_Mode = i1045[0]
  i1044.m_WrapAround = !!i1045[1]
  request.r(i1045[2], i1045[3], 0, i1044, 'm_SelectOnUp')
  request.r(i1045[4], i1045[5], 0, i1044, 'm_SelectOnDown')
  request.r(i1045[6], i1045[7], 0, i1044, 'm_SelectOnLeft')
  request.r(i1045[8], i1045[9], 0, i1044, 'm_SelectOnRight')
  return i1044
}

Deserializers["UnityEngine.UI.ColorBlock"] = function (request, data, root) {
  var i1046 = root || request.c( 'UnityEngine.UI.ColorBlock' )
  var i1047 = data
  i1046.m_NormalColor = new pc.Color(i1047[0], i1047[1], i1047[2], i1047[3])
  i1046.m_HighlightedColor = new pc.Color(i1047[4], i1047[5], i1047[6], i1047[7])
  i1046.m_PressedColor = new pc.Color(i1047[8], i1047[9], i1047[10], i1047[11])
  i1046.m_SelectedColor = new pc.Color(i1047[12], i1047[13], i1047[14], i1047[15])
  i1046.m_DisabledColor = new pc.Color(i1047[16], i1047[17], i1047[18], i1047[19])
  i1046.m_ColorMultiplier = i1047[20]
  i1046.m_FadeDuration = i1047[21]
  return i1046
}

Deserializers["UnityEngine.UI.SpriteState"] = function (request, data, root) {
  var i1048 = root || request.c( 'UnityEngine.UI.SpriteState' )
  var i1049 = data
  request.r(i1049[0], i1049[1], 0, i1048, 'm_HighlightedSprite')
  request.r(i1049[2], i1049[3], 0, i1048, 'm_PressedSprite')
  request.r(i1049[4], i1049[5], 0, i1048, 'm_SelectedSprite')
  request.r(i1049[6], i1049[7], 0, i1048, 'm_DisabledSprite')
  return i1048
}

Deserializers["UnityEngine.UI.AnimationTriggers"] = function (request, data, root) {
  var i1050 = root || request.c( 'UnityEngine.UI.AnimationTriggers' )
  var i1051 = data
  i1050.m_NormalTrigger = i1051[0]
  i1050.m_HighlightedTrigger = i1051[1]
  i1050.m_PressedTrigger = i1051[2]
  i1050.m_SelectedTrigger = i1051[3]
  i1050.m_DisabledTrigger = i1051[4]
  return i1050
}

Deserializers["SoundUIElement"] = function (request, data, root) {
  var i1052 = root || request.c( 'SoundUIElement' )
  var i1053 = data
  i1052.Sound = request.d('SoundDefine', i1053[0], i1052.Sound)
  i1052.PlayOnEnable = !!i1053[1]
  i1052.StopOnDisable = !!i1053[2]
  i1052.playWithInteractable = !!i1053[3]
  i1052.isPlayRandomBackGroundMusic = !!i1053[4]
  return i1052
}

Deserializers["SoundDefine"] = function (request, data, root) {
  var i1054 = root || request.c( 'SoundDefine' )
  var i1055 = data
  i1054.soundType = i1055[0]
  i1054.Loop = !!i1055[1]
  request.r(i1055[2], i1055[3], 0, i1054, 'Clip')
  var i1057 = i1055[4]
  var i1056 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.AudioClip')))
  for(var i = 0; i < i1057.length; i += 2) {
  request.r(i1057[i + 0], i1057[i + 1], 1, i1056, '')
  }
  i1054.ClipList = i1056
  return i1054
}

Deserializers["PlayNowButtonAnim"] = function (request, data, root) {
  var i1060 = root || request.c( 'PlayNowButtonAnim' )
  var i1061 = data
  request.r(i1061[0], i1061[1], 0, i1060, 'playerNowButton')
  i1060.maxScale = new pc.Vec3( i1061[2], i1061[3], i1061[4] )
  i1060.minScale = new pc.Vec3( i1061[5], i1061[6], i1061[7] )
  i1060.scaleDuration = i1061[8]
  i1060.m_autoStart = !!i1061[9]
  return i1060
}

Deserializers["UnityEngine.UI.Text"] = function (request, data, root) {
  var i1062 = root || request.c( 'UnityEngine.UI.Text' )
  var i1063 = data
  i1062.m_FontData = request.d('UnityEngine.UI.FontData', i1063[0], i1062.m_FontData)
  i1062.m_Text = i1063[1]
  request.r(i1063[2], i1063[3], 0, i1062, 'm_Material')
  i1062.m_Maskable = !!i1063[4]
  i1062.m_Color = new pc.Color(i1063[5], i1063[6], i1063[7], i1063[8])
  i1062.m_RaycastTarget = !!i1063[9]
  i1062.m_RaycastPadding = new pc.Vec4( i1063[10], i1063[11], i1063[12], i1063[13] )
  return i1062
}

Deserializers["UnityEngine.UI.FontData"] = function (request, data, root) {
  var i1064 = root || request.c( 'UnityEngine.UI.FontData' )
  var i1065 = data
  request.r(i1065[0], i1065[1], 0, i1064, 'm_Font')
  i1064.m_FontSize = i1065[2]
  i1064.m_FontStyle = i1065[3]
  i1064.m_BestFit = !!i1065[4]
  i1064.m_MinSize = i1065[5]
  i1064.m_MaxSize = i1065[6]
  i1064.m_Alignment = i1065[7]
  i1064.m_AlignByGeometry = !!i1065[8]
  i1064.m_RichText = !!i1065[9]
  i1064.m_HorizontalOverflow = i1065[10]
  i1064.m_VerticalOverflow = i1065[11]
  i1064.m_LineSpacing = i1065[12]
  return i1064
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i1066 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i1067 = data
  i1066.name = i1067[0]
  i1066.tagId = i1067[1]
  i1066.enabled = !!i1067[2]
  i1066.isStatic = !!i1067[3]
  i1066.layer = i1067[4]
  return i1066
}

Deserializers["UiEndGame"] = function (request, data, root) {
  var i1068 = root || request.c( 'UiEndGame' )
  var i1069 = data
  request.r(i1069[0], i1069[1], 0, i1068, 'm_endGameCanvas')
  request.r(i1069[2], i1069[3], 0, i1068, 'm_headerCanvas')
  request.r(i1069[4], i1069[5], 0, i1068, 'm_levelTransitionOverlay')
  request.r(i1069[6], i1069[7], 0, i1068, 'm_endText')
  request.r(i1069[8], i1069[9], 0, i1068, 'm_endText1')
  request.r(i1069[10], i1069[11], 0, i1068, 'm_buttonText')
  request.r(i1069[12], i1069[13], 0, i1068, 'm_buttonText1')
  var i1071 = i1069[14]
  var i1070 = []
  for(var i = 0; i < i1071.length; i += 2) {
  request.r(i1071[i + 0], i1071[i + 1], 2, i1070, '')
  }
  i1068.m_endGameSound = i1070
  request.r(i1069[15], i1069[16], 0, i1068, 'm_loadingText')
  return i1068
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Transform"] = function (request, data, root) {
  var i1074 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Transform' )
  var i1075 = data
  i1074.position = new pc.Vec3( i1075[0], i1075[1], i1075[2] )
  i1074.scale = new pc.Vec3( i1075[3], i1075[4], i1075[5] )
  i1074.rotation = new pc.Quat(i1075[6], i1075[7], i1075[8], i1075[9])
  return i1074
}

Deserializers["UnityEngine.EventSystems.EventSystem"] = function (request, data, root) {
  var i1076 = root || request.c( 'UnityEngine.EventSystems.EventSystem' )
  var i1077 = data
  request.r(i1077[0], i1077[1], 0, i1076, 'm_FirstSelected')
  i1076.m_sendNavigationEvents = !!i1077[2]
  i1076.m_DragThreshold = i1077[3]
  return i1076
}

Deserializers["UnityEngine.EventSystems.StandaloneInputModule"] = function (request, data, root) {
  var i1078 = root || request.c( 'UnityEngine.EventSystems.StandaloneInputModule' )
  var i1079 = data
  i1078.m_HorizontalAxis = i1079[0]
  i1078.m_VerticalAxis = i1079[1]
  i1078.m_SubmitButton = i1079[2]
  i1078.m_CancelButton = i1079[3]
  i1078.m_InputActionsPerSecond = i1079[4]
  i1078.m_RepeatDelay = i1079[5]
  i1078.m_ForceModuleActive = !!i1079[6]
  i1078.m_SendPointerHoverToParent = !!i1079[7]
  return i1078
}

Deserializers["TutorialLayer"] = function (request, data, root) {
  var i1080 = root || request.c( 'TutorialLayer' )
  var i1081 = data
  request.r(i1081[0], i1081[1], 0, i1080, 'handTrans')
  request.r(i1081[2], i1081[3], 0, i1080, 'handImage')
  var i1083 = i1081[4]
  var i1082 = []
  for(var i = 0; i < i1083.length; i += 2) {
  request.r(i1083[i + 0], i1083[i + 1], 2, i1082, '')
  }
  i1080.handSprites = i1082
  request.r(i1081[5], i1081[6], 0, i1080, 'tutorialText')
  request.r(i1081[7], i1081[8], 0, i1080, 'flareImage')
  request.r(i1081[9], i1081[10], 0, i1080, 'canvas')
  return i1080
}

Deserializers["TMPro.TextMeshProUGUI"] = function (request, data, root) {
  var i1086 = root || request.c( 'TMPro.TextMeshProUGUI' )
  var i1087 = data
  i1086.m_hasFontAssetChanged = !!i1087[0]
  request.r(i1087[1], i1087[2], 0, i1086, 'm_baseMaterial')
  i1086.m_maskOffset = new pc.Vec4( i1087[3], i1087[4], i1087[5], i1087[6] )
  i1086.m_text = i1087[7]
  i1086.m_isRightToLeft = !!i1087[8]
  request.r(i1087[9], i1087[10], 0, i1086, 'm_fontAsset')
  request.r(i1087[11], i1087[12], 0, i1086, 'm_sharedMaterial')
  var i1089 = i1087[13]
  var i1088 = []
  for(var i = 0; i < i1089.length; i += 2) {
  request.r(i1089[i + 0], i1089[i + 1], 2, i1088, '')
  }
  i1086.m_fontSharedMaterials = i1088
  request.r(i1087[14], i1087[15], 0, i1086, 'm_fontMaterial')
  var i1091 = i1087[16]
  var i1090 = []
  for(var i = 0; i < i1091.length; i += 2) {
  request.r(i1091[i + 0], i1091[i + 1], 2, i1090, '')
  }
  i1086.m_fontMaterials = i1090
  i1086.m_fontColor32 = UnityEngine.Color32.ConstructColor(i1087[17], i1087[18], i1087[19], i1087[20])
  i1086.m_fontColor = new pc.Color(i1087[21], i1087[22], i1087[23], i1087[24])
  i1086.m_enableVertexGradient = !!i1087[25]
  i1086.m_colorMode = i1087[26]
  i1086.m_fontColorGradient = request.d('TMPro.VertexGradient', i1087[27], i1086.m_fontColorGradient)
  request.r(i1087[28], i1087[29], 0, i1086, 'm_fontColorGradientPreset')
  request.r(i1087[30], i1087[31], 0, i1086, 'm_spriteAsset')
  i1086.m_tintAllSprites = !!i1087[32]
  request.r(i1087[33], i1087[34], 0, i1086, 'm_StyleSheet')
  i1086.m_TextStyleHashCode = i1087[35]
  i1086.m_overrideHtmlColors = !!i1087[36]
  i1086.m_faceColor = UnityEngine.Color32.ConstructColor(i1087[37], i1087[38], i1087[39], i1087[40])
  i1086.m_fontSize = i1087[41]
  i1086.m_fontSizeBase = i1087[42]
  i1086.m_fontWeight = i1087[43]
  i1086.m_enableAutoSizing = !!i1087[44]
  i1086.m_fontSizeMin = i1087[45]
  i1086.m_fontSizeMax = i1087[46]
  i1086.m_fontStyle = i1087[47]
  i1086.m_HorizontalAlignment = i1087[48]
  i1086.m_VerticalAlignment = i1087[49]
  i1086.m_textAlignment = i1087[50]
  i1086.m_characterSpacing = i1087[51]
  i1086.m_wordSpacing = i1087[52]
  i1086.m_lineSpacing = i1087[53]
  i1086.m_lineSpacingMax = i1087[54]
  i1086.m_paragraphSpacing = i1087[55]
  i1086.m_charWidthMaxAdj = i1087[56]
  i1086.m_enableWordWrapping = !!i1087[57]
  i1086.m_wordWrappingRatios = i1087[58]
  i1086.m_overflowMode = i1087[59]
  request.r(i1087[60], i1087[61], 0, i1086, 'm_linkedTextComponent')
  request.r(i1087[62], i1087[63], 0, i1086, 'parentLinkedComponent')
  i1086.m_enableKerning = !!i1087[64]
  i1086.m_enableExtraPadding = !!i1087[65]
  i1086.checkPaddingRequired = !!i1087[66]
  i1086.m_isRichText = !!i1087[67]
  i1086.m_parseCtrlCharacters = !!i1087[68]
  i1086.m_isOrthographic = !!i1087[69]
  i1086.m_isCullingEnabled = !!i1087[70]
  i1086.m_horizontalMapping = i1087[71]
  i1086.m_verticalMapping = i1087[72]
  i1086.m_uvLineOffset = i1087[73]
  i1086.m_geometrySortingOrder = i1087[74]
  i1086.m_IsTextObjectScaleStatic = !!i1087[75]
  i1086.m_VertexBufferAutoSizeReduction = !!i1087[76]
  i1086.m_useMaxVisibleDescender = !!i1087[77]
  i1086.m_pageToDisplay = i1087[78]
  i1086.m_margin = new pc.Vec4( i1087[79], i1087[80], i1087[81], i1087[82] )
  i1086.m_isUsingLegacyAnimationComponent = !!i1087[83]
  i1086.m_isVolumetricText = !!i1087[84]
  request.r(i1087[85], i1087[86], 0, i1086, 'm_Material')
  i1086.m_Maskable = !!i1087[87]
  i1086.m_Color = new pc.Color(i1087[88], i1087[89], i1087[90], i1087[91])
  i1086.m_RaycastTarget = !!i1087[92]
  i1086.m_RaycastPadding = new pc.Vec4( i1087[93], i1087[94], i1087[95], i1087[96] )
  return i1086
}

Deserializers["TMPro.VertexGradient"] = function (request, data, root) {
  var i1094 = root || request.c( 'TMPro.VertexGradient' )
  var i1095 = data
  i1094.topLeft = new pc.Color(i1095[0], i1095[1], i1095[2], i1095[3])
  i1094.topRight = new pc.Color(i1095[4], i1095[5], i1095[6], i1095[7])
  i1094.bottomLeft = new pc.Color(i1095[8], i1095[9], i1095[10], i1095[11])
  i1094.bottomRight = new pc.Color(i1095[12], i1095[13], i1095[14], i1095[15])
  return i1094
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i1096 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i1097 = data
  i1096.name = i1097[0]
  i1096.width = i1097[1]
  i1096.height = i1097[2]
  i1096.mipmapCount = i1097[3]
  i1096.anisoLevel = i1097[4]
  i1096.filterMode = i1097[5]
  i1096.hdr = !!i1097[6]
  i1096.format = i1097[7]
  i1096.wrapMode = i1097[8]
  i1096.alphaIsTransparency = !!i1097[9]
  i1096.alphaSource = i1097[10]
  i1096.graphicsFormat = i1097[11]
  i1096.sRGBTexture = !!i1097[12]
  i1096.desiredColorSpace = i1097[13]
  i1096.wrapU = i1097[14]
  i1096.wrapV = i1097[15]
  return i1096
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i1098 = root || new pc.UnityMaterial()
  var i1099 = data
  i1098.name = i1099[0]
  request.r(i1099[1], i1099[2], 0, i1098, 'shader')
  i1098.renderQueue = i1099[3]
  i1098.enableInstancing = !!i1099[4]
  var i1101 = i1099[5]
  var i1100 = []
  for(var i = 0; i < i1101.length; i += 1) {
    i1100.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i1101[i + 0]) );
  }
  i1098.floatParameters = i1100
  var i1103 = i1099[6]
  var i1102 = []
  for(var i = 0; i < i1103.length; i += 1) {
    i1102.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i1103[i + 0]) );
  }
  i1098.colorParameters = i1102
  var i1105 = i1099[7]
  var i1104 = []
  for(var i = 0; i < i1105.length; i += 1) {
    i1104.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i1105[i + 0]) );
  }
  i1098.vectorParameters = i1104
  var i1107 = i1099[8]
  var i1106 = []
  for(var i = 0; i < i1107.length; i += 1) {
    i1106.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i1107[i + 0]) );
  }
  i1098.textureParameters = i1106
  var i1109 = i1099[9]
  var i1108 = []
  for(var i = 0; i < i1109.length; i += 1) {
    i1108.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i1109[i + 0]) );
  }
  i1098.materialFlags = i1108
  return i1098
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i1112 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i1113 = data
  i1112.name = i1113[0]
  i1112.value = i1113[1]
  return i1112
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i1116 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i1117 = data
  i1116.name = i1117[0]
  i1116.value = new pc.Color(i1117[1], i1117[2], i1117[3], i1117[4])
  return i1116
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i1120 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i1121 = data
  i1120.name = i1121[0]
  i1120.value = new pc.Vec4( i1121[1], i1121[2], i1121[3], i1121[4] )
  return i1120
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i1124 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i1125 = data
  i1124.name = i1125[0]
  request.r(i1125[1], i1125[2], 0, i1124, 'value')
  return i1124
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i1128 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i1129 = data
  i1128.name = i1129[0]
  i1128.enabled = !!i1129[1]
  return i1128
}

Deserializers["CollectorGameManager"] = function (request, data, root) {
  var i1130 = root || request.c( 'CollectorGameManager' )
  var i1131 = data
  request.r(i1131[0], i1131[1], 0, i1130, 'queueManager')
  request.r(i1131[2], i1131[3], 0, i1130, 'moveLimiter')
  request.r(i1131[4], i1131[5], 0, i1130, 'inputManager')
  request.r(i1131[6], i1131[7], 0, i1130, 'gamePlaySound')
  request.r(i1131[8], i1131[9], 0, i1130, 'gameplayManager')
  i1130.distanceTf = i1131[10]
  i1130.pendingStartInterval = i1131[11]
  var i1133 = i1131[12]
  var i1132 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorController')))
  for(var i = 0; i < i1133.length; i += 2) {
  request.r(i1133[i + 0], i1133[i + 1], 1, i1132, '')
  }
  i1130.collectorOnDead = i1132
  i1130.countClickToMoveStore = i1131[13]
  i1130.canMoveStoreEqualCount = !!i1131[14]
  return i1130
}

Deserializers["GameplayManager"] = function (request, data, root) {
  var i1136 = root || request.c( 'GameplayManager' )
  var i1137 = data
  request.r(i1137[0], i1137[1], 0, i1136, 'levelGamePlayConfig')
  request.r(i1137[2], i1137[3], 0, i1136, 'levelSetup')
  request.r(i1137[4], i1137[5], 0, i1136, 'levelCollectorsSystem')
  request.r(i1137[6], i1137[7], 0, i1136, 'currentLevelConfig')
  i1136.levelId = i1137[8]
  i1136.enableStoreLimitForFirstLevel = !!i1137[9]
  return i1136
}

Deserializers["SoundManager"] = function (request, data, root) {
  var i1138 = root || request.c( 'SoundManager' )
  var i1139 = data
  request.r(i1139[0], i1139[1], 0, i1138, 'audioMixer')
  request.r(i1139[2], i1139[3], 0, i1138, 'fxMusicSource')
  request.r(i1139[4], i1139[5], 0, i1138, 'specialBgmSource')
  i1138.isPlayBgmOnStart = !!i1139[6]
  i1138.BGM = request.d('SoundDefine', i1139[7], i1138.BGM)
  return i1138
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.AudioSource"] = function (request, data, root) {
  var i1140 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.AudioSource' )
  var i1141 = data
  request.r(i1141[0], i1141[1], 0, i1140, 'clip')
  request.r(i1141[2], i1141[3], 0, i1140, 'outputAudioMixerGroup')
  i1140.playOnAwake = !!i1141[4]
  i1140.loop = !!i1141[5]
  i1140.time = i1141[6]
  i1140.volume = i1141[7]
  i1140.pitch = i1141[8]
  i1140.enabled = !!i1141[9]
  return i1140
}

Deserializers["InputManager"] = function (request, data, root) {
  var i1142 = root || request.c( 'InputManager' )
  var i1143 = data
  i1142.BlockInput = !!i1143[0]
  i1142.IsChoosingBlock = !!i1143[1]
  i1142.IsFreePicking = !!i1143[2]
  i1142.CubeLayermask = UnityEngine.LayerMask.FromIntegerValue( i1143[3] )
  i1142.clickCooldown = i1143[4]
  request.r(i1143[5], i1143[6], 0, i1142, 'gamePlayCamera')
  return i1142
}

Deserializers["CollectorMoveLimiter"] = function (request, data, root) {
  var i1144 = root || request.c( 'CollectorMoveLimiter' )
  var i1145 = data
  request.r(i1145[0], i1145[1], 0, i1144, 'padController')
  i1144.maxActiveMoving = i1145[2]
  request.r(i1145[3], i1145[4], 0, i1144, 'defaultPosition')
  request.r(i1145[5], i1145[6], 0, i1144, 'poolParent')
  request.r(i1145[7], i1145[8], 0, i1144, 'limiterText')
  i1144.padSpacing = i1145[9]
  return i1144
}

Deserializers["CollectorQueueManager"] = function (request, data, root) {
  var i1146 = root || request.c( 'CollectorQueueManager' )
  var i1147 = data
  var i1149 = i1147[0]
  var i1148 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorController')))
  for(var i = 0; i < i1149.length; i += 2) {
  request.r(i1149[i + 0], i1149[i + 1], 1, i1148, '')
  }
  i1146.deadQueue = i1148
  i1146.maxSlot = i1147[1]
  var i1151 = i1147[2]
  var i1150 = []
  for(var i = 0; i < i1151.length; i += 2) {
  request.r(i1151[i + 0], i1151[i + 1], 2, i1150, '')
  }
  i1146.queuePositions = i1150
  request.r(i1147[3], i1147[4], 0, i1146, 'defaultDeadPosition')
  i1146.deadOffset = new pc.Vec3( i1147[5], i1147[6], i1147[7] )
  var i1153 = i1147[8]
  var i1152 = []
  for(var i = 0; i < i1153.length; i += 2) {
  request.r(i1153[i + 0], i1153[i + 1], 2, i1152, '')
  }
  i1146.queueArray = i1152
  request.r(i1147[9], i1147[10], 0, i1146, 'alertFullSlotAnim')
  return i1146
}

Deserializers["AlertFullSlotAnim"] = function (request, data, root) {
  var i1158 = root || request.c( 'AlertFullSlotAnim' )
  var i1159 = data
  var i1161 = i1159[0]
  var i1160 = []
  for(var i = 0; i < i1161.length; i += 2) {
  request.r(i1161[i + 0], i1161[i + 1], 2, i1160, '')
  }
  i1158.alertImages = i1160
  i1158.animDuration = i1159[1]
  return i1158
}

Deserializers["Grid3DLayout"] = function (request, data, root) {
  var i1164 = root || request.c( 'Grid3DLayout' )
  var i1165 = data
  i1164.columns = i1165[0]
  i1164.spacing = i1165[1]
  return i1164
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshFilter"] = function (request, data, root) {
  var i1166 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshFilter' )
  var i1167 = data
  request.r(i1167[0], i1167[1], 0, i1166, 'sharedMesh')
  return i1166
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshRenderer"] = function (request, data, root) {
  var i1168 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshRenderer' )
  var i1169 = data
  request.r(i1169[0], i1169[1], 0, i1168, 'additionalVertexStreams')
  i1168.enabled = !!i1169[2]
  request.r(i1169[3], i1169[4], 0, i1168, 'sharedMaterial')
  var i1171 = i1169[5]
  var i1170 = []
  for(var i = 0; i < i1171.length; i += 2) {
  request.r(i1171[i + 0], i1171[i + 1], 2, i1170, '')
  }
  i1168.sharedMaterials = i1170
  i1168.receiveShadows = !!i1169[6]
  i1168.shadowCastingMode = i1169[7]
  i1168.sortingLayerID = i1169[8]
  i1168.sortingOrder = i1169[9]
  i1168.lightmapIndex = i1169[10]
  i1168.lightmapSceneIndex = i1169[11]
  i1168.lightmapScaleOffset = new pc.Vec4( i1169[12], i1169[13], i1169[14], i1169[15] )
  i1168.lightProbeUsage = i1169[16]
  i1168.reflectionProbeUsage = i1169[17]
  return i1168
}

Deserializers["MeshCombiner"] = function (request, data, root) {
  var i1172 = root || request.c( 'MeshCombiner' )
  var i1173 = data
  i1172.createMultiMaterialMesh = !!i1173[0]
  i1172.combineInactiveChildren = !!i1173[1]
  i1172.deactivateCombinedChildren = !!i1173[2]
  i1172.deactivateCombinedChildrenMeshRenderers = !!i1173[3]
  i1172.generateUVMap = !!i1173[4]
  i1172.destroyCombinedChildren = !!i1173[5]
  i1172.folderPath = i1173[6]
  var i1175 = i1173[7]
  var i1174 = []
  for(var i = 0; i < i1175.length; i += 2) {
  request.r(i1175[i + 0], i1175[i + 1], 2, i1174, '')
  }
  i1172.meshFiltersToSkip = i1174
  return i1172
}

Deserializers["GamePlaySound"] = function (request, data, root) {
  var i1178 = root || request.c( 'GamePlaySound' )
  var i1179 = data
  request.r(i1179[0], i1179[1], 0, i1178, 'clickCatSound')
  return i1178
}

Deserializers["PadController"] = function (request, data, root) {
  var i1180 = root || request.c( 'PadController' )
  var i1181 = data
  request.r(i1181[0], i1181[1], 0, i1180, 'meshTrans')
  request.r(i1181[2], i1181[3], 0, i1180, 'startTrans')
  request.r(i1181[4], i1181[5], 0, i1180, 'PadSmokeFX')
  i1180.HasCollectorOnPad = !!i1181[6]
  return i1180
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystem"] = function (request, data, root) {
  var i1182 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystem' )
  var i1183 = data
  i1182.main = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule', i1183[0], i1182.main)
  i1182.colorBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule', i1183[1], i1182.colorBySpeed)
  i1182.colorOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule', i1183[2], i1182.colorOverLifetime)
  i1182.emission = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule', i1183[3], i1182.emission)
  i1182.rotationBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule', i1183[4], i1182.rotationBySpeed)
  i1182.rotationOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule', i1183[5], i1182.rotationOverLifetime)
  i1182.shape = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule', i1183[6], i1182.shape)
  i1182.sizeBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule', i1183[7], i1182.sizeBySpeed)
  i1182.sizeOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule', i1183[8], i1182.sizeOverLifetime)
  i1182.textureSheetAnimation = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule', i1183[9], i1182.textureSheetAnimation)
  i1182.velocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule', i1183[10], i1182.velocityOverLifetime)
  i1182.noise = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule', i1183[11], i1182.noise)
  i1182.inheritVelocity = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule', i1183[12], i1182.inheritVelocity)
  i1182.forceOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule', i1183[13], i1182.forceOverLifetime)
  i1182.limitVelocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule', i1183[14], i1182.limitVelocityOverLifetime)
  i1182.useAutoRandomSeed = !!i1183[15]
  i1182.randomSeed = i1183[16]
  return i1182
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule"] = function (request, data, root) {
  var i1184 = root || new pc.ParticleSystemMain()
  var i1185 = data
  i1184.duration = i1185[0]
  i1184.loop = !!i1185[1]
  i1184.prewarm = !!i1185[2]
  i1184.startDelay = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[3], i1184.startDelay)
  i1184.startLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[4], i1184.startLifetime)
  i1184.startSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[5], i1184.startSpeed)
  i1184.startSize3D = !!i1185[6]
  i1184.startSizeX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[7], i1184.startSizeX)
  i1184.startSizeY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[8], i1184.startSizeY)
  i1184.startSizeZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[9], i1184.startSizeZ)
  i1184.startRotation3D = !!i1185[10]
  i1184.startRotationX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[11], i1184.startRotationX)
  i1184.startRotationY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[12], i1184.startRotationY)
  i1184.startRotationZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[13], i1184.startRotationZ)
  i1184.startColor = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i1185[14], i1184.startColor)
  i1184.gravityModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[15], i1184.gravityModifier)
  i1184.simulationSpace = i1185[16]
  request.r(i1185[17], i1185[18], 0, i1184, 'customSimulationSpace')
  i1184.simulationSpeed = i1185[19]
  i1184.useUnscaledTime = !!i1185[20]
  i1184.scalingMode = i1185[21]
  i1184.playOnAwake = !!i1185[22]
  i1184.maxParticles = i1185[23]
  i1184.emitterVelocityMode = i1185[24]
  i1184.stopAction = i1185[25]
  return i1184
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve"] = function (request, data, root) {
  var i1186 = root || new pc.MinMaxCurve()
  var i1187 = data
  i1186.mode = i1187[0]
  i1186.curveMin = new pc.AnimationCurve( { keys_flow: i1187[1] } )
  i1186.curveMax = new pc.AnimationCurve( { keys_flow: i1187[2] } )
  i1186.curveMultiplier = i1187[3]
  i1186.constantMin = i1187[4]
  i1186.constantMax = i1187[5]
  return i1186
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient"] = function (request, data, root) {
  var i1188 = root || new pc.MinMaxGradient()
  var i1189 = data
  i1188.mode = i1189[0]
  i1188.gradientMin = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i1189[1], i1188.gradientMin)
  i1188.gradientMax = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i1189[2], i1188.gradientMax)
  i1188.colorMin = new pc.Color(i1189[3], i1189[4], i1189[5], i1189[6])
  i1188.colorMax = new pc.Color(i1189[7], i1189[8], i1189[9], i1189[10])
  return i1188
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient"] = function (request, data, root) {
  var i1190 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient' )
  var i1191 = data
  i1190.mode = i1191[0]
  var i1193 = i1191[1]
  var i1192 = []
  for(var i = 0; i < i1193.length; i += 1) {
    i1192.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey', i1193[i + 0]) );
  }
  i1190.colorKeys = i1192
  var i1195 = i1191[2]
  var i1194 = []
  for(var i = 0; i < i1195.length; i += 1) {
    i1194.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey', i1195[i + 0]) );
  }
  i1190.alphaKeys = i1194
  return i1190
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule"] = function (request, data, root) {
  var i1196 = root || new pc.ParticleSystemColorBySpeed()
  var i1197 = data
  i1196.enabled = !!i1197[0]
  i1196.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i1197[1], i1196.color)
  i1196.range = new pc.Vec2( i1197[2], i1197[3] )
  return i1196
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey"] = function (request, data, root) {
  var i1200 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey' )
  var i1201 = data
  i1200.color = new pc.Color(i1201[0], i1201[1], i1201[2], i1201[3])
  i1200.time = i1201[4]
  return i1200
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey"] = function (request, data, root) {
  var i1204 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey' )
  var i1205 = data
  i1204.alpha = i1205[0]
  i1204.time = i1205[1]
  return i1204
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule"] = function (request, data, root) {
  var i1206 = root || new pc.ParticleSystemColorOverLifetime()
  var i1207 = data
  i1206.enabled = !!i1207[0]
  i1206.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i1207[1], i1206.color)
  return i1206
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule"] = function (request, data, root) {
  var i1208 = root || new pc.ParticleSystemEmitter()
  var i1209 = data
  i1208.enabled = !!i1209[0]
  i1208.rateOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1209[1], i1208.rateOverTime)
  i1208.rateOverDistance = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1209[2], i1208.rateOverDistance)
  var i1211 = i1209[3]
  var i1210 = []
  for(var i = 0; i < i1211.length; i += 1) {
    i1210.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst', i1211[i + 0]) );
  }
  i1208.bursts = i1210
  return i1208
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst"] = function (request, data, root) {
  var i1214 = root || new pc.ParticleSystemBurst()
  var i1215 = data
  i1214.count = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1215[0], i1214.count)
  i1214.cycleCount = i1215[1]
  i1214.minCount = i1215[2]
  i1214.maxCount = i1215[3]
  i1214.repeatInterval = i1215[4]
  i1214.time = i1215[5]
  return i1214
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule"] = function (request, data, root) {
  var i1216 = root || new pc.ParticleSystemRotationBySpeed()
  var i1217 = data
  i1216.enabled = !!i1217[0]
  i1216.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1217[1], i1216.x)
  i1216.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1217[2], i1216.y)
  i1216.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1217[3], i1216.z)
  i1216.separateAxes = !!i1217[4]
  i1216.range = new pc.Vec2( i1217[5], i1217[6] )
  return i1216
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule"] = function (request, data, root) {
  var i1218 = root || new pc.ParticleSystemRotationOverLifetime()
  var i1219 = data
  i1218.enabled = !!i1219[0]
  i1218.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1219[1], i1218.x)
  i1218.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1219[2], i1218.y)
  i1218.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1219[3], i1218.z)
  i1218.separateAxes = !!i1219[4]
  return i1218
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule"] = function (request, data, root) {
  var i1220 = root || new pc.ParticleSystemShape()
  var i1221 = data
  i1220.enabled = !!i1221[0]
  i1220.shapeType = i1221[1]
  i1220.randomDirectionAmount = i1221[2]
  i1220.sphericalDirectionAmount = i1221[3]
  i1220.randomPositionAmount = i1221[4]
  i1220.alignToDirection = !!i1221[5]
  i1220.radius = i1221[6]
  i1220.radiusMode = i1221[7]
  i1220.radiusSpread = i1221[8]
  i1220.radiusSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1221[9], i1220.radiusSpeed)
  i1220.radiusThickness = i1221[10]
  i1220.angle = i1221[11]
  i1220.length = i1221[12]
  i1220.boxThickness = new pc.Vec3( i1221[13], i1221[14], i1221[15] )
  i1220.meshShapeType = i1221[16]
  request.r(i1221[17], i1221[18], 0, i1220, 'mesh')
  request.r(i1221[19], i1221[20], 0, i1220, 'meshRenderer')
  request.r(i1221[21], i1221[22], 0, i1220, 'skinnedMeshRenderer')
  i1220.useMeshMaterialIndex = !!i1221[23]
  i1220.meshMaterialIndex = i1221[24]
  i1220.useMeshColors = !!i1221[25]
  i1220.normalOffset = i1221[26]
  i1220.arc = i1221[27]
  i1220.arcMode = i1221[28]
  i1220.arcSpread = i1221[29]
  i1220.arcSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1221[30], i1220.arcSpeed)
  i1220.donutRadius = i1221[31]
  i1220.position = new pc.Vec3( i1221[32], i1221[33], i1221[34] )
  i1220.rotation = new pc.Vec3( i1221[35], i1221[36], i1221[37] )
  i1220.scale = new pc.Vec3( i1221[38], i1221[39], i1221[40] )
  return i1220
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule"] = function (request, data, root) {
  var i1222 = root || new pc.ParticleSystemSizeBySpeed()
  var i1223 = data
  i1222.enabled = !!i1223[0]
  i1222.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1223[1], i1222.x)
  i1222.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1223[2], i1222.y)
  i1222.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1223[3], i1222.z)
  i1222.separateAxes = !!i1223[4]
  i1222.range = new pc.Vec2( i1223[5], i1223[6] )
  return i1222
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule"] = function (request, data, root) {
  var i1224 = root || new pc.ParticleSystemSizeOverLifetime()
  var i1225 = data
  i1224.enabled = !!i1225[0]
  i1224.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1225[1], i1224.x)
  i1224.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1225[2], i1224.y)
  i1224.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1225[3], i1224.z)
  i1224.separateAxes = !!i1225[4]
  return i1224
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule"] = function (request, data, root) {
  var i1226 = root || new pc.ParticleSystemTextureSheetAnimation()
  var i1227 = data
  i1226.enabled = !!i1227[0]
  i1226.mode = i1227[1]
  i1226.animation = i1227[2]
  i1226.numTilesX = i1227[3]
  i1226.numTilesY = i1227[4]
  i1226.useRandomRow = !!i1227[5]
  i1226.frameOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1227[6], i1226.frameOverTime)
  i1226.startFrame = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1227[7], i1226.startFrame)
  i1226.cycleCount = i1227[8]
  i1226.rowIndex = i1227[9]
  i1226.flipU = i1227[10]
  i1226.flipV = i1227[11]
  i1226.spriteCount = i1227[12]
  var i1229 = i1227[13]
  var i1228 = []
  for(var i = 0; i < i1229.length; i += 2) {
  request.r(i1229[i + 0], i1229[i + 1], 2, i1228, '')
  }
  i1226.sprites = i1228
  return i1226
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule"] = function (request, data, root) {
  var i1230 = root || new pc.ParticleSystemVelocityOverLifetime()
  var i1231 = data
  i1230.enabled = !!i1231[0]
  i1230.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[1], i1230.x)
  i1230.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[2], i1230.y)
  i1230.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[3], i1230.z)
  i1230.radial = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[4], i1230.radial)
  i1230.speedModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[5], i1230.speedModifier)
  i1230.space = i1231[6]
  i1230.orbitalX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[7], i1230.orbitalX)
  i1230.orbitalY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[8], i1230.orbitalY)
  i1230.orbitalZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[9], i1230.orbitalZ)
  i1230.orbitalOffsetX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[10], i1230.orbitalOffsetX)
  i1230.orbitalOffsetY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[11], i1230.orbitalOffsetY)
  i1230.orbitalOffsetZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1231[12], i1230.orbitalOffsetZ)
  return i1230
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule"] = function (request, data, root) {
  var i1232 = root || new pc.ParticleSystemNoise()
  var i1233 = data
  i1232.enabled = !!i1233[0]
  i1232.separateAxes = !!i1233[1]
  i1232.strengthX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1233[2], i1232.strengthX)
  i1232.strengthY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1233[3], i1232.strengthY)
  i1232.strengthZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1233[4], i1232.strengthZ)
  i1232.frequency = i1233[5]
  i1232.damping = !!i1233[6]
  i1232.octaveCount = i1233[7]
  i1232.octaveMultiplier = i1233[8]
  i1232.octaveScale = i1233[9]
  i1232.quality = i1233[10]
  i1232.scrollSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1233[11], i1232.scrollSpeed)
  i1232.scrollSpeedMultiplier = i1233[12]
  i1232.remapEnabled = !!i1233[13]
  i1232.remapX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1233[14], i1232.remapX)
  i1232.remapY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1233[15], i1232.remapY)
  i1232.remapZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1233[16], i1232.remapZ)
  i1232.positionAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1233[17], i1232.positionAmount)
  i1232.rotationAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1233[18], i1232.rotationAmount)
  i1232.sizeAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1233[19], i1232.sizeAmount)
  return i1232
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule"] = function (request, data, root) {
  var i1234 = root || new pc.ParticleSystemInheritVelocity()
  var i1235 = data
  i1234.enabled = !!i1235[0]
  i1234.mode = i1235[1]
  i1234.curve = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1235[2], i1234.curve)
  return i1234
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule"] = function (request, data, root) {
  var i1236 = root || new pc.ParticleSystemForceOverLifetime()
  var i1237 = data
  i1236.enabled = !!i1237[0]
  i1236.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1237[1], i1236.x)
  i1236.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1237[2], i1236.y)
  i1236.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1237[3], i1236.z)
  i1236.space = i1237[4]
  i1236.randomized = !!i1237[5]
  return i1236
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule"] = function (request, data, root) {
  var i1238 = root || new pc.ParticleSystemLimitVelocityOverLifetime()
  var i1239 = data
  i1238.enabled = !!i1239[0]
  i1238.limit = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1239[1], i1238.limit)
  i1238.limitX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1239[2], i1238.limitX)
  i1238.limitY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1239[3], i1238.limitY)
  i1238.limitZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1239[4], i1238.limitZ)
  i1238.dampen = i1239[5]
  i1238.separateAxes = !!i1239[6]
  i1238.space = i1239[7]
  i1238.drag = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1239[8], i1238.drag)
  i1238.multiplyDragByParticleSize = !!i1239[9]
  i1238.multiplyDragByParticleVelocity = !!i1239[10]
  return i1238
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer"] = function (request, data, root) {
  var i1240 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer' )
  var i1241 = data
  request.r(i1241[0], i1241[1], 0, i1240, 'mesh')
  i1240.meshCount = i1241[2]
  i1240.activeVertexStreamsCount = i1241[3]
  i1240.alignment = i1241[4]
  i1240.renderMode = i1241[5]
  i1240.sortMode = i1241[6]
  i1240.lengthScale = i1241[7]
  i1240.velocityScale = i1241[8]
  i1240.cameraVelocityScale = i1241[9]
  i1240.normalDirection = i1241[10]
  i1240.sortingFudge = i1241[11]
  i1240.minParticleSize = i1241[12]
  i1240.maxParticleSize = i1241[13]
  i1240.pivot = new pc.Vec3( i1241[14], i1241[15], i1241[16] )
  request.r(i1241[17], i1241[18], 0, i1240, 'trailMaterial')
  i1240.applyActiveColorSpace = !!i1241[19]
  i1240.enabled = !!i1241[20]
  request.r(i1241[21], i1241[22], 0, i1240, 'sharedMaterial')
  var i1243 = i1241[23]
  var i1242 = []
  for(var i = 0; i < i1243.length; i += 2) {
  request.r(i1243[i + 0], i1243[i + 1], 2, i1242, '')
  }
  i1240.sharedMaterials = i1242
  i1240.receiveShadows = !!i1241[24]
  i1240.shadowCastingMode = i1241[25]
  i1240.sortingLayerID = i1241[26]
  i1240.sortingOrder = i1241[27]
  i1240.lightmapIndex = i1241[28]
  i1240.lightmapSceneIndex = i1241[29]
  i1240.lightmapScaleOffset = new pc.Vec4( i1241[30], i1241[31], i1241[32], i1241[33] )
  i1240.lightProbeUsage = i1241[34]
  i1240.reflectionProbeUsage = i1241[35]
  return i1240
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer"] = function (request, data, root) {
  var i1244 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer' )
  var i1245 = data
  i1244.color = new pc.Color(i1245[0], i1245[1], i1245[2], i1245[3])
  request.r(i1245[4], i1245[5], 0, i1244, 'sprite')
  i1244.flipX = !!i1245[6]
  i1244.flipY = !!i1245[7]
  i1244.drawMode = i1245[8]
  i1244.size = new pc.Vec2( i1245[9], i1245[10] )
  i1244.tileMode = i1245[11]
  i1244.adaptiveModeThreshold = i1245[12]
  i1244.maskInteraction = i1245[13]
  i1244.spriteSortPoint = i1245[14]
  i1244.enabled = !!i1245[15]
  request.r(i1245[16], i1245[17], 0, i1244, 'sharedMaterial')
  var i1247 = i1245[18]
  var i1246 = []
  for(var i = 0; i < i1247.length; i += 2) {
  request.r(i1247[i + 0], i1247[i + 1], 2, i1246, '')
  }
  i1244.sharedMaterials = i1246
  i1244.receiveShadows = !!i1245[19]
  i1244.shadowCastingMode = i1245[20]
  i1244.sortingLayerID = i1245[21]
  i1244.sortingOrder = i1245[22]
  i1244.lightmapIndex = i1245[23]
  i1244.lightmapSceneIndex = i1245[24]
  i1244.lightmapScaleOffset = new pc.Vec4( i1245[25], i1245[26], i1245[27], i1245[28] )
  i1244.lightProbeUsage = i1245[29]
  i1244.reflectionProbeUsage = i1245[30]
  return i1244
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh"] = function (request, data, root) {
  var i1248 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh' )
  var i1249 = data
  i1248.name = i1249[0]
  i1248.halfPrecision = !!i1249[1]
  i1248.useSimplification = !!i1249[2]
  i1248.useUInt32IndexFormat = !!i1249[3]
  i1248.vertexCount = i1249[4]
  i1248.aabb = i1249[5]
  var i1251 = i1249[6]
  var i1250 = []
  for(var i = 0; i < i1251.length; i += 1) {
    i1250.push( !!i1251[i + 0] );
  }
  i1248.streams = i1250
  i1248.vertices = i1249[7]
  var i1253 = i1249[8]
  var i1252 = []
  for(var i = 0; i < i1253.length; i += 1) {
    i1252.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh', i1253[i + 0]) );
  }
  i1248.subMeshes = i1252
  var i1255 = i1249[9]
  var i1254 = []
  for(var i = 0; i < i1255.length; i += 16) {
    i1254.push( new pc.Mat4().setData(i1255[i + 0], i1255[i + 1], i1255[i + 2], i1255[i + 3],  i1255[i + 4], i1255[i + 5], i1255[i + 6], i1255[i + 7],  i1255[i + 8], i1255[i + 9], i1255[i + 10], i1255[i + 11],  i1255[i + 12], i1255[i + 13], i1255[i + 14], i1255[i + 15]) );
  }
  i1248.bindposes = i1254
  var i1257 = i1249[10]
  var i1256 = []
  for(var i = 0; i < i1257.length; i += 1) {
    i1256.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape', i1257[i + 0]) );
  }
  i1248.blendShapes = i1256
  return i1248
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh"] = function (request, data, root) {
  var i1262 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh' )
  var i1263 = data
  i1262.triangles = i1263[0]
  return i1262
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape"] = function (request, data, root) {
  var i1268 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape' )
  var i1269 = data
  i1268.name = i1269[0]
  var i1271 = i1269[1]
  var i1270 = []
  for(var i = 0; i < i1271.length; i += 1) {
    i1270.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame', i1271[i + 0]) );
  }
  i1268.frames = i1270
  return i1268
}

Deserializers["PaintingGridObject"] = function (request, data, root) {
  var i1272 = root || request.c( 'PaintingGridObject' )
  var i1273 = data
  request.r(i1273[0], i1273[1], 0, i1272, 'EffectHandler')
  request.r(i1273[2], i1273[3], 0, i1272, 'EffectOptions')
  request.r(i1273[4], i1273[5], 0, i1272, 'ColorPalette')
  i1272.gridSize = new pc.Vec2( i1273[6], i1273[7] )
  var i1275 = i1273[8]
  var i1274 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1275.length; i += 1) {
    i1274.add(request.d('PaintingPixel', i1275[i + 0]));
  }
  i1272.paintingPixels = i1274
  request.r(i1273[9], i1273[10], 0, i1272, 'GridTransform')
  var i1277 = i1273[11]
  var i1276 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i1277.length; i += 1) {
    i1276.add(i1277[i + 0]);
  }
  i1272.CurrentLevelColor = i1276
  var i1279 = i1273[12]
  var i1278 = new (System.Collections.Generic.List$1(Bridge.ns('PipeObject')))
  for(var i = 0; i < i1279.length; i += 2) {
  request.r(i1279[i + 0], i1279[i + 1], 1, i1278, '')
  }
  i1272.PipeObjects = i1278
  var i1281 = i1273[13]
  var i1280 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1281.length; i += 1) {
    i1280.add(request.d('PaintingPixel', i1281[i + 0]));
  }
  i1272.pipeObjectsPixels = i1280
  var i1283 = i1273[14]
  var i1282 = new (System.Collections.Generic.List$1(Bridge.ns('WallObject')))
  for(var i = 0; i < i1283.length; i += 2) {
  request.r(i1283[i + 0], i1283[i + 1], 1, i1282, '')
  }
  i1272.WallObjects = i1282
  var i1285 = i1273[15]
  var i1284 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1285.length; i += 1) {
    i1284.add(request.d('PaintingPixel', i1285[i + 0]));
  }
  i1272.wallObjectsPixels = i1284
  var i1287 = i1273[16]
  var i1286 = new (System.Collections.Generic.List$1(Bridge.ns('KeyObject')))
  for(var i = 0; i < i1287.length; i += 2) {
  request.r(i1287[i + 0], i1287[i + 1], 1, i1286, '')
  }
  i1272.KeyObjects = i1286
  var i1289 = i1273[17]
  var i1288 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1289.length; i += 1) {
    i1288.add(request.d('PaintingPixel', i1289[i + 0]));
  }
  i1272.keyObjectsPixels = i1288
  var i1291 = i1273[18]
  var i1290 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1291.length; i += 1) {
    i1290.add(request.d('PaintingPixel', i1291[i + 0]));
  }
  i1272.AdditionPaintingPixels = i1290
  i1272.pixelArrangeSpace = i1273[19]
  i1272.blockScale = new pc.Vec3( i1273[20], i1273[21], i1273[22] )
  request.r(i1273[23], i1273[24], 0, i1272, 'pixelPrefab')
  i1272.YOffset = i1273[25]
  i1272.colorVariationAmount = i1273[26]
  request.r(i1273[27], i1273[28], 0, i1272, 'PrefabSource')
  request.r(i1273[29], i1273[30], 0, i1272, 'basePixelSharedMaterial')
  request.r(i1273[31], i1273[32], 0, i1272, 'BlockFountainModule')
  var i1293 = i1273[33]
  var i1292 = new (System.Collections.Generic.List$1(Bridge.ns('BlockFountainObject')))
  for(var i = 0; i < i1293.length; i += 2) {
  request.r(i1293[i + 0], i1293[i + 1], 1, i1292, '')
  }
  i1272.BlockFountainObjects = i1292
  var i1295 = i1273[34]
  var i1294 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1295.length; i += 1) {
    i1294.add(request.d('PaintingPixel', i1295[i + 0]));
  }
  i1272.BlockFountainObjectsPixels = i1294
  i1272.PixelCount = i1273[35]
  i1272.PixelDestroyed = i1273[36]
  i1272.MinRow = i1273[37]
  i1272.MaxRow = i1273[38]
  i1272.MinColumn = i1273[39]
  i1272.MaxColumn = i1273[40]
  return i1272
}

Deserializers["PaintingPixel"] = function (request, data, root) {
  var i1298 = root || request.c( 'PaintingPixel' )
  var i1299 = data
  i1298.name = i1299[0]
  i1298.column = i1299[1]
  i1298.row = i1299[2]
  i1298.color = new pc.Color(i1299[3], i1299[4], i1299[5], i1299[6])
  i1298.colorCode = i1299[7]
  i1298.worldPos = new pc.Vec3( i1299[8], i1299[9], i1299[10] )
  i1298.Hearts = i1299[11]
  i1298.destroyed = !!i1299[12]
  request.r(i1299[13], i1299[14], 0, i1298, 'pixelObject')
  request.r(i1299[15], i1299[16], 0, i1298, 'PixelComponent')
  i1298.Hidden = !!i1299[17]
  i1298.VunableToAll = !!i1299[18]
  i1298.Indestructible = !!i1299[19]
  i1298.IsPipePixel = !!i1299[20]
  i1298.IsWallPixel = !!i1299[21]
  i1298.IsFountainObjectPixel = !!i1299[22]
  i1298.UsePaletteMaterialColor = !!i1299[23]
  request.r(i1299[24], i1299[25], 0, i1298, 'Material')
  return i1298
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.BoxCollider"] = function (request, data, root) {
  var i1310 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.BoxCollider' )
  var i1311 = data
  i1310.center = new pc.Vec3( i1311[0], i1311[1], i1311[2] )
  i1310.size = new pc.Vec3( i1311[3], i1311[4], i1311[5] )
  i1310.enabled = !!i1311[6]
  i1310.isTrigger = !!i1311[7]
  request.r(i1311[8], i1311[9], 0, i1310, 'material')
  return i1310
}

Deserializers["PaintingPixelComponent"] = function (request, data, root) {
  var i1312 = root || request.c( 'PaintingPixelComponent' )
  var i1313 = data
  request.r(i1313[0], i1313[1], 0, i1312, 'CubeTransform')
  i1312.PixelData = request.d('PaintingPixel', i1313[2], i1312.PixelData)
  request.r(i1313[3], i1313[4], 0, i1312, 'CubeRenderer')
  i1312.CurrentHearts = i1313[5]
  request.r(i1313[6], i1313[7], 0, i1312, 'EffectOptions')
  i1312.shakeLoops = i1313[8]
  i1312.shakeDuration = i1313[9]
  i1312.UseLazyTweens = !!i1313[10]
  i1312.initScale = new pc.Vec3( i1313[11], i1313[12], i1313[13] )
  return i1312
}

Deserializers["PipeObject"] = function (request, data, root) {
  var i1314 = root || request.c( 'PipeObject' )
  var i1315 = data
  request.r(i1315[0], i1315[1], 0, i1314, 'VisualHandler')
  var i1317 = i1315[2]
  var i1316 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1317.length; i += 1) {
    i1316.add(request.d('PaintingPixel', i1317[i + 0]));
  }
  i1314.PaintingPixelsCovered = i1316
  i1314.PixelDestroyed = i1315[3]
  request.r(i1315[4], i1315[5], 0, i1314, 'HeartText')
  i1314.Hearts = i1315[6]
  i1314.ColorCode = i1315[7]
  i1314.IsHorizontal = !!i1315[8]
  request.r(i1315[9], i1315[10], 0, i1314, 'PipeTransform')
  request.r(i1315[11], i1315[12], 0, i1314, 'PipeBodyTransform')
  i1314.PipeHeadDefaultScale = new pc.Vec3( i1315[13], i1315[14], i1315[15] )
  i1314.PipeBodyDefaultScale = new pc.Vec3( i1315[16], i1315[17], i1315[18] )
  i1314.Destroyed = !!i1315[19]
  i1314.RemainingHearts = i1315[20]
  i1314.HeartLoss = i1315[21]
  i1314.WorldPos = new pc.Vec3( i1315[22], i1315[23], i1315[24] )
  return i1314
}

Deserializers["PipePartVisualHandle"] = function (request, data, root) {
  var i1318 = root || request.c( 'PipePartVisualHandle' )
  var i1319 = data
  request.r(i1319[0], i1319[1], 0, i1318, 'TextureScaler')
  var i1321 = i1319[2]
  var i1320 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Renderer')))
  for(var i = 0; i < i1321.length; i += 2) {
  request.r(i1321[i + 0], i1321[i + 1], 1, i1320, '')
  }
  i1318.pipeRenderers = i1320
  request.r(i1319[3], i1319[4], 0, i1318, 'PipeScaleBody')
  i1318.BrightnessRange = new pc.Vec2( i1319[5], i1319[6] )
  i1318.FlashDuration = i1319[7]
  return i1318
}

Deserializers["AutoTextureScale"] = function (request, data, root) {
  var i1324 = root || request.c( 'AutoTextureScale' )
  var i1325 = data
  i1324.Active = !!i1325[0]
  i1324.referenceScale = new pc.Vec3( i1325[1], i1325[2], i1325[3] )
  i1324.referenceTiling = new pc.Vec2( i1325[4], i1325[5] )
  request.r(i1325[6], i1325[7], 0, i1324, 'rend')
  i1324.updateEveryNFrames = i1325[8]
  return i1324
}

Deserializers["KeyObject"] = function (request, data, root) {
  var i1326 = root || request.c( 'KeyObject' )
  var i1327 = data
  request.r(i1327[0], i1327[1], 0, i1326, 'KeyTransform')
  var i1329 = i1327[2]
  var i1328 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1329.length; i += 1) {
    i1328.add(request.d('PaintingPixel', i1329[i + 0]));
  }
  i1326.PaintingPixelsCovered = i1328
  var i1331 = i1327[3]
  var i1330 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Vector2')))
  for(var i = 0; i < i1331.length; i += 2) {
    i1330.add(new pc.Vec2( i1331[i + 0], i1331[i + 1] ));
  }
  i1326.BorderPixels = i1330
  request.r(i1327[4], i1327[5], 0, i1326, 'KeyRenderer')
  request.r(i1327[6], i1327[7], 0, i1326, 'Rotating')
  request.r(i1327[8], i1327[9], 0, i1326, 'IdleMoving')
  request.r(i1327[10], i1327[11], 0, i1326, 'CollectedFX')
  request.r(i1327[12], i1327[13], 0, i1326, 'TwinkleFX')
  request.r(i1327[14], i1327[15], 0, i1326, 'KeyAudioSource')
  request.r(i1327[16], i1327[17], 0, i1326, 'CollectedSoundFX')
  request.r(i1327[18], i1327[19], 0, i1326, 'KeyFlySoundFX')
  i1326.CurrentState = i1327[20]
  return i1326
}

Deserializers["IdleRotate"] = function (request, data, root) {
  var i1334 = root || request.c( 'IdleRotate' )
  var i1335 = data
  i1334.Active = !!i1335[0]
  request.r(i1335[1], i1335[2], 0, i1334, 'target')
  i1334.rotationSpeed = new pc.Vec3( i1335[3], i1335[4], i1335[5] )
  i1334.frameInterval = i1335[6]
  return i1334
}

Deserializers["IdleMoveUpDown"] = function (request, data, root) {
  var i1336 = root || request.c( 'IdleMoveUpDown' )
  var i1337 = data
  request.r(i1337[0], i1337[1], 0, i1336, 'target')
  i1336.moveAmount = i1337[2]
  i1336.duration = i1337[3]
  i1336.ease = i1337[4]
  return i1336
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.TrailRenderer"] = function (request, data, root) {
  var i1338 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.TrailRenderer' )
  var i1339 = data
  var i1341 = i1339[0]
  var i1340 = []
  for(var i = 0; i < i1341.length; i += 3) {
    i1340.push( new pc.Vec3( i1341[i + 0], i1341[i + 1], i1341[i + 2] ) );
  }
  i1338.positions = i1340
  i1338.positionCount = i1339[1]
  i1338.time = i1339[2]
  i1338.startWidth = i1339[3]
  i1338.endWidth = i1339[4]
  i1338.widthMultiplier = i1339[5]
  i1338.autodestruct = !!i1339[6]
  i1338.emitting = !!i1339[7]
  i1338.numCornerVertices = i1339[8]
  i1338.numCapVertices = i1339[9]
  i1338.minVertexDistance = i1339[10]
  i1338.colorGradient = i1339[11] ? new pc.ColorGradient(i1339[11][0], i1339[11][1], i1339[11][2]) : null
  i1338.startColor = new pc.Color(i1339[12], i1339[13], i1339[14], i1339[15])
  i1338.endColor = new pc.Color(i1339[16], i1339[17], i1339[18], i1339[19])
  i1338.generateLightingData = !!i1339[20]
  i1338.textureMode = i1339[21]
  i1338.alignment = i1339[22]
  i1338.widthCurve = new pc.AnimationCurve( { keys_flow: i1339[23] } )
  i1338.enabled = !!i1339[24]
  request.r(i1339[25], i1339[26], 0, i1338, 'sharedMaterial')
  var i1343 = i1339[27]
  var i1342 = []
  for(var i = 0; i < i1343.length; i += 2) {
  request.r(i1343[i + 0], i1343[i + 1], 2, i1342, '')
  }
  i1338.sharedMaterials = i1342
  i1338.receiveShadows = !!i1339[28]
  i1338.shadowCastingMode = i1339[29]
  i1338.sortingLayerID = i1339[30]
  i1338.sortingOrder = i1339[31]
  i1338.lightmapIndex = i1339[32]
  i1338.lightmapSceneIndex = i1339[33]
  i1338.lightmapScaleOffset = new pc.Vec4( i1339[34], i1339[35], i1339[36], i1339[37] )
  i1338.lightProbeUsage = i1339[38]
  i1338.reflectionProbeUsage = i1339[39]
  return i1338
}

Deserializers["LockObject"] = function (request, data, root) {
  var i1346 = root || request.c( 'LockObject' )
  var i1347 = data
  i1346.Row = i1347[0]
  i1346.IsUnlocked = !!i1347[1]
  i1346.ID = i1347[2]
  request.r(i1347[3], i1347[4], 0, i1346, 'collectorController')
  return i1346
}

Deserializers["CollectorController"] = function (request, data, root) {
  var i1348 = root || request.c( 'CollectorController' )
  var i1349 = data
  request.r(i1349[0], i1349[1], 0, i1348, 'movementHandle')
  request.r(i1349[2], i1349[3], 0, i1348, 'ColorCollector')
  var i1351 = i1349[4]
  var i1350 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorController')))
  for(var i = 0; i < i1351.length; i += 2) {
  request.r(i1351[i + 0], i1351[i + 1], 1, i1350, '')
  }
  i1348.collectorConnect = i1350
  i1348.State = i1349[5]
  i1348.IndexInColumn = i1349[6]
  i1348.ColumnIndex = i1349[7]
  i1348.SlotOnQueue = i1349[8]
  request.r(i1349[9], i1349[10], 0, i1348, 'LockController')
  i1348.IsLockObject = !!i1349[11]
  i1348.IsCompleteColor = !!i1349[12]
  request.r(i1349[13], i1349[14], 0, i1348, 'anim')
  request.r(i1349[15], i1349[16], 0, i1348, 'bulletDisplayHandler')
  return i1348
}

Deserializers["CollectorAnimation"] = function (request, data, root) {
  var i1352 = root || request.c( 'CollectorAnimation' )
  var i1353 = data
  request.r(i1353[0], i1353[1], 0, i1352, 'CollectorController')
  request.r(i1353[2], i1353[3], 0, i1352, 'CollectorInfoController')
  request.r(i1353[4], i1353[5], 0, i1352, 'EffectOptions')
  request.r(i1353[6], i1353[7], 0, i1352, 'RabbitAnimator')
  request.r(i1353[8], i1353[9], 0, i1352, 'BoxAnimator')
  request.r(i1353[10], i1353[11], 0, i1352, 'RootTransform')
  request.r(i1353[12], i1353[13], 0, i1352, 'RabbitTransform')
  i1352.DefaultScale = i1353[14]
  i1352.OnBeltScale = i1353[15]
  i1352.OnDeadScale = i1353[16]
  request.r(i1353[17], i1353[18], 0, i1352, 'CollectorBody')
  i1352.jumpHeight = i1353[19]
  i1352.jumpScaleY = i1353[20]
  i1352.squashScaleY = i1353[21]
  i1352.squashScaleX = i1353[22]
  i1352.durationUp = i1353[23]
  i1352.durationDown = i1353[24]
  i1352.durationRecover = i1353[25]
  i1352.upEase = i1353[26]
  i1352.downEase = i1353[27]
  i1352.recoverEase = i1353[28]
  i1352.squashScaleY2 = i1353[29]
  i1352.squashScaleX2 = i1353[30]
  i1352.ShootAnimation = i1353[31]
  i1352.ShootRate = i1353[32]
  var i1355 = i1353[33]
  var i1354 = []
  for(var i = 0; i < i1355.length; i += 1) {
    i1354.push( i1355[i + 0] );
  }
  i1352.IdleAnimations = i1354
  i1352.RareIdleAnimation = i1353[34]
  i1352.EarIdleAnimation = i1353[35]
  i1352.EarIdleRate = new pc.Vec2( i1353[36], i1353[37] )
  request.r(i1353[38], i1353[39], 0, i1352, 'RabbitRoot')
  i1352.breathScaleX = i1353[40]
  i1352.breathScaleY = i1353[41]
  i1352.duration = i1353[42]
  i1352.stretchScaleY = i1353[43]
  i1352.stretchDuration = i1353[44]
  i1352.BoxJumpAnimation = i1353[45]
  i1352.BoxRevealAnimation = i1353[46]
  request.r(i1353[47], i1353[48], 0, i1352, 'BoxRandomRotate')
  i1352.JumpingToBelt = !!i1353[49]
  return i1352
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i1358 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i1359 = data
  i1358.aspect = i1359[0]
  i1358.orthographic = !!i1359[1]
  i1358.orthographicSize = i1359[2]
  i1358.backgroundColor = new pc.Color(i1359[3], i1359[4], i1359[5], i1359[6])
  i1358.nearClipPlane = i1359[7]
  i1358.farClipPlane = i1359[8]
  i1358.fieldOfView = i1359[9]
  i1358.depth = i1359[10]
  i1358.clearFlags = i1359[11]
  i1358.cullingMask = i1359[12]
  i1358.rect = i1359[13]
  request.r(i1359[14], i1359[15], 0, i1358, 'targetTexture')
  i1358.usePhysicalProperties = !!i1359[16]
  i1358.focalLength = i1359[17]
  i1358.sensorSize = new pc.Vec2( i1359[18], i1359[19] )
  i1358.lensShift = new pc.Vec2( i1359[20], i1359[21] )
  i1358.gateFit = i1359[22]
  i1358.commandBufferCount = i1359[23]
  i1358.cameraType = i1359[24]
  i1358.enabled = !!i1359[25]
  return i1358
}

Deserializers["WallObject"] = function (request, data, root) {
  var i1360 = root || request.c( 'WallObject' )
  var i1361 = data
  request.r(i1361[0], i1361[1], 0, i1360, 'WallTransform')
  var i1363 = i1361[2]
  var i1362 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1363.length; i += 1) {
    i1362.add(request.d('PaintingPixel', i1363[i + 0]));
  }
  i1360.PaintingPixelsCovered = i1362
  i1360.ColorCode = i1361[3]
  i1360.Hearts = i1361[4]
  i1360.HeartsLoss = i1361[5]
  i1360.RemainingHearts = i1361[6]
  request.r(i1361[7], i1361[8], 0, i1360, 'WallRenderer')
  request.r(i1361[9], i1361[10], 0, i1360, 'HeartsText')
  i1360.Destroyed = !!i1361[11]
  i1360.WorldPos = new pc.Vec3( i1361[12], i1361[13], i1361[14] )
  return i1360
}

Deserializers["TMPro.TextMeshPro"] = function (request, data, root) {
  var i1364 = root || request.c( 'TMPro.TextMeshPro' )
  var i1365 = data
  i1364._SortingLayer = i1365[0]
  i1364._SortingLayerID = i1365[1]
  i1364._SortingOrder = i1365[2]
  i1364.m_hasFontAssetChanged = !!i1365[3]
  request.r(i1365[4], i1365[5], 0, i1364, 'm_renderer')
  i1364.m_maskType = i1365[6]
  i1364.m_text = i1365[7]
  i1364.m_isRightToLeft = !!i1365[8]
  request.r(i1365[9], i1365[10], 0, i1364, 'm_fontAsset')
  request.r(i1365[11], i1365[12], 0, i1364, 'm_sharedMaterial')
  var i1367 = i1365[13]
  var i1366 = []
  for(var i = 0; i < i1367.length; i += 2) {
  request.r(i1367[i + 0], i1367[i + 1], 2, i1366, '')
  }
  i1364.m_fontSharedMaterials = i1366
  request.r(i1365[14], i1365[15], 0, i1364, 'm_fontMaterial')
  var i1369 = i1365[16]
  var i1368 = []
  for(var i = 0; i < i1369.length; i += 2) {
  request.r(i1369[i + 0], i1369[i + 1], 2, i1368, '')
  }
  i1364.m_fontMaterials = i1368
  i1364.m_fontColor32 = UnityEngine.Color32.ConstructColor(i1365[17], i1365[18], i1365[19], i1365[20])
  i1364.m_fontColor = new pc.Color(i1365[21], i1365[22], i1365[23], i1365[24])
  i1364.m_enableVertexGradient = !!i1365[25]
  i1364.m_colorMode = i1365[26]
  i1364.m_fontColorGradient = request.d('TMPro.VertexGradient', i1365[27], i1364.m_fontColorGradient)
  request.r(i1365[28], i1365[29], 0, i1364, 'm_fontColorGradientPreset')
  request.r(i1365[30], i1365[31], 0, i1364, 'm_spriteAsset')
  i1364.m_tintAllSprites = !!i1365[32]
  request.r(i1365[33], i1365[34], 0, i1364, 'm_StyleSheet')
  i1364.m_TextStyleHashCode = i1365[35]
  i1364.m_overrideHtmlColors = !!i1365[36]
  i1364.m_faceColor = UnityEngine.Color32.ConstructColor(i1365[37], i1365[38], i1365[39], i1365[40])
  i1364.m_fontSize = i1365[41]
  i1364.m_fontSizeBase = i1365[42]
  i1364.m_fontWeight = i1365[43]
  i1364.m_enableAutoSizing = !!i1365[44]
  i1364.m_fontSizeMin = i1365[45]
  i1364.m_fontSizeMax = i1365[46]
  i1364.m_fontStyle = i1365[47]
  i1364.m_HorizontalAlignment = i1365[48]
  i1364.m_VerticalAlignment = i1365[49]
  i1364.m_textAlignment = i1365[50]
  i1364.m_characterSpacing = i1365[51]
  i1364.m_wordSpacing = i1365[52]
  i1364.m_lineSpacing = i1365[53]
  i1364.m_lineSpacingMax = i1365[54]
  i1364.m_paragraphSpacing = i1365[55]
  i1364.m_charWidthMaxAdj = i1365[56]
  i1364.m_enableWordWrapping = !!i1365[57]
  i1364.m_wordWrappingRatios = i1365[58]
  i1364.m_overflowMode = i1365[59]
  request.r(i1365[60], i1365[61], 0, i1364, 'm_linkedTextComponent')
  request.r(i1365[62], i1365[63], 0, i1364, 'parentLinkedComponent')
  i1364.m_enableKerning = !!i1365[64]
  i1364.m_enableExtraPadding = !!i1365[65]
  i1364.checkPaddingRequired = !!i1365[66]
  i1364.m_isRichText = !!i1365[67]
  i1364.m_parseCtrlCharacters = !!i1365[68]
  i1364.m_isOrthographic = !!i1365[69]
  i1364.m_isCullingEnabled = !!i1365[70]
  i1364.m_horizontalMapping = i1365[71]
  i1364.m_verticalMapping = i1365[72]
  i1364.m_uvLineOffset = i1365[73]
  i1364.m_geometrySortingOrder = i1365[74]
  i1364.m_IsTextObjectScaleStatic = !!i1365[75]
  i1364.m_VertexBufferAutoSizeReduction = !!i1365[76]
  i1364.m_useMaxVisibleDescender = !!i1365[77]
  i1364.m_pageToDisplay = i1365[78]
  i1364.m_margin = new pc.Vec4( i1365[79], i1365[80], i1365[81], i1365[82] )
  i1364.m_isUsingLegacyAnimationComponent = !!i1365[83]
  i1364.m_isVolumetricText = !!i1365[84]
  request.r(i1365[85], i1365[86], 0, i1364, 'm_Material')
  i1364.m_Maskable = !!i1365[87]
  i1364.m_Color = new pc.Color(i1365[88], i1365[89], i1365[90], i1365[91])
  i1364.m_RaycastTarget = !!i1365[92]
  i1364.m_RaycastPadding = new pc.Vec4( i1365[93], i1365[94], i1365[95], i1365[96] )
  return i1364
}

Deserializers["CullableObject"] = function (request, data, root) {
  var i1370 = root || request.c( 'CullableObject' )
  var i1371 = data
  var i1373 = i1371[0]
  var i1372 = []
  for(var i = 0; i < i1373.length; i += 2) {
  request.r(i1373[i + 0], i1373[i + 1], 2, i1372, '')
  }
  i1370.targetRenderers = i1372
  i1370.checkInterval = i1371[1]
  i1370.maxDistance = i1371[2]
  i1370.showDebug = !!i1371[3]
  return i1370
}

Deserializers["CachedTransformPathMover"] = function (request, data, root) {
  var i1376 = root || request.c( 'CachedTransformPathMover' )
  var i1377 = data
  i1376.speed = i1377[0]
  i1376.currentTF = i1377[1]
  i1376.endTF = i1377[2]
  i1376.direction = i1377[3]
  i1376.useDistanceBasedMovement = !!i1377[4]
  i1376.currentDistance = i1377[5]
  i1376.movementType = i1377[6]
  i1376.PingPongClamp = new pc.Vec2( i1377[7], i1377[8] )
  i1376.LoopClamp = new pc.Vec2( i1377[9], i1377[10] )
  i1376.autoMove = !!i1377[11]
  i1376.orientToPath = !!i1377[12]
  i1376.orientationSpace = i1377[13]
  i1376.smoothOrientation = !!i1377[14]
  i1376.orientationSpeed = i1377[15]
  i1376.rotationInterpolation = i1377[16]
  return i1376
}

Deserializers["ColorPixelsCollectorObject"] = function (request, data, root) {
  var i1378 = root || request.c( 'ColorPixelsCollectorObject' )
  var i1379 = data
  request.r(i1379[0], i1379[1], 0, i1378, 'RabbitRootTransform')
  request.r(i1379[2], i1379[3], 0, i1378, 'RabbitRotateTransform')
  request.r(i1379[4], i1379[5], 0, i1378, 'VisualHandler')
  request.r(i1379[6], i1379[7], 0, i1378, 'CollectorAnimator')
  i1378.CurrentState = i1379[8]
  request.r(i1379[9], i1379[10], 0, i1378, 'MovementHandle')
  i1378.NormalSpeed = i1379[11]
  i1378.AbsoluteWinSpeed = i1379[12]
  request.r(i1379[13], i1379[14], 0, i1378, 'CurrentGrid')
  i1378.BulletCapacity = i1379[15]
  i1378.BulletLeft = i1379[16]
  i1378.CollectorColor = i1379[17]
  i1378.DetectionRadius = i1379[18]
  i1378.IsLocked = !!i1379[19]
  i1378.IsHidden = !!i1379[20]
  var i1381 = i1379[21]
  var i1380 = new (System.Collections.Generic.List$1(Bridge.ns('System.Int32')))
  for(var i = 0; i < i1381.length; i += 1) {
    i1380.add(i1381[i + 0]);
  }
  i1378.ConnectedCollectorsIDs = i1380
  i1378.IsCollectorActive = !!i1379[22]
  i1378.CurrentTargetPosition = new pc.Vec3( i1379[23], i1379[24], i1379[25] )
  i1378.currentMovementDirection = i1379[26]
  i1378.CurrentColor = new pc.Color(i1379[27], i1379[28], i1379[29], i1379[30])
  var i1383 = i1379[31]
  var i1382 = []
  for(var i = 0; i < i1383.length; i += 1) {
    i1382.push( request.d('PaintingPixel', i1383[i + 0]) );
  }
  i1378.possibleTargets = i1382
  i1378.AbsoluteCheckRate = i1379[32]
  i1378.IsHided = !!i1379[33]
  i1378.OnCompleteAllColorPixels = request.d('System.Action', i1379[34], i1378.OnCompleteAllColorPixels)
  i1378.CheckIntervalFrames = i1379[35]
  i1378.MinMoveDistance = i1379[36]
  i1378.ID = i1379[37]
  request.r(i1379[38], i1379[39], 0, i1378, 'bulletDisplayHandler')
  return i1378
}

Deserializers["System.Action"] = function (request, data, root) {
  var i1388 = root || request.c( 'System.Action' )
  var i1389 = data
  return i1388
}

Deserializers["CollectorVisualHandler"] = function (request, data, root) {
  var i1390 = root || request.c( 'CollectorVisualHandler' )
  var i1391 = data
  request.r(i1391[0], i1391[1], 0, i1390, 'CollectorHandle')
  request.r(i1391[2], i1391[3], 0, i1390, 'Animator')
  request.r(i1391[4], i1391[5], 0, i1390, 'colorPalette')
  var i1393 = i1391[6]
  var i1392 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Renderer')))
  for(var i = 0; i < i1393.length; i += 2) {
  request.r(i1393[i + 0], i1393[i + 1], 1, i1392, '')
  }
  i1390.GunnerRenderers = i1392
  request.r(i1391[7], i1391[8], 0, i1390, 'BulletsText')
  request.r(i1391[9], i1391[10], 0, i1390, 'TankRopeObj')
  request.r(i1391[11], i1391[12], 0, i1390, 'TankRope')
  request.r(i1391[13], i1391[14], 0, i1390, 'TankRopeMesh')
  request.r(i1391[15], i1391[16], 0, i1390, 'LockSpriteRenderer')
  request.r(i1391[17], i1391[18], 0, i1390, 'CattonBox')
  request.r(i1391[19], i1391[20], 0, i1390, 'QuestionMarkSpriteRenderer')
  request.r(i1391[21], i1391[22], 0, i1390, 'OriginalMat')
  i1390.HiddenOutlineColor = new pc.Color(i1391[23], i1391[24], i1391[25], i1391[26])
  i1390.HiddenRopeColor = new pc.Color(i1391[27], i1391[28], i1391[29], i1391[30])
  request.r(i1391[31], i1391[32], 0, i1390, 'BulletSpawner')
  request.r(i1391[33], i1391[34], 0, i1390, 'CollectorMuzzleVFX')
  request.r(i1391[35], i1391[36], 0, i1390, 'ReavealVFX')
  request.r(i1391[37], i1391[38], 0, i1390, 'HighSpeedTrail')
  i1390.CurrentColor = new pc.Color(i1391[39], i1391[40], i1391[41], i1391[42])
  return i1390
}

Deserializers["BulletDisplayHandler"] = function (request, data, root) {
  var i1394 = root || request.c( 'BulletDisplayHandler' )
  var i1395 = data
  request.r(i1395[0], i1395[1], 0, i1394, 'Collector')
  request.r(i1395[2], i1395[3], 0, i1394, 'text')
  return i1394
}

Deserializers["EnableRandomRotate"] = function (request, data, root) {
  var i1396 = root || request.c( 'EnableRandomRotate' )
  var i1397 = data
  request.r(i1397[0], i1397[1], 0, i1396, 'Target')
  i1396.minY = i1397[2]
  i1396.maxY = i1397[3]
  return i1396
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Animator"] = function (request, data, root) {
  var i1398 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Animator' )
  var i1399 = data
  request.r(i1399[0], i1399[1], 0, i1398, 'animatorController')
  request.r(i1399[2], i1399[3], 0, i1398, 'avatar')
  i1398.updateMode = i1399[4]
  i1398.hasTransformHierarchy = !!i1399[5]
  i1398.applyRootMotion = !!i1399[6]
  var i1401 = i1399[7]
  var i1400 = []
  for(var i = 0; i < i1401.length; i += 2) {
  request.r(i1401[i + 0], i1401[i + 1], 2, i1400, '')
  }
  i1398.humanBones = i1400
  i1398.enabled = !!i1399[8]
  return i1398
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.LineRenderer"] = function (request, data, root) {
  var i1402 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.LineRenderer' )
  var i1403 = data
  i1402.textureMode = i1403[0]
  i1402.alignment = i1403[1]
  i1402.widthCurve = new pc.AnimationCurve( { keys_flow: i1403[2] } )
  i1402.colorGradient = i1403[3] ? new pc.ColorGradient(i1403[3][0], i1403[3][1], i1403[3][2]) : null
  var i1405 = i1403[4]
  var i1404 = []
  for(var i = 0; i < i1405.length; i += 3) {
    i1404.push( new pc.Vec3( i1405[i + 0], i1405[i + 1], i1405[i + 2] ) );
  }
  i1402.positions = i1404
  i1402.positionCount = i1403[5]
  i1402.widthMultiplier = i1403[6]
  i1402.startWidth = i1403[7]
  i1402.endWidth = i1403[8]
  i1402.numCornerVertices = i1403[9]
  i1402.numCapVertices = i1403[10]
  i1402.useWorldSpace = !!i1403[11]
  i1402.loop = !!i1403[12]
  i1402.startColor = new pc.Color(i1403[13], i1403[14], i1403[15], i1403[16])
  i1402.endColor = new pc.Color(i1403[17], i1403[18], i1403[19], i1403[20])
  i1402.generateLightingData = !!i1403[21]
  i1402.enabled = !!i1403[22]
  request.r(i1403[23], i1403[24], 0, i1402, 'sharedMaterial')
  var i1407 = i1403[25]
  var i1406 = []
  for(var i = 0; i < i1407.length; i += 2) {
  request.r(i1407[i + 0], i1407[i + 1], 2, i1406, '')
  }
  i1402.sharedMaterials = i1406
  i1402.receiveShadows = !!i1403[26]
  i1402.shadowCastingMode = i1403[27]
  i1402.sortingLayerID = i1403[28]
  i1402.sortingOrder = i1403[29]
  i1402.lightmapIndex = i1403[30]
  i1402.lightmapSceneIndex = i1403[31]
  i1402.lightmapScaleOffset = new pc.Vec4( i1403[32], i1403[33], i1403[34], i1403[35] )
  i1402.lightProbeUsage = i1403[36]
  i1402.reflectionProbeUsage = i1403[37]
  return i1402
}

Deserializers["GogoGaga.OptimizedRopesAndCables.Rope"] = function (request, data, root) {
  var i1408 = root || request.c( 'GogoGaga.OptimizedRopesAndCables.Rope' )
  var i1409 = data
  i1408.linePoints = i1409[0]
  i1408.stiffness = i1409[1]
  i1408.damping = i1409[2]
  i1408.ropeLength = i1409[3]
  i1408.ropeWidth = i1409[4]
  i1408.midPointWeight = i1409[5]
  i1408.midPointPosition = i1409[6]
  request.r(i1409[7], i1409[8], 0, i1408, 'startPoint')
  request.r(i1409[9], i1409[10], 0, i1408, 'midPoint')
  request.r(i1409[11], i1409[12], 0, i1408, 'endPoint')
  return i1408
}

Deserializers["GogoGaga.OptimizedRopesAndCables.RopeMesh"] = function (request, data, root) {
  var i1410 = root || request.c( 'GogoGaga.OptimizedRopesAndCables.RopeMesh' )
  var i1411 = data
  i1410.OverallDivision = i1411[0]
  i1410.ropeWidth = i1411[1]
  i1410.radialDivision = i1411[2]
  request.r(i1411[3], i1411[4], 0, i1410, 'material')
  i1410.tilingPerMeter = i1411[5]
  request.r(i1411[6], i1411[7], 0, i1410, 'RopeLogic')
  request.r(i1411[8], i1411[9], 0, i1410, 'StartPoint')
  request.r(i1411[10], i1411[11], 0, i1410, 'EndPoint')
  i1410.FirstHalfColor = new pc.Color(i1411[12], i1411[13], i1411[14], i1411[15])
  i1410.SecondHalfColor = new pc.Color(i1411[16], i1411[17], i1411[18], i1411[19])
  i1410.ThresholdDistance = i1411[20]
  i1410.ThresholdOffsetY = i1411[21]
  i1410.UpdateRate = i1411[22]
  i1410.EnableDebugColorLogs = !!i1411[23]
  i1410.OverrideColorProperty = i1411[24]
  i1410.RebuildOnEveryFrame = !!i1411[25]
  i1410.MinSecondsBetweenRebuilds = i1411[26]
  i1410.RebuildMovementThreshold = i1411[27]
  i1410.RecalculateNormals = !!i1411[28]
  return i1410
}

Deserializers["BlockFountainObject"] = function (request, data, root) {
  var i1412 = root || request.c( 'BlockFountainObject' )
  var i1413 = data
  request.r(i1413[0], i1413[1], 0, i1412, 'CurrentGrid')
  request.r(i1413[2], i1413[3], 0, i1412, 'VisualHandler')
  request.r(i1413[4], i1413[5], 0, i1412, 'LevelMechanicPrefabs')
  request.r(i1413[6], i1413[7], 0, i1412, 'FountainRootObject')
  request.r(i1413[8], i1413[9], 0, i1412, 'ProjectileSpawnPos')
  i1412.RotateToTarget = !!i1413[10]
  request.r(i1413[11], i1413[12], 0, i1412, 'RotatePart')
  i1412.RotateSpeed = i1413[13]
  var i1415 = i1413[14]
  var i1414 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1415.length; i += 1) {
    i1414.add(request.d('PaintingPixel', i1415[i + 0]));
  }
  i1412.PaintingPixelsCovered = i1414
  i1412.CurrentColorCode = i1413[15]
  i1412.CurrentColor = new pc.Color(i1413[16], i1413[17], i1413[18], i1413[19])
  i1412.BulletLeft = i1413[20]
  i1412.Destroyed = !!i1413[21]
  i1412.TotalBlocksHolding = i1413[22]
  i1412.BlocksReleased = i1413[23]
  return i1412
}

Deserializers["BlockFountainVisualHandler"] = function (request, data, root) {
  var i1416 = root || request.c( 'BlockFountainVisualHandler' )
  var i1417 = data
  var i1419 = i1417[0]
  var i1418 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Renderer')))
  for(var i = 0; i < i1419.length; i += 2) {
  request.r(i1419[i + 0], i1419[i + 1], 1, i1418, '')
  }
  i1416.FontainMeshes = i1418
  request.r(i1417[1], i1417[2], 0, i1416, 'BaseMaterial')
  request.r(i1417[3], i1417[4], 0, i1416, 'MuzzleParticle')
  request.r(i1417[5], i1417[6], 0, i1416, 'BulletText')
  request.r(i1417[7], i1417[8], 0, i1416, 'FountainAnimator')
  i1416.ShootStateName = i1417[9]
  return i1416
}

Deserializers["FountainProjectileController"] = function (request, data, root) {
  var i1420 = root || request.c( 'FountainProjectileController' )
  var i1421 = data
  request.r(i1421[0], i1421[1], 0, i1420, 'EffectOptions')
  request.r(i1421[2], i1421[3], 0, i1420, 'MyTransform')
  i1420.Target = new pc.Vec3( i1421[4], i1421[5], i1421[6] )
  var i1423 = i1421[7]
  var i1422 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.ParticleSystem')))
  for(var i = 0; i < i1423.length; i += 2) {
  request.r(i1423[i + 0], i1423[i + 1], 1, i1422, '')
  }
  i1420.FlyingFXs = i1422
  var i1425 = i1421[8]
  var i1424 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.ParticleSystem')))
  for(var i = 0; i < i1425.length; i += 2) {
  request.r(i1425[i + 0], i1425[i + 1], 1, i1424, '')
  }
  i1420.HitFXs = i1424
  request.r(i1421[9], i1421[10], 0, i1420, 'TrailFX')
  request.r(i1421[11], i1421[12], 0, i1420, 'BulletMeshParticle')
  return i1420
}

Deserializers["CollectorProjectileController"] = function (request, data, root) {
  var i1428 = root || request.c( 'CollectorProjectileController' )
  var i1429 = data
  request.r(i1429[0], i1429[1], 0, i1428, 'EffectOptions')
  request.r(i1429[2], i1429[3], 0, i1428, 'MyTransform')
  i1428.SuperAmmo = !!i1429[4]
  i1428.Speed = i1429[5]
  i1428.Target = new pc.Vec3( i1429[6], i1429[7], i1429[8] )
  i1428.TimeExisted = i1429[9]
  i1428.TimeExistedAfterHit = i1429[10]
  i1428.CurrentTimer = i1429[11]
  i1428.Stopped = !!i1429[12]
  var i1431 = i1429[13]
  var i1430 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.ParticleSystem')))
  for(var i = 0; i < i1431.length; i += 2) {
  request.r(i1431[i + 0], i1431[i + 1], 1, i1430, '')
  }
  i1428.FlyingFXs = i1430
  var i1433 = i1429[14]
  var i1432 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.ParticleSystem')))
  for(var i = 0; i < i1433.length; i += 2) {
  request.r(i1433[i + 0], i1433[i + 1], 1, i1432, '')
  }
  i1428.HitFXs = i1432
  request.r(i1429[15], i1429[16], 0, i1428, 'TrailFX')
  request.r(i1429[17], i1429[18], 0, i1428, 'BulletMeshParticle')
  return i1428
}

Deserializers["PathTransformBasedCached"] = function (request, data, root) {
  var i1434 = root || request.c( 'PathTransformBasedCached' )
  var i1435 = data
  var i1437 = i1435[0]
  var i1436 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Transform')))
  for(var i = 0; i < i1437.length; i += 2) {
  request.r(i1437[i + 0], i1437[i + 1], 1, i1436, '')
  }
  i1434.PathPoints = i1436
  i1434.totalDistance = i1435[1]
  return i1434
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Cubemap"] = function (request, data, root) {
  var i1440 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Cubemap' )
  var i1441 = data
  i1440.name = i1441[0]
  i1440.atlasId = i1441[1]
  i1440.mipmapCount = i1441[2]
  i1440.hdr = !!i1441[3]
  i1440.size = i1441[4]
  i1440.anisoLevel = i1441[5]
  i1440.filterMode = i1441[6]
  var i1443 = i1441[7]
  var i1442 = []
  for(var i = 0; i < i1443.length; i += 4) {
    i1442.push( UnityEngine.Rect.MinMaxRect(i1443[i + 0], i1443[i + 1], i1443[i + 2], i1443[i + 3]) );
  }
  i1440.rects = i1442
  i1440.wrapU = i1441[8]
  i1440.wrapV = i1441[9]
  return i1440
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i1446 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i1447 = data
  i1446.name = i1447[0]
  i1446.index = i1447[1]
  i1446.startup = !!i1447[2]
  return i1446
}

Deserializers["LevelConfigSetup"] = function (request, data, root) {
  var i1448 = root || request.c( 'LevelConfigSetup' )
  var i1449 = data
  i1448.EDITOR = !!i1449[0]
  request.r(i1449[1], i1449[2], 0, i1448, 'NewTargetPainting')
  var i1451 = i1449[3]
  var i1450 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i1451.length; i += 1) {
    i1450.add(i1451[i + 0]);
  }
  i1448.ColorCodesUsed = i1450
  request.r(i1449[4], i1449[5], 0, i1448, 'CurrentGridObject')
  var i1453 = i1449[6]
  var i1452 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingGridObject')))
  for(var i = 0; i < i1453.length; i += 2) {
  request.r(i1453[i + 0], i1453[i + 1], 1, i1452, '')
  }
  i1448.CurrentGridObjects = i1452
  request.r(i1449[7], i1449[8], 0, i1448, 'CurrentLevel')
  request.r(i1449[9], i1449[10], 0, i1448, 'CurrentLevelPaintingConfig')
  request.r(i1449[11], i1449[12], 0, i1448, 'CurrentLevelCollectorConfig')
  request.r(i1449[13], i1449[14], 0, i1448, 'CurrentLevelPainting')
  var i1455 = i1449[15]
  var i1454 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i1455.length; i += 1) {
    i1454.add(i1455[i + 0]);
  }
  i1448.CurrentLevelColorCodes = i1454
  request.r(i1449[16], i1449[17], 0, i1448, 'PaintingSetup')
  request.r(i1449[18], i1449[19], 0, i1448, 'PipeObjectSetup')
  request.r(i1449[20], i1449[21], 0, i1448, 'WallObjectSetup')
  request.r(i1449[22], i1449[23], 0, i1448, 'KeyObjectSetup')
  request.r(i1449[24], i1449[25], 0, i1448, 'LevelCollectorsManager')
  request.r(i1449[26], i1449[27], 0, i1448, 'LevelCollectorsSetup')
  return i1448
}

Deserializers["GridBlockFountainModule"] = function (request, data, root) {
  var i1458 = root || request.c( 'GridBlockFountainModule' )
  var i1459 = data
  request.r(i1459[0], i1459[1], 0, i1458, 'CurrentGrid')
  return i1458
}

Deserializers["LevelCollectorsSystem"] = function (request, data, root) {
  var i1460 = root || request.c( 'LevelCollectorsSystem' )
  var i1461 = data
  request.r(i1461[0], i1461[1], 0, i1460, 'LevelSetup')
  request.r(i1461[2], i1461[3], 0, i1460, 'CurrentLevelCollectorsConfig')
  request.r(i1461[4], i1461[5], 0, i1460, 'FormationCenter')
  i1460.SpaceBetweenColumns = i1461[6]
  i1460.SpaceBetweenCollectors = i1461[7]
  request.r(i1461[8], i1461[9], 0, i1460, 'CollectorContainer')
  i1460.CollectorRotation = new pc.Vec3( i1461[10], i1461[11], i1461[12] )
  request.r(i1461[13], i1461[14], 0, i1460, 'PrefabSource')
  var i1463 = i1461[15]
  var i1462 = new (System.Collections.Generic.List$1(Bridge.ns('LockObject')))
  for(var i = 0; i < i1463.length; i += 2) {
  request.r(i1463[i + 0], i1463[i + 1], 1, i1462, '')
  }
  i1460.CurrentLocks = i1462
  var i1465 = i1461[16]
  var i1464 = new (System.Collections.Generic.List$1(Bridge.ns('ColorPixelsCollectorObject')))
  for(var i = 0; i < i1465.length; i += 2) {
  request.r(i1465[i + 0], i1465[i + 1], 1, i1464, '')
  }
  i1460.CurrentCollectors = i1464
  var i1467 = i1461[17]
  var i1466 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorColumn')))
  for(var i = 0; i < i1467.length; i += 1) {
    i1466.add(request.d('CollectorColumn', i1467[i + 0]));
  }
  i1460.ObjectsInColumns = i1466
  var i1469 = i1461[18]
  var i1468 = new (System.Collections.Generic.List$1(Bridge.ns('System.Collections.Generic.List`1[[CollectorController, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]')))
  for(var i = 0; i < i1469.length; i += 1) {
  var i1471 = i1469[i + 0]
  var i1470 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorController')))
  for(var i = 0; i < i1471.length; i += 2) {
  request.r(i1471[i + 0], i1471[i + 1], 1, i1470, '')
  }
    i1468.add(i1470);
  }
  i1460.CollectorControllersColumns = i1468
  i1460.CurrentTotalCollectors = i1461[19]
  return i1460
}

Deserializers["CollectorColumn"] = function (request, data, root) {
  var i1478 = root || request.c( 'CollectorColumn' )
  var i1479 = data
  var i1481 = i1479[0]
  var i1480 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorMachanicObjectBase')))
  for(var i = 0; i < i1481.length; i += 2) {
  request.r(i1481[i + 0], i1481[i + 1], 1, i1480, '')
  }
  i1478.CollectorsInColumn = i1480
  return i1478
}

Deserializers["CollectorColumnController"] = function (request, data, root) {
  var i1488 = root || request.c( 'CollectorColumnController' )
  var i1489 = data
  request.r(i1489[0], i1489[1], 0, i1488, 'collectorsSystem')
  return i1488
}

Deserializers["PaintingGridEffects"] = function (request, data, root) {
  var i1490 = root || request.c( 'PaintingGridEffects' )
  var i1491 = data
  request.r(i1491[0], i1491[1], 0, i1490, 'GridAudioSource')
  request.r(i1491[2], i1491[3], 0, i1490, 'BlockDestroyedClip')
  return i1490
}

Deserializers["CollectorProjectilePool"] = function (request, data, root) {
  var i1492 = root || request.c( 'CollectorProjectilePool' )
  var i1493 = data
  request.r(i1493[0], i1493[1], 0, i1492, 'normalProjectilePrefab')
  request.r(i1493[2], i1493[3], 0, i1492, 'superProjectilePrefab')
  i1492.initialCount = i1493[4]
  return i1492
}

Deserializers["BlockFountainProjectilePool"] = function (request, data, root) {
  var i1494 = root || request.c( 'BlockFountainProjectilePool' )
  var i1495 = data
  request.r(i1495[0], i1495[1], 0, i1494, 'fountainProjectilePrefab')
  i1494.initialCount = i1495[2]
  return i1494
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Light"] = function (request, data, root) {
  var i1496 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Light' )
  var i1497 = data
  i1496.type = i1497[0]
  i1496.color = new pc.Color(i1497[1], i1497[2], i1497[3], i1497[4])
  i1496.cullingMask = i1497[5]
  i1496.intensity = i1497[6]
  i1496.range = i1497[7]
  i1496.spotAngle = i1497[8]
  i1496.shadows = i1497[9]
  i1496.shadowNormalBias = i1497[10]
  i1496.shadowBias = i1497[11]
  i1496.shadowStrength = i1497[12]
  i1496.shadowResolution = i1497[13]
  i1496.lightmapBakeType = i1497[14]
  i1496.renderMode = i1497[15]
  request.r(i1497[16], i1497[17], 0, i1496, 'cookie')
  i1496.cookieSize = i1497[18]
  i1496.shadowNearPlane = i1497[19]
  i1496.enabled = !!i1497[20]
  return i1496
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i1498 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i1499 = data
  i1498.ambientIntensity = i1499[0]
  i1498.reflectionIntensity = i1499[1]
  i1498.ambientMode = i1499[2]
  i1498.ambientLight = new pc.Color(i1499[3], i1499[4], i1499[5], i1499[6])
  i1498.ambientSkyColor = new pc.Color(i1499[7], i1499[8], i1499[9], i1499[10])
  i1498.ambientGroundColor = new pc.Color(i1499[11], i1499[12], i1499[13], i1499[14])
  i1498.ambientEquatorColor = new pc.Color(i1499[15], i1499[16], i1499[17], i1499[18])
  i1498.fogColor = new pc.Color(i1499[19], i1499[20], i1499[21], i1499[22])
  i1498.fogEndDistance = i1499[23]
  i1498.fogStartDistance = i1499[24]
  i1498.fogDensity = i1499[25]
  i1498.fog = !!i1499[26]
  request.r(i1499[27], i1499[28], 0, i1498, 'skybox')
  i1498.fogMode = i1499[29]
  var i1501 = i1499[30]
  var i1500 = []
  for(var i = 0; i < i1501.length; i += 1) {
    i1500.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i1501[i + 0]) );
  }
  i1498.lightmaps = i1500
  i1498.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i1499[31], i1498.lightProbes)
  i1498.lightmapsMode = i1499[32]
  i1498.mixedBakeMode = i1499[33]
  i1498.environmentLightingMode = i1499[34]
  i1498.ambientProbe = new pc.SphericalHarmonicsL2(i1499[35])
  i1498.referenceAmbientProbe = new pc.SphericalHarmonicsL2(i1499[36])
  i1498.useReferenceAmbientProbe = !!i1499[37]
  request.r(i1499[38], i1499[39], 0, i1498, 'customReflection')
  request.r(i1499[40], i1499[41], 0, i1498, 'defaultReflection')
  i1498.defaultReflectionMode = i1499[42]
  i1498.defaultReflectionResolution = i1499[43]
  i1498.sunLightObjectId = i1499[44]
  i1498.pixelLightCount = i1499[45]
  i1498.defaultReflectionHDR = !!i1499[46]
  i1498.hasLightDataAsset = !!i1499[47]
  i1498.hasManualGenerate = !!i1499[48]
  return i1498
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i1504 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i1505 = data
  request.r(i1505[0], i1505[1], 0, i1504, 'lightmapColor')
  request.r(i1505[2], i1505[3], 0, i1504, 'lightmapDirection')
  return i1504
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i1506 = root || new UnityEngine.LightProbes()
  var i1507 = data
  return i1506
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i1512 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i1513 = data
  var i1515 = i1513[0]
  var i1514 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i1515.length; i += 1) {
    i1514.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i1515[i + 0]));
  }
  i1512.ShaderCompilationErrors = i1514
  i1512.name = i1513[1]
  i1512.guid = i1513[2]
  var i1517 = i1513[3]
  var i1516 = []
  for(var i = 0; i < i1517.length; i += 1) {
    i1516.push( i1517[i + 0] );
  }
  i1512.shaderDefinedKeywords = i1516
  var i1519 = i1513[4]
  var i1518 = []
  for(var i = 0; i < i1519.length; i += 1) {
    i1518.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i1519[i + 0]) );
  }
  i1512.passes = i1518
  var i1521 = i1513[5]
  var i1520 = []
  for(var i = 0; i < i1521.length; i += 1) {
    i1520.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i1521[i + 0]) );
  }
  i1512.usePasses = i1520
  var i1523 = i1513[6]
  var i1522 = []
  for(var i = 0; i < i1523.length; i += 1) {
    i1522.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i1523[i + 0]) );
  }
  i1512.defaultParameterValues = i1522
  request.r(i1513[7], i1513[8], 0, i1512, 'unityFallbackShader')
  i1512.readDepth = !!i1513[9]
  i1512.hasDepthOnlyPass = !!i1513[10]
  i1512.isCreatedByShaderGraph = !!i1513[11]
  i1512.disableBatching = !!i1513[12]
  i1512.compiled = !!i1513[13]
  return i1512
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i1526 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i1527 = data
  i1526.shaderName = i1527[0]
  i1526.errorMessage = i1527[1]
  return i1526
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i1530 = root || new pc.UnityShaderPass()
  var i1531 = data
  i1530.id = i1531[0]
  i1530.subShaderIndex = i1531[1]
  i1530.name = i1531[2]
  i1530.passType = i1531[3]
  i1530.grabPassTextureName = i1531[4]
  i1530.usePass = !!i1531[5]
  i1530.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1531[6], i1530.zTest)
  i1530.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1531[7], i1530.zWrite)
  i1530.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1531[8], i1530.culling)
  i1530.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i1531[9], i1530.blending)
  i1530.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i1531[10], i1530.alphaBlending)
  i1530.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1531[11], i1530.colorWriteMask)
  i1530.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1531[12], i1530.offsetUnits)
  i1530.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1531[13], i1530.offsetFactor)
  i1530.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1531[14], i1530.stencilRef)
  i1530.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1531[15], i1530.stencilReadMask)
  i1530.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1531[16], i1530.stencilWriteMask)
  i1530.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i1531[17], i1530.stencilOp)
  i1530.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i1531[18], i1530.stencilOpFront)
  i1530.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i1531[19], i1530.stencilOpBack)
  var i1533 = i1531[20]
  var i1532 = []
  for(var i = 0; i < i1533.length; i += 1) {
    i1532.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i1533[i + 0]) );
  }
  i1530.tags = i1532
  var i1535 = i1531[21]
  var i1534 = []
  for(var i = 0; i < i1535.length; i += 1) {
    i1534.push( i1535[i + 0] );
  }
  i1530.passDefinedKeywords = i1534
  var i1537 = i1531[22]
  var i1536 = []
  for(var i = 0; i < i1537.length; i += 1) {
    i1536.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i1537[i + 0]) );
  }
  i1530.passDefinedKeywordGroups = i1536
  var i1539 = i1531[23]
  var i1538 = []
  for(var i = 0; i < i1539.length; i += 1) {
    i1538.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i1539[i + 0]) );
  }
  i1530.variants = i1538
  var i1541 = i1531[24]
  var i1540 = []
  for(var i = 0; i < i1541.length; i += 1) {
    i1540.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i1541[i + 0]) );
  }
  i1530.excludedVariants = i1540
  i1530.hasDepthReader = !!i1531[25]
  return i1530
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i1542 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i1543 = data
  i1542.val = i1543[0]
  i1542.name = i1543[1]
  return i1542
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i1544 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i1545 = data
  i1544.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1545[0], i1544.src)
  i1544.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1545[1], i1544.dst)
  i1544.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1545[2], i1544.op)
  return i1544
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i1546 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i1547 = data
  i1546.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1547[0], i1546.pass)
  i1546.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1547[1], i1546.fail)
  i1546.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1547[2], i1546.zFail)
  i1546.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1547[3], i1546.comp)
  return i1546
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i1550 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i1551 = data
  i1550.name = i1551[0]
  i1550.value = i1551[1]
  return i1550
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i1554 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i1555 = data
  var i1557 = i1555[0]
  var i1556 = []
  for(var i = 0; i < i1557.length; i += 1) {
    i1556.push( i1557[i + 0] );
  }
  i1554.keywords = i1556
  i1554.hasDiscard = !!i1555[1]
  return i1554
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i1560 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i1561 = data
  i1560.passId = i1561[0]
  i1560.subShaderIndex = i1561[1]
  var i1563 = i1561[2]
  var i1562 = []
  for(var i = 0; i < i1563.length; i += 1) {
    i1562.push( i1563[i + 0] );
  }
  i1560.keywords = i1562
  i1560.vertexProgram = i1561[3]
  i1560.fragmentProgram = i1561[4]
  i1560.exportedForWebGl2 = !!i1561[5]
  i1560.readDepth = !!i1561[6]
  return i1560
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i1566 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i1567 = data
  request.r(i1567[0], i1567[1], 0, i1566, 'shader')
  i1566.pass = i1567[2]
  return i1566
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i1570 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i1571 = data
  i1570.name = i1571[0]
  i1570.type = i1571[1]
  i1570.value = new pc.Vec4( i1571[2], i1571[3], i1571[4], i1571[5] )
  i1570.textureValue = i1571[6]
  i1570.shaderPropertyFlag = i1571[7]
  return i1570
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Sprite"] = function (request, data, root) {
  var i1572 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Sprite' )
  var i1573 = data
  i1572.name = i1573[0]
  request.r(i1573[1], i1573[2], 0, i1572, 'texture')
  i1572.aabb = i1573[3]
  i1572.vertices = i1573[4]
  i1572.triangles = i1573[5]
  i1572.textureRect = UnityEngine.Rect.MinMaxRect(i1573[6], i1573[7], i1573[8], i1573[9])
  i1572.packedRect = UnityEngine.Rect.MinMaxRect(i1573[10], i1573[11], i1573[12], i1573[13])
  i1572.border = new pc.Vec4( i1573[14], i1573[15], i1573[16], i1573[17] )
  i1572.transparency = i1573[18]
  i1572.bounds = i1573[19]
  i1572.pixelsPerUnit = i1573[20]
  i1572.textureWidth = i1573[21]
  i1572.textureHeight = i1573[22]
  i1572.nativeSize = new pc.Vec2( i1573[23], i1573[24] )
  i1572.pivot = new pc.Vec2( i1573[25], i1573[26] )
  i1572.textureRectOffset = new pc.Vec2( i1573[27], i1573[28] )
  return i1572
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.AudioClip"] = function (request, data, root) {
  var i1574 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.AudioClip' )
  var i1575 = data
  i1574.name = i1575[0]
  return i1574
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip"] = function (request, data, root) {
  var i1576 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip' )
  var i1577 = data
  i1576.name = i1577[0]
  i1576.wrapMode = i1577[1]
  i1576.isLooping = !!i1577[2]
  i1576.length = i1577[3]
  var i1579 = i1577[4]
  var i1578 = []
  for(var i = 0; i < i1579.length; i += 1) {
    i1578.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve', i1579[i + 0]) );
  }
  i1576.curves = i1578
  var i1581 = i1577[5]
  var i1580 = []
  for(var i = 0; i < i1581.length; i += 1) {
    i1580.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationEvent', i1581[i + 0]) );
  }
  i1576.events = i1580
  i1576.halfPrecision = !!i1577[6]
  i1576._frameRate = i1577[7]
  i1576.localBounds = request.d('Luna.Unity.DTO.UnityEngine.Animation.Data.Bounds', i1577[8], i1576.localBounds)
  i1576.hasMuscleCurves = !!i1577[9]
  var i1583 = i1577[10]
  var i1582 = []
  for(var i = 0; i < i1583.length; i += 1) {
    i1582.push( i1583[i + 0] );
  }
  i1576.clipMuscleConstant = i1582
  i1576.clipBindingConstant = request.d('Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip+AnimationClipBindingConstant', i1577[11], i1576.clipBindingConstant)
  return i1576
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve"] = function (request, data, root) {
  var i1586 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve' )
  var i1587 = data
  i1586.path = i1587[0]
  i1586.hash = i1587[1]
  i1586.componentType = i1587[2]
  i1586.property = i1587[3]
  i1586.keys = i1587[4]
  var i1589 = i1587[5]
  var i1588 = []
  for(var i = 0; i < i1589.length; i += 1) {
    i1588.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve+ObjectReferenceKey', i1589[i + 0]) );
  }
  i1586.objectReferenceKeys = i1588
  return i1586
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve+ObjectReferenceKey"] = function (request, data, root) {
  var i1592 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve+ObjectReferenceKey' )
  var i1593 = data
  i1592.time = i1593[0]
  request.r(i1593[1], i1593[2], 0, i1592, 'value')
  return i1592
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationEvent"] = function (request, data, root) {
  var i1596 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationEvent' )
  var i1597 = data
  i1596.functionName = i1597[0]
  i1596.floatParameter = i1597[1]
  i1596.intParameter = i1597[2]
  i1596.stringParameter = i1597[3]
  request.r(i1597[4], i1597[5], 0, i1596, 'objectReferenceParameter')
  i1596.time = i1597[6]
  return i1596
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.Bounds"] = function (request, data, root) {
  var i1598 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.Bounds' )
  var i1599 = data
  i1598.center = new pc.Vec3( i1599[0], i1599[1], i1599[2] )
  i1598.extends = new pc.Vec3( i1599[3], i1599[4], i1599[5] )
  return i1598
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip+AnimationClipBindingConstant"] = function (request, data, root) {
  var i1602 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip+AnimationClipBindingConstant' )
  var i1603 = data
  var i1605 = i1603[0]
  var i1604 = []
  for(var i = 0; i < i1605.length; i += 1) {
    i1604.push( i1605[i + 0] );
  }
  i1602.genericBindings = i1604
  var i1607 = i1603[1]
  var i1606 = []
  for(var i = 0; i < i1607.length; i += 1) {
    i1606.push( i1607[i + 0] );
  }
  i1602.pptrCurveMapping = i1606
  return i1602
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font"] = function (request, data, root) {
  var i1608 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font' )
  var i1609 = data
  i1608.name = i1609[0]
  i1608.ascent = i1609[1]
  i1608.originalLineHeight = i1609[2]
  i1608.fontSize = i1609[3]
  var i1611 = i1609[4]
  var i1610 = []
  for(var i = 0; i < i1611.length; i += 1) {
    i1610.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo', i1611[i + 0]) );
  }
  i1608.characterInfo = i1610
  request.r(i1609[5], i1609[6], 0, i1608, 'texture')
  i1608.originalFontSize = i1609[7]
  return i1608
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo"] = function (request, data, root) {
  var i1614 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo' )
  var i1615 = data
  i1614.index = i1615[0]
  i1614.advance = i1615[1]
  i1614.bearing = i1615[2]
  i1614.glyphWidth = i1615[3]
  i1614.glyphHeight = i1615[4]
  i1614.minX = i1615[5]
  i1614.maxX = i1615[6]
  i1614.minY = i1615[7]
  i1614.maxY = i1615[8]
  i1614.uvBottomLeftX = i1615[9]
  i1614.uvBottomLeftY = i1615[10]
  i1614.uvBottomRightX = i1615[11]
  i1614.uvBottomRightY = i1615[12]
  i1614.uvTopLeftX = i1615[13]
  i1614.uvTopLeftY = i1615[14]
  i1614.uvTopRightX = i1615[15]
  i1614.uvTopRightY = i1615[16]
  return i1614
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorController"] = function (request, data, root) {
  var i1616 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorController' )
  var i1617 = data
  i1616.name = i1617[0]
  var i1619 = i1617[1]
  var i1618 = []
  for(var i = 0; i < i1619.length; i += 1) {
    i1618.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerLayer', i1619[i + 0]) );
  }
  i1616.layers = i1618
  var i1621 = i1617[2]
  var i1620 = []
  for(var i = 0; i < i1621.length; i += 1) {
    i1620.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerParameter', i1621[i + 0]) );
  }
  i1616.parameters = i1620
  i1616.animationClips = i1617[3]
  i1616.avatarUnsupported = i1617[4]
  return i1616
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerLayer"] = function (request, data, root) {
  var i1624 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerLayer' )
  var i1625 = data
  i1624.name = i1625[0]
  i1624.defaultWeight = i1625[1]
  i1624.blendingMode = i1625[2]
  i1624.avatarMask = i1625[3]
  i1624.syncedLayerIndex = i1625[4]
  i1624.syncedLayerAffectsTiming = !!i1625[5]
  i1624.syncedLayers = i1625[6]
  i1624.stateMachine = request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateMachine', i1625[7], i1624.stateMachine)
  return i1624
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateMachine"] = function (request, data, root) {
  var i1626 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateMachine' )
  var i1627 = data
  i1626.id = i1627[0]
  i1626.name = i1627[1]
  i1626.path = i1627[2]
  var i1629 = i1627[3]
  var i1628 = []
  for(var i = 0; i < i1629.length; i += 1) {
    i1628.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorState', i1629[i + 0]) );
  }
  i1626.states = i1628
  var i1631 = i1627[4]
  var i1630 = []
  for(var i = 0; i < i1631.length; i += 1) {
    i1630.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateMachine', i1631[i + 0]) );
  }
  i1626.machines = i1630
  var i1633 = i1627[5]
  var i1632 = []
  for(var i = 0; i < i1633.length; i += 1) {
    i1632.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorTransition', i1633[i + 0]) );
  }
  i1626.entryStateTransitions = i1632
  var i1635 = i1627[6]
  var i1634 = []
  for(var i = 0; i < i1635.length; i += 1) {
    i1634.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorTransition', i1635[i + 0]) );
  }
  i1626.exitStateTransitions = i1634
  var i1637 = i1627[7]
  var i1636 = []
  for(var i = 0; i < i1637.length; i += 1) {
    i1636.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateTransition', i1637[i + 0]) );
  }
  i1626.anyStateTransitions = i1636
  i1626.defaultStateId = i1627[8]
  return i1626
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorState"] = function (request, data, root) {
  var i1640 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorState' )
  var i1641 = data
  i1640.id = i1641[0]
  i1640.name = i1641[1]
  i1640.cycleOffset = i1641[2]
  i1640.cycleOffsetParameter = i1641[3]
  i1640.cycleOffsetParameterActive = !!i1641[4]
  i1640.mirror = !!i1641[5]
  i1640.mirrorParameter = i1641[6]
  i1640.mirrorParameterActive = !!i1641[7]
  i1640.motionId = i1641[8]
  i1640.nameHash = i1641[9]
  i1640.fullPathHash = i1641[10]
  i1640.speed = i1641[11]
  i1640.speedParameter = i1641[12]
  i1640.speedParameterActive = !!i1641[13]
  i1640.tag = i1641[14]
  i1640.tagHash = i1641[15]
  i1640.writeDefaultValues = !!i1641[16]
  var i1643 = i1641[17]
  var i1642 = []
  for(var i = 0; i < i1643.length; i += 2) {
  request.r(i1643[i + 0], i1643[i + 1], 2, i1642, '')
  }
  i1640.behaviours = i1642
  var i1645 = i1641[18]
  var i1644 = []
  for(var i = 0; i < i1645.length; i += 1) {
    i1644.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateTransition', i1645[i + 0]) );
  }
  i1640.transitions = i1644
  return i1640
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateTransition"] = function (request, data, root) {
  var i1650 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateTransition' )
  var i1651 = data
  i1650.fullPath = i1651[0]
  i1650.canTransitionToSelf = !!i1651[1]
  i1650.duration = i1651[2]
  i1650.exitTime = i1651[3]
  i1650.hasExitTime = !!i1651[4]
  i1650.hasFixedDuration = !!i1651[5]
  i1650.interruptionSource = i1651[6]
  i1650.offset = i1651[7]
  i1650.orderedInterruption = !!i1651[8]
  i1650.destinationStateId = i1651[9]
  i1650.isExit = !!i1651[10]
  i1650.mute = !!i1651[11]
  i1650.solo = !!i1651[12]
  var i1653 = i1651[13]
  var i1652 = []
  for(var i = 0; i < i1653.length; i += 1) {
    i1652.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorCondition', i1653[i + 0]) );
  }
  i1650.conditions = i1652
  return i1650
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorCondition"] = function (request, data, root) {
  var i1656 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorCondition' )
  var i1657 = data
  i1656.mode = i1657[0]
  i1656.parameter = i1657[1]
  i1656.threshold = i1657[2]
  return i1656
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorTransition"] = function (request, data, root) {
  var i1662 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorTransition' )
  var i1663 = data
  i1662.destinationStateId = i1663[0]
  i1662.isExit = !!i1663[1]
  i1662.mute = !!i1663[2]
  i1662.solo = !!i1663[3]
  var i1665 = i1663[4]
  var i1664 = []
  for(var i = 0; i < i1665.length; i += 1) {
    i1664.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorCondition', i1665[i + 0]) );
  }
  i1662.conditions = i1664
  return i1662
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerParameter"] = function (request, data, root) {
  var i1668 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerParameter' )
  var i1669 = data
  i1668.defaultBool = !!i1669[0]
  i1668.defaultFloat = i1669[1]
  i1668.defaultInt = i1669[2]
  i1668.name = i1669[3]
  i1668.nameHash = i1669[4]
  i1668.type = i1669[5]
  return i1668
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.TextAsset"] = function (request, data, root) {
  var i1670 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.TextAsset' )
  var i1671 = data
  i1670.name = i1671[0]
  i1670.bytes64 = i1671[1]
  i1670.data = i1671[2]
  return i1670
}

Deserializers["TMPro.TMP_FontAsset"] = function (request, data, root) {
  var i1672 = root || request.c( 'TMPro.TMP_FontAsset' )
  var i1673 = data
  request.r(i1673[0], i1673[1], 0, i1672, 'atlas')
  i1672.normalStyle = i1673[2]
  i1672.normalSpacingOffset = i1673[3]
  i1672.boldStyle = i1673[4]
  i1672.boldSpacing = i1673[5]
  i1672.italicStyle = i1673[6]
  i1672.tabSize = i1673[7]
  i1672.hashCode = i1673[8]
  request.r(i1673[9], i1673[10], 0, i1672, 'material')
  i1672.materialHashCode = i1673[11]
  i1672.m_Version = i1673[12]
  i1672.m_SourceFontFileGUID = i1673[13]
  request.r(i1673[14], i1673[15], 0, i1672, 'm_SourceFontFile_EditorRef')
  request.r(i1673[16], i1673[17], 0, i1672, 'm_SourceFontFile')
  i1672.m_AtlasPopulationMode = i1673[18]
  i1672.m_FaceInfo = request.d('UnityEngine.TextCore.FaceInfo', i1673[19], i1672.m_FaceInfo)
  var i1675 = i1673[20]
  var i1674 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.Glyph')))
  for(var i = 0; i < i1675.length; i += 1) {
    i1674.add(request.d('UnityEngine.TextCore.Glyph', i1675[i + 0]));
  }
  i1672.m_GlyphTable = i1674
  var i1677 = i1673[21]
  var i1676 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Character')))
  for(var i = 0; i < i1677.length; i += 1) {
    i1676.add(request.d('TMPro.TMP_Character', i1677[i + 0]));
  }
  i1672.m_CharacterTable = i1676
  var i1679 = i1673[22]
  var i1678 = []
  for(var i = 0; i < i1679.length; i += 2) {
  request.r(i1679[i + 0], i1679[i + 1], 2, i1678, '')
  }
  i1672.m_AtlasTextures = i1678
  i1672.m_AtlasTextureIndex = i1673[23]
  i1672.m_IsMultiAtlasTexturesEnabled = !!i1673[24]
  i1672.m_ClearDynamicDataOnBuild = !!i1673[25]
  var i1681 = i1673[26]
  var i1680 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.GlyphRect')))
  for(var i = 0; i < i1681.length; i += 1) {
    i1680.add(request.d('UnityEngine.TextCore.GlyphRect', i1681[i + 0]));
  }
  i1672.m_UsedGlyphRects = i1680
  var i1683 = i1673[27]
  var i1682 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.GlyphRect')))
  for(var i = 0; i < i1683.length; i += 1) {
    i1682.add(request.d('UnityEngine.TextCore.GlyphRect', i1683[i + 0]));
  }
  i1672.m_FreeGlyphRects = i1682
  i1672.m_fontInfo = request.d('TMPro.FaceInfo_Legacy', i1673[28], i1672.m_fontInfo)
  i1672.m_AtlasWidth = i1673[29]
  i1672.m_AtlasHeight = i1673[30]
  i1672.m_AtlasPadding = i1673[31]
  i1672.m_AtlasRenderMode = i1673[32]
  var i1685 = i1673[33]
  var i1684 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Glyph')))
  for(var i = 0; i < i1685.length; i += 1) {
    i1684.add(request.d('TMPro.TMP_Glyph', i1685[i + 0]));
  }
  i1672.m_glyphInfoList = i1684
  i1672.m_KerningTable = request.d('TMPro.KerningTable', i1673[34], i1672.m_KerningTable)
  i1672.m_FontFeatureTable = request.d('TMPro.TMP_FontFeatureTable', i1673[35], i1672.m_FontFeatureTable)
  var i1687 = i1673[36]
  var i1686 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_FontAsset')))
  for(var i = 0; i < i1687.length; i += 2) {
  request.r(i1687[i + 0], i1687[i + 1], 1, i1686, '')
  }
  i1672.fallbackFontAssets = i1686
  var i1689 = i1673[37]
  var i1688 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_FontAsset')))
  for(var i = 0; i < i1689.length; i += 2) {
  request.r(i1689[i + 0], i1689[i + 1], 1, i1688, '')
  }
  i1672.m_FallbackFontAssetTable = i1688
  i1672.m_CreationSettings = request.d('TMPro.FontAssetCreationSettings', i1673[38], i1672.m_CreationSettings)
  var i1691 = i1673[39]
  var i1690 = []
  for(var i = 0; i < i1691.length; i += 1) {
    i1690.push( request.d('TMPro.TMP_FontWeightPair', i1691[i + 0]) );
  }
  i1672.m_FontWeightTable = i1690
  var i1693 = i1673[40]
  var i1692 = []
  for(var i = 0; i < i1693.length; i += 1) {
    i1692.push( request.d('TMPro.TMP_FontWeightPair', i1693[i + 0]) );
  }
  i1672.fontWeights = i1692
  return i1672
}

Deserializers["UnityEngine.TextCore.FaceInfo"] = function (request, data, root) {
  var i1694 = root || request.c( 'UnityEngine.TextCore.FaceInfo' )
  var i1695 = data
  i1694.m_FaceIndex = i1695[0]
  i1694.m_FamilyName = i1695[1]
  i1694.m_StyleName = i1695[2]
  i1694.m_PointSize = i1695[3]
  i1694.m_Scale = i1695[4]
  i1694.m_UnitsPerEM = i1695[5]
  i1694.m_LineHeight = i1695[6]
  i1694.m_AscentLine = i1695[7]
  i1694.m_CapLine = i1695[8]
  i1694.m_MeanLine = i1695[9]
  i1694.m_Baseline = i1695[10]
  i1694.m_DescentLine = i1695[11]
  i1694.m_SuperscriptOffset = i1695[12]
  i1694.m_SuperscriptSize = i1695[13]
  i1694.m_SubscriptOffset = i1695[14]
  i1694.m_SubscriptSize = i1695[15]
  i1694.m_UnderlineOffset = i1695[16]
  i1694.m_UnderlineThickness = i1695[17]
  i1694.m_StrikethroughOffset = i1695[18]
  i1694.m_StrikethroughThickness = i1695[19]
  i1694.m_TabWidth = i1695[20]
  return i1694
}

Deserializers["UnityEngine.TextCore.Glyph"] = function (request, data, root) {
  var i1698 = root || request.c( 'UnityEngine.TextCore.Glyph' )
  var i1699 = data
  i1698.m_Index = i1699[0]
  i1698.m_Metrics = request.d('UnityEngine.TextCore.GlyphMetrics', i1699[1], i1698.m_Metrics)
  i1698.m_GlyphRect = request.d('UnityEngine.TextCore.GlyphRect', i1699[2], i1698.m_GlyphRect)
  i1698.m_Scale = i1699[3]
  i1698.m_AtlasIndex = i1699[4]
  i1698.m_ClassDefinitionType = i1699[5]
  return i1698
}

Deserializers["UnityEngine.TextCore.GlyphMetrics"] = function (request, data, root) {
  var i1700 = root || request.c( 'UnityEngine.TextCore.GlyphMetrics' )
  var i1701 = data
  i1700.m_Width = i1701[0]
  i1700.m_Height = i1701[1]
  i1700.m_HorizontalBearingX = i1701[2]
  i1700.m_HorizontalBearingY = i1701[3]
  i1700.m_HorizontalAdvance = i1701[4]
  return i1700
}

Deserializers["UnityEngine.TextCore.GlyphRect"] = function (request, data, root) {
  var i1702 = root || request.c( 'UnityEngine.TextCore.GlyphRect' )
  var i1703 = data
  i1702.m_X = i1703[0]
  i1702.m_Y = i1703[1]
  i1702.m_Width = i1703[2]
  i1702.m_Height = i1703[3]
  return i1702
}

Deserializers["TMPro.TMP_Character"] = function (request, data, root) {
  var i1706 = root || request.c( 'TMPro.TMP_Character' )
  var i1707 = data
  i1706.m_ElementType = i1707[0]
  i1706.m_Unicode = i1707[1]
  i1706.m_GlyphIndex = i1707[2]
  i1706.m_Scale = i1707[3]
  return i1706
}

Deserializers["TMPro.FaceInfo_Legacy"] = function (request, data, root) {
  var i1712 = root || request.c( 'TMPro.FaceInfo_Legacy' )
  var i1713 = data
  i1712.Name = i1713[0]
  i1712.PointSize = i1713[1]
  i1712.Scale = i1713[2]
  i1712.CharacterCount = i1713[3]
  i1712.LineHeight = i1713[4]
  i1712.Baseline = i1713[5]
  i1712.Ascender = i1713[6]
  i1712.CapHeight = i1713[7]
  i1712.Descender = i1713[8]
  i1712.CenterLine = i1713[9]
  i1712.SuperscriptOffset = i1713[10]
  i1712.SubscriptOffset = i1713[11]
  i1712.SubSize = i1713[12]
  i1712.Underline = i1713[13]
  i1712.UnderlineThickness = i1713[14]
  i1712.strikethrough = i1713[15]
  i1712.strikethroughThickness = i1713[16]
  i1712.TabWidth = i1713[17]
  i1712.Padding = i1713[18]
  i1712.AtlasWidth = i1713[19]
  i1712.AtlasHeight = i1713[20]
  return i1712
}

Deserializers["TMPro.TMP_Glyph"] = function (request, data, root) {
  var i1716 = root || request.c( 'TMPro.TMP_Glyph' )
  var i1717 = data
  i1716.id = i1717[0]
  i1716.x = i1717[1]
  i1716.y = i1717[2]
  i1716.width = i1717[3]
  i1716.height = i1717[4]
  i1716.xOffset = i1717[5]
  i1716.yOffset = i1717[6]
  i1716.xAdvance = i1717[7]
  i1716.scale = i1717[8]
  return i1716
}

Deserializers["TMPro.KerningTable"] = function (request, data, root) {
  var i1718 = root || request.c( 'TMPro.KerningTable' )
  var i1719 = data
  var i1721 = i1719[0]
  var i1720 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.KerningPair')))
  for(var i = 0; i < i1721.length; i += 1) {
    i1720.add(request.d('TMPro.KerningPair', i1721[i + 0]));
  }
  i1718.kerningPairs = i1720
  return i1718
}

Deserializers["TMPro.KerningPair"] = function (request, data, root) {
  var i1724 = root || request.c( 'TMPro.KerningPair' )
  var i1725 = data
  i1724.xOffset = i1725[0]
  i1724.m_FirstGlyph = i1725[1]
  i1724.m_FirstGlyphAdjustments = request.d('TMPro.GlyphValueRecord_Legacy', i1725[2], i1724.m_FirstGlyphAdjustments)
  i1724.m_SecondGlyph = i1725[3]
  i1724.m_SecondGlyphAdjustments = request.d('TMPro.GlyphValueRecord_Legacy', i1725[4], i1724.m_SecondGlyphAdjustments)
  i1724.m_IgnoreSpacingAdjustments = !!i1725[5]
  return i1724
}

Deserializers["TMPro.TMP_FontFeatureTable"] = function (request, data, root) {
  var i1726 = root || request.c( 'TMPro.TMP_FontFeatureTable' )
  var i1727 = data
  var i1729 = i1727[0]
  var i1728 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_GlyphPairAdjustmentRecord')))
  for(var i = 0; i < i1729.length; i += 1) {
    i1728.add(request.d('TMPro.TMP_GlyphPairAdjustmentRecord', i1729[i + 0]));
  }
  i1726.m_GlyphPairAdjustmentRecords = i1728
  return i1726
}

Deserializers["TMPro.TMP_GlyphPairAdjustmentRecord"] = function (request, data, root) {
  var i1732 = root || request.c( 'TMPro.TMP_GlyphPairAdjustmentRecord' )
  var i1733 = data
  i1732.m_FirstAdjustmentRecord = request.d('TMPro.TMP_GlyphAdjustmentRecord', i1733[0], i1732.m_FirstAdjustmentRecord)
  i1732.m_SecondAdjustmentRecord = request.d('TMPro.TMP_GlyphAdjustmentRecord', i1733[1], i1732.m_SecondAdjustmentRecord)
  i1732.m_FeatureLookupFlags = i1733[2]
  return i1732
}

Deserializers["TMPro.FontAssetCreationSettings"] = function (request, data, root) {
  var i1736 = root || request.c( 'TMPro.FontAssetCreationSettings' )
  var i1737 = data
  i1736.sourceFontFileName = i1737[0]
  i1736.sourceFontFileGUID = i1737[1]
  i1736.pointSizeSamplingMode = i1737[2]
  i1736.pointSize = i1737[3]
  i1736.padding = i1737[4]
  i1736.packingMode = i1737[5]
  i1736.atlasWidth = i1737[6]
  i1736.atlasHeight = i1737[7]
  i1736.characterSetSelectionMode = i1737[8]
  i1736.characterSequence = i1737[9]
  i1736.referencedFontAssetGUID = i1737[10]
  i1736.referencedTextAssetGUID = i1737[11]
  i1736.fontStyle = i1737[12]
  i1736.fontStyleModifier = i1737[13]
  i1736.renderMode = i1737[14]
  i1736.includeFontFeatures = !!i1737[15]
  return i1736
}

Deserializers["TMPro.TMP_FontWeightPair"] = function (request, data, root) {
  var i1740 = root || request.c( 'TMPro.TMP_FontWeightPair' )
  var i1741 = data
  request.r(i1741[0], i1741[1], 0, i1740, 'regularTypeface')
  request.r(i1741[2], i1741[3], 0, i1740, 'italicTypeface')
  return i1740
}

Deserializers["LevelGamePlayConfigSO"] = function (request, data, root) {
  var i1742 = root || request.c( 'LevelGamePlayConfigSO' )
  var i1743 = data
  var i1745 = i1743[0]
  var i1744 = new (System.Collections.Generic.List$1(Bridge.ns('LevelConfig')))
  for(var i = 0; i < i1745.length; i += 2) {
  request.r(i1745[i + 0], i1745[i + 1], 1, i1744, '')
  }
  i1742.levelConfigDataList = i1744
  return i1742
}

Deserializers["LevelConfig"] = function (request, data, root) {
  var i1748 = root || request.c( 'LevelConfig' )
  var i1749 = data
  request.r(i1749[0], i1749[1], 0, i1748, 'BlocksPaintingConfig')
  request.r(i1749[2], i1749[3], 0, i1748, 'CollectorsConfig')
  var i1751 = i1749[4]
  var i1750 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i1751.length; i += 1) {
    i1750.add(i1751[i + 0]);
  }
  i1748.ColorsUsed = i1750
  return i1748
}

Deserializers["PaintingConfig"] = function (request, data, root) {
  var i1752 = root || request.c( 'PaintingConfig' )
  var i1753 = data
  i1752.PaintingSize = new pc.Vec2( i1753[0], i1753[1] )
  request.r(i1753[2], i1753[3], 0, i1752, 'Sprite')
  var i1755 = i1753[4]
  var i1754 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1755.length; i += 1) {
    i1754.add(request.d('PaintingPixelConfig', i1755[i + 0]));
  }
  i1752.Pixels = i1754
  var i1757 = i1753[5]
  var i1756 = new (System.Collections.Generic.List$1(Bridge.ns('PipeObjectSetup')))
  for(var i = 0; i < i1757.length; i += 1) {
    i1756.add(request.d('PipeObjectSetup', i1757[i + 0]));
  }
  i1752.PipeSetups = i1756
  var i1759 = i1753[6]
  var i1758 = new (System.Collections.Generic.List$1(Bridge.ns('WallObjectSetup')))
  for(var i = 0; i < i1759.length; i += 1) {
    i1758.add(request.d('WallObjectSetup', i1759[i + 0]));
  }
  i1752.WallSetups = i1758
  var i1761 = i1753[7]
  var i1760 = new (System.Collections.Generic.List$1(Bridge.ns('KeyObjectSetup')))
  for(var i = 0; i < i1761.length; i += 1) {
    i1760.add(request.d('KeyObjectSetup', i1761[i + 0]));
  }
  i1752.KeySetups = i1760
  var i1763 = i1753[8]
  var i1762 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1763.length; i += 1) {
    i1762.add(request.d('PaintingPixelConfig', i1763[i + 0]));
  }
  i1752.AdditionPixels = i1762
  var i1765 = i1753[9]
  var i1764 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingSharedAttributes+BlockFountainObjectSetup')))
  for(var i = 0; i < i1765.length; i += 1) {
    i1764.add(request.d('PaintingSharedAttributes+BlockFountainObjectSetup', i1765[i + 0]));
  }
  i1752.BlockFountainSetup = i1764
  var i1767 = i1753[10]
  var i1766 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingConfigBackUp')))
  for(var i = 0; i < i1767.length; i += 1) {
    i1766.add(request.d('PaintingConfigBackUp', i1767[i + 0]));
  }
  i1752.BackUpVariants = i1766
  i1752.CurrentBackUpIndex = i1753[11]
  return i1752
}

Deserializers["PaintingPixelConfig"] = function (request, data, root) {
  var i1770 = root || request.c( 'PaintingPixelConfig' )
  var i1771 = data
  i1770.column = i1771[0]
  i1770.row = i1771[1]
  i1770.color = new pc.Color(i1771[2], i1771[3], i1771[4], i1771[5])
  i1770.colorCode = i1771[6]
  i1770.Hidden = !!i1771[7]
  return i1770
}

Deserializers["PipeObjectSetup"] = function (request, data, root) {
  var i1774 = root || request.c( 'PipeObjectSetup' )
  var i1775 = data
  i1774.Hearts = i1775[0]
  var i1777 = i1775[1]
  var i1776 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1777.length; i += 1) {
    i1776.add(request.d('PaintingPixelConfig', i1777[i + 0]));
  }
  i1774.PixelCovered = i1776
  i1774.ColorCode = i1775[2]
  i1774.Scale = new pc.Vec3( i1775[3], i1775[4], i1775[5] )
  return i1774
}

Deserializers["WallObjectSetup"] = function (request, data, root) {
  var i1780 = root || request.c( 'WallObjectSetup' )
  var i1781 = data
  i1780.Hearts = i1781[0]
  var i1783 = i1781[1]
  var i1782 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1783.length; i += 1) {
    i1782.add(request.d('PaintingPixelConfig', i1783[i + 0]));
  }
  i1780.PixelCovered = i1782
  i1780.ColorCode = i1781[2]
  return i1780
}

Deserializers["KeyObjectSetup"] = function (request, data, root) {
  var i1786 = root || request.c( 'KeyObjectSetup' )
  var i1787 = data
  var i1789 = i1787[0]
  var i1788 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1789.length; i += 1) {
    i1788.add(request.d('PaintingPixelConfig', i1789[i + 0]));
  }
  i1786.PixelCovered = i1788
  i1786.ColorCode = i1787[1]
  return i1786
}

Deserializers["PaintingSharedAttributes+BlockFountainObjectSetup"] = function (request, data, root) {
  var i1792 = root || request.c( 'PaintingSharedAttributes+BlockFountainObjectSetup' )
  var i1793 = data
  var i1795 = i1793[0]
  var i1794 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingSharedAttributes+BlockFountainBulletSet')))
  for(var i = 0; i < i1795.length; i += 1) {
    i1794.add(request.d('PaintingSharedAttributes+BlockFountainBulletSet', i1795[i + 0]));
  }
  i1792.BlockSets = i1794
  var i1797 = i1793[1]
  var i1796 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1797.length; i += 1) {
    i1796.add(request.d('PaintingPixelConfig', i1797[i + 0]));
  }
  i1792.PixelCovered = i1796
  return i1792
}

Deserializers["PaintingConfigBackUp"] = function (request, data, root) {
  var i1800 = root || request.c( 'PaintingConfigBackUp' )
  var i1801 = data
  i1800.DateTime = i1801[0]
  i1800._paintingSize = new pc.Vec2( i1801[1], i1801[2] )
  var i1803 = i1801[3]
  var i1802 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1803.length; i += 1) {
    i1802.add(request.d('PaintingPixelConfig', i1803[i + 0]));
  }
  i1800.Pixels = i1802
  var i1805 = i1801[4]
  var i1804 = new (System.Collections.Generic.List$1(Bridge.ns('PipeObjectSetup')))
  for(var i = 0; i < i1805.length; i += 1) {
    i1804.add(request.d('PipeObjectSetup', i1805[i + 0]));
  }
  i1800.PipeSetup = i1804
  var i1807 = i1801[5]
  var i1806 = new (System.Collections.Generic.List$1(Bridge.ns('WallObjectSetup')))
  for(var i = 0; i < i1807.length; i += 1) {
    i1806.add(request.d('WallObjectSetup', i1807[i + 0]));
  }
  i1800.WallSetup = i1806
  var i1809 = i1801[6]
  var i1808 = new (System.Collections.Generic.List$1(Bridge.ns('KeyObjectSetup')))
  for(var i = 0; i < i1809.length; i += 1) {
    i1808.add(request.d('KeyObjectSetup', i1809[i + 0]));
  }
  i1800.KeySetup = i1808
  var i1811 = i1801[7]
  var i1810 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1811.length; i += 1) {
    i1810.add(request.d('PaintingPixelConfig', i1811[i + 0]));
  }
  i1800.AdditionPixels = i1810
  return i1800
}

Deserializers["LevelColorCollectorsConfig"] = function (request, data, root) {
  var i1812 = root || request.c( 'LevelColorCollectorsConfig' )
  var i1813 = data
  var i1815 = i1813[0]
  var i1814 = new (System.Collections.Generic.List$1(Bridge.ns('ColumnOfCollectorConfig')))
  for(var i = 0; i < i1815.length; i += 1) {
    i1814.add(request.d('ColumnOfCollectorConfig', i1815[i + 0]));
  }
  i1812.CollectorColumns = i1814
  var i1817 = i1813[1]
  var i1816 = new (System.Collections.Generic.List$1(Bridge.ns('LevelColorCollectorsConfigBackUp')))
  for(var i = 0; i < i1817.length; i += 1) {
    i1816.add(request.d('LevelColorCollectorsConfigBackUp', i1817[i + 0]));
  }
  i1812.BackUpVariants = i1816
  i1812.CurrentBackUpIndex = i1813[2]
  return i1812
}

Deserializers["ColumnOfCollectorConfig"] = function (request, data, root) {
  var i1820 = root || request.c( 'ColumnOfCollectorConfig' )
  var i1821 = data
  var i1823 = i1821[0]
  var i1822 = new (System.Collections.Generic.List$1(Bridge.ns('SingleColorCollectorConfig')))
  for(var i = 0; i < i1823.length; i += 1) {
    i1822.add(request.d('SingleColorCollectorConfig', i1823[i + 0]));
  }
  i1820.Collectors = i1822
  var i1825 = i1821[1]
  var i1824 = new (System.Collections.Generic.List$1(Bridge.ns('LockObjectConfig')))
  for(var i = 0; i < i1825.length; i += 1) {
    i1824.add(request.d('LockObjectConfig', i1825[i + 0]));
  }
  i1820.Locks = i1824
  return i1820
}

Deserializers["SingleColorCollectorConfig"] = function (request, data, root) {
  var i1828 = root || request.c( 'SingleColorCollectorConfig' )
  var i1829 = data
  i1828.ID = i1829[0]
  i1828.ColorCode = i1829[1]
  i1828.Bullets = i1829[2]
  i1828.Locked = !!i1829[3]
  i1828.Hidden = !!i1829[4]
  var i1831 = i1829[5]
  var i1830 = new (System.Collections.Generic.List$1(Bridge.ns('System.Int32')))
  for(var i = 0; i < i1831.length; i += 1) {
    i1830.add(i1831[i + 0]);
  }
  i1828.ConnectedCollectorsIDs = i1830
  return i1828
}

Deserializers["LockObjectConfig"] = function (request, data, root) {
  var i1834 = root || request.c( 'LockObjectConfig' )
  var i1835 = data
  i1834.ID = i1835[0]
  i1834.Row = i1835[1]
  return i1834
}

Deserializers["LevelColorCollectorsConfigBackUp"] = function (request, data, root) {
  var i1838 = root || request.c( 'LevelColorCollectorsConfigBackUp' )
  var i1839 = data
  i1838.DateTime = i1839[0]
  var i1841 = i1839[1]
  var i1840 = new (System.Collections.Generic.List$1(Bridge.ns('ColumnOfCollectorConfig')))
  for(var i = 0; i < i1841.length; i += 1) {
    i1840.add(request.d('ColumnOfCollectorConfig', i1841[i + 0]));
  }
  i1838.CollectorColumns = i1840
  return i1838
}

Deserializers["InGameEffectOptions"] = function (request, data, root) {
  var i1842 = root || request.c( 'InGameEffectOptions' )
  var i1843 = data
  i1842.BulletSpeed = i1843[0]
  i1842.BulletScale = i1843[1]
  i1842.ChangeBulletColor = !!i1843[2]
  i1842.ChangeOutlineColor = !!i1843[3]
  i1842.IdleRate = i1843[4]
  i1842.RabbitRandomIdleAnimation = !!i1843[5]
  i1842.RabbitEarAnimation = !!i1843[6]
  i1842.ShakeNeighborBlocks = !!i1843[7]
  i1842.ShakeValue = i1843[8]
  i1842.BlockScaleFactorWhenDestroyed = i1843[9]
  return i1842
}

Deserializers["ColorPalleteData"] = function (request, data, root) {
  var i1844 = root || request.c( 'ColorPalleteData' )
  var i1845 = data
  var i1847 = i1845[0]
  var i1846 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i1847.length; i += 1) {
    i1846.add(i1847[i + 0]);
  }
  i1844.ColorKeys = i1846
  var i1849 = i1845[1]
  var i1848 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Color')))
  for(var i = 0; i < i1849.length; i += 4) {
    i1848.add(new pc.Color(i1849[i + 0], i1849[i + 1], i1849[i + 2], i1849[i + 3]));
  }
  i1844.ColorsValues = i1848
  var i1851 = i1845[2]
  var i1850 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Material')))
  for(var i = 0; i < i1851.length; i += 2) {
  request.r(i1851[i + 0], i1851[i + 1], 1, i1850, '')
  }
  i1844.MatValues = i1850
  return i1844
}

Deserializers["LevelMechanicObjectPrefabs"] = function (request, data, root) {
  var i1856 = root || request.c( 'LevelMechanicObjectPrefabs' )
  var i1857 = data
  request.r(i1857[0], i1857[1], 0, i1856, 'PipeObjectPrefab')
  request.r(i1857[2], i1857[3], 0, i1856, 'PipeHeadPrefab')
  request.r(i1857[4], i1857[5], 0, i1856, 'PipeBodyPrefab')
  request.r(i1857[6], i1857[7], 0, i1856, 'PipeTailPrefab')
  request.r(i1857[8], i1857[9], 0, i1856, 'KeyObjectPrefab')
  request.r(i1857[10], i1857[11], 0, i1856, 'LockPrefab')
  request.r(i1857[12], i1857[13], 0, i1856, 'BigBlockPrefab')
  request.r(i1857[14], i1857[15], 0, i1856, 'DefaultBlockPrefab')
  request.r(i1857[16], i1857[17], 0, i1856, 'GunnerPrefab')
  request.r(i1857[18], i1857[19], 0, i1856, 'ColorPallete')
  request.r(i1857[20], i1857[21], 0, i1856, 'BlockFountainPrefab')
  request.r(i1857[22], i1857[23], 0, i1856, 'FountainProjectilePrefab')
  return i1856
}

Deserializers["DG.Tweening.Core.DOTweenSettings"] = function (request, data, root) {
  var i1858 = root || request.c( 'DG.Tweening.Core.DOTweenSettings' )
  var i1859 = data
  i1858.useSafeMode = !!i1859[0]
  i1858.safeModeOptions = request.d('DG.Tweening.Core.DOTweenSettings+SafeModeOptions', i1859[1], i1858.safeModeOptions)
  i1858.timeScale = i1859[2]
  i1858.unscaledTimeScale = i1859[3]
  i1858.useSmoothDeltaTime = !!i1859[4]
  i1858.maxSmoothUnscaledTime = i1859[5]
  i1858.rewindCallbackMode = i1859[6]
  i1858.showUnityEditorReport = !!i1859[7]
  i1858.logBehaviour = i1859[8]
  i1858.drawGizmos = !!i1859[9]
  i1858.defaultRecyclable = !!i1859[10]
  i1858.defaultAutoPlay = i1859[11]
  i1858.defaultUpdateType = i1859[12]
  i1858.defaultTimeScaleIndependent = !!i1859[13]
  i1858.defaultEaseType = i1859[14]
  i1858.defaultEaseOvershootOrAmplitude = i1859[15]
  i1858.defaultEasePeriod = i1859[16]
  i1858.defaultAutoKill = !!i1859[17]
  i1858.defaultLoopType = i1859[18]
  i1858.debugMode = !!i1859[19]
  i1858.debugStoreTargetId = !!i1859[20]
  i1858.showPreviewPanel = !!i1859[21]
  i1858.storeSettingsLocation = i1859[22]
  i1858.modules = request.d('DG.Tweening.Core.DOTweenSettings+ModulesSetup', i1859[23], i1858.modules)
  i1858.createASMDEF = !!i1859[24]
  i1858.showPlayingTweens = !!i1859[25]
  i1858.showPausedTweens = !!i1859[26]
  return i1858
}

Deserializers["DG.Tweening.Core.DOTweenSettings+SafeModeOptions"] = function (request, data, root) {
  var i1860 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+SafeModeOptions' )
  var i1861 = data
  i1860.logBehaviour = i1861[0]
  i1860.nestedTweenFailureBehaviour = i1861[1]
  return i1860
}

Deserializers["DG.Tweening.Core.DOTweenSettings+ModulesSetup"] = function (request, data, root) {
  var i1862 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+ModulesSetup' )
  var i1863 = data
  i1862.showPanel = !!i1863[0]
  i1862.audioEnabled = !!i1863[1]
  i1862.physicsEnabled = !!i1863[2]
  i1862.physics2DEnabled = !!i1863[3]
  i1862.spriteEnabled = !!i1863[4]
  i1862.uiEnabled = !!i1863[5]
  i1862.textMeshProEnabled = !!i1863[6]
  i1862.tk2DEnabled = !!i1863[7]
  i1862.deAudioEnabled = !!i1863[8]
  i1862.deUnityExtendedEnabled = !!i1863[9]
  i1862.epoOutlineEnabled = !!i1863[10]
  return i1862
}

Deserializers["TMPro.TMP_Settings"] = function (request, data, root) {
  var i1864 = root || request.c( 'TMPro.TMP_Settings' )
  var i1865 = data
  i1864.m_enableWordWrapping = !!i1865[0]
  i1864.m_enableKerning = !!i1865[1]
  i1864.m_enableExtraPadding = !!i1865[2]
  i1864.m_enableTintAllSprites = !!i1865[3]
  i1864.m_enableParseEscapeCharacters = !!i1865[4]
  i1864.m_EnableRaycastTarget = !!i1865[5]
  i1864.m_GetFontFeaturesAtRuntime = !!i1865[6]
  i1864.m_missingGlyphCharacter = i1865[7]
  i1864.m_warningsDisabled = !!i1865[8]
  request.r(i1865[9], i1865[10], 0, i1864, 'm_defaultFontAsset')
  i1864.m_defaultFontAssetPath = i1865[11]
  i1864.m_defaultFontSize = i1865[12]
  i1864.m_defaultAutoSizeMinRatio = i1865[13]
  i1864.m_defaultAutoSizeMaxRatio = i1865[14]
  i1864.m_defaultTextMeshProTextContainerSize = new pc.Vec2( i1865[15], i1865[16] )
  i1864.m_defaultTextMeshProUITextContainerSize = new pc.Vec2( i1865[17], i1865[18] )
  i1864.m_autoSizeTextContainer = !!i1865[19]
  i1864.m_IsTextObjectScaleStatic = !!i1865[20]
  var i1867 = i1865[21]
  var i1866 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_FontAsset')))
  for(var i = 0; i < i1867.length; i += 2) {
  request.r(i1867[i + 0], i1867[i + 1], 1, i1866, '')
  }
  i1864.m_fallbackFontAssets = i1866
  i1864.m_matchMaterialPreset = !!i1865[22]
  request.r(i1865[23], i1865[24], 0, i1864, 'm_defaultSpriteAsset')
  i1864.m_defaultSpriteAssetPath = i1865[25]
  i1864.m_enableEmojiSupport = !!i1865[26]
  i1864.m_MissingCharacterSpriteUnicode = i1865[27]
  i1864.m_defaultColorGradientPresetsPath = i1865[28]
  request.r(i1865[29], i1865[30], 0, i1864, 'm_defaultStyleSheet')
  i1864.m_StyleSheetsResourcePath = i1865[31]
  request.r(i1865[32], i1865[33], 0, i1864, 'm_leadingCharacters')
  request.r(i1865[34], i1865[35], 0, i1864, 'm_followingCharacters')
  i1864.m_UseModernHangulLineBreakingRules = !!i1865[36]
  return i1864
}

Deserializers["TMPro.TMP_SpriteAsset"] = function (request, data, root) {
  var i1868 = root || request.c( 'TMPro.TMP_SpriteAsset' )
  var i1869 = data
  request.r(i1869[0], i1869[1], 0, i1868, 'spriteSheet')
  var i1871 = i1869[2]
  var i1870 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Sprite')))
  for(var i = 0; i < i1871.length; i += 1) {
    i1870.add(request.d('TMPro.TMP_Sprite', i1871[i + 0]));
  }
  i1868.spriteInfoList = i1870
  var i1873 = i1869[3]
  var i1872 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_SpriteAsset')))
  for(var i = 0; i < i1873.length; i += 2) {
  request.r(i1873[i + 0], i1873[i + 1], 1, i1872, '')
  }
  i1868.fallbackSpriteAssets = i1872
  i1868.hashCode = i1869[4]
  request.r(i1869[5], i1869[6], 0, i1868, 'material')
  i1868.materialHashCode = i1869[7]
  i1868.m_Version = i1869[8]
  i1868.m_FaceInfo = request.d('UnityEngine.TextCore.FaceInfo', i1869[9], i1868.m_FaceInfo)
  var i1875 = i1869[10]
  var i1874 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_SpriteCharacter')))
  for(var i = 0; i < i1875.length; i += 1) {
    i1874.add(request.d('TMPro.TMP_SpriteCharacter', i1875[i + 0]));
  }
  i1868.m_SpriteCharacterTable = i1874
  var i1877 = i1869[11]
  var i1876 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_SpriteGlyph')))
  for(var i = 0; i < i1877.length; i += 1) {
    i1876.add(request.d('TMPro.TMP_SpriteGlyph', i1877[i + 0]));
  }
  i1868.m_SpriteGlyphTable = i1876
  return i1868
}

Deserializers["TMPro.TMP_Sprite"] = function (request, data, root) {
  var i1880 = root || request.c( 'TMPro.TMP_Sprite' )
  var i1881 = data
  i1880.name = i1881[0]
  i1880.hashCode = i1881[1]
  i1880.unicode = i1881[2]
  i1880.pivot = new pc.Vec2( i1881[3], i1881[4] )
  request.r(i1881[5], i1881[6], 0, i1880, 'sprite')
  i1880.id = i1881[7]
  i1880.x = i1881[8]
  i1880.y = i1881[9]
  i1880.width = i1881[10]
  i1880.height = i1881[11]
  i1880.xOffset = i1881[12]
  i1880.yOffset = i1881[13]
  i1880.xAdvance = i1881[14]
  i1880.scale = i1881[15]
  return i1880
}

Deserializers["TMPro.TMP_SpriteCharacter"] = function (request, data, root) {
  var i1886 = root || request.c( 'TMPro.TMP_SpriteCharacter' )
  var i1887 = data
  i1886.m_Name = i1887[0]
  i1886.m_HashCode = i1887[1]
  i1886.m_ElementType = i1887[2]
  i1886.m_Unicode = i1887[3]
  i1886.m_GlyphIndex = i1887[4]
  i1886.m_Scale = i1887[5]
  return i1886
}

Deserializers["TMPro.TMP_SpriteGlyph"] = function (request, data, root) {
  var i1890 = root || request.c( 'TMPro.TMP_SpriteGlyph' )
  var i1891 = data
  request.r(i1891[0], i1891[1], 0, i1890, 'sprite')
  i1890.m_Index = i1891[2]
  i1890.m_Metrics = request.d('UnityEngine.TextCore.GlyphMetrics', i1891[3], i1890.m_Metrics)
  i1890.m_GlyphRect = request.d('UnityEngine.TextCore.GlyphRect', i1891[4], i1890.m_GlyphRect)
  i1890.m_Scale = i1891[5]
  i1890.m_AtlasIndex = i1891[6]
  i1890.m_ClassDefinitionType = i1891[7]
  return i1890
}

Deserializers["TMPro.TMP_StyleSheet"] = function (request, data, root) {
  var i1892 = root || request.c( 'TMPro.TMP_StyleSheet' )
  var i1893 = data
  var i1895 = i1893[0]
  var i1894 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Style')))
  for(var i = 0; i < i1895.length; i += 1) {
    i1894.add(request.d('TMPro.TMP_Style', i1895[i + 0]));
  }
  i1892.m_StyleList = i1894
  return i1892
}

Deserializers["TMPro.TMP_Style"] = function (request, data, root) {
  var i1898 = root || request.c( 'TMPro.TMP_Style' )
  var i1899 = data
  i1898.m_Name = i1899[0]
  i1898.m_HashCode = i1899[1]
  i1898.m_OpeningDefinition = i1899[2]
  i1898.m_ClosingDefinition = i1899[3]
  i1898.m_OpeningTagArray = i1899[4]
  i1898.m_ClosingTagArray = i1899[5]
  i1898.m_OpeningTagUnicodeArray = i1899[6]
  i1898.m_ClosingTagUnicodeArray = i1899[7]
  return i1898
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i1900 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i1901 = data
  var i1903 = i1901[0]
  var i1902 = []
  for(var i = 0; i < i1903.length; i += 1) {
    i1902.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i1903[i + 0]) );
  }
  i1900.files = i1902
  i1900.componentToPrefabIds = i1901[1]
  return i1900
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i1906 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i1907 = data
  i1906.path = i1907[0]
  request.r(i1907[1], i1907[2], 0, i1906, 'unityObject')
  return i1906
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i1908 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i1909 = data
  var i1911 = i1909[0]
  var i1910 = []
  for(var i = 0; i < i1911.length; i += 1) {
    i1910.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i1911[i + 0]) );
  }
  i1908.scriptsExecutionOrder = i1910
  var i1913 = i1909[1]
  var i1912 = []
  for(var i = 0; i < i1913.length; i += 1) {
    i1912.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i1913[i + 0]) );
  }
  i1908.sortingLayers = i1912
  var i1915 = i1909[2]
  var i1914 = []
  for(var i = 0; i < i1915.length; i += 1) {
    i1914.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i1915[i + 0]) );
  }
  i1908.cullingLayers = i1914
  i1908.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i1909[3], i1908.timeSettings)
  i1908.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i1909[4], i1908.physicsSettings)
  i1908.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i1909[5], i1908.physics2DSettings)
  i1908.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i1909[6], i1908.qualitySettings)
  i1908.enableRealtimeShadows = !!i1909[7]
  i1908.enableAutoInstancing = !!i1909[8]
  i1908.enableStaticBatching = !!i1909[9]
  i1908.enableDynamicBatching = !!i1909[10]
  i1908.lightmapEncodingQuality = i1909[11]
  i1908.desiredColorSpace = i1909[12]
  var i1917 = i1909[13]
  var i1916 = []
  for(var i = 0; i < i1917.length; i += 1) {
    i1916.push( i1917[i + 0] );
  }
  i1908.allTags = i1916
  return i1908
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i1920 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i1921 = data
  i1920.name = i1921[0]
  i1920.value = i1921[1]
  return i1920
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i1924 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i1925 = data
  i1924.id = i1925[0]
  i1924.name = i1925[1]
  i1924.value = i1925[2]
  return i1924
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i1928 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i1929 = data
  i1928.id = i1929[0]
  i1928.name = i1929[1]
  return i1928
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i1930 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i1931 = data
  i1930.fixedDeltaTime = i1931[0]
  i1930.maximumDeltaTime = i1931[1]
  i1930.timeScale = i1931[2]
  i1930.maximumParticleTimestep = i1931[3]
  return i1930
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i1932 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i1933 = data
  i1932.gravity = new pc.Vec3( i1933[0], i1933[1], i1933[2] )
  i1932.defaultSolverIterations = i1933[3]
  i1932.bounceThreshold = i1933[4]
  i1932.autoSyncTransforms = !!i1933[5]
  i1932.autoSimulation = !!i1933[6]
  var i1935 = i1933[7]
  var i1934 = []
  for(var i = 0; i < i1935.length; i += 1) {
    i1934.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i1935[i + 0]) );
  }
  i1932.collisionMatrix = i1934
  return i1932
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i1938 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i1939 = data
  i1938.enabled = !!i1939[0]
  i1938.layerId = i1939[1]
  i1938.otherLayerId = i1939[2]
  return i1938
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i1940 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i1941 = data
  request.r(i1941[0], i1941[1], 0, i1940, 'material')
  i1940.gravity = new pc.Vec2( i1941[2], i1941[3] )
  i1940.positionIterations = i1941[4]
  i1940.velocityIterations = i1941[5]
  i1940.velocityThreshold = i1941[6]
  i1940.maxLinearCorrection = i1941[7]
  i1940.maxAngularCorrection = i1941[8]
  i1940.maxTranslationSpeed = i1941[9]
  i1940.maxRotationSpeed = i1941[10]
  i1940.baumgarteScale = i1941[11]
  i1940.baumgarteTOIScale = i1941[12]
  i1940.timeToSleep = i1941[13]
  i1940.linearSleepTolerance = i1941[14]
  i1940.angularSleepTolerance = i1941[15]
  i1940.defaultContactOffset = i1941[16]
  i1940.autoSimulation = !!i1941[17]
  i1940.queriesHitTriggers = !!i1941[18]
  i1940.queriesStartInColliders = !!i1941[19]
  i1940.callbacksOnDisable = !!i1941[20]
  i1940.reuseCollisionCallbacks = !!i1941[21]
  i1940.autoSyncTransforms = !!i1941[22]
  var i1943 = i1941[23]
  var i1942 = []
  for(var i = 0; i < i1943.length; i += 1) {
    i1942.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i1943[i + 0]) );
  }
  i1940.collisionMatrix = i1942
  return i1940
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i1946 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i1947 = data
  i1946.enabled = !!i1947[0]
  i1946.layerId = i1947[1]
  i1946.otherLayerId = i1947[2]
  return i1946
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i1948 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i1949 = data
  var i1951 = i1949[0]
  var i1950 = []
  for(var i = 0; i < i1951.length; i += 1) {
    i1950.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i1951[i + 0]) );
  }
  i1948.qualityLevels = i1950
  var i1953 = i1949[1]
  var i1952 = []
  for(var i = 0; i < i1953.length; i += 1) {
    i1952.push( i1953[i + 0] );
  }
  i1948.names = i1952
  i1948.shadows = i1949[2]
  i1948.anisotropicFiltering = i1949[3]
  i1948.antiAliasing = i1949[4]
  i1948.lodBias = i1949[5]
  i1948.shadowCascades = i1949[6]
  i1948.shadowDistance = i1949[7]
  i1948.shadowmaskMode = i1949[8]
  i1948.shadowProjection = i1949[9]
  i1948.shadowResolution = i1949[10]
  i1948.softParticles = !!i1949[11]
  i1948.softVegetation = !!i1949[12]
  i1948.activeColorSpace = i1949[13]
  i1948.desiredColorSpace = i1949[14]
  i1948.masterTextureLimit = i1949[15]
  i1948.maxQueuedFrames = i1949[16]
  i1948.particleRaycastBudget = i1949[17]
  i1948.pixelLightCount = i1949[18]
  i1948.realtimeReflectionProbes = !!i1949[19]
  i1948.shadowCascade2Split = i1949[20]
  i1948.shadowCascade4Split = new pc.Vec3( i1949[21], i1949[22], i1949[23] )
  i1948.streamingMipmapsActive = !!i1949[24]
  i1948.vSyncCount = i1949[25]
  i1948.asyncUploadBufferSize = i1949[26]
  i1948.asyncUploadTimeSlice = i1949[27]
  i1948.billboardsFaceCameraPosition = !!i1949[28]
  i1948.shadowNearPlaneOffset = i1949[29]
  i1948.streamingMipmapsMemoryBudget = i1949[30]
  i1948.maximumLODLevel = i1949[31]
  i1948.streamingMipmapsAddAllCameras = !!i1949[32]
  i1948.streamingMipmapsMaxLevelReduction = i1949[33]
  i1948.streamingMipmapsRenderersPerFrame = i1949[34]
  i1948.resolutionScalingFixedDPIFactor = i1949[35]
  i1948.streamingMipmapsMaxFileIORequests = i1949[36]
  i1948.currentQualityLevel = i1949[37]
  return i1948
}

Deserializers["Luna.Unity.DTO.UnityEngine.Audio.AudioMixer"] = function (request, data, root) {
  var i1956 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Audio.AudioMixer' )
  var i1957 = data
  var i1959 = i1957[0]
  var i1958 = []
  for(var i = 0; i < i1959.length; i += 1) {
    i1958.push( request.d('Luna.Unity.DTO.UnityEngine.Audio.AudioMixerGroup', i1959[i + 0]) );
  }
  i1956.groups = i1958
  var i1961 = i1957[1]
  var i1960 = []
  for(var i = 0; i < i1961.length; i += 1) {
    i1960.push( request.d('Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot', i1961[i + 0]) );
  }
  i1956.snapshots = i1960
  return i1956
}

Deserializers["Luna.Unity.DTO.UnityEngine.Audio.AudioMixerGroup"] = function (request, data, root) {
  var i1964 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Audio.AudioMixerGroup' )
  var i1965 = data
  i1964.id = i1965[0]
  i1964.childGroupIds = i1965[1]
  i1964.name = i1965[2]
  return i1964
}

Deserializers["Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot"] = function (request, data, root) {
  var i1968 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot' )
  var i1969 = data
  i1968.id = i1969[0]
  var i1971 = i1969[1]
  var i1970 = []
  for(var i = 0; i < i1971.length; i += 1) {
    i1970.push( request.d('Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot+Parameter', i1971[i + 0]) );
  }
  i1968.parameters = i1970
  return i1968
}

Deserializers["Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot+Parameter"] = function (request, data, root) {
  var i1974 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot+Parameter' )
  var i1975 = data
  i1974.name = i1975[0]
  i1974.value = i1975[1]
  return i1974
}

Deserializers["UnityEngine.Events.ArgumentCache"] = function (request, data, root) {
  var i1976 = root || request.c( 'UnityEngine.Events.ArgumentCache' )
  var i1977 = data
  request.r(i1977[0], i1977[1], 0, i1976, 'm_ObjectArgument')
  i1976.m_ObjectArgumentAssemblyTypeName = i1977[2]
  i1976.m_IntArgument = i1977[3]
  i1976.m_FloatArgument = i1977[4]
  i1976.m_StringArgument = i1977[5]
  i1976.m_BoolArgument = !!i1977[6]
  return i1976
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame"] = function (request, data, root) {
  var i1980 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame' )
  var i1981 = data
  i1980.weight = i1981[0]
  i1980.vertices = i1981[1]
  i1980.normals = i1981[2]
  i1980.tangents = i1981[3]
  return i1980
}

Deserializers["TMPro.GlyphValueRecord_Legacy"] = function (request, data, root) {
  var i1982 = root || request.c( 'TMPro.GlyphValueRecord_Legacy' )
  var i1983 = data
  i1982.xPlacement = i1983[0]
  i1982.yPlacement = i1983[1]
  i1982.xAdvance = i1983[2]
  i1982.yAdvance = i1983[3]
  return i1982
}

Deserializers["TMPro.TMP_GlyphAdjustmentRecord"] = function (request, data, root) {
  var i1984 = root || request.c( 'TMPro.TMP_GlyphAdjustmentRecord' )
  var i1985 = data
  i1984.m_GlyphIndex = i1985[0]
  i1984.m_GlyphValueRecord = request.d('TMPro.TMP_GlyphValueRecord', i1985[1], i1984.m_GlyphValueRecord)
  return i1984
}

Deserializers["PaintingSharedAttributes+BlockFountainBulletSet"] = function (request, data, root) {
  var i1988 = root || request.c( 'PaintingSharedAttributes+BlockFountainBulletSet' )
  var i1989 = data
  i1988.ColorCode = i1989[0]
  i1988.BlockCount = i1989[1]
  return i1988
}

Deserializers["TMPro.TMP_GlyphValueRecord"] = function (request, data, root) {
  var i1990 = root || request.c( 'TMPro.TMP_GlyphValueRecord' )
  var i1991 = data
  i1990.m_XPlacement = i1991[0]
  i1990.m_YPlacement = i1991[1]
  i1990.m_XAdvance = i1991[2]
  i1990.m_YAdvance = i1991[3]
  return i1990
}

Deserializers.fields = {"Luna.Unity.DTO.UnityEngine.Components.RectTransform":{"pivot":0,"anchorMin":2,"anchorMax":4,"sizeDelta":6,"anchoredPosition3D":8,"rotation":11,"scale":15},"Luna.Unity.DTO.UnityEngine.Components.Canvas":{"planeDistance":0,"referencePixelsPerUnit":1,"isFallbackOverlay":2,"renderMode":3,"renderOrder":4,"sortingLayerName":5,"sortingOrder":6,"scaleFactor":7,"worldCamera":8,"overrideSorting":10,"pixelPerfect":11,"targetDisplay":12,"overridePixelPerfect":13,"enabled":14},"Luna.Unity.DTO.UnityEngine.Components.CanvasGroup":{"m_Alpha":0,"m_Interactable":1,"m_BlocksRaycasts":2,"m_IgnoreParentGroups":3,"enabled":4},"Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer":{"cullTransparentMesh":0},"Luna.Unity.DTO.UnityEngine.Scene.GameObject":{"name":0,"tagId":1,"enabled":2,"isStatic":3,"layer":4},"Luna.Unity.DTO.UnityEngine.Components.Transform":{"position":0,"scale":3,"rotation":6},"Luna.Unity.DTO.UnityEngine.Textures.Texture2D":{"name":0,"width":1,"height":2,"mipmapCount":3,"anisoLevel":4,"filterMode":5,"hdr":6,"format":7,"wrapMode":8,"alphaIsTransparency":9,"alphaSource":10,"graphicsFormat":11,"sRGBTexture":12,"desiredColorSpace":13,"wrapU":14,"wrapV":15},"Luna.Unity.DTO.UnityEngine.Assets.Material":{"name":0,"shader":1,"renderQueue":3,"enableInstancing":4,"floatParameters":5,"colorParameters":6,"vectorParameters":7,"textureParameters":8,"materialFlags":9},"Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag":{"name":0,"enabled":1},"Luna.Unity.DTO.UnityEngine.Components.AudioSource":{"clip":0,"outputAudioMixerGroup":2,"playOnAwake":4,"loop":5,"time":6,"volume":7,"pitch":8,"enabled":9},"Luna.Unity.DTO.UnityEngine.Components.MeshFilter":{"sharedMesh":0},"Luna.Unity.DTO.UnityEngine.Components.MeshRenderer":{"additionalVertexStreams":0,"enabled":2,"sharedMaterial":3,"sharedMaterials":5,"receiveShadows":6,"shadowCastingMode":7,"sortingLayerID":8,"sortingOrder":9,"lightmapIndex":10,"lightmapSceneIndex":11,"lightmapScaleOffset":12,"lightProbeUsage":16,"reflectionProbeUsage":17},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystem":{"main":0,"colorBySpeed":1,"colorOverLifetime":2,"emission":3,"rotationBySpeed":4,"rotationOverLifetime":5,"shape":6,"sizeBySpeed":7,"sizeOverLifetime":8,"textureSheetAnimation":9,"velocityOverLifetime":10,"noise":11,"inheritVelocity":12,"forceOverLifetime":13,"limitVelocityOverLifetime":14,"useAutoRandomSeed":15,"randomSeed":16},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule":{"duration":0,"loop":1,"prewarm":2,"startDelay":3,"startLifetime":4,"startSpeed":5,"startSize3D":6,"startSizeX":7,"startSizeY":8,"startSizeZ":9,"startRotation3D":10,"startRotationX":11,"startRotationY":12,"startRotationZ":13,"startColor":14,"gravityModifier":15,"simulationSpace":16,"customSimulationSpace":17,"simulationSpeed":19,"useUnscaledTime":20,"scalingMode":21,"playOnAwake":22,"maxParticles":23,"emitterVelocityMode":24,"stopAction":25},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve":{"mode":0,"curveMin":1,"curveMax":2,"curveMultiplier":3,"constantMin":4,"constantMax":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient":{"mode":0,"gradientMin":1,"gradientMax":2,"colorMin":3,"colorMax":7},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient":{"mode":0,"colorKeys":1,"alphaKeys":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule":{"enabled":0,"color":1,"range":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey":{"color":0,"time":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey":{"alpha":0,"time":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule":{"enabled":0,"color":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule":{"enabled":0,"rateOverTime":1,"rateOverDistance":2,"bursts":3},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst":{"count":0,"cycleCount":1,"minCount":2,"maxCount":3,"repeatInterval":4,"time":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule":{"enabled":0,"shapeType":1,"randomDirectionAmount":2,"sphericalDirectionAmount":3,"randomPositionAmount":4,"alignToDirection":5,"radius":6,"radiusMode":7,"radiusSpread":8,"radiusSpeed":9,"radiusThickness":10,"angle":11,"length":12,"boxThickness":13,"meshShapeType":16,"mesh":17,"meshRenderer":19,"skinnedMeshRenderer":21,"useMeshMaterialIndex":23,"meshMaterialIndex":24,"useMeshColors":25,"normalOffset":26,"arc":27,"arcMode":28,"arcSpread":29,"arcSpeed":30,"donutRadius":31,"position":32,"rotation":35,"scale":38},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule":{"enabled":0,"mode":1,"animation":2,"numTilesX":3,"numTilesY":4,"useRandomRow":5,"frameOverTime":6,"startFrame":7,"cycleCount":8,"rowIndex":9,"flipU":10,"flipV":11,"spriteCount":12,"sprites":13},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"radial":4,"speedModifier":5,"space":6,"orbitalX":7,"orbitalY":8,"orbitalZ":9,"orbitalOffsetX":10,"orbitalOffsetY":11,"orbitalOffsetZ":12},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule":{"enabled":0,"separateAxes":1,"strengthX":2,"strengthY":3,"strengthZ":4,"frequency":5,"damping":6,"octaveCount":7,"octaveMultiplier":8,"octaveScale":9,"quality":10,"scrollSpeed":11,"scrollSpeedMultiplier":12,"remapEnabled":13,"remapX":14,"remapY":15,"remapZ":16,"positionAmount":17,"rotationAmount":18,"sizeAmount":19},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule":{"enabled":0,"mode":1,"curve":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"space":4,"randomized":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule":{"enabled":0,"limit":1,"limitX":2,"limitY":3,"limitZ":4,"dampen":5,"separateAxes":6,"space":7,"drag":8,"multiplyDragByParticleSize":9,"multiplyDragByParticleVelocity":10},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer":{"mesh":0,"meshCount":2,"activeVertexStreamsCount":3,"alignment":4,"renderMode":5,"sortMode":6,"lengthScale":7,"velocityScale":8,"cameraVelocityScale":9,"normalDirection":10,"sortingFudge":11,"minParticleSize":12,"maxParticleSize":13,"pivot":14,"trailMaterial":17,"applyActiveColorSpace":19,"enabled":20,"sharedMaterial":21,"sharedMaterials":23,"receiveShadows":24,"shadowCastingMode":25,"sortingLayerID":26,"sortingOrder":27,"lightmapIndex":28,"lightmapSceneIndex":29,"lightmapScaleOffset":30,"lightProbeUsage":34,"reflectionProbeUsage":35},"Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer":{"color":0,"sprite":4,"flipX":6,"flipY":7,"drawMode":8,"size":9,"tileMode":11,"adaptiveModeThreshold":12,"maskInteraction":13,"spriteSortPoint":14,"enabled":15,"sharedMaterial":16,"sharedMaterials":18,"receiveShadows":19,"shadowCastingMode":20,"sortingLayerID":21,"sortingOrder":22,"lightmapIndex":23,"lightmapSceneIndex":24,"lightmapScaleOffset":25,"lightProbeUsage":29,"reflectionProbeUsage":30},"Luna.Unity.DTO.UnityEngine.Assets.Mesh":{"name":0,"halfPrecision":1,"useSimplification":2,"useUInt32IndexFormat":3,"vertexCount":4,"aabb":5,"streams":6,"vertices":7,"subMeshes":8,"bindposes":9,"blendShapes":10},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh":{"triangles":0},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape":{"name":0,"frames":1},"Luna.Unity.DTO.UnityEngine.Components.BoxCollider":{"center":0,"size":3,"enabled":6,"isTrigger":7,"material":8},"Luna.Unity.DTO.UnityEngine.Components.TrailRenderer":{"positions":0,"positionCount":1,"time":2,"startWidth":3,"endWidth":4,"widthMultiplier":5,"autodestruct":6,"emitting":7,"numCornerVertices":8,"numCapVertices":9,"minVertexDistance":10,"colorGradient":11,"startColor":12,"endColor":16,"generateLightingData":20,"textureMode":21,"alignment":22,"widthCurve":23,"enabled":24,"sharedMaterial":25,"sharedMaterials":27,"receiveShadows":28,"shadowCastingMode":29,"sortingLayerID":30,"sortingOrder":31,"lightmapIndex":32,"lightmapSceneIndex":33,"lightmapScaleOffset":34,"lightProbeUsage":38,"reflectionProbeUsage":39},"Luna.Unity.DTO.UnityEngine.Components.Camera":{"aspect":0,"orthographic":1,"orthographicSize":2,"backgroundColor":3,"nearClipPlane":7,"farClipPlane":8,"fieldOfView":9,"depth":10,"clearFlags":11,"cullingMask":12,"rect":13,"targetTexture":14,"usePhysicalProperties":16,"focalLength":17,"sensorSize":18,"lensShift":20,"gateFit":22,"commandBufferCount":23,"cameraType":24,"enabled":25},"Luna.Unity.DTO.UnityEngine.Components.Animator":{"animatorController":0,"avatar":2,"updateMode":4,"hasTransformHierarchy":5,"applyRootMotion":6,"humanBones":7,"enabled":8},"Luna.Unity.DTO.UnityEngine.Components.LineRenderer":{"textureMode":0,"alignment":1,"widthCurve":2,"colorGradient":3,"positions":4,"positionCount":5,"widthMultiplier":6,"startWidth":7,"endWidth":8,"numCornerVertices":9,"numCapVertices":10,"useWorldSpace":11,"loop":12,"startColor":13,"endColor":17,"generateLightingData":21,"enabled":22,"sharedMaterial":23,"sharedMaterials":25,"receiveShadows":26,"shadowCastingMode":27,"sortingLayerID":28,"sortingOrder":29,"lightmapIndex":30,"lightmapSceneIndex":31,"lightmapScaleOffset":32,"lightProbeUsage":36,"reflectionProbeUsage":37},"Luna.Unity.DTO.UnityEngine.Textures.Cubemap":{"name":0,"atlasId":1,"mipmapCount":2,"hdr":3,"size":4,"anisoLevel":5,"filterMode":6,"rects":7,"wrapU":8,"wrapV":9},"Luna.Unity.DTO.UnityEngine.Scene.Scene":{"name":0,"index":1,"startup":2},"Luna.Unity.DTO.UnityEngine.Components.Light":{"type":0,"color":1,"cullingMask":5,"intensity":6,"range":7,"spotAngle":8,"shadows":9,"shadowNormalBias":10,"shadowBias":11,"shadowStrength":12,"shadowResolution":13,"lightmapBakeType":14,"renderMode":15,"cookie":16,"cookieSize":18,"shadowNearPlane":19,"enabled":20},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings":{"ambientIntensity":0,"reflectionIntensity":1,"ambientMode":2,"ambientLight":3,"ambientSkyColor":7,"ambientGroundColor":11,"ambientEquatorColor":15,"fogColor":19,"fogEndDistance":23,"fogStartDistance":24,"fogDensity":25,"fog":26,"skybox":27,"fogMode":29,"lightmaps":30,"lightProbes":31,"lightmapsMode":32,"mixedBakeMode":33,"environmentLightingMode":34,"ambientProbe":35,"referenceAmbientProbe":36,"useReferenceAmbientProbe":37,"customReflection":38,"defaultReflection":40,"defaultReflectionMode":42,"defaultReflectionResolution":43,"sunLightObjectId":44,"pixelLightCount":45,"defaultReflectionHDR":46,"hasLightDataAsset":47,"hasManualGenerate":48},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap":{"lightmapColor":0,"lightmapDirection":2},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes":{"bakedProbes":0,"positions":1,"hullRays":2,"tetrahedra":3,"neighbours":4,"matrices":5},"Luna.Unity.DTO.UnityEngine.Assets.Shader":{"ShaderCompilationErrors":0,"name":1,"guid":2,"shaderDefinedKeywords":3,"passes":4,"usePasses":5,"defaultParameterValues":6,"unityFallbackShader":7,"readDepth":9,"hasDepthOnlyPass":10,"isCreatedByShaderGraph":11,"disableBatching":12,"compiled":13},"Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError":{"shaderName":0,"errorMessage":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass":{"id":0,"subShaderIndex":1,"name":2,"passType":3,"grabPassTextureName":4,"usePass":5,"zTest":6,"zWrite":7,"culling":8,"blending":9,"alphaBlending":10,"colorWriteMask":11,"offsetUnits":12,"offsetFactor":13,"stencilRef":14,"stencilReadMask":15,"stencilWriteMask":16,"stencilOp":17,"stencilOpFront":18,"stencilOpBack":19,"tags":20,"passDefinedKeywords":21,"passDefinedKeywordGroups":22,"variants":23,"excludedVariants":24,"hasDepthReader":25},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value":{"val":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending":{"src":0,"dst":1,"op":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp":{"pass":0,"fail":1,"zFail":2,"comp":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup":{"keywords":0,"hasDiscard":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant":{"passId":0,"subShaderIndex":1,"keywords":2,"vertexProgram":3,"fragmentProgram":4,"exportedForWebGl2":5,"readDepth":6},"Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass":{"shader":0,"pass":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue":{"name":0,"type":1,"value":2,"textureValue":6,"shaderPropertyFlag":7},"Luna.Unity.DTO.UnityEngine.Textures.Sprite":{"name":0,"texture":1,"aabb":3,"vertices":4,"triangles":5,"textureRect":6,"packedRect":10,"border":14,"transparency":18,"bounds":19,"pixelsPerUnit":20,"textureWidth":21,"textureHeight":22,"nativeSize":23,"pivot":25,"textureRectOffset":27},"Luna.Unity.DTO.UnityEngine.Assets.AudioClip":{"name":0},"Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip":{"name":0,"wrapMode":1,"isLooping":2,"length":3,"curves":4,"events":5,"halfPrecision":6,"_frameRate":7,"localBounds":8,"hasMuscleCurves":9,"clipMuscleConstant":10,"clipBindingConstant":11},"Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve":{"path":0,"hash":1,"componentType":2,"property":3,"keys":4,"objectReferenceKeys":5},"Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve+ObjectReferenceKey":{"time":0,"value":1},"Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationEvent":{"functionName":0,"floatParameter":1,"intParameter":2,"stringParameter":3,"objectReferenceParameter":4,"time":6},"Luna.Unity.DTO.UnityEngine.Animation.Data.Bounds":{"center":0,"extends":3},"Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip+AnimationClipBindingConstant":{"genericBindings":0,"pptrCurveMapping":1},"Luna.Unity.DTO.UnityEngine.Assets.Font":{"name":0,"ascent":1,"originalLineHeight":2,"fontSize":3,"characterInfo":4,"texture":5,"originalFontSize":7},"Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo":{"index":0,"advance":1,"bearing":2,"glyphWidth":3,"glyphHeight":4,"minX":5,"maxX":6,"minY":7,"maxY":8,"uvBottomLeftX":9,"uvBottomLeftY":10,"uvBottomRightX":11,"uvBottomRightY":12,"uvTopLeftX":13,"uvTopLeftY":14,"uvTopRightX":15,"uvTopRightY":16},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorController":{"name":0,"layers":1,"parameters":2,"animationClips":3,"avatarUnsupported":4},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerLayer":{"name":0,"defaultWeight":1,"blendingMode":2,"avatarMask":3,"syncedLayerIndex":4,"syncedLayerAffectsTiming":5,"syncedLayers":6,"stateMachine":7},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateMachine":{"id":0,"name":1,"path":2,"states":3,"machines":4,"entryStateTransitions":5,"exitStateTransitions":6,"anyStateTransitions":7,"defaultStateId":8},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorState":{"id":0,"name":1,"cycleOffset":2,"cycleOffsetParameter":3,"cycleOffsetParameterActive":4,"mirror":5,"mirrorParameter":6,"mirrorParameterActive":7,"motionId":8,"nameHash":9,"fullPathHash":10,"speed":11,"speedParameter":12,"speedParameterActive":13,"tag":14,"tagHash":15,"writeDefaultValues":16,"behaviours":17,"transitions":18},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateTransition":{"fullPath":0,"canTransitionToSelf":1,"duration":2,"exitTime":3,"hasExitTime":4,"hasFixedDuration":5,"interruptionSource":6,"offset":7,"orderedInterruption":8,"destinationStateId":9,"isExit":10,"mute":11,"solo":12,"conditions":13},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorCondition":{"mode":0,"parameter":1,"threshold":2},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorTransition":{"destinationStateId":0,"isExit":1,"mute":2,"solo":3,"conditions":4},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerParameter":{"defaultBool":0,"defaultFloat":1,"defaultInt":2,"name":3,"nameHash":4,"type":5},"Luna.Unity.DTO.UnityEngine.Assets.TextAsset":{"name":0,"bytes64":1,"data":2},"Luna.Unity.DTO.UnityEngine.Assets.Resources":{"files":0,"componentToPrefabIds":1},"Luna.Unity.DTO.UnityEngine.Assets.Resources+File":{"path":0,"unityObject":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings":{"scriptsExecutionOrder":0,"sortingLayers":1,"cullingLayers":2,"timeSettings":3,"physicsSettings":4,"physics2DSettings":5,"qualitySettings":6,"enableRealtimeShadows":7,"enableAutoInstancing":8,"enableStaticBatching":9,"enableDynamicBatching":10,"lightmapEncodingQuality":11,"desiredColorSpace":12,"allTags":13},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer":{"id":0,"name":1,"value":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer":{"id":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings":{"fixedDeltaTime":0,"maximumDeltaTime":1,"timeScale":2,"maximumParticleTimestep":3},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings":{"gravity":0,"defaultSolverIterations":3,"bounceThreshold":4,"autoSyncTransforms":5,"autoSimulation":6,"collisionMatrix":7},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings":{"material":0,"gravity":2,"positionIterations":4,"velocityIterations":5,"velocityThreshold":6,"maxLinearCorrection":7,"maxAngularCorrection":8,"maxTranslationSpeed":9,"maxRotationSpeed":10,"baumgarteScale":11,"baumgarteTOIScale":12,"timeToSleep":13,"linearSleepTolerance":14,"angularSleepTolerance":15,"defaultContactOffset":16,"autoSimulation":17,"queriesHitTriggers":18,"queriesStartInColliders":19,"callbacksOnDisable":20,"reuseCollisionCallbacks":21,"autoSyncTransforms":22,"collisionMatrix":23},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.QualitySettings":{"qualityLevels":0,"names":1,"shadows":2,"anisotropicFiltering":3,"antiAliasing":4,"lodBias":5,"shadowCascades":6,"shadowDistance":7,"shadowmaskMode":8,"shadowProjection":9,"shadowResolution":10,"softParticles":11,"softVegetation":12,"activeColorSpace":13,"desiredColorSpace":14,"masterTextureLimit":15,"maxQueuedFrames":16,"particleRaycastBudget":17,"pixelLightCount":18,"realtimeReflectionProbes":19,"shadowCascade2Split":20,"shadowCascade4Split":21,"streamingMipmapsActive":24,"vSyncCount":25,"asyncUploadBufferSize":26,"asyncUploadTimeSlice":27,"billboardsFaceCameraPosition":28,"shadowNearPlaneOffset":29,"streamingMipmapsMemoryBudget":30,"maximumLODLevel":31,"streamingMipmapsAddAllCameras":32,"streamingMipmapsMaxLevelReduction":33,"streamingMipmapsRenderersPerFrame":34,"resolutionScalingFixedDPIFactor":35,"streamingMipmapsMaxFileIORequests":36,"currentQualityLevel":37},"Luna.Unity.DTO.UnityEngine.Audio.AudioMixer":{"groups":0,"snapshots":1},"Luna.Unity.DTO.UnityEngine.Audio.AudioMixerGroup":{"id":0,"childGroupIds":1,"name":2},"Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot":{"id":0,"parameters":1},"Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot+Parameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame":{"weight":0,"vertices":1,"normals":2,"tangents":3}}

Deserializers.requiredComponents = {"102":[103],"104":[103],"105":[103],"106":[103],"107":[103],"108":[103],"109":[110],"111":[50],"112":[113],"114":[113],"115":[113],"116":[113],"117":[113],"118":[113],"119":[113],"120":[121],"122":[121],"123":[121],"124":[121],"125":[121],"126":[121],"127":[121],"128":[121],"129":[121],"130":[121],"131":[121],"132":[121],"133":[121],"134":[50],"135":[42],"136":[137],"138":[137],"1":[0],"43":[40,42],"89":[51],"75":[80],"76":[40,42,75],"139":[140],"141":[0],"142":[0],"4":[1],"7":[6,0],"143":[0],"3":[1],"144":[0],"145":[0],"146":[0],"147":[0],"148":[0],"149":[0],"150":[0],"151":[0],"152":[0],"153":[6,0],"154":[0],"155":[0],"156":[0],"157":[0],"14":[6,0],"158":[0],"159":[18],"160":[18],"19":[18],"161":[18],"162":[50],"163":[50],"164":[140],"165":[0],"69":[42,0],"21":[0,6],"166":[0],"167":[6,0],"168":[42],"169":[6,0],"170":[0],"171":[140]}

Deserializers.types = ["UnityEngine.RectTransform","UnityEngine.Canvas","UnityEngine.EventSystems.UIBehaviour","UnityEngine.UI.CanvasScaler","UnityEngine.UI.GraphicRaycaster","UnityEngine.CanvasGroup","UnityEngine.CanvasRenderer","UnityEngine.UI.Image","UnityEngine.Sprite","UnityEngine.UI.Button","UnityEngine.MonoBehaviour","SoundUIElement","UnityEngine.AudioClip","PlayNowButtonAnim","UnityEngine.UI.Text","UnityEngine.Font","UiEndGame","UnityEngine.Transform","UnityEngine.EventSystems.EventSystem","UnityEngine.EventSystems.StandaloneInputModule","TutorialLayer","TMPro.TextMeshProUGUI","TMPro.TMP_FontAsset","UnityEngine.Material","UnityEngine.Shader","UnityEngine.Texture2D","CollectorGameManager","CollectorQueueManager","CollectorMoveLimiter","InputManager","GamePlaySound","GameplayManager","LevelGamePlayConfigSO","SoundManager","UnityEditor.Audio.AudioMixerController","UnityEngine.AudioSource","UnityEditor.Audio.AudioMixerGroupController","PadController","AlertFullSlotAnim","Grid3DLayout","UnityEngine.MeshFilter","UnityEngine.Mesh","UnityEngine.MeshRenderer","MeshCombiner","UnityEngine.ParticleSystem","UnityEngine.ParticleSystemRenderer","UnityEngine.SpriteRenderer","LevelConfigSetup","LevelCollectorsSystem","LevelConfig","UnityEngine.Camera","PaintingGridObject","InGameEffectOptions","ColorPalleteData","UnityEngine.GameObject","PaintingPixelComponent","LevelMechanicObjectPrefabs","UnityEngine.BoxCollider","PipeObject","PipePartVisualHandle","AutoTextureScale","KeyObject","IdleRotate","IdleMoveUpDown","UnityEngine.TrailRenderer","LockObject","CollectorController","CollectorAnimation","WallObject","TMPro.TextMeshPro","CullableObject","CachedTransformPathMover","ColorPixelsCollectorObject","CollectorVisualHandler","BulletDisplayHandler","GogoGaga.OptimizedRopesAndCables.Rope","GogoGaga.OptimizedRopesAndCables.RopeMesh","UnityEngine.Animator","EnableRandomRotate","UnityEditor.Animations.AnimatorController","UnityEngine.LineRenderer","BlockFountainObject","BlockFountainVisualHandler","FountainProjectileController","CollectorProjectileController","PathTransformBasedCached","PaintingConfig","LevelColorCollectorsConfig","PaintingGridEffects","GridBlockFountainModule","CollectorColumnController","CollectorProjectilePool","BlockFountainProjectilePool","UnityEngine.AudioListener","UnityEngine.Light","UnityEngine.Cubemap","DG.Tweening.Core.DOTweenSettings","TMPro.TMP_Settings","TMPro.TMP_SpriteAsset","TMPro.TMP_StyleSheet","UnityEngine.TextAsset","UnityEditor.Audio.AudioMixerSnapshotController","UnityEngine.AudioLowPassFilter","UnityEngine.AudioBehaviour","UnityEngine.AudioHighPassFilter","UnityEngine.AudioReverbFilter","UnityEngine.AudioDistortionFilter","UnityEngine.AudioEchoFilter","UnityEngine.AudioChorusFilter","UnityEngine.Cloth","UnityEngine.SkinnedMeshRenderer","UnityEngine.FlareLayer","UnityEngine.ConstantForce","UnityEngine.Rigidbody","UnityEngine.Joint","UnityEngine.HingeJoint","UnityEngine.SpringJoint","UnityEngine.FixedJoint","UnityEngine.CharacterJoint","UnityEngine.ConfigurableJoint","UnityEngine.CompositeCollider2D","UnityEngine.Rigidbody2D","UnityEngine.Joint2D","UnityEngine.AnchoredJoint2D","UnityEngine.SpringJoint2D","UnityEngine.DistanceJoint2D","UnityEngine.FrictionJoint2D","UnityEngine.HingeJoint2D","UnityEngine.RelativeJoint2D","UnityEngine.SliderJoint2D","UnityEngine.TargetJoint2D","UnityEngine.FixedJoint2D","UnityEngine.WheelJoint2D","UnityEngine.ConstantForce2D","UnityEngine.StreamingController","UnityEngine.TextMesh","UnityEngine.Tilemaps.TilemapRenderer","UnityEngine.Tilemaps.Tilemap","UnityEngine.Tilemaps.TilemapCollider2D","Unity.VisualScripting.SceneVariables","Unity.VisualScripting.Variables","UnityEngine.UI.Dropdown","UnityEngine.UI.Graphic","UnityEngine.UI.AspectRatioFitter","UnityEngine.UI.ContentSizeFitter","UnityEngine.UI.GridLayoutGroup","UnityEngine.UI.HorizontalLayoutGroup","UnityEngine.UI.HorizontalOrVerticalLayoutGroup","UnityEngine.UI.LayoutElement","UnityEngine.UI.LayoutGroup","UnityEngine.UI.VerticalLayoutGroup","UnityEngine.UI.Mask","UnityEngine.UI.MaskableGraphic","UnityEngine.UI.RawImage","UnityEngine.UI.RectMask2D","UnityEngine.UI.Scrollbar","UnityEngine.UI.ScrollRect","UnityEngine.UI.Slider","UnityEngine.UI.Toggle","UnityEngine.EventSystems.BaseInputModule","UnityEngine.EventSystems.PointerInputModule","UnityEngine.EventSystems.TouchInputModule","UnityEngine.EventSystems.Physics2DRaycaster","UnityEngine.EventSystems.PhysicsRaycaster","Unity.VisualScripting.ScriptMachine","TMPro.TextContainer","TMPro.TMP_Dropdown","TMPro.TMP_SelectionCaret","TMPro.TMP_SubMesh","TMPro.TMP_SubMeshUI","TMPro.TMP_Text","Unity.VisualScripting.StateMachine"]

Deserializers.unityVersion = "2022.3.62f3";

Deserializers.productName = "Cat Blast Blocks";

Deserializers.lunaInitializationTime = "12/18/2025 07:52:51";

Deserializers.lunaDaysRunning = "0.7";

Deserializers.lunaVersion = "7.0.0";

Deserializers.lunaSHA = "3bcc3e343f23b4c67e768a811a8d088c7f7adbc5";

Deserializers.creativeName = "Cat_Blast_Blocks_Lv134_Clickbait";

Deserializers.lunaAppID = "34759";

Deserializers.projectId = "b1592133a3dd6b14fa12d21db0d63d5f";

Deserializers.packagesInfo = "com.unity.textmeshpro: 3.0.6\ncom.unity.timeline: 1.7.7\ncom.unity.ugui: 1.0.0";

Deserializers.externalJsLibraries = "";

Deserializers.androidLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.androidLink?window.$environment.packageConfig.androidLink:'Empty';

Deserializers.iosLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.iosLink?window.$environment.packageConfig.iosLink:'Empty';

Deserializers.base64Enabled = "False";

Deserializers.minifyEnabled = "True";

Deserializers.isForceUncompressed = "False";

Deserializers.isAntiAliasingEnabled = "False";

Deserializers.isRuntimeAnalysisEnabledForCode = "True";

Deserializers.runtimeAnalysisExcludedClassesCount = "0";

Deserializers.runtimeAnalysisExcludedMethodsCount = "0";

Deserializers.runtimeAnalysisExcludedModules = "";

Deserializers.isRuntimeAnalysisEnabledForShaders = "True";

Deserializers.isRealtimeShadowsEnabled = "False";

Deserializers.isReferenceAmbientProbeBaked = "False";

Deserializers.isLunaCompilerV2Used = "False";

Deserializers.companyName = "DefaultCompany";

Deserializers.buildPlatform = "StandaloneWindows64";

Deserializers.applicationIdentifier = "com.DefaultCompany.Cat-Blast-Blocks";

Deserializers.disableAntiAliasing = true;

Deserializers.graphicsConstraint = 28;

Deserializers.linearColorSpace = true;

Deserializers.buildID = "a69c5106-e500-4b31-8f8a-0458e82cbcdb";

Deserializers.runtimeInitializeOnLoadInfos = [[["Unity","MemoryProfiler","Editor","MemoryProfilerSettings","RuntimeInitialize"],["Unity","MemoryProfiler","Editor","EditorGUICompatibilityHelper","RuntimeInitialize"],["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[["Unity","VisualScripting","RuntimeVSUsageUtility","RuntimeInitializeOnLoadBeforeSceneLoad"]],[["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"]],[["Unity","MemoryProfiler","MetadataInjector","PlayerInitMetadata"]],[]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

