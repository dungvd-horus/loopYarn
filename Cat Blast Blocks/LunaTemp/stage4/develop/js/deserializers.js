var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i958 = root || request.c( 'UnityEngine.JointSpring' )
  var i959 = data
  i958.spring = i959[0]
  i958.damper = i959[1]
  i958.targetPosition = i959[2]
  return i958
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i960 = root || request.c( 'UnityEngine.JointMotor' )
  var i961 = data
  i960.m_TargetVelocity = i961[0]
  i960.m_Force = i961[1]
  i960.m_FreeSpin = i961[2]
  return i960
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i962 = root || request.c( 'UnityEngine.JointLimits' )
  var i963 = data
  i962.m_Min = i963[0]
  i962.m_Max = i963[1]
  i962.m_Bounciness = i963[2]
  i962.m_BounceMinVelocity = i963[3]
  i962.m_ContactDistance = i963[4]
  i962.minBounce = i963[5]
  i962.maxBounce = i963[6]
  return i962
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i964 = root || request.c( 'UnityEngine.JointDrive' )
  var i965 = data
  i964.m_PositionSpring = i965[0]
  i964.m_PositionDamper = i965[1]
  i964.m_MaximumForce = i965[2]
  i964.m_UseAcceleration = i965[3]
  return i964
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i966 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i967 = data
  i966.m_Spring = i967[0]
  i966.m_Damper = i967[1]
  return i966
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i968 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i969 = data
  i968.m_Limit = i969[0]
  i968.m_Bounciness = i969[1]
  i968.m_ContactDistance = i969[2]
  return i968
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i970 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i971 = data
  i970.m_ExtremumSlip = i971[0]
  i970.m_ExtremumValue = i971[1]
  i970.m_AsymptoteSlip = i971[2]
  i970.m_AsymptoteValue = i971[3]
  i970.m_Stiffness = i971[4]
  return i970
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i972 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i973 = data
  i972.m_LowerAngle = i973[0]
  i972.m_UpperAngle = i973[1]
  return i972
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i974 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i975 = data
  i974.m_MotorSpeed = i975[0]
  i974.m_MaximumMotorTorque = i975[1]
  return i974
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i976 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i977 = data
  i976.m_DampingRatio = i977[0]
  i976.m_Frequency = i977[1]
  i976.m_Angle = i977[2]
  return i976
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i978 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i979 = data
  i978.m_LowerTranslation = i979[0]
  i978.m_UpperTranslation = i979[1]
  return i978
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.RectTransform"] = function (request, data, root) {
  var i980 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.RectTransform' )
  var i981 = data
  i980.pivot = new pc.Vec2( i981[0], i981[1] )
  i980.anchorMin = new pc.Vec2( i981[2], i981[3] )
  i980.anchorMax = new pc.Vec2( i981[4], i981[5] )
  i980.sizeDelta = new pc.Vec2( i981[6], i981[7] )
  i980.anchoredPosition3D = new pc.Vec3( i981[8], i981[9], i981[10] )
  i980.rotation = new pc.Quat(i981[11], i981[12], i981[13], i981[14])
  i980.scale = new pc.Vec3( i981[15], i981[16], i981[17] )
  return i980
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Canvas"] = function (request, data, root) {
  var i982 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Canvas' )
  var i983 = data
  i982.planeDistance = i983[0]
  i982.referencePixelsPerUnit = i983[1]
  i982.isFallbackOverlay = !!i983[2]
  i982.renderMode = i983[3]
  i982.renderOrder = i983[4]
  i982.sortingLayerName = i983[5]
  i982.sortingOrder = i983[6]
  i982.scaleFactor = i983[7]
  request.r(i983[8], i983[9], 0, i982, 'worldCamera')
  i982.overrideSorting = !!i983[10]
  i982.pixelPerfect = !!i983[11]
  i982.targetDisplay = i983[12]
  i982.overridePixelPerfect = !!i983[13]
  i982.enabled = !!i983[14]
  return i982
}

Deserializers["UnityEngine.UI.CanvasScaler"] = function (request, data, root) {
  var i984 = root || request.c( 'UnityEngine.UI.CanvasScaler' )
  var i985 = data
  i984.m_UiScaleMode = i985[0]
  i984.m_ReferencePixelsPerUnit = i985[1]
  i984.m_ScaleFactor = i985[2]
  i984.m_ReferenceResolution = new pc.Vec2( i985[3], i985[4] )
  i984.m_ScreenMatchMode = i985[5]
  i984.m_MatchWidthOrHeight = i985[6]
  i984.m_PhysicalUnit = i985[7]
  i984.m_FallbackScreenDPI = i985[8]
  i984.m_DefaultSpriteDPI = i985[9]
  i984.m_DynamicPixelsPerUnit = i985[10]
  i984.m_PresetInfoIsWorld = !!i985[11]
  return i984
}

Deserializers["UnityEngine.UI.GraphicRaycaster"] = function (request, data, root) {
  var i986 = root || request.c( 'UnityEngine.UI.GraphicRaycaster' )
  var i987 = data
  i986.m_IgnoreReversedGraphics = !!i987[0]
  i986.m_BlockingObjects = i987[1]
  i986.m_BlockingMask = UnityEngine.LayerMask.FromIntegerValue( i987[2] )
  return i986
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasGroup"] = function (request, data, root) {
  var i988 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasGroup' )
  var i989 = data
  i988.m_Alpha = i989[0]
  i988.m_Interactable = !!i989[1]
  i988.m_BlocksRaycasts = !!i989[2]
  i988.m_IgnoreParentGroups = !!i989[3]
  i988.enabled = !!i989[4]
  return i988
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer"] = function (request, data, root) {
  var i990 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer' )
  var i991 = data
  i990.cullTransparentMesh = !!i991[0]
  return i990
}

Deserializers["UnityEngine.UI.Image"] = function (request, data, root) {
  var i992 = root || request.c( 'UnityEngine.UI.Image' )
  var i993 = data
  request.r(i993[0], i993[1], 0, i992, 'm_Sprite')
  i992.m_Type = i993[2]
  i992.m_PreserveAspect = !!i993[3]
  i992.m_FillCenter = !!i993[4]
  i992.m_FillMethod = i993[5]
  i992.m_FillAmount = i993[6]
  i992.m_FillClockwise = !!i993[7]
  i992.m_FillOrigin = i993[8]
  i992.m_UseSpriteMesh = !!i993[9]
  i992.m_PixelsPerUnitMultiplier = i993[10]
  i992.m_Maskable = !!i993[11]
  request.r(i993[12], i993[13], 0, i992, 'm_Material')
  i992.m_Color = new pc.Color(i993[14], i993[15], i993[16], i993[17])
  i992.m_RaycastTarget = !!i993[18]
  i992.m_RaycastPadding = new pc.Vec4( i993[19], i993[20], i993[21], i993[22] )
  return i992
}

Deserializers["UnityEngine.UI.Button"] = function (request, data, root) {
  var i994 = root || request.c( 'UnityEngine.UI.Button' )
  var i995 = data
  i994.m_OnClick = request.d('UnityEngine.UI.Button+ButtonClickedEvent', i995[0], i994.m_OnClick)
  i994.m_Navigation = request.d('UnityEngine.UI.Navigation', i995[1], i994.m_Navigation)
  i994.m_Transition = i995[2]
  i994.m_Colors = request.d('UnityEngine.UI.ColorBlock', i995[3], i994.m_Colors)
  i994.m_SpriteState = request.d('UnityEngine.UI.SpriteState', i995[4], i994.m_SpriteState)
  i994.m_AnimationTriggers = request.d('UnityEngine.UI.AnimationTriggers', i995[5], i994.m_AnimationTriggers)
  i994.m_Interactable = !!i995[6]
  request.r(i995[7], i995[8], 0, i994, 'm_TargetGraphic')
  return i994
}

Deserializers["UnityEngine.UI.Button+ButtonClickedEvent"] = function (request, data, root) {
  var i996 = root || request.c( 'UnityEngine.UI.Button+ButtonClickedEvent' )
  var i997 = data
  i996.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i997[0], i996.m_PersistentCalls)
  return i996
}

Deserializers["UnityEngine.Events.PersistentCallGroup"] = function (request, data, root) {
  var i998 = root || request.c( 'UnityEngine.Events.PersistentCallGroup' )
  var i999 = data
  var i1001 = i999[0]
  var i1000 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Events.PersistentCall')))
  for(var i = 0; i < i1001.length; i += 1) {
    i1000.add(request.d('UnityEngine.Events.PersistentCall', i1001[i + 0]));
  }
  i998.m_Calls = i1000
  return i998
}

Deserializers["UnityEngine.Events.PersistentCall"] = function (request, data, root) {
  var i1004 = root || request.c( 'UnityEngine.Events.PersistentCall' )
  var i1005 = data
  request.r(i1005[0], i1005[1], 0, i1004, 'm_Target')
  i1004.m_TargetAssemblyTypeName = i1005[2]
  i1004.m_MethodName = i1005[3]
  i1004.m_Mode = i1005[4]
  i1004.m_Arguments = request.d('UnityEngine.Events.ArgumentCache', i1005[5], i1004.m_Arguments)
  i1004.m_CallState = i1005[6]
  return i1004
}

Deserializers["UnityEngine.UI.Navigation"] = function (request, data, root) {
  var i1006 = root || request.c( 'UnityEngine.UI.Navigation' )
  var i1007 = data
  i1006.m_Mode = i1007[0]
  i1006.m_WrapAround = !!i1007[1]
  request.r(i1007[2], i1007[3], 0, i1006, 'm_SelectOnUp')
  request.r(i1007[4], i1007[5], 0, i1006, 'm_SelectOnDown')
  request.r(i1007[6], i1007[7], 0, i1006, 'm_SelectOnLeft')
  request.r(i1007[8], i1007[9], 0, i1006, 'm_SelectOnRight')
  return i1006
}

Deserializers["UnityEngine.UI.ColorBlock"] = function (request, data, root) {
  var i1008 = root || request.c( 'UnityEngine.UI.ColorBlock' )
  var i1009 = data
  i1008.m_NormalColor = new pc.Color(i1009[0], i1009[1], i1009[2], i1009[3])
  i1008.m_HighlightedColor = new pc.Color(i1009[4], i1009[5], i1009[6], i1009[7])
  i1008.m_PressedColor = new pc.Color(i1009[8], i1009[9], i1009[10], i1009[11])
  i1008.m_SelectedColor = new pc.Color(i1009[12], i1009[13], i1009[14], i1009[15])
  i1008.m_DisabledColor = new pc.Color(i1009[16], i1009[17], i1009[18], i1009[19])
  i1008.m_ColorMultiplier = i1009[20]
  i1008.m_FadeDuration = i1009[21]
  return i1008
}

Deserializers["UnityEngine.UI.SpriteState"] = function (request, data, root) {
  var i1010 = root || request.c( 'UnityEngine.UI.SpriteState' )
  var i1011 = data
  request.r(i1011[0], i1011[1], 0, i1010, 'm_HighlightedSprite')
  request.r(i1011[2], i1011[3], 0, i1010, 'm_PressedSprite')
  request.r(i1011[4], i1011[5], 0, i1010, 'm_SelectedSprite')
  request.r(i1011[6], i1011[7], 0, i1010, 'm_DisabledSprite')
  return i1010
}

Deserializers["UnityEngine.UI.AnimationTriggers"] = function (request, data, root) {
  var i1012 = root || request.c( 'UnityEngine.UI.AnimationTriggers' )
  var i1013 = data
  i1012.m_NormalTrigger = i1013[0]
  i1012.m_HighlightedTrigger = i1013[1]
  i1012.m_PressedTrigger = i1013[2]
  i1012.m_SelectedTrigger = i1013[3]
  i1012.m_DisabledTrigger = i1013[4]
  return i1012
}

Deserializers["SoundUIElement"] = function (request, data, root) {
  var i1014 = root || request.c( 'SoundUIElement' )
  var i1015 = data
  i1014.Sound = request.d('SoundDefine', i1015[0], i1014.Sound)
  i1014.PlayOnEnable = !!i1015[1]
  i1014.StopOnDisable = !!i1015[2]
  i1014.playWithInteractable = !!i1015[3]
  i1014.isPlayRandomBackGroundMusic = !!i1015[4]
  return i1014
}

Deserializers["SoundDefine"] = function (request, data, root) {
  var i1016 = root || request.c( 'SoundDefine' )
  var i1017 = data
  i1016.soundType = i1017[0]
  i1016.Loop = !!i1017[1]
  request.r(i1017[2], i1017[3], 0, i1016, 'Clip')
  var i1019 = i1017[4]
  var i1018 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.AudioClip')))
  for(var i = 0; i < i1019.length; i += 2) {
  request.r(i1019[i + 0], i1019[i + 1], 1, i1018, '')
  }
  i1016.ClipList = i1018
  return i1016
}

Deserializers["PlayNowButtonAnim"] = function (request, data, root) {
  var i1022 = root || request.c( 'PlayNowButtonAnim' )
  var i1023 = data
  request.r(i1023[0], i1023[1], 0, i1022, 'playerNowButton')
  i1022.maxScale = new pc.Vec3( i1023[2], i1023[3], i1023[4] )
  i1022.minScale = new pc.Vec3( i1023[5], i1023[6], i1023[7] )
  i1022.scaleDuration = i1023[8]
  i1022.m_autoStart = !!i1023[9]
  return i1022
}

Deserializers["UnityEngine.UI.Text"] = function (request, data, root) {
  var i1024 = root || request.c( 'UnityEngine.UI.Text' )
  var i1025 = data
  i1024.m_FontData = request.d('UnityEngine.UI.FontData', i1025[0], i1024.m_FontData)
  i1024.m_Text = i1025[1]
  i1024.m_Maskable = !!i1025[2]
  request.r(i1025[3], i1025[4], 0, i1024, 'm_Material')
  i1024.m_Color = new pc.Color(i1025[5], i1025[6], i1025[7], i1025[8])
  i1024.m_RaycastTarget = !!i1025[9]
  i1024.m_RaycastPadding = new pc.Vec4( i1025[10], i1025[11], i1025[12], i1025[13] )
  return i1024
}

Deserializers["UnityEngine.UI.FontData"] = function (request, data, root) {
  var i1026 = root || request.c( 'UnityEngine.UI.FontData' )
  var i1027 = data
  request.r(i1027[0], i1027[1], 0, i1026, 'm_Font')
  i1026.m_FontSize = i1027[2]
  i1026.m_FontStyle = i1027[3]
  i1026.m_BestFit = !!i1027[4]
  i1026.m_MinSize = i1027[5]
  i1026.m_MaxSize = i1027[6]
  i1026.m_Alignment = i1027[7]
  i1026.m_AlignByGeometry = !!i1027[8]
  i1026.m_RichText = !!i1027[9]
  i1026.m_HorizontalOverflow = i1027[10]
  i1026.m_VerticalOverflow = i1027[11]
  i1026.m_LineSpacing = i1027[12]
  return i1026
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i1028 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i1029 = data
  i1028.name = i1029[0]
  i1028.tagId = i1029[1]
  i1028.enabled = !!i1029[2]
  i1028.isStatic = !!i1029[3]
  i1028.layer = i1029[4]
  return i1028
}

Deserializers["UiEndGame"] = function (request, data, root) {
  var i1030 = root || request.c( 'UiEndGame' )
  var i1031 = data
  request.r(i1031[0], i1031[1], 0, i1030, 'm_endGameCanvas')
  request.r(i1031[2], i1031[3], 0, i1030, 'm_headerCanvas')
  request.r(i1031[4], i1031[5], 0, i1030, 'm_levelTransitionOverlay')
  request.r(i1031[6], i1031[7], 0, i1030, 'm_endText')
  request.r(i1031[8], i1031[9], 0, i1030, 'm_endText1')
  request.r(i1031[10], i1031[11], 0, i1030, 'm_buttonText')
  request.r(i1031[12], i1031[13], 0, i1030, 'm_buttonText1')
  var i1033 = i1031[14]
  var i1032 = []
  for(var i = 0; i < i1033.length; i += 2) {
  request.r(i1033[i + 0], i1033[i + 1], 2, i1032, '')
  }
  i1030.m_endGameSound = i1032
  request.r(i1031[15], i1031[16], 0, i1030, 'm_loadingText')
  return i1030
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Transform"] = function (request, data, root) {
  var i1036 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Transform' )
  var i1037 = data
  i1036.position = new pc.Vec3( i1037[0], i1037[1], i1037[2] )
  i1036.scale = new pc.Vec3( i1037[3], i1037[4], i1037[5] )
  i1036.rotation = new pc.Quat(i1037[6], i1037[7], i1037[8], i1037[9])
  return i1036
}

Deserializers["UnityEngine.EventSystems.EventSystem"] = function (request, data, root) {
  var i1038 = root || request.c( 'UnityEngine.EventSystems.EventSystem' )
  var i1039 = data
  request.r(i1039[0], i1039[1], 0, i1038, 'm_FirstSelected')
  i1038.m_sendNavigationEvents = !!i1039[2]
  i1038.m_DragThreshold = i1039[3]
  return i1038
}

Deserializers["UnityEngine.EventSystems.StandaloneInputModule"] = function (request, data, root) {
  var i1040 = root || request.c( 'UnityEngine.EventSystems.StandaloneInputModule' )
  var i1041 = data
  i1040.m_HorizontalAxis = i1041[0]
  i1040.m_VerticalAxis = i1041[1]
  i1040.m_SubmitButton = i1041[2]
  i1040.m_CancelButton = i1041[3]
  i1040.m_InputActionsPerSecond = i1041[4]
  i1040.m_RepeatDelay = i1041[5]
  i1040.m_ForceModuleActive = !!i1041[6]
  i1040.m_SendPointerHoverToParent = !!i1041[7]
  return i1040
}

Deserializers["TutorialLayer"] = function (request, data, root) {
  var i1042 = root || request.c( 'TutorialLayer' )
  var i1043 = data
  request.r(i1043[0], i1043[1], 0, i1042, 'handTrans')
  request.r(i1043[2], i1043[3], 0, i1042, 'handImage')
  var i1045 = i1043[4]
  var i1044 = []
  for(var i = 0; i < i1045.length; i += 2) {
  request.r(i1045[i + 0], i1045[i + 1], 2, i1044, '')
  }
  i1042.handSprites = i1044
  request.r(i1043[5], i1043[6], 0, i1042, 'tutorialText')
  request.r(i1043[7], i1043[8], 0, i1042, 'flareImage')
  request.r(i1043[9], i1043[10], 0, i1042, 'canvas')
  return i1042
}

Deserializers["TMPro.TextMeshProUGUI"] = function (request, data, root) {
  var i1048 = root || request.c( 'TMPro.TextMeshProUGUI' )
  var i1049 = data
  i1048.m_hasFontAssetChanged = !!i1049[0]
  request.r(i1049[1], i1049[2], 0, i1048, 'm_baseMaterial')
  i1048.m_maskOffset = new pc.Vec4( i1049[3], i1049[4], i1049[5], i1049[6] )
  i1048.m_text = i1049[7]
  i1048.m_isRightToLeft = !!i1049[8]
  request.r(i1049[9], i1049[10], 0, i1048, 'm_fontAsset')
  request.r(i1049[11], i1049[12], 0, i1048, 'm_sharedMaterial')
  var i1051 = i1049[13]
  var i1050 = []
  for(var i = 0; i < i1051.length; i += 2) {
  request.r(i1051[i + 0], i1051[i + 1], 2, i1050, '')
  }
  i1048.m_fontSharedMaterials = i1050
  request.r(i1049[14], i1049[15], 0, i1048, 'm_fontMaterial')
  var i1053 = i1049[16]
  var i1052 = []
  for(var i = 0; i < i1053.length; i += 2) {
  request.r(i1053[i + 0], i1053[i + 1], 2, i1052, '')
  }
  i1048.m_fontMaterials = i1052
  i1048.m_fontColor32 = UnityEngine.Color32.ConstructColor(i1049[17], i1049[18], i1049[19], i1049[20])
  i1048.m_fontColor = new pc.Color(i1049[21], i1049[22], i1049[23], i1049[24])
  i1048.m_enableVertexGradient = !!i1049[25]
  i1048.m_colorMode = i1049[26]
  i1048.m_fontColorGradient = request.d('TMPro.VertexGradient', i1049[27], i1048.m_fontColorGradient)
  request.r(i1049[28], i1049[29], 0, i1048, 'm_fontColorGradientPreset')
  request.r(i1049[30], i1049[31], 0, i1048, 'm_spriteAsset')
  i1048.m_tintAllSprites = !!i1049[32]
  request.r(i1049[33], i1049[34], 0, i1048, 'm_StyleSheet')
  i1048.m_TextStyleHashCode = i1049[35]
  i1048.m_overrideHtmlColors = !!i1049[36]
  i1048.m_faceColor = UnityEngine.Color32.ConstructColor(i1049[37], i1049[38], i1049[39], i1049[40])
  i1048.m_fontSize = i1049[41]
  i1048.m_fontSizeBase = i1049[42]
  i1048.m_fontWeight = i1049[43]
  i1048.m_enableAutoSizing = !!i1049[44]
  i1048.m_fontSizeMin = i1049[45]
  i1048.m_fontSizeMax = i1049[46]
  i1048.m_fontStyle = i1049[47]
  i1048.m_HorizontalAlignment = i1049[48]
  i1048.m_VerticalAlignment = i1049[49]
  i1048.m_textAlignment = i1049[50]
  i1048.m_characterSpacing = i1049[51]
  i1048.m_wordSpacing = i1049[52]
  i1048.m_lineSpacing = i1049[53]
  i1048.m_lineSpacingMax = i1049[54]
  i1048.m_paragraphSpacing = i1049[55]
  i1048.m_charWidthMaxAdj = i1049[56]
  i1048.m_enableWordWrapping = !!i1049[57]
  i1048.m_wordWrappingRatios = i1049[58]
  i1048.m_overflowMode = i1049[59]
  request.r(i1049[60], i1049[61], 0, i1048, 'm_linkedTextComponent')
  request.r(i1049[62], i1049[63], 0, i1048, 'parentLinkedComponent')
  i1048.m_enableKerning = !!i1049[64]
  i1048.m_enableExtraPadding = !!i1049[65]
  i1048.checkPaddingRequired = !!i1049[66]
  i1048.m_isRichText = !!i1049[67]
  i1048.m_parseCtrlCharacters = !!i1049[68]
  i1048.m_isOrthographic = !!i1049[69]
  i1048.m_isCullingEnabled = !!i1049[70]
  i1048.m_horizontalMapping = i1049[71]
  i1048.m_verticalMapping = i1049[72]
  i1048.m_uvLineOffset = i1049[73]
  i1048.m_geometrySortingOrder = i1049[74]
  i1048.m_IsTextObjectScaleStatic = !!i1049[75]
  i1048.m_VertexBufferAutoSizeReduction = !!i1049[76]
  i1048.m_useMaxVisibleDescender = !!i1049[77]
  i1048.m_pageToDisplay = i1049[78]
  i1048.m_margin = new pc.Vec4( i1049[79], i1049[80], i1049[81], i1049[82] )
  i1048.m_isUsingLegacyAnimationComponent = !!i1049[83]
  i1048.m_isVolumetricText = !!i1049[84]
  i1048.m_Maskable = !!i1049[85]
  request.r(i1049[86], i1049[87], 0, i1048, 'm_Material')
  i1048.m_Color = new pc.Color(i1049[88], i1049[89], i1049[90], i1049[91])
  i1048.m_RaycastTarget = !!i1049[92]
  i1048.m_RaycastPadding = new pc.Vec4( i1049[93], i1049[94], i1049[95], i1049[96] )
  return i1048
}

Deserializers["TMPro.VertexGradient"] = function (request, data, root) {
  var i1056 = root || request.c( 'TMPro.VertexGradient' )
  var i1057 = data
  i1056.topLeft = new pc.Color(i1057[0], i1057[1], i1057[2], i1057[3])
  i1056.topRight = new pc.Color(i1057[4], i1057[5], i1057[6], i1057[7])
  i1056.bottomLeft = new pc.Color(i1057[8], i1057[9], i1057[10], i1057[11])
  i1056.bottomRight = new pc.Color(i1057[12], i1057[13], i1057[14], i1057[15])
  return i1056
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i1058 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i1059 = data
  i1058.name = i1059[0]
  i1058.width = i1059[1]
  i1058.height = i1059[2]
  i1058.mipmapCount = i1059[3]
  i1058.anisoLevel = i1059[4]
  i1058.filterMode = i1059[5]
  i1058.hdr = !!i1059[6]
  i1058.format = i1059[7]
  i1058.wrapMode = i1059[8]
  i1058.alphaIsTransparency = !!i1059[9]
  i1058.alphaSource = i1059[10]
  i1058.graphicsFormat = i1059[11]
  i1058.sRGBTexture = !!i1059[12]
  i1058.desiredColorSpace = i1059[13]
  i1058.wrapU = i1059[14]
  i1058.wrapV = i1059[15]
  return i1058
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i1060 = root || new pc.UnityMaterial()
  var i1061 = data
  i1060.name = i1061[0]
  request.r(i1061[1], i1061[2], 0, i1060, 'shader')
  i1060.renderQueue = i1061[3]
  i1060.enableInstancing = !!i1061[4]
  var i1063 = i1061[5]
  var i1062 = []
  for(var i = 0; i < i1063.length; i += 1) {
    i1062.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i1063[i + 0]) );
  }
  i1060.floatParameters = i1062
  var i1065 = i1061[6]
  var i1064 = []
  for(var i = 0; i < i1065.length; i += 1) {
    i1064.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i1065[i + 0]) );
  }
  i1060.colorParameters = i1064
  var i1067 = i1061[7]
  var i1066 = []
  for(var i = 0; i < i1067.length; i += 1) {
    i1066.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i1067[i + 0]) );
  }
  i1060.vectorParameters = i1066
  var i1069 = i1061[8]
  var i1068 = []
  for(var i = 0; i < i1069.length; i += 1) {
    i1068.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i1069[i + 0]) );
  }
  i1060.textureParameters = i1068
  var i1071 = i1061[9]
  var i1070 = []
  for(var i = 0; i < i1071.length; i += 1) {
    i1070.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i1071[i + 0]) );
  }
  i1060.materialFlags = i1070
  return i1060
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i1074 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i1075 = data
  i1074.name = i1075[0]
  i1074.value = i1075[1]
  return i1074
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i1078 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i1079 = data
  i1078.name = i1079[0]
  i1078.value = new pc.Color(i1079[1], i1079[2], i1079[3], i1079[4])
  return i1078
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i1082 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i1083 = data
  i1082.name = i1083[0]
  i1082.value = new pc.Vec4( i1083[1], i1083[2], i1083[3], i1083[4] )
  return i1082
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i1086 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i1087 = data
  i1086.name = i1087[0]
  request.r(i1087[1], i1087[2], 0, i1086, 'value')
  return i1086
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i1090 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i1091 = data
  i1090.name = i1091[0]
  i1090.enabled = !!i1091[1]
  return i1090
}

Deserializers["CollectorGameManager"] = function (request, data, root) {
  var i1092 = root || request.c( 'CollectorGameManager' )
  var i1093 = data
  request.r(i1093[0], i1093[1], 0, i1092, 'queueManager')
  request.r(i1093[2], i1093[3], 0, i1092, 'moveLimiter')
  request.r(i1093[4], i1093[5], 0, i1092, 'inputManager')
  request.r(i1093[6], i1093[7], 0, i1092, 'gamePlaySound')
  request.r(i1093[8], i1093[9], 0, i1092, 'gameplayManager')
  i1092.distanceTf = i1093[10]
  i1092.pendingStartInterval = i1093[11]
  var i1095 = i1093[12]
  var i1094 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorController')))
  for(var i = 0; i < i1095.length; i += 2) {
  request.r(i1095[i + 0], i1095[i + 1], 1, i1094, '')
  }
  i1092.collectorOnDead = i1094
  i1092.countClickToMoveStore = i1093[13]
  i1092.canMoveStoreEqualCount = !!i1093[14]
  return i1092
}

Deserializers["GameplayManager"] = function (request, data, root) {
  var i1098 = root || request.c( 'GameplayManager' )
  var i1099 = data
  request.r(i1099[0], i1099[1], 0, i1098, 'levelGamePlayConfig')
  request.r(i1099[2], i1099[3], 0, i1098, 'levelSetup')
  request.r(i1099[4], i1099[5], 0, i1098, 'levelCollectorsSystem')
  request.r(i1099[6], i1099[7], 0, i1098, 'currentLevelConfig')
  i1098.levelId = i1099[8]
  i1098.enableStoreLimitForFirstLevel = !!i1099[9]
  return i1098
}

Deserializers["SoundManager"] = function (request, data, root) {
  var i1100 = root || request.c( 'SoundManager' )
  var i1101 = data
  request.r(i1101[0], i1101[1], 0, i1100, 'audioMixer')
  request.r(i1101[2], i1101[3], 0, i1100, 'fxMusicSource')
  request.r(i1101[4], i1101[5], 0, i1100, 'specialBgmSource')
  i1100.isPlayBgmOnStart = !!i1101[6]
  i1100.BGM = request.d('SoundDefine', i1101[7], i1100.BGM)
  return i1100
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.AudioSource"] = function (request, data, root) {
  var i1102 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.AudioSource' )
  var i1103 = data
  request.r(i1103[0], i1103[1], 0, i1102, 'clip')
  request.r(i1103[2], i1103[3], 0, i1102, 'outputAudioMixerGroup')
  i1102.playOnAwake = !!i1103[4]
  i1102.loop = !!i1103[5]
  i1102.time = i1103[6]
  i1102.volume = i1103[7]
  i1102.pitch = i1103[8]
  i1102.enabled = !!i1103[9]
  return i1102
}

Deserializers["InputManager"] = function (request, data, root) {
  var i1104 = root || request.c( 'InputManager' )
  var i1105 = data
  i1104.BlockInput = !!i1105[0]
  i1104.IsChoosingBlock = !!i1105[1]
  i1104.IsFreePicking = !!i1105[2]
  i1104.CubeLayermask = UnityEngine.LayerMask.FromIntegerValue( i1105[3] )
  i1104.clickCooldown = i1105[4]
  request.r(i1105[5], i1105[6], 0, i1104, 'gamePlayCamera')
  return i1104
}

Deserializers["CollectorMoveLimiter"] = function (request, data, root) {
  var i1106 = root || request.c( 'CollectorMoveLimiter' )
  var i1107 = data
  request.r(i1107[0], i1107[1], 0, i1106, 'padController')
  i1106.maxActiveMoving = i1107[2]
  request.r(i1107[3], i1107[4], 0, i1106, 'defaultPosition')
  request.r(i1107[5], i1107[6], 0, i1106, 'poolParent')
  request.r(i1107[7], i1107[8], 0, i1106, 'limiterText')
  i1106.padSpacing = i1107[9]
  return i1106
}

Deserializers["CollectorQueueManager"] = function (request, data, root) {
  var i1108 = root || request.c( 'CollectorQueueManager' )
  var i1109 = data
  var i1111 = i1109[0]
  var i1110 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorController')))
  for(var i = 0; i < i1111.length; i += 2) {
  request.r(i1111[i + 0], i1111[i + 1], 1, i1110, '')
  }
  i1108.deadQueue = i1110
  i1108.maxSlot = i1109[1]
  var i1113 = i1109[2]
  var i1112 = []
  for(var i = 0; i < i1113.length; i += 2) {
  request.r(i1113[i + 0], i1113[i + 1], 2, i1112, '')
  }
  i1108.queuePositions = i1112
  request.r(i1109[3], i1109[4], 0, i1108, 'defaultDeadPosition')
  i1108.deadOffset = new pc.Vec3( i1109[5], i1109[6], i1109[7] )
  var i1115 = i1109[8]
  var i1114 = []
  for(var i = 0; i < i1115.length; i += 2) {
  request.r(i1115[i + 0], i1115[i + 1], 2, i1114, '')
  }
  i1108.queueArray = i1114
  request.r(i1109[9], i1109[10], 0, i1108, 'alertFullSlotAnim')
  return i1108
}

Deserializers["AlertFullSlotAnim"] = function (request, data, root) {
  var i1120 = root || request.c( 'AlertFullSlotAnim' )
  var i1121 = data
  var i1123 = i1121[0]
  var i1122 = []
  for(var i = 0; i < i1123.length; i += 2) {
  request.r(i1123[i + 0], i1123[i + 1], 2, i1122, '')
  }
  i1120.alertImages = i1122
  i1120.animDuration = i1121[1]
  return i1120
}

Deserializers["Grid3DLayout"] = function (request, data, root) {
  var i1126 = root || request.c( 'Grid3DLayout' )
  var i1127 = data
  i1126.columns = i1127[0]
  i1126.spacing = i1127[1]
  return i1126
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshFilter"] = function (request, data, root) {
  var i1128 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshFilter' )
  var i1129 = data
  request.r(i1129[0], i1129[1], 0, i1128, 'sharedMesh')
  return i1128
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshRenderer"] = function (request, data, root) {
  var i1130 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshRenderer' )
  var i1131 = data
  request.r(i1131[0], i1131[1], 0, i1130, 'additionalVertexStreams')
  i1130.enabled = !!i1131[2]
  request.r(i1131[3], i1131[4], 0, i1130, 'sharedMaterial')
  var i1133 = i1131[5]
  var i1132 = []
  for(var i = 0; i < i1133.length; i += 2) {
  request.r(i1133[i + 0], i1133[i + 1], 2, i1132, '')
  }
  i1130.sharedMaterials = i1132
  i1130.receiveShadows = !!i1131[6]
  i1130.shadowCastingMode = i1131[7]
  i1130.sortingLayerID = i1131[8]
  i1130.sortingOrder = i1131[9]
  i1130.lightmapIndex = i1131[10]
  i1130.lightmapSceneIndex = i1131[11]
  i1130.lightmapScaleOffset = new pc.Vec4( i1131[12], i1131[13], i1131[14], i1131[15] )
  i1130.lightProbeUsage = i1131[16]
  i1130.reflectionProbeUsage = i1131[17]
  return i1130
}

Deserializers["MeshCombiner"] = function (request, data, root) {
  var i1134 = root || request.c( 'MeshCombiner' )
  var i1135 = data
  i1134.createMultiMaterialMesh = !!i1135[0]
  i1134.combineInactiveChildren = !!i1135[1]
  i1134.deactivateCombinedChildren = !!i1135[2]
  i1134.deactivateCombinedChildrenMeshRenderers = !!i1135[3]
  i1134.generateUVMap = !!i1135[4]
  i1134.destroyCombinedChildren = !!i1135[5]
  i1134.folderPath = i1135[6]
  var i1137 = i1135[7]
  var i1136 = []
  for(var i = 0; i < i1137.length; i += 2) {
  request.r(i1137[i + 0], i1137[i + 1], 2, i1136, '')
  }
  i1134.meshFiltersToSkip = i1136
  return i1134
}

Deserializers["GamePlaySound"] = function (request, data, root) {
  var i1140 = root || request.c( 'GamePlaySound' )
  var i1141 = data
  request.r(i1141[0], i1141[1], 0, i1140, 'clickCatSound')
  return i1140
}

Deserializers["PadController"] = function (request, data, root) {
  var i1142 = root || request.c( 'PadController' )
  var i1143 = data
  request.r(i1143[0], i1143[1], 0, i1142, 'meshTrans')
  request.r(i1143[2], i1143[3], 0, i1142, 'startTrans')
  request.r(i1143[4], i1143[5], 0, i1142, 'PadSmokeFX')
  i1142.HasCollectorOnPad = !!i1143[6]
  return i1142
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystem"] = function (request, data, root) {
  var i1144 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystem' )
  var i1145 = data
  i1144.main = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule', i1145[0], i1144.main)
  i1144.colorBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule', i1145[1], i1144.colorBySpeed)
  i1144.colorOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule', i1145[2], i1144.colorOverLifetime)
  i1144.emission = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule', i1145[3], i1144.emission)
  i1144.rotationBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule', i1145[4], i1144.rotationBySpeed)
  i1144.rotationOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule', i1145[5], i1144.rotationOverLifetime)
  i1144.shape = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule', i1145[6], i1144.shape)
  i1144.sizeBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule', i1145[7], i1144.sizeBySpeed)
  i1144.sizeOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule', i1145[8], i1144.sizeOverLifetime)
  i1144.textureSheetAnimation = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule', i1145[9], i1144.textureSheetAnimation)
  i1144.velocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule', i1145[10], i1144.velocityOverLifetime)
  i1144.noise = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule', i1145[11], i1144.noise)
  i1144.inheritVelocity = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule', i1145[12], i1144.inheritVelocity)
  i1144.forceOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule', i1145[13], i1144.forceOverLifetime)
  i1144.limitVelocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule', i1145[14], i1144.limitVelocityOverLifetime)
  i1144.useAutoRandomSeed = !!i1145[15]
  i1144.randomSeed = i1145[16]
  return i1144
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule"] = function (request, data, root) {
  var i1146 = root || new pc.ParticleSystemMain()
  var i1147 = data
  i1146.duration = i1147[0]
  i1146.loop = !!i1147[1]
  i1146.prewarm = !!i1147[2]
  i1146.startDelay = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1147[3], i1146.startDelay)
  i1146.startLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1147[4], i1146.startLifetime)
  i1146.startSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1147[5], i1146.startSpeed)
  i1146.startSize3D = !!i1147[6]
  i1146.startSizeX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1147[7], i1146.startSizeX)
  i1146.startSizeY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1147[8], i1146.startSizeY)
  i1146.startSizeZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1147[9], i1146.startSizeZ)
  i1146.startRotation3D = !!i1147[10]
  i1146.startRotationX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1147[11], i1146.startRotationX)
  i1146.startRotationY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1147[12], i1146.startRotationY)
  i1146.startRotationZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1147[13], i1146.startRotationZ)
  i1146.startColor = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i1147[14], i1146.startColor)
  i1146.gravityModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1147[15], i1146.gravityModifier)
  i1146.simulationSpace = i1147[16]
  request.r(i1147[17], i1147[18], 0, i1146, 'customSimulationSpace')
  i1146.simulationSpeed = i1147[19]
  i1146.useUnscaledTime = !!i1147[20]
  i1146.scalingMode = i1147[21]
  i1146.playOnAwake = !!i1147[22]
  i1146.maxParticles = i1147[23]
  i1146.emitterVelocityMode = i1147[24]
  i1146.stopAction = i1147[25]
  return i1146
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve"] = function (request, data, root) {
  var i1148 = root || new pc.MinMaxCurve()
  var i1149 = data
  i1148.mode = i1149[0]
  i1148.curveMin = new pc.AnimationCurve( { keys_flow: i1149[1] } )
  i1148.curveMax = new pc.AnimationCurve( { keys_flow: i1149[2] } )
  i1148.curveMultiplier = i1149[3]
  i1148.constantMin = i1149[4]
  i1148.constantMax = i1149[5]
  return i1148
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient"] = function (request, data, root) {
  var i1150 = root || new pc.MinMaxGradient()
  var i1151 = data
  i1150.mode = i1151[0]
  i1150.gradientMin = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i1151[1], i1150.gradientMin)
  i1150.gradientMax = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i1151[2], i1150.gradientMax)
  i1150.colorMin = new pc.Color(i1151[3], i1151[4], i1151[5], i1151[6])
  i1150.colorMax = new pc.Color(i1151[7], i1151[8], i1151[9], i1151[10])
  return i1150
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient"] = function (request, data, root) {
  var i1152 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient' )
  var i1153 = data
  i1152.mode = i1153[0]
  var i1155 = i1153[1]
  var i1154 = []
  for(var i = 0; i < i1155.length; i += 1) {
    i1154.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey', i1155[i + 0]) );
  }
  i1152.colorKeys = i1154
  var i1157 = i1153[2]
  var i1156 = []
  for(var i = 0; i < i1157.length; i += 1) {
    i1156.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey', i1157[i + 0]) );
  }
  i1152.alphaKeys = i1156
  return i1152
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule"] = function (request, data, root) {
  var i1158 = root || new pc.ParticleSystemColorBySpeed()
  var i1159 = data
  i1158.enabled = !!i1159[0]
  i1158.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i1159[1], i1158.color)
  i1158.range = new pc.Vec2( i1159[2], i1159[3] )
  return i1158
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey"] = function (request, data, root) {
  var i1162 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey' )
  var i1163 = data
  i1162.color = new pc.Color(i1163[0], i1163[1], i1163[2], i1163[3])
  i1162.time = i1163[4]
  return i1162
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey"] = function (request, data, root) {
  var i1166 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey' )
  var i1167 = data
  i1166.alpha = i1167[0]
  i1166.time = i1167[1]
  return i1166
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule"] = function (request, data, root) {
  var i1168 = root || new pc.ParticleSystemColorOverLifetime()
  var i1169 = data
  i1168.enabled = !!i1169[0]
  i1168.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i1169[1], i1168.color)
  return i1168
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule"] = function (request, data, root) {
  var i1170 = root || new pc.ParticleSystemEmitter()
  var i1171 = data
  i1170.enabled = !!i1171[0]
  i1170.rateOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1171[1], i1170.rateOverTime)
  i1170.rateOverDistance = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1171[2], i1170.rateOverDistance)
  var i1173 = i1171[3]
  var i1172 = []
  for(var i = 0; i < i1173.length; i += 1) {
    i1172.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst', i1173[i + 0]) );
  }
  i1170.bursts = i1172
  return i1170
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst"] = function (request, data, root) {
  var i1176 = root || new pc.ParticleSystemBurst()
  var i1177 = data
  i1176.count = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1177[0], i1176.count)
  i1176.cycleCount = i1177[1]
  i1176.minCount = i1177[2]
  i1176.maxCount = i1177[3]
  i1176.repeatInterval = i1177[4]
  i1176.time = i1177[5]
  return i1176
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule"] = function (request, data, root) {
  var i1178 = root || new pc.ParticleSystemRotationBySpeed()
  var i1179 = data
  i1178.enabled = !!i1179[0]
  i1178.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1179[1], i1178.x)
  i1178.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1179[2], i1178.y)
  i1178.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1179[3], i1178.z)
  i1178.separateAxes = !!i1179[4]
  i1178.range = new pc.Vec2( i1179[5], i1179[6] )
  return i1178
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule"] = function (request, data, root) {
  var i1180 = root || new pc.ParticleSystemRotationOverLifetime()
  var i1181 = data
  i1180.enabled = !!i1181[0]
  i1180.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1181[1], i1180.x)
  i1180.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1181[2], i1180.y)
  i1180.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1181[3], i1180.z)
  i1180.separateAxes = !!i1181[4]
  return i1180
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule"] = function (request, data, root) {
  var i1182 = root || new pc.ParticleSystemShape()
  var i1183 = data
  i1182.enabled = !!i1183[0]
  i1182.shapeType = i1183[1]
  i1182.randomDirectionAmount = i1183[2]
  i1182.sphericalDirectionAmount = i1183[3]
  i1182.randomPositionAmount = i1183[4]
  i1182.alignToDirection = !!i1183[5]
  i1182.radius = i1183[6]
  i1182.radiusMode = i1183[7]
  i1182.radiusSpread = i1183[8]
  i1182.radiusSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1183[9], i1182.radiusSpeed)
  i1182.radiusThickness = i1183[10]
  i1182.angle = i1183[11]
  i1182.length = i1183[12]
  i1182.boxThickness = new pc.Vec3( i1183[13], i1183[14], i1183[15] )
  i1182.meshShapeType = i1183[16]
  request.r(i1183[17], i1183[18], 0, i1182, 'mesh')
  request.r(i1183[19], i1183[20], 0, i1182, 'meshRenderer')
  request.r(i1183[21], i1183[22], 0, i1182, 'skinnedMeshRenderer')
  i1182.useMeshMaterialIndex = !!i1183[23]
  i1182.meshMaterialIndex = i1183[24]
  i1182.useMeshColors = !!i1183[25]
  i1182.normalOffset = i1183[26]
  i1182.arc = i1183[27]
  i1182.arcMode = i1183[28]
  i1182.arcSpread = i1183[29]
  i1182.arcSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1183[30], i1182.arcSpeed)
  i1182.donutRadius = i1183[31]
  i1182.position = new pc.Vec3( i1183[32], i1183[33], i1183[34] )
  i1182.rotation = new pc.Vec3( i1183[35], i1183[36], i1183[37] )
  i1182.scale = new pc.Vec3( i1183[38], i1183[39], i1183[40] )
  return i1182
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule"] = function (request, data, root) {
  var i1184 = root || new pc.ParticleSystemSizeBySpeed()
  var i1185 = data
  i1184.enabled = !!i1185[0]
  i1184.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[1], i1184.x)
  i1184.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[2], i1184.y)
  i1184.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1185[3], i1184.z)
  i1184.separateAxes = !!i1185[4]
  i1184.range = new pc.Vec2( i1185[5], i1185[6] )
  return i1184
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule"] = function (request, data, root) {
  var i1186 = root || new pc.ParticleSystemSizeOverLifetime()
  var i1187 = data
  i1186.enabled = !!i1187[0]
  i1186.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1187[1], i1186.x)
  i1186.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1187[2], i1186.y)
  i1186.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1187[3], i1186.z)
  i1186.separateAxes = !!i1187[4]
  return i1186
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule"] = function (request, data, root) {
  var i1188 = root || new pc.ParticleSystemTextureSheetAnimation()
  var i1189 = data
  i1188.enabled = !!i1189[0]
  i1188.mode = i1189[1]
  i1188.animation = i1189[2]
  i1188.numTilesX = i1189[3]
  i1188.numTilesY = i1189[4]
  i1188.useRandomRow = !!i1189[5]
  i1188.frameOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1189[6], i1188.frameOverTime)
  i1188.startFrame = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1189[7], i1188.startFrame)
  i1188.cycleCount = i1189[8]
  i1188.rowIndex = i1189[9]
  i1188.flipU = i1189[10]
  i1188.flipV = i1189[11]
  i1188.spriteCount = i1189[12]
  var i1191 = i1189[13]
  var i1190 = []
  for(var i = 0; i < i1191.length; i += 2) {
  request.r(i1191[i + 0], i1191[i + 1], 2, i1190, '')
  }
  i1188.sprites = i1190
  return i1188
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule"] = function (request, data, root) {
  var i1192 = root || new pc.ParticleSystemVelocityOverLifetime()
  var i1193 = data
  i1192.enabled = !!i1193[0]
  i1192.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[1], i1192.x)
  i1192.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[2], i1192.y)
  i1192.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[3], i1192.z)
  i1192.radial = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[4], i1192.radial)
  i1192.speedModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[5], i1192.speedModifier)
  i1192.space = i1193[6]
  i1192.orbitalX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[7], i1192.orbitalX)
  i1192.orbitalY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[8], i1192.orbitalY)
  i1192.orbitalZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[9], i1192.orbitalZ)
  i1192.orbitalOffsetX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[10], i1192.orbitalOffsetX)
  i1192.orbitalOffsetY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[11], i1192.orbitalOffsetY)
  i1192.orbitalOffsetZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1193[12], i1192.orbitalOffsetZ)
  return i1192
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule"] = function (request, data, root) {
  var i1194 = root || new pc.ParticleSystemNoise()
  var i1195 = data
  i1194.enabled = !!i1195[0]
  i1194.separateAxes = !!i1195[1]
  i1194.strengthX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1195[2], i1194.strengthX)
  i1194.strengthY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1195[3], i1194.strengthY)
  i1194.strengthZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1195[4], i1194.strengthZ)
  i1194.frequency = i1195[5]
  i1194.damping = !!i1195[6]
  i1194.octaveCount = i1195[7]
  i1194.octaveMultiplier = i1195[8]
  i1194.octaveScale = i1195[9]
  i1194.quality = i1195[10]
  i1194.scrollSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1195[11], i1194.scrollSpeed)
  i1194.scrollSpeedMultiplier = i1195[12]
  i1194.remapEnabled = !!i1195[13]
  i1194.remapX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1195[14], i1194.remapX)
  i1194.remapY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1195[15], i1194.remapY)
  i1194.remapZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1195[16], i1194.remapZ)
  i1194.positionAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1195[17], i1194.positionAmount)
  i1194.rotationAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1195[18], i1194.rotationAmount)
  i1194.sizeAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1195[19], i1194.sizeAmount)
  return i1194
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule"] = function (request, data, root) {
  var i1196 = root || new pc.ParticleSystemInheritVelocity()
  var i1197 = data
  i1196.enabled = !!i1197[0]
  i1196.mode = i1197[1]
  i1196.curve = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1197[2], i1196.curve)
  return i1196
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule"] = function (request, data, root) {
  var i1198 = root || new pc.ParticleSystemForceOverLifetime()
  var i1199 = data
  i1198.enabled = !!i1199[0]
  i1198.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1199[1], i1198.x)
  i1198.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1199[2], i1198.y)
  i1198.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1199[3], i1198.z)
  i1198.space = i1199[4]
  i1198.randomized = !!i1199[5]
  return i1198
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule"] = function (request, data, root) {
  var i1200 = root || new pc.ParticleSystemLimitVelocityOverLifetime()
  var i1201 = data
  i1200.enabled = !!i1201[0]
  i1200.limit = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1201[1], i1200.limit)
  i1200.limitX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1201[2], i1200.limitX)
  i1200.limitY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1201[3], i1200.limitY)
  i1200.limitZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1201[4], i1200.limitZ)
  i1200.dampen = i1201[5]
  i1200.separateAxes = !!i1201[6]
  i1200.space = i1201[7]
  i1200.drag = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i1201[8], i1200.drag)
  i1200.multiplyDragByParticleSize = !!i1201[9]
  i1200.multiplyDragByParticleVelocity = !!i1201[10]
  return i1200
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer"] = function (request, data, root) {
  var i1202 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer' )
  var i1203 = data
  request.r(i1203[0], i1203[1], 0, i1202, 'mesh')
  i1202.meshCount = i1203[2]
  i1202.activeVertexStreamsCount = i1203[3]
  i1202.alignment = i1203[4]
  i1202.renderMode = i1203[5]
  i1202.sortMode = i1203[6]
  i1202.lengthScale = i1203[7]
  i1202.velocityScale = i1203[8]
  i1202.cameraVelocityScale = i1203[9]
  i1202.normalDirection = i1203[10]
  i1202.sortingFudge = i1203[11]
  i1202.minParticleSize = i1203[12]
  i1202.maxParticleSize = i1203[13]
  i1202.pivot = new pc.Vec3( i1203[14], i1203[15], i1203[16] )
  request.r(i1203[17], i1203[18], 0, i1202, 'trailMaterial')
  i1202.applyActiveColorSpace = !!i1203[19]
  i1202.enabled = !!i1203[20]
  request.r(i1203[21], i1203[22], 0, i1202, 'sharedMaterial')
  var i1205 = i1203[23]
  var i1204 = []
  for(var i = 0; i < i1205.length; i += 2) {
  request.r(i1205[i + 0], i1205[i + 1], 2, i1204, '')
  }
  i1202.sharedMaterials = i1204
  i1202.receiveShadows = !!i1203[24]
  i1202.shadowCastingMode = i1203[25]
  i1202.sortingLayerID = i1203[26]
  i1202.sortingOrder = i1203[27]
  i1202.lightmapIndex = i1203[28]
  i1202.lightmapSceneIndex = i1203[29]
  i1202.lightmapScaleOffset = new pc.Vec4( i1203[30], i1203[31], i1203[32], i1203[33] )
  i1202.lightProbeUsage = i1203[34]
  i1202.reflectionProbeUsage = i1203[35]
  return i1202
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer"] = function (request, data, root) {
  var i1206 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer' )
  var i1207 = data
  i1206.color = new pc.Color(i1207[0], i1207[1], i1207[2], i1207[3])
  request.r(i1207[4], i1207[5], 0, i1206, 'sprite')
  i1206.flipX = !!i1207[6]
  i1206.flipY = !!i1207[7]
  i1206.drawMode = i1207[8]
  i1206.size = new pc.Vec2( i1207[9], i1207[10] )
  i1206.tileMode = i1207[11]
  i1206.adaptiveModeThreshold = i1207[12]
  i1206.maskInteraction = i1207[13]
  i1206.spriteSortPoint = i1207[14]
  i1206.enabled = !!i1207[15]
  request.r(i1207[16], i1207[17], 0, i1206, 'sharedMaterial')
  var i1209 = i1207[18]
  var i1208 = []
  for(var i = 0; i < i1209.length; i += 2) {
  request.r(i1209[i + 0], i1209[i + 1], 2, i1208, '')
  }
  i1206.sharedMaterials = i1208
  i1206.receiveShadows = !!i1207[19]
  i1206.shadowCastingMode = i1207[20]
  i1206.sortingLayerID = i1207[21]
  i1206.sortingOrder = i1207[22]
  i1206.lightmapIndex = i1207[23]
  i1206.lightmapSceneIndex = i1207[24]
  i1206.lightmapScaleOffset = new pc.Vec4( i1207[25], i1207[26], i1207[27], i1207[28] )
  i1206.lightProbeUsage = i1207[29]
  i1206.reflectionProbeUsage = i1207[30]
  return i1206
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh"] = function (request, data, root) {
  var i1210 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh' )
  var i1211 = data
  i1210.name = i1211[0]
  i1210.halfPrecision = !!i1211[1]
  i1210.useUInt32IndexFormat = !!i1211[2]
  i1210.vertexCount = i1211[3]
  i1210.aabb = i1211[4]
  var i1213 = i1211[5]
  var i1212 = []
  for(var i = 0; i < i1213.length; i += 1) {
    i1212.push( !!i1213[i + 0] );
  }
  i1210.streams = i1212
  i1210.vertices = i1211[6]
  var i1215 = i1211[7]
  var i1214 = []
  for(var i = 0; i < i1215.length; i += 1) {
    i1214.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh', i1215[i + 0]) );
  }
  i1210.subMeshes = i1214
  var i1217 = i1211[8]
  var i1216 = []
  for(var i = 0; i < i1217.length; i += 16) {
    i1216.push( new pc.Mat4().setData(i1217[i + 0], i1217[i + 1], i1217[i + 2], i1217[i + 3],  i1217[i + 4], i1217[i + 5], i1217[i + 6], i1217[i + 7],  i1217[i + 8], i1217[i + 9], i1217[i + 10], i1217[i + 11],  i1217[i + 12], i1217[i + 13], i1217[i + 14], i1217[i + 15]) );
  }
  i1210.bindposes = i1216
  var i1219 = i1211[9]
  var i1218 = []
  for(var i = 0; i < i1219.length; i += 1) {
    i1218.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape', i1219[i + 0]) );
  }
  i1210.blendShapes = i1218
  return i1210
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh"] = function (request, data, root) {
  var i1224 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh' )
  var i1225 = data
  i1224.triangles = i1225[0]
  return i1224
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape"] = function (request, data, root) {
  var i1230 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape' )
  var i1231 = data
  i1230.name = i1231[0]
  var i1233 = i1231[1]
  var i1232 = []
  for(var i = 0; i < i1233.length; i += 1) {
    i1232.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame', i1233[i + 0]) );
  }
  i1230.frames = i1232
  return i1230
}

Deserializers["PaintingGridObject"] = function (request, data, root) {
  var i1234 = root || request.c( 'PaintingGridObject' )
  var i1235 = data
  request.r(i1235[0], i1235[1], 0, i1234, 'EffectHandler')
  request.r(i1235[2], i1235[3], 0, i1234, 'EffectOptions')
  request.r(i1235[4], i1235[5], 0, i1234, 'ColorPalette')
  i1234.gridSize = new pc.Vec2( i1235[6], i1235[7] )
  var i1237 = i1235[8]
  var i1236 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1237.length; i += 1) {
    i1236.add(request.d('PaintingPixel', i1237[i + 0]));
  }
  i1234.paintingPixels = i1236
  request.r(i1235[9], i1235[10], 0, i1234, 'GridTransform')
  var i1239 = i1235[11]
  var i1238 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i1239.length; i += 1) {
    i1238.add(i1239[i + 0]);
  }
  i1234.CurrentLevelColor = i1238
  var i1241 = i1235[12]
  var i1240 = new (System.Collections.Generic.List$1(Bridge.ns('PipeObject')))
  for(var i = 0; i < i1241.length; i += 2) {
  request.r(i1241[i + 0], i1241[i + 1], 1, i1240, '')
  }
  i1234.PipeObjects = i1240
  var i1243 = i1235[13]
  var i1242 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1243.length; i += 1) {
    i1242.add(request.d('PaintingPixel', i1243[i + 0]));
  }
  i1234.pipeObjectsPixels = i1242
  var i1245 = i1235[14]
  var i1244 = new (System.Collections.Generic.List$1(Bridge.ns('WallObject')))
  for(var i = 0; i < i1245.length; i += 2) {
  request.r(i1245[i + 0], i1245[i + 1], 1, i1244, '')
  }
  i1234.WallObjects = i1244
  var i1247 = i1235[15]
  var i1246 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1247.length; i += 1) {
    i1246.add(request.d('PaintingPixel', i1247[i + 0]));
  }
  i1234.wallObjectsPixels = i1246
  var i1249 = i1235[16]
  var i1248 = new (System.Collections.Generic.List$1(Bridge.ns('KeyObject')))
  for(var i = 0; i < i1249.length; i += 2) {
  request.r(i1249[i + 0], i1249[i + 1], 1, i1248, '')
  }
  i1234.KeyObjects = i1248
  var i1251 = i1235[17]
  var i1250 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1251.length; i += 1) {
    i1250.add(request.d('PaintingPixel', i1251[i + 0]));
  }
  i1234.keyObjectsPixels = i1250
  var i1253 = i1235[18]
  var i1252 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1253.length; i += 1) {
    i1252.add(request.d('PaintingPixel', i1253[i + 0]));
  }
  i1234.AdditionPaintingPixels = i1252
  i1234.pixelArrangeSpace = i1235[19]
  i1234.blockScale = new pc.Vec3( i1235[20], i1235[21], i1235[22] )
  request.r(i1235[23], i1235[24], 0, i1234, 'pixelPrefab')
  i1234.YOffset = i1235[25]
  i1234.colorVariationAmount = i1235[26]
  request.r(i1235[27], i1235[28], 0, i1234, 'PrefabSource')
  request.r(i1235[29], i1235[30], 0, i1234, 'basePixelSharedMaterial')
  i1234.PixelCount = i1235[31]
  i1234.PixelDestroyed = i1235[32]
  i1234.MinRow = i1235[33]
  i1234.MaxRow = i1235[34]
  i1234.MinColumn = i1235[35]
  i1234.MaxColumn = i1235[36]
  return i1234
}

Deserializers["PaintingPixel"] = function (request, data, root) {
  var i1256 = root || request.c( 'PaintingPixel' )
  var i1257 = data
  i1256.name = i1257[0]
  i1256.column = i1257[1]
  i1256.row = i1257[2]
  i1256.color = new pc.Color(i1257[3], i1257[4], i1257[5], i1257[6])
  i1256.colorCode = i1257[7]
  i1256.worldPos = new pc.Vec3( i1257[8], i1257[9], i1257[10] )
  i1256.Hearts = i1257[11]
  i1256.destroyed = !!i1257[12]
  request.r(i1257[13], i1257[14], 0, i1256, 'pixelObject')
  request.r(i1257[15], i1257[16], 0, i1256, 'PixelComponent')
  i1256.Hidden = !!i1257[17]
  i1256.VunableToAll = !!i1257[18]
  i1256.Indestructible = !!i1257[19]
  i1256.IsPipePixel = !!i1257[20]
  i1256.IsWallPixel = !!i1257[21]
  i1256.UsePaletteMaterialColor = !!i1257[22]
  request.r(i1257[23], i1257[24], 0, i1256, 'Material')
  return i1256
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.BoxCollider"] = function (request, data, root) {
  var i1266 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.BoxCollider' )
  var i1267 = data
  i1266.center = new pc.Vec3( i1267[0], i1267[1], i1267[2] )
  i1266.size = new pc.Vec3( i1267[3], i1267[4], i1267[5] )
  i1266.enabled = !!i1267[6]
  i1266.isTrigger = !!i1267[7]
  request.r(i1267[8], i1267[9], 0, i1266, 'material')
  return i1266
}

Deserializers["PaintingPixelComponent"] = function (request, data, root) {
  var i1268 = root || request.c( 'PaintingPixelComponent' )
  var i1269 = data
  request.r(i1269[0], i1269[1], 0, i1268, 'CubeTransform')
  i1268.PixelData = request.d('PaintingPixel', i1269[2], i1268.PixelData)
  request.r(i1269[3], i1269[4], 0, i1268, 'CubeRenderer')
  i1268.CurrentHearts = i1269[5]
  request.r(i1269[6], i1269[7], 0, i1268, 'EffectOptions')
  i1268.shakeLoops = i1269[8]
  i1268.shakeDuration = i1269[9]
  i1268.UseLazyTweens = !!i1269[10]
  i1268.initScale = new pc.Vec3( i1269[11], i1269[12], i1269[13] )
  return i1268
}

Deserializers["PipeObject"] = function (request, data, root) {
  var i1270 = root || request.c( 'PipeObject' )
  var i1271 = data
  request.r(i1271[0], i1271[1], 0, i1270, 'VisualHandler')
  var i1273 = i1271[2]
  var i1272 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1273.length; i += 1) {
    i1272.add(request.d('PaintingPixel', i1273[i + 0]));
  }
  i1270.PaintingPixelsCovered = i1272
  i1270.PixelDestroyed = i1271[3]
  request.r(i1271[4], i1271[5], 0, i1270, 'HeartText')
  i1270.Hearts = i1271[6]
  i1270.ColorCode = i1271[7]
  i1270.IsHorizontal = !!i1271[8]
  request.r(i1271[9], i1271[10], 0, i1270, 'PipeTransform')
  request.r(i1271[11], i1271[12], 0, i1270, 'PipeBodyTransform')
  i1270.PipeHeadDefaultScale = new pc.Vec3( i1271[13], i1271[14], i1271[15] )
  i1270.PipeBodyDefaultScale = new pc.Vec3( i1271[16], i1271[17], i1271[18] )
  i1270.Destroyed = !!i1271[19]
  i1270.RemainingHearts = i1271[20]
  i1270.HeartLoss = i1271[21]
  i1270.WorldPos = new pc.Vec3( i1271[22], i1271[23], i1271[24] )
  return i1270
}

Deserializers["PipePartVisualHandle"] = function (request, data, root) {
  var i1274 = root || request.c( 'PipePartVisualHandle' )
  var i1275 = data
  request.r(i1275[0], i1275[1], 0, i1274, 'TextureScaler')
  var i1277 = i1275[2]
  var i1276 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Renderer')))
  for(var i = 0; i < i1277.length; i += 2) {
  request.r(i1277[i + 0], i1277[i + 1], 1, i1276, '')
  }
  i1274.pipeRenderers = i1276
  request.r(i1275[3], i1275[4], 0, i1274, 'PipeScaleBody')
  i1274.BrightnessRange = new pc.Vec2( i1275[5], i1275[6] )
  i1274.FlashDuration = i1275[7]
  return i1274
}

Deserializers["AutoTextureScale"] = function (request, data, root) {
  var i1280 = root || request.c( 'AutoTextureScale' )
  var i1281 = data
  i1280.Active = !!i1281[0]
  i1280.referenceScale = new pc.Vec3( i1281[1], i1281[2], i1281[3] )
  i1280.referenceTiling = new pc.Vec2( i1281[4], i1281[5] )
  request.r(i1281[6], i1281[7], 0, i1280, 'rend')
  i1280.updateEveryNFrames = i1281[8]
  return i1280
}

Deserializers["KeyObject"] = function (request, data, root) {
  var i1282 = root || request.c( 'KeyObject' )
  var i1283 = data
  request.r(i1283[0], i1283[1], 0, i1282, 'KeyTransform')
  var i1285 = i1283[2]
  var i1284 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1285.length; i += 1) {
    i1284.add(request.d('PaintingPixel', i1285[i + 0]));
  }
  i1282.PaintingPixelsCovered = i1284
  var i1287 = i1283[3]
  var i1286 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Vector2')))
  for(var i = 0; i < i1287.length; i += 2) {
    i1286.add(new pc.Vec2( i1287[i + 0], i1287[i + 1] ));
  }
  i1282.BorderPixels = i1286
  request.r(i1283[4], i1283[5], 0, i1282, 'KeyRenderer')
  request.r(i1283[6], i1283[7], 0, i1282, 'Rotating')
  request.r(i1283[8], i1283[9], 0, i1282, 'IdleMoving')
  request.r(i1283[10], i1283[11], 0, i1282, 'CollectedFX')
  request.r(i1283[12], i1283[13], 0, i1282, 'TwinkleFX')
  request.r(i1283[14], i1283[15], 0, i1282, 'KeyAudioSource')
  request.r(i1283[16], i1283[17], 0, i1282, 'CollectedSoundFX')
  request.r(i1283[18], i1283[19], 0, i1282, 'KeyFlySoundFX')
  i1282.CurrentState = i1283[20]
  return i1282
}

Deserializers["IdleRotate"] = function (request, data, root) {
  var i1290 = root || request.c( 'IdleRotate' )
  var i1291 = data
  i1290.Active = !!i1291[0]
  request.r(i1291[1], i1291[2], 0, i1290, 'target')
  i1290.rotationSpeed = new pc.Vec3( i1291[3], i1291[4], i1291[5] )
  i1290.frameInterval = i1291[6]
  return i1290
}

Deserializers["IdleMoveUpDown"] = function (request, data, root) {
  var i1292 = root || request.c( 'IdleMoveUpDown' )
  var i1293 = data
  request.r(i1293[0], i1293[1], 0, i1292, 'target')
  i1292.moveAmount = i1293[2]
  i1292.duration = i1293[3]
  i1292.ease = i1293[4]
  return i1292
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.TrailRenderer"] = function (request, data, root) {
  var i1294 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.TrailRenderer' )
  var i1295 = data
  var i1297 = i1295[0]
  var i1296 = []
  for(var i = 0; i < i1297.length; i += 3) {
    i1296.push( new pc.Vec3( i1297[i + 0], i1297[i + 1], i1297[i + 2] ) );
  }
  i1294.positions = i1296
  i1294.positionCount = i1295[1]
  i1294.time = i1295[2]
  i1294.startWidth = i1295[3]
  i1294.endWidth = i1295[4]
  i1294.widthMultiplier = i1295[5]
  i1294.autodestruct = !!i1295[6]
  i1294.emitting = !!i1295[7]
  i1294.numCornerVertices = i1295[8]
  i1294.numCapVertices = i1295[9]
  i1294.minVertexDistance = i1295[10]
  i1294.colorGradient = i1295[11] ? new pc.ColorGradient(i1295[11][0], i1295[11][1], i1295[11][2]) : null
  i1294.startColor = new pc.Color(i1295[12], i1295[13], i1295[14], i1295[15])
  i1294.endColor = new pc.Color(i1295[16], i1295[17], i1295[18], i1295[19])
  i1294.generateLightingData = !!i1295[20]
  i1294.textureMode = i1295[21]
  i1294.alignment = i1295[22]
  i1294.widthCurve = new pc.AnimationCurve( { keys_flow: i1295[23] } )
  i1294.enabled = !!i1295[24]
  request.r(i1295[25], i1295[26], 0, i1294, 'sharedMaterial')
  var i1299 = i1295[27]
  var i1298 = []
  for(var i = 0; i < i1299.length; i += 2) {
  request.r(i1299[i + 0], i1299[i + 1], 2, i1298, '')
  }
  i1294.sharedMaterials = i1298
  i1294.receiveShadows = !!i1295[28]
  i1294.shadowCastingMode = i1295[29]
  i1294.sortingLayerID = i1295[30]
  i1294.sortingOrder = i1295[31]
  i1294.lightmapIndex = i1295[32]
  i1294.lightmapSceneIndex = i1295[33]
  i1294.lightmapScaleOffset = new pc.Vec4( i1295[34], i1295[35], i1295[36], i1295[37] )
  i1294.lightProbeUsage = i1295[38]
  i1294.reflectionProbeUsage = i1295[39]
  return i1294
}

Deserializers["LockObject"] = function (request, data, root) {
  var i1302 = root || request.c( 'LockObject' )
  var i1303 = data
  i1302.Row = i1303[0]
  i1302.IsUnlocked = !!i1303[1]
  i1302.ID = i1303[2]
  request.r(i1303[3], i1303[4], 0, i1302, 'collectorController')
  return i1302
}

Deserializers["CollectorController"] = function (request, data, root) {
  var i1304 = root || request.c( 'CollectorController' )
  var i1305 = data
  request.r(i1305[0], i1305[1], 0, i1304, 'movementHandle')
  request.r(i1305[2], i1305[3], 0, i1304, 'ColorCollector')
  var i1307 = i1305[4]
  var i1306 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorController')))
  for(var i = 0; i < i1307.length; i += 2) {
  request.r(i1307[i + 0], i1307[i + 1], 1, i1306, '')
  }
  i1304.collectorConnect = i1306
  i1304.State = i1305[5]
  i1304.IndexInColumn = i1305[6]
  i1304.ColumnIndex = i1305[7]
  i1304.SlotOnQueue = i1305[8]
  request.r(i1305[9], i1305[10], 0, i1304, 'LockController')
  i1304.IsLockObject = !!i1305[11]
  i1304.IsCompleteColor = !!i1305[12]
  request.r(i1305[13], i1305[14], 0, i1304, 'anim')
  request.r(i1305[15], i1305[16], 0, i1304, 'bulletDisplayHandler')
  return i1304
}

Deserializers["CollectorAnimation"] = function (request, data, root) {
  var i1308 = root || request.c( 'CollectorAnimation' )
  var i1309 = data
  request.r(i1309[0], i1309[1], 0, i1308, 'CollectorController')
  request.r(i1309[2], i1309[3], 0, i1308, 'CollectorInfoController')
  request.r(i1309[4], i1309[5], 0, i1308, 'EffectOptions')
  request.r(i1309[6], i1309[7], 0, i1308, 'RabbitAnimator')
  request.r(i1309[8], i1309[9], 0, i1308, 'BoxAnimator')
  request.r(i1309[10], i1309[11], 0, i1308, 'RootTransform')
  request.r(i1309[12], i1309[13], 0, i1308, 'RabbitTransform')
  i1308.DefaultScale = i1309[14]
  i1308.OnBeltScale = i1309[15]
  i1308.OnDeadScale = i1309[16]
  request.r(i1309[17], i1309[18], 0, i1308, 'CollectorBody')
  i1308.jumpHeight = i1309[19]
  i1308.jumpScaleY = i1309[20]
  i1308.squashScaleY = i1309[21]
  i1308.squashScaleX = i1309[22]
  i1308.durationUp = i1309[23]
  i1308.durationDown = i1309[24]
  i1308.durationRecover = i1309[25]
  i1308.upEase = i1309[26]
  i1308.downEase = i1309[27]
  i1308.recoverEase = i1309[28]
  i1308.squashScaleY2 = i1309[29]
  i1308.squashScaleX2 = i1309[30]
  i1308.ShootAnimation = i1309[31]
  i1308.ShootRate = i1309[32]
  var i1311 = i1309[33]
  var i1310 = []
  for(var i = 0; i < i1311.length; i += 1) {
    i1310.push( i1311[i + 0] );
  }
  i1308.IdleAnimations = i1310
  i1308.RareIdleAnimation = i1309[34]
  i1308.EarIdleAnimation = i1309[35]
  i1308.EarIdleRate = new pc.Vec2( i1309[36], i1309[37] )
  request.r(i1309[38], i1309[39], 0, i1308, 'RabbitRoot')
  i1308.breathScaleX = i1309[40]
  i1308.breathScaleY = i1309[41]
  i1308.duration = i1309[42]
  i1308.stretchScaleY = i1309[43]
  i1308.stretchDuration = i1309[44]
  i1308.BoxJumpAnimation = i1309[45]
  i1308.BoxRevealAnimation = i1309[46]
  request.r(i1309[47], i1309[48], 0, i1308, 'BoxRandomRotate')
  i1308.JumpingToBelt = !!i1309[49]
  return i1308
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i1314 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i1315 = data
  i1314.aspect = i1315[0]
  i1314.orthographic = !!i1315[1]
  i1314.orthographicSize = i1315[2]
  i1314.backgroundColor = new pc.Color(i1315[3], i1315[4], i1315[5], i1315[6])
  i1314.nearClipPlane = i1315[7]
  i1314.farClipPlane = i1315[8]
  i1314.fieldOfView = i1315[9]
  i1314.depth = i1315[10]
  i1314.clearFlags = i1315[11]
  i1314.cullingMask = i1315[12]
  i1314.rect = i1315[13]
  request.r(i1315[14], i1315[15], 0, i1314, 'targetTexture')
  i1314.usePhysicalProperties = !!i1315[16]
  i1314.focalLength = i1315[17]
  i1314.sensorSize = new pc.Vec2( i1315[18], i1315[19] )
  i1314.lensShift = new pc.Vec2( i1315[20], i1315[21] )
  i1314.gateFit = i1315[22]
  i1314.commandBufferCount = i1315[23]
  i1314.cameraType = i1315[24]
  i1314.enabled = !!i1315[25]
  return i1314
}

Deserializers["WallObject"] = function (request, data, root) {
  var i1316 = root || request.c( 'WallObject' )
  var i1317 = data
  request.r(i1317[0], i1317[1], 0, i1316, 'WallTransform')
  var i1319 = i1317[2]
  var i1318 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixel')))
  for(var i = 0; i < i1319.length; i += 1) {
    i1318.add(request.d('PaintingPixel', i1319[i + 0]));
  }
  i1316.PaintingPixelsCovered = i1318
  i1316.ColorCode = i1317[3]
  i1316.Hearts = i1317[4]
  i1316.HeartsLoss = i1317[5]
  i1316.RemainingHearts = i1317[6]
  request.r(i1317[7], i1317[8], 0, i1316, 'WallRenderer')
  request.r(i1317[9], i1317[10], 0, i1316, 'HeartsText')
  i1316.Destroyed = !!i1317[11]
  i1316.WorldPos = new pc.Vec3( i1317[12], i1317[13], i1317[14] )
  return i1316
}

Deserializers["TMPro.TextMeshPro"] = function (request, data, root) {
  var i1320 = root || request.c( 'TMPro.TextMeshPro' )
  var i1321 = data
  i1320._SortingLayer = i1321[0]
  i1320._SortingLayerID = i1321[1]
  i1320._SortingOrder = i1321[2]
  i1320.m_hasFontAssetChanged = !!i1321[3]
  request.r(i1321[4], i1321[5], 0, i1320, 'm_renderer')
  i1320.m_maskType = i1321[6]
  i1320.m_text = i1321[7]
  i1320.m_isRightToLeft = !!i1321[8]
  request.r(i1321[9], i1321[10], 0, i1320, 'm_fontAsset')
  request.r(i1321[11], i1321[12], 0, i1320, 'm_sharedMaterial')
  var i1323 = i1321[13]
  var i1322 = []
  for(var i = 0; i < i1323.length; i += 2) {
  request.r(i1323[i + 0], i1323[i + 1], 2, i1322, '')
  }
  i1320.m_fontSharedMaterials = i1322
  request.r(i1321[14], i1321[15], 0, i1320, 'm_fontMaterial')
  var i1325 = i1321[16]
  var i1324 = []
  for(var i = 0; i < i1325.length; i += 2) {
  request.r(i1325[i + 0], i1325[i + 1], 2, i1324, '')
  }
  i1320.m_fontMaterials = i1324
  i1320.m_fontColor32 = UnityEngine.Color32.ConstructColor(i1321[17], i1321[18], i1321[19], i1321[20])
  i1320.m_fontColor = new pc.Color(i1321[21], i1321[22], i1321[23], i1321[24])
  i1320.m_enableVertexGradient = !!i1321[25]
  i1320.m_colorMode = i1321[26]
  i1320.m_fontColorGradient = request.d('TMPro.VertexGradient', i1321[27], i1320.m_fontColorGradient)
  request.r(i1321[28], i1321[29], 0, i1320, 'm_fontColorGradientPreset')
  request.r(i1321[30], i1321[31], 0, i1320, 'm_spriteAsset')
  i1320.m_tintAllSprites = !!i1321[32]
  request.r(i1321[33], i1321[34], 0, i1320, 'm_StyleSheet')
  i1320.m_TextStyleHashCode = i1321[35]
  i1320.m_overrideHtmlColors = !!i1321[36]
  i1320.m_faceColor = UnityEngine.Color32.ConstructColor(i1321[37], i1321[38], i1321[39], i1321[40])
  i1320.m_fontSize = i1321[41]
  i1320.m_fontSizeBase = i1321[42]
  i1320.m_fontWeight = i1321[43]
  i1320.m_enableAutoSizing = !!i1321[44]
  i1320.m_fontSizeMin = i1321[45]
  i1320.m_fontSizeMax = i1321[46]
  i1320.m_fontStyle = i1321[47]
  i1320.m_HorizontalAlignment = i1321[48]
  i1320.m_VerticalAlignment = i1321[49]
  i1320.m_textAlignment = i1321[50]
  i1320.m_characterSpacing = i1321[51]
  i1320.m_wordSpacing = i1321[52]
  i1320.m_lineSpacing = i1321[53]
  i1320.m_lineSpacingMax = i1321[54]
  i1320.m_paragraphSpacing = i1321[55]
  i1320.m_charWidthMaxAdj = i1321[56]
  i1320.m_enableWordWrapping = !!i1321[57]
  i1320.m_wordWrappingRatios = i1321[58]
  i1320.m_overflowMode = i1321[59]
  request.r(i1321[60], i1321[61], 0, i1320, 'm_linkedTextComponent')
  request.r(i1321[62], i1321[63], 0, i1320, 'parentLinkedComponent')
  i1320.m_enableKerning = !!i1321[64]
  i1320.m_enableExtraPadding = !!i1321[65]
  i1320.checkPaddingRequired = !!i1321[66]
  i1320.m_isRichText = !!i1321[67]
  i1320.m_parseCtrlCharacters = !!i1321[68]
  i1320.m_isOrthographic = !!i1321[69]
  i1320.m_isCullingEnabled = !!i1321[70]
  i1320.m_horizontalMapping = i1321[71]
  i1320.m_verticalMapping = i1321[72]
  i1320.m_uvLineOffset = i1321[73]
  i1320.m_geometrySortingOrder = i1321[74]
  i1320.m_IsTextObjectScaleStatic = !!i1321[75]
  i1320.m_VertexBufferAutoSizeReduction = !!i1321[76]
  i1320.m_useMaxVisibleDescender = !!i1321[77]
  i1320.m_pageToDisplay = i1321[78]
  i1320.m_margin = new pc.Vec4( i1321[79], i1321[80], i1321[81], i1321[82] )
  i1320.m_isUsingLegacyAnimationComponent = !!i1321[83]
  i1320.m_isVolumetricText = !!i1321[84]
  i1320.m_Maskable = !!i1321[85]
  request.r(i1321[86], i1321[87], 0, i1320, 'm_Material')
  i1320.m_Color = new pc.Color(i1321[88], i1321[89], i1321[90], i1321[91])
  i1320.m_RaycastTarget = !!i1321[92]
  i1320.m_RaycastPadding = new pc.Vec4( i1321[93], i1321[94], i1321[95], i1321[96] )
  return i1320
}

Deserializers["CullableObject"] = function (request, data, root) {
  var i1326 = root || request.c( 'CullableObject' )
  var i1327 = data
  var i1329 = i1327[0]
  var i1328 = []
  for(var i = 0; i < i1329.length; i += 2) {
  request.r(i1329[i + 0], i1329[i + 1], 2, i1328, '')
  }
  i1326.targetRenderers = i1328
  i1326.checkInterval = i1327[1]
  i1326.maxDistance = i1327[2]
  i1326.showDebug = !!i1327[3]
  return i1326
}

Deserializers["CachedTransformPathMover"] = function (request, data, root) {
  var i1332 = root || request.c( 'CachedTransformPathMover' )
  var i1333 = data
  i1332.speed = i1333[0]
  i1332.currentTF = i1333[1]
  i1332.endTF = i1333[2]
  i1332.direction = i1333[3]
  i1332.useDistanceBasedMovement = !!i1333[4]
  i1332.currentDistance = i1333[5]
  i1332.movementType = i1333[6]
  i1332.PingPongClamp = new pc.Vec2( i1333[7], i1333[8] )
  i1332.LoopClamp = new pc.Vec2( i1333[9], i1333[10] )
  i1332.autoMove = !!i1333[11]
  i1332.orientToPath = !!i1333[12]
  i1332.orientationSpace = i1333[13]
  i1332.smoothOrientation = !!i1333[14]
  i1332.orientationSpeed = i1333[15]
  i1332.rotationInterpolation = i1333[16]
  return i1332
}

Deserializers["ColorPixelsCollectorObject"] = function (request, data, root) {
  var i1334 = root || request.c( 'ColorPixelsCollectorObject' )
  var i1335 = data
  request.r(i1335[0], i1335[1], 0, i1334, 'RabbitRootTransform')
  request.r(i1335[2], i1335[3], 0, i1334, 'RabbitRotateTransform')
  request.r(i1335[4], i1335[5], 0, i1334, 'VisualHandler')
  request.r(i1335[6], i1335[7], 0, i1334, 'CollectorAnimator')
  i1334.CurrentState = i1335[8]
  request.r(i1335[9], i1335[10], 0, i1334, 'MovementHandle')
  i1334.NormalSpeed = i1335[11]
  i1334.AbsoluteWinSpeed = i1335[12]
  request.r(i1335[13], i1335[14], 0, i1334, 'CurrentGrid')
  i1334.BulletCapacity = i1335[15]
  i1334.BulletLeft = i1335[16]
  i1334.CollectorColor = i1335[17]
  i1334.DetectionRadius = i1335[18]
  i1334.IsLocked = !!i1335[19]
  i1334.IsHidden = !!i1335[20]
  var i1337 = i1335[21]
  var i1336 = new (System.Collections.Generic.List$1(Bridge.ns('System.Int32')))
  for(var i = 0; i < i1337.length; i += 1) {
    i1336.add(i1337[i + 0]);
  }
  i1334.ConnectedCollectorsIDs = i1336
  i1334.IsCollectorActive = !!i1335[22]
  i1334.CurrentTargetPosition = new pc.Vec3( i1335[23], i1335[24], i1335[25] )
  i1334.currentMovementDirection = i1335[26]
  i1334.CurrentColor = new pc.Color(i1335[27], i1335[28], i1335[29], i1335[30])
  var i1339 = i1335[31]
  var i1338 = []
  for(var i = 0; i < i1339.length; i += 1) {
    i1338.push( request.d('PaintingPixel', i1339[i + 0]) );
  }
  i1334.possibleTargets = i1338
  i1334.AbsoluteCheckRate = i1335[32]
  i1334.IsHided = !!i1335[33]
  i1334.OnCompleteAllColorPixels = request.d('System.Action', i1335[34], i1334.OnCompleteAllColorPixels)
  i1334.CheckIntervalFrames = i1335[35]
  i1334.MinMoveDistance = i1335[36]
  i1334.ID = i1335[37]
  request.r(i1335[38], i1335[39], 0, i1334, 'bulletDisplayHandler')
  return i1334
}

Deserializers["System.Action"] = function (request, data, root) {
  var i1344 = root || request.c( 'System.Action' )
  var i1345 = data
  return i1344
}

Deserializers["CollectorVisualHandler"] = function (request, data, root) {
  var i1346 = root || request.c( 'CollectorVisualHandler' )
  var i1347 = data
  request.r(i1347[0], i1347[1], 0, i1346, 'CollectorHandle')
  request.r(i1347[2], i1347[3], 0, i1346, 'Animator')
  request.r(i1347[4], i1347[5], 0, i1346, 'colorPalette')
  var i1349 = i1347[6]
  var i1348 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Renderer')))
  for(var i = 0; i < i1349.length; i += 2) {
  request.r(i1349[i + 0], i1349[i + 1], 1, i1348, '')
  }
  i1346.GunnerRenderers = i1348
  request.r(i1347[7], i1347[8], 0, i1346, 'BulletsText')
  request.r(i1347[9], i1347[10], 0, i1346, 'TankRopeObj')
  request.r(i1347[11], i1347[12], 0, i1346, 'TankRope')
  request.r(i1347[13], i1347[14], 0, i1346, 'TankRopeMesh')
  request.r(i1347[15], i1347[16], 0, i1346, 'LockSpriteRenderer')
  request.r(i1347[17], i1347[18], 0, i1346, 'CattonBox')
  request.r(i1347[19], i1347[20], 0, i1346, 'QuestionMarkSpriteRenderer')
  request.r(i1347[21], i1347[22], 0, i1346, 'OriginalMat')
  i1346.HiddenOutlineColor = new pc.Color(i1347[23], i1347[24], i1347[25], i1347[26])
  i1346.HiddenRopeColor = new pc.Color(i1347[27], i1347[28], i1347[29], i1347[30])
  request.r(i1347[31], i1347[32], 0, i1346, 'BulletSpawner')
  request.r(i1347[33], i1347[34], 0, i1346, 'CollectorMuzzleVFX')
  request.r(i1347[35], i1347[36], 0, i1346, 'ReavealVFX')
  request.r(i1347[37], i1347[38], 0, i1346, 'HighSpeedTrail')
  i1346.CurrentColor = new pc.Color(i1347[39], i1347[40], i1347[41], i1347[42])
  return i1346
}

Deserializers["BulletDisplayHandler"] = function (request, data, root) {
  var i1350 = root || request.c( 'BulletDisplayHandler' )
  var i1351 = data
  request.r(i1351[0], i1351[1], 0, i1350, 'Collector')
  request.r(i1351[2], i1351[3], 0, i1350, 'text')
  return i1350
}

Deserializers["EnableRandomRotate"] = function (request, data, root) {
  var i1352 = root || request.c( 'EnableRandomRotate' )
  var i1353 = data
  request.r(i1353[0], i1353[1], 0, i1352, 'Target')
  i1352.minY = i1353[2]
  i1352.maxY = i1353[3]
  return i1352
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Animator"] = function (request, data, root) {
  var i1354 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Animator' )
  var i1355 = data
  request.r(i1355[0], i1355[1], 0, i1354, 'animatorController')
  request.r(i1355[2], i1355[3], 0, i1354, 'avatar')
  i1354.updateMode = i1355[4]
  i1354.hasTransformHierarchy = !!i1355[5]
  i1354.applyRootMotion = !!i1355[6]
  var i1357 = i1355[7]
  var i1356 = []
  for(var i = 0; i < i1357.length; i += 2) {
  request.r(i1357[i + 0], i1357[i + 1], 2, i1356, '')
  }
  i1354.humanBones = i1356
  i1354.enabled = !!i1355[8]
  return i1354
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.LineRenderer"] = function (request, data, root) {
  var i1358 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.LineRenderer' )
  var i1359 = data
  i1358.textureMode = i1359[0]
  i1358.alignment = i1359[1]
  i1358.widthCurve = new pc.AnimationCurve( { keys_flow: i1359[2] } )
  i1358.colorGradient = i1359[3] ? new pc.ColorGradient(i1359[3][0], i1359[3][1], i1359[3][2]) : null
  var i1361 = i1359[4]
  var i1360 = []
  for(var i = 0; i < i1361.length; i += 3) {
    i1360.push( new pc.Vec3( i1361[i + 0], i1361[i + 1], i1361[i + 2] ) );
  }
  i1358.positions = i1360
  i1358.positionCount = i1359[5]
  i1358.widthMultiplier = i1359[6]
  i1358.startWidth = i1359[7]
  i1358.endWidth = i1359[8]
  i1358.numCornerVertices = i1359[9]
  i1358.numCapVertices = i1359[10]
  i1358.useWorldSpace = !!i1359[11]
  i1358.loop = !!i1359[12]
  i1358.startColor = new pc.Color(i1359[13], i1359[14], i1359[15], i1359[16])
  i1358.endColor = new pc.Color(i1359[17], i1359[18], i1359[19], i1359[20])
  i1358.generateLightingData = !!i1359[21]
  i1358.enabled = !!i1359[22]
  request.r(i1359[23], i1359[24], 0, i1358, 'sharedMaterial')
  var i1363 = i1359[25]
  var i1362 = []
  for(var i = 0; i < i1363.length; i += 2) {
  request.r(i1363[i + 0], i1363[i + 1], 2, i1362, '')
  }
  i1358.sharedMaterials = i1362
  i1358.receiveShadows = !!i1359[26]
  i1358.shadowCastingMode = i1359[27]
  i1358.sortingLayerID = i1359[28]
  i1358.sortingOrder = i1359[29]
  i1358.lightmapIndex = i1359[30]
  i1358.lightmapSceneIndex = i1359[31]
  i1358.lightmapScaleOffset = new pc.Vec4( i1359[32], i1359[33], i1359[34], i1359[35] )
  i1358.lightProbeUsage = i1359[36]
  i1358.reflectionProbeUsage = i1359[37]
  return i1358
}

Deserializers["GogoGaga.OptimizedRopesAndCables.Rope"] = function (request, data, root) {
  var i1364 = root || request.c( 'GogoGaga.OptimizedRopesAndCables.Rope' )
  var i1365 = data
  i1364.linePoints = i1365[0]
  i1364.stiffness = i1365[1]
  i1364.damping = i1365[2]
  i1364.ropeLength = i1365[3]
  i1364.ropeWidth = i1365[4]
  i1364.midPointWeight = i1365[5]
  i1364.midPointPosition = i1365[6]
  request.r(i1365[7], i1365[8], 0, i1364, 'startPoint')
  request.r(i1365[9], i1365[10], 0, i1364, 'midPoint')
  request.r(i1365[11], i1365[12], 0, i1364, 'endPoint')
  return i1364
}

Deserializers["GogoGaga.OptimizedRopesAndCables.RopeMesh"] = function (request, data, root) {
  var i1366 = root || request.c( 'GogoGaga.OptimizedRopesAndCables.RopeMesh' )
  var i1367 = data
  i1366.OverallDivision = i1367[0]
  i1366.ropeWidth = i1367[1]
  i1366.radialDivision = i1367[2]
  request.r(i1367[3], i1367[4], 0, i1366, 'material')
  i1366.tilingPerMeter = i1367[5]
  request.r(i1367[6], i1367[7], 0, i1366, 'RopeLogic')
  request.r(i1367[8], i1367[9], 0, i1366, 'StartPoint')
  request.r(i1367[10], i1367[11], 0, i1366, 'EndPoint')
  i1366.FirstHalfColor = new pc.Color(i1367[12], i1367[13], i1367[14], i1367[15])
  i1366.SecondHalfColor = new pc.Color(i1367[16], i1367[17], i1367[18], i1367[19])
  i1366.ThresholdDistance = i1367[20]
  i1366.ThresholdOffsetY = i1367[21]
  i1366.UpdateRate = i1367[22]
  i1366.EnableDebugColorLogs = !!i1367[23]
  i1366.OverrideColorProperty = i1367[24]
  i1366.RebuildOnEveryFrame = !!i1367[25]
  i1366.MinSecondsBetweenRebuilds = i1367[26]
  i1366.RebuildMovementThreshold = i1367[27]
  i1366.RecalculateNormals = !!i1367[28]
  return i1366
}

Deserializers["CollectorProjectileController"] = function (request, data, root) {
  var i1368 = root || request.c( 'CollectorProjectileController' )
  var i1369 = data
  request.r(i1369[0], i1369[1], 0, i1368, 'EffectOptions')
  request.r(i1369[2], i1369[3], 0, i1368, 'MyTransform')
  i1368.SuperAmmo = !!i1369[4]
  i1368.Speed = i1369[5]
  i1368.Target = new pc.Vec3( i1369[6], i1369[7], i1369[8] )
  i1368.TimeExisted = i1369[9]
  i1368.TimeExistedAfterHit = i1369[10]
  i1368.CurrentTimer = i1369[11]
  i1368.Stopped = !!i1369[12]
  var i1371 = i1369[13]
  var i1370 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.ParticleSystem')))
  for(var i = 0; i < i1371.length; i += 2) {
  request.r(i1371[i + 0], i1371[i + 1], 1, i1370, '')
  }
  i1368.FlyingFXs = i1370
  var i1373 = i1369[14]
  var i1372 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.ParticleSystem')))
  for(var i = 0; i < i1373.length; i += 2) {
  request.r(i1373[i + 0], i1373[i + 1], 1, i1372, '')
  }
  i1368.HitFXs = i1372
  request.r(i1369[15], i1369[16], 0, i1368, 'TrailFX')
  request.r(i1369[17], i1369[18], 0, i1368, 'BulletMeshParticle')
  return i1368
}

Deserializers["PathTransformBasedCached"] = function (request, data, root) {
  var i1376 = root || request.c( 'PathTransformBasedCached' )
  var i1377 = data
  var i1379 = i1377[0]
  var i1378 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Transform')))
  for(var i = 0; i < i1379.length; i += 2) {
  request.r(i1379[i + 0], i1379[i + 1], 1, i1378, '')
  }
  i1376.PathPoints = i1378
  i1376.totalDistance = i1377[1]
  return i1376
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Cubemap"] = function (request, data, root) {
  var i1382 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Cubemap' )
  var i1383 = data
  i1382.name = i1383[0]
  i1382.atlasId = i1383[1]
  i1382.mipmapCount = i1383[2]
  i1382.hdr = !!i1383[3]
  i1382.size = i1383[4]
  i1382.anisoLevel = i1383[5]
  i1382.filterMode = i1383[6]
  var i1385 = i1383[7]
  var i1384 = []
  for(var i = 0; i < i1385.length; i += 4) {
    i1384.push( UnityEngine.Rect.MinMaxRect(i1385[i + 0], i1385[i + 1], i1385[i + 2], i1385[i + 3]) );
  }
  i1382.rects = i1384
  i1382.wrapU = i1383[8]
  i1382.wrapV = i1383[9]
  return i1382
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i1388 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i1389 = data
  i1388.name = i1389[0]
  i1388.index = i1389[1]
  i1388.startup = !!i1389[2]
  return i1388
}

Deserializers["LevelConfigSetup"] = function (request, data, root) {
  var i1390 = root || request.c( 'LevelConfigSetup' )
  var i1391 = data
  i1390.EDITOR = !!i1391[0]
  request.r(i1391[1], i1391[2], 0, i1390, 'NewTargetPainting')
  var i1393 = i1391[3]
  var i1392 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i1393.length; i += 1) {
    i1392.add(i1393[i + 0]);
  }
  i1390.ColorCodesUsed = i1392
  request.r(i1391[4], i1391[5], 0, i1390, 'CurrentGridObject')
  var i1395 = i1391[6]
  var i1394 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingGridObject')))
  for(var i = 0; i < i1395.length; i += 2) {
  request.r(i1395[i + 0], i1395[i + 1], 1, i1394, '')
  }
  i1390.CurrentGridObjects = i1394
  request.r(i1391[7], i1391[8], 0, i1390, 'CurrentLevel')
  request.r(i1391[9], i1391[10], 0, i1390, 'CurrentLevelPaintingConfig')
  request.r(i1391[11], i1391[12], 0, i1390, 'CurrentLevelCollectorConfig')
  request.r(i1391[13], i1391[14], 0, i1390, 'CurrentLevelPainting')
  var i1397 = i1391[15]
  var i1396 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i1397.length; i += 1) {
    i1396.add(i1397[i + 0]);
  }
  i1390.CurrentLevelColorCodes = i1396
  request.r(i1391[16], i1391[17], 0, i1390, 'PaintingSetup')
  request.r(i1391[18], i1391[19], 0, i1390, 'PipeObjectSetup')
  request.r(i1391[20], i1391[21], 0, i1390, 'WallObjectSetup')
  request.r(i1391[22], i1391[23], 0, i1390, 'KeyObjectSetup')
  request.r(i1391[24], i1391[25], 0, i1390, 'LevelCollectorsManager')
  request.r(i1391[26], i1391[27], 0, i1390, 'LevelCollectorsSetup')
  return i1390
}

Deserializers["LevelCollectorsSystem"] = function (request, data, root) {
  var i1400 = root || request.c( 'LevelCollectorsSystem' )
  var i1401 = data
  request.r(i1401[0], i1401[1], 0, i1400, 'LevelSetup')
  request.r(i1401[2], i1401[3], 0, i1400, 'CurrentLevelCollectorsConfig')
  request.r(i1401[4], i1401[5], 0, i1400, 'FormationCenter')
  i1400.SpaceBetweenColumns = i1401[6]
  i1400.SpaceBetweenCollectors = i1401[7]
  request.r(i1401[8], i1401[9], 0, i1400, 'CollectorContainer')
  i1400.CollectorRotation = new pc.Vec3( i1401[10], i1401[11], i1401[12] )
  request.r(i1401[13], i1401[14], 0, i1400, 'PrefabSource')
  var i1403 = i1401[15]
  var i1402 = new (System.Collections.Generic.List$1(Bridge.ns('LockObject')))
  for(var i = 0; i < i1403.length; i += 2) {
  request.r(i1403[i + 0], i1403[i + 1], 1, i1402, '')
  }
  i1400.CurrentLocks = i1402
  var i1405 = i1401[16]
  var i1404 = new (System.Collections.Generic.List$1(Bridge.ns('ColorPixelsCollectorObject')))
  for(var i = 0; i < i1405.length; i += 2) {
  request.r(i1405[i + 0], i1405[i + 1], 1, i1404, '')
  }
  i1400.CurrentCollectors = i1404
  var i1407 = i1401[17]
  var i1406 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorColumn')))
  for(var i = 0; i < i1407.length; i += 1) {
    i1406.add(request.d('CollectorColumn', i1407[i + 0]));
  }
  i1400.ObjectsInColumns = i1406
  var i1409 = i1401[18]
  var i1408 = new (System.Collections.Generic.List$1(Bridge.ns('System.Collections.Generic.List`1[[CollectorController, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null]]')))
  for(var i = 0; i < i1409.length; i += 1) {
  var i1411 = i1409[i + 0]
  var i1410 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorController')))
  for(var i = 0; i < i1411.length; i += 2) {
  request.r(i1411[i + 0], i1411[i + 1], 1, i1410, '')
  }
    i1408.add(i1410);
  }
  i1400.CollectorControllersColumns = i1408
  i1400.CurrentTotalCollectors = i1401[19]
  return i1400
}

Deserializers["CollectorColumn"] = function (request, data, root) {
  var i1418 = root || request.c( 'CollectorColumn' )
  var i1419 = data
  var i1421 = i1419[0]
  var i1420 = new (System.Collections.Generic.List$1(Bridge.ns('CollectorMachanicObjectBase')))
  for(var i = 0; i < i1421.length; i += 2) {
  request.r(i1421[i + 0], i1421[i + 1], 1, i1420, '')
  }
  i1418.CollectorsInColumn = i1420
  return i1418
}

Deserializers["CollectorColumnController"] = function (request, data, root) {
  var i1428 = root || request.c( 'CollectorColumnController' )
  var i1429 = data
  request.r(i1429[0], i1429[1], 0, i1428, 'collectorsSystem')
  return i1428
}

Deserializers["PaintingGridEffects"] = function (request, data, root) {
  var i1430 = root || request.c( 'PaintingGridEffects' )
  var i1431 = data
  request.r(i1431[0], i1431[1], 0, i1430, 'GridAudioSource')
  request.r(i1431[2], i1431[3], 0, i1430, 'BlockDestroyedClip')
  return i1430
}

Deserializers["CollectorProjectilePool"] = function (request, data, root) {
  var i1432 = root || request.c( 'CollectorProjectilePool' )
  var i1433 = data
  request.r(i1433[0], i1433[1], 0, i1432, 'normalProjectilePrefab')
  request.r(i1433[2], i1433[3], 0, i1432, 'superProjectilePrefab')
  i1432.initialCount = i1433[4]
  return i1432
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Light"] = function (request, data, root) {
  var i1434 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Light' )
  var i1435 = data
  i1434.type = i1435[0]
  i1434.color = new pc.Color(i1435[1], i1435[2], i1435[3], i1435[4])
  i1434.cullingMask = i1435[5]
  i1434.intensity = i1435[6]
  i1434.range = i1435[7]
  i1434.spotAngle = i1435[8]
  i1434.shadows = i1435[9]
  i1434.shadowNormalBias = i1435[10]
  i1434.shadowBias = i1435[11]
  i1434.shadowStrength = i1435[12]
  i1434.shadowResolution = i1435[13]
  i1434.lightmapBakeType = i1435[14]
  i1434.renderMode = i1435[15]
  request.r(i1435[16], i1435[17], 0, i1434, 'cookie')
  i1434.cookieSize = i1435[18]
  i1434.enabled = !!i1435[19]
  return i1434
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i1436 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i1437 = data
  i1436.ambientIntensity = i1437[0]
  i1436.reflectionIntensity = i1437[1]
  i1436.ambientMode = i1437[2]
  i1436.ambientLight = new pc.Color(i1437[3], i1437[4], i1437[5], i1437[6])
  i1436.ambientSkyColor = new pc.Color(i1437[7], i1437[8], i1437[9], i1437[10])
  i1436.ambientGroundColor = new pc.Color(i1437[11], i1437[12], i1437[13], i1437[14])
  i1436.ambientEquatorColor = new pc.Color(i1437[15], i1437[16], i1437[17], i1437[18])
  i1436.fogColor = new pc.Color(i1437[19], i1437[20], i1437[21], i1437[22])
  i1436.fogEndDistance = i1437[23]
  i1436.fogStartDistance = i1437[24]
  i1436.fogDensity = i1437[25]
  i1436.fog = !!i1437[26]
  request.r(i1437[27], i1437[28], 0, i1436, 'skybox')
  i1436.fogMode = i1437[29]
  var i1439 = i1437[30]
  var i1438 = []
  for(var i = 0; i < i1439.length; i += 1) {
    i1438.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i1439[i + 0]) );
  }
  i1436.lightmaps = i1438
  i1436.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i1437[31], i1436.lightProbes)
  i1436.lightmapsMode = i1437[32]
  i1436.mixedBakeMode = i1437[33]
  i1436.environmentLightingMode = i1437[34]
  i1436.ambientProbe = new pc.SphericalHarmonicsL2(i1437[35])
  i1436.referenceAmbientProbe = new pc.SphericalHarmonicsL2(i1437[36])
  i1436.useReferenceAmbientProbe = !!i1437[37]
  request.r(i1437[38], i1437[39], 0, i1436, 'customReflection')
  request.r(i1437[40], i1437[41], 0, i1436, 'defaultReflection')
  i1436.defaultReflectionMode = i1437[42]
  i1436.defaultReflectionResolution = i1437[43]
  i1436.sunLightObjectId = i1437[44]
  i1436.pixelLightCount = i1437[45]
  i1436.defaultReflectionHDR = !!i1437[46]
  i1436.hasLightDataAsset = !!i1437[47]
  i1436.hasManualGenerate = !!i1437[48]
  return i1436
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i1442 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i1443 = data
  request.r(i1443[0], i1443[1], 0, i1442, 'lightmapColor')
  request.r(i1443[2], i1443[3], 0, i1442, 'lightmapDirection')
  return i1442
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i1444 = root || new UnityEngine.LightProbes()
  var i1445 = data
  return i1444
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i1450 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i1451 = data
  var i1453 = i1451[0]
  var i1452 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i1453.length; i += 1) {
    i1452.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i1453[i + 0]));
  }
  i1450.ShaderCompilationErrors = i1452
  i1450.name = i1451[1]
  i1450.guid = i1451[2]
  var i1455 = i1451[3]
  var i1454 = []
  for(var i = 0; i < i1455.length; i += 1) {
    i1454.push( i1455[i + 0] );
  }
  i1450.shaderDefinedKeywords = i1454
  var i1457 = i1451[4]
  var i1456 = []
  for(var i = 0; i < i1457.length; i += 1) {
    i1456.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i1457[i + 0]) );
  }
  i1450.passes = i1456
  var i1459 = i1451[5]
  var i1458 = []
  for(var i = 0; i < i1459.length; i += 1) {
    i1458.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i1459[i + 0]) );
  }
  i1450.usePasses = i1458
  var i1461 = i1451[6]
  var i1460 = []
  for(var i = 0; i < i1461.length; i += 1) {
    i1460.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i1461[i + 0]) );
  }
  i1450.defaultParameterValues = i1460
  request.r(i1451[7], i1451[8], 0, i1450, 'unityFallbackShader')
  i1450.readDepth = !!i1451[9]
  i1450.isCreatedByShaderGraph = !!i1451[10]
  i1450.disableBatching = !!i1451[11]
  i1450.compiled = !!i1451[12]
  return i1450
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i1464 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i1465 = data
  i1464.shaderName = i1465[0]
  i1464.errorMessage = i1465[1]
  return i1464
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i1468 = root || new pc.UnityShaderPass()
  var i1469 = data
  i1468.id = i1469[0]
  i1468.subShaderIndex = i1469[1]
  i1468.name = i1469[2]
  i1468.passType = i1469[3]
  i1468.grabPassTextureName = i1469[4]
  i1468.usePass = !!i1469[5]
  i1468.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1469[6], i1468.zTest)
  i1468.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1469[7], i1468.zWrite)
  i1468.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1469[8], i1468.culling)
  i1468.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i1469[9], i1468.blending)
  i1468.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i1469[10], i1468.alphaBlending)
  i1468.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1469[11], i1468.colorWriteMask)
  i1468.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1469[12], i1468.offsetUnits)
  i1468.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1469[13], i1468.offsetFactor)
  i1468.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1469[14], i1468.stencilRef)
  i1468.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1469[15], i1468.stencilReadMask)
  i1468.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1469[16], i1468.stencilWriteMask)
  i1468.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i1469[17], i1468.stencilOp)
  i1468.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i1469[18], i1468.stencilOpFront)
  i1468.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i1469[19], i1468.stencilOpBack)
  var i1471 = i1469[20]
  var i1470 = []
  for(var i = 0; i < i1471.length; i += 1) {
    i1470.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i1471[i + 0]) );
  }
  i1468.tags = i1470
  var i1473 = i1469[21]
  var i1472 = []
  for(var i = 0; i < i1473.length; i += 1) {
    i1472.push( i1473[i + 0] );
  }
  i1468.passDefinedKeywords = i1472
  var i1475 = i1469[22]
  var i1474 = []
  for(var i = 0; i < i1475.length; i += 1) {
    i1474.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i1475[i + 0]) );
  }
  i1468.passDefinedKeywordGroups = i1474
  var i1477 = i1469[23]
  var i1476 = []
  for(var i = 0; i < i1477.length; i += 1) {
    i1476.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i1477[i + 0]) );
  }
  i1468.variants = i1476
  var i1479 = i1469[24]
  var i1478 = []
  for(var i = 0; i < i1479.length; i += 1) {
    i1478.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i1479[i + 0]) );
  }
  i1468.excludedVariants = i1478
  i1468.hasDepthReader = !!i1469[25]
  return i1468
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i1480 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i1481 = data
  i1480.val = i1481[0]
  i1480.name = i1481[1]
  return i1480
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i1482 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i1483 = data
  i1482.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1483[0], i1482.src)
  i1482.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1483[1], i1482.dst)
  i1482.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1483[2], i1482.op)
  return i1482
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i1484 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i1485 = data
  i1484.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1485[0], i1484.pass)
  i1484.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1485[1], i1484.fail)
  i1484.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1485[2], i1484.zFail)
  i1484.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i1485[3], i1484.comp)
  return i1484
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i1488 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i1489 = data
  i1488.name = i1489[0]
  i1488.value = i1489[1]
  return i1488
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i1492 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i1493 = data
  var i1495 = i1493[0]
  var i1494 = []
  for(var i = 0; i < i1495.length; i += 1) {
    i1494.push( i1495[i + 0] );
  }
  i1492.keywords = i1494
  i1492.hasDiscard = !!i1493[1]
  return i1492
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i1498 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i1499 = data
  i1498.passId = i1499[0]
  i1498.subShaderIndex = i1499[1]
  var i1501 = i1499[2]
  var i1500 = []
  for(var i = 0; i < i1501.length; i += 1) {
    i1500.push( i1501[i + 0] );
  }
  i1498.keywords = i1500
  i1498.vertexProgram = i1499[3]
  i1498.fragmentProgram = i1499[4]
  i1498.exportedForWebGl2 = !!i1499[5]
  i1498.readDepth = !!i1499[6]
  return i1498
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i1504 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i1505 = data
  request.r(i1505[0], i1505[1], 0, i1504, 'shader')
  i1504.pass = i1505[2]
  return i1504
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i1508 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i1509 = data
  i1508.name = i1509[0]
  i1508.type = i1509[1]
  i1508.value = new pc.Vec4( i1509[2], i1509[3], i1509[4], i1509[5] )
  i1508.textureValue = i1509[6]
  i1508.shaderPropertyFlag = i1509[7]
  return i1508
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Sprite"] = function (request, data, root) {
  var i1510 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Sprite' )
  var i1511 = data
  i1510.name = i1511[0]
  request.r(i1511[1], i1511[2], 0, i1510, 'texture')
  i1510.aabb = i1511[3]
  i1510.vertices = i1511[4]
  i1510.triangles = i1511[5]
  i1510.textureRect = UnityEngine.Rect.MinMaxRect(i1511[6], i1511[7], i1511[8], i1511[9])
  i1510.packedRect = UnityEngine.Rect.MinMaxRect(i1511[10], i1511[11], i1511[12], i1511[13])
  i1510.border = new pc.Vec4( i1511[14], i1511[15], i1511[16], i1511[17] )
  i1510.transparency = i1511[18]
  i1510.bounds = i1511[19]
  i1510.pixelsPerUnit = i1511[20]
  i1510.textureWidth = i1511[21]
  i1510.textureHeight = i1511[22]
  i1510.nativeSize = new pc.Vec2( i1511[23], i1511[24] )
  i1510.pivot = new pc.Vec2( i1511[25], i1511[26] )
  i1510.textureRectOffset = new pc.Vec2( i1511[27], i1511[28] )
  return i1510
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.AudioClip"] = function (request, data, root) {
  var i1512 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.AudioClip' )
  var i1513 = data
  i1512.name = i1513[0]
  return i1512
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip"] = function (request, data, root) {
  var i1514 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip' )
  var i1515 = data
  i1514.name = i1515[0]
  i1514.wrapMode = i1515[1]
  i1514.isLooping = !!i1515[2]
  i1514.length = i1515[3]
  var i1517 = i1515[4]
  var i1516 = []
  for(var i = 0; i < i1517.length; i += 1) {
    i1516.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve', i1517[i + 0]) );
  }
  i1514.curves = i1516
  var i1519 = i1515[5]
  var i1518 = []
  for(var i = 0; i < i1519.length; i += 1) {
    i1518.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationEvent', i1519[i + 0]) );
  }
  i1514.events = i1518
  i1514.halfPrecision = !!i1515[6]
  i1514._frameRate = i1515[7]
  i1514.localBounds = request.d('Luna.Unity.DTO.UnityEngine.Animation.Data.Bounds', i1515[8], i1514.localBounds)
  i1514.hasMuscleCurves = !!i1515[9]
  var i1521 = i1515[10]
  var i1520 = []
  for(var i = 0; i < i1521.length; i += 1) {
    i1520.push( i1521[i + 0] );
  }
  i1514.clipMuscleConstant = i1520
  i1514.clipBindingConstant = request.d('Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip+AnimationClipBindingConstant', i1515[11], i1514.clipBindingConstant)
  return i1514
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve"] = function (request, data, root) {
  var i1524 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve' )
  var i1525 = data
  i1524.path = i1525[0]
  i1524.hash = i1525[1]
  i1524.componentType = i1525[2]
  i1524.property = i1525[3]
  i1524.keys = i1525[4]
  var i1527 = i1525[5]
  var i1526 = []
  for(var i = 0; i < i1527.length; i += 1) {
    i1526.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve+ObjectReferenceKey', i1527[i + 0]) );
  }
  i1524.objectReferenceKeys = i1526
  return i1524
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve+ObjectReferenceKey"] = function (request, data, root) {
  var i1530 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve+ObjectReferenceKey' )
  var i1531 = data
  i1530.time = i1531[0]
  request.r(i1531[1], i1531[2], 0, i1530, 'value')
  return i1530
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationEvent"] = function (request, data, root) {
  var i1534 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationEvent' )
  var i1535 = data
  i1534.functionName = i1535[0]
  i1534.floatParameter = i1535[1]
  i1534.intParameter = i1535[2]
  i1534.stringParameter = i1535[3]
  request.r(i1535[4], i1535[5], 0, i1534, 'objectReferenceParameter')
  i1534.time = i1535[6]
  return i1534
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.Bounds"] = function (request, data, root) {
  var i1536 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.Bounds' )
  var i1537 = data
  i1536.center = new pc.Vec3( i1537[0], i1537[1], i1537[2] )
  i1536.extends = new pc.Vec3( i1537[3], i1537[4], i1537[5] )
  return i1536
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip+AnimationClipBindingConstant"] = function (request, data, root) {
  var i1540 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip+AnimationClipBindingConstant' )
  var i1541 = data
  var i1543 = i1541[0]
  var i1542 = []
  for(var i = 0; i < i1543.length; i += 1) {
    i1542.push( i1543[i + 0] );
  }
  i1540.genericBindings = i1542
  var i1545 = i1541[1]
  var i1544 = []
  for(var i = 0; i < i1545.length; i += 1) {
    i1544.push( i1545[i + 0] );
  }
  i1540.pptrCurveMapping = i1544
  return i1540
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font"] = function (request, data, root) {
  var i1546 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font' )
  var i1547 = data
  i1546.name = i1547[0]
  i1546.ascent = i1547[1]
  i1546.originalLineHeight = i1547[2]
  i1546.fontSize = i1547[3]
  var i1549 = i1547[4]
  var i1548 = []
  for(var i = 0; i < i1549.length; i += 1) {
    i1548.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo', i1549[i + 0]) );
  }
  i1546.characterInfo = i1548
  request.r(i1547[5], i1547[6], 0, i1546, 'texture')
  i1546.originalFontSize = i1547[7]
  return i1546
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo"] = function (request, data, root) {
  var i1552 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo' )
  var i1553 = data
  i1552.index = i1553[0]
  i1552.advance = i1553[1]
  i1552.bearing = i1553[2]
  i1552.glyphWidth = i1553[3]
  i1552.glyphHeight = i1553[4]
  i1552.minX = i1553[5]
  i1552.maxX = i1553[6]
  i1552.minY = i1553[7]
  i1552.maxY = i1553[8]
  i1552.uvBottomLeftX = i1553[9]
  i1552.uvBottomLeftY = i1553[10]
  i1552.uvBottomRightX = i1553[11]
  i1552.uvBottomRightY = i1553[12]
  i1552.uvTopLeftX = i1553[13]
  i1552.uvTopLeftY = i1553[14]
  i1552.uvTopRightX = i1553[15]
  i1552.uvTopRightY = i1553[16]
  return i1552
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorController"] = function (request, data, root) {
  var i1554 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorController' )
  var i1555 = data
  i1554.name = i1555[0]
  var i1557 = i1555[1]
  var i1556 = []
  for(var i = 0; i < i1557.length; i += 1) {
    i1556.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerLayer', i1557[i + 0]) );
  }
  i1554.layers = i1556
  var i1559 = i1555[2]
  var i1558 = []
  for(var i = 0; i < i1559.length; i += 1) {
    i1558.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerParameter', i1559[i + 0]) );
  }
  i1554.parameters = i1558
  i1554.animationClips = i1555[3]
  i1554.avatarUnsupported = i1555[4]
  return i1554
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerLayer"] = function (request, data, root) {
  var i1562 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerLayer' )
  var i1563 = data
  i1562.name = i1563[0]
  i1562.defaultWeight = i1563[1]
  i1562.blendingMode = i1563[2]
  i1562.avatarMask = i1563[3]
  i1562.syncedLayerIndex = i1563[4]
  i1562.syncedLayerAffectsTiming = !!i1563[5]
  i1562.syncedLayers = i1563[6]
  i1562.stateMachine = request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateMachine', i1563[7], i1562.stateMachine)
  return i1562
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateMachine"] = function (request, data, root) {
  var i1564 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateMachine' )
  var i1565 = data
  i1564.id = i1565[0]
  i1564.name = i1565[1]
  i1564.path = i1565[2]
  var i1567 = i1565[3]
  var i1566 = []
  for(var i = 0; i < i1567.length; i += 1) {
    i1566.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorState', i1567[i + 0]) );
  }
  i1564.states = i1566
  var i1569 = i1565[4]
  var i1568 = []
  for(var i = 0; i < i1569.length; i += 1) {
    i1568.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateMachine', i1569[i + 0]) );
  }
  i1564.machines = i1568
  var i1571 = i1565[5]
  var i1570 = []
  for(var i = 0; i < i1571.length; i += 1) {
    i1570.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorTransition', i1571[i + 0]) );
  }
  i1564.entryStateTransitions = i1570
  var i1573 = i1565[6]
  var i1572 = []
  for(var i = 0; i < i1573.length; i += 1) {
    i1572.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorTransition', i1573[i + 0]) );
  }
  i1564.exitStateTransitions = i1572
  var i1575 = i1565[7]
  var i1574 = []
  for(var i = 0; i < i1575.length; i += 1) {
    i1574.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateTransition', i1575[i + 0]) );
  }
  i1564.anyStateTransitions = i1574
  i1564.defaultStateId = i1565[8]
  return i1564
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorState"] = function (request, data, root) {
  var i1578 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorState' )
  var i1579 = data
  i1578.id = i1579[0]
  i1578.name = i1579[1]
  i1578.cycleOffset = i1579[2]
  i1578.cycleOffsetParameter = i1579[3]
  i1578.cycleOffsetParameterActive = !!i1579[4]
  i1578.mirror = !!i1579[5]
  i1578.mirrorParameter = i1579[6]
  i1578.mirrorParameterActive = !!i1579[7]
  i1578.motionId = i1579[8]
  i1578.nameHash = i1579[9]
  i1578.fullPathHash = i1579[10]
  i1578.speed = i1579[11]
  i1578.speedParameter = i1579[12]
  i1578.speedParameterActive = !!i1579[13]
  i1578.tag = i1579[14]
  i1578.tagHash = i1579[15]
  i1578.writeDefaultValues = !!i1579[16]
  var i1581 = i1579[17]
  var i1580 = []
  for(var i = 0; i < i1581.length; i += 2) {
  request.r(i1581[i + 0], i1581[i + 1], 2, i1580, '')
  }
  i1578.behaviours = i1580
  var i1583 = i1579[18]
  var i1582 = []
  for(var i = 0; i < i1583.length; i += 1) {
    i1582.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateTransition', i1583[i + 0]) );
  }
  i1578.transitions = i1582
  return i1578
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateTransition"] = function (request, data, root) {
  var i1588 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateTransition' )
  var i1589 = data
  i1588.fullPath = i1589[0]
  i1588.canTransitionToSelf = !!i1589[1]
  i1588.duration = i1589[2]
  i1588.exitTime = i1589[3]
  i1588.hasExitTime = !!i1589[4]
  i1588.hasFixedDuration = !!i1589[5]
  i1588.interruptionSource = i1589[6]
  i1588.offset = i1589[7]
  i1588.orderedInterruption = !!i1589[8]
  i1588.destinationStateId = i1589[9]
  i1588.isExit = !!i1589[10]
  i1588.mute = !!i1589[11]
  i1588.solo = !!i1589[12]
  var i1591 = i1589[13]
  var i1590 = []
  for(var i = 0; i < i1591.length; i += 1) {
    i1590.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorCondition', i1591[i + 0]) );
  }
  i1588.conditions = i1590
  return i1588
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorCondition"] = function (request, data, root) {
  var i1594 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorCondition' )
  var i1595 = data
  i1594.mode = i1595[0]
  i1594.parameter = i1595[1]
  i1594.threshold = i1595[2]
  return i1594
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorTransition"] = function (request, data, root) {
  var i1600 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorTransition' )
  var i1601 = data
  i1600.destinationStateId = i1601[0]
  i1600.isExit = !!i1601[1]
  i1600.mute = !!i1601[2]
  i1600.solo = !!i1601[3]
  var i1603 = i1601[4]
  var i1602 = []
  for(var i = 0; i < i1603.length; i += 1) {
    i1602.push( request.d('Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorCondition', i1603[i + 0]) );
  }
  i1600.conditions = i1602
  return i1600
}

Deserializers["Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerParameter"] = function (request, data, root) {
  var i1606 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerParameter' )
  var i1607 = data
  i1606.defaultBool = !!i1607[0]
  i1606.defaultFloat = i1607[1]
  i1606.defaultInt = i1607[2]
  i1606.name = i1607[3]
  i1606.nameHash = i1607[4]
  i1606.type = i1607[5]
  return i1606
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.TextAsset"] = function (request, data, root) {
  var i1608 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.TextAsset' )
  var i1609 = data
  i1608.name = i1609[0]
  i1608.bytes64 = i1609[1]
  i1608.data = i1609[2]
  return i1608
}

Deserializers["TMPro.TMP_FontAsset"] = function (request, data, root) {
  var i1610 = root || request.c( 'TMPro.TMP_FontAsset' )
  var i1611 = data
  request.r(i1611[0], i1611[1], 0, i1610, 'atlas')
  i1610.normalStyle = i1611[2]
  i1610.normalSpacingOffset = i1611[3]
  i1610.boldStyle = i1611[4]
  i1610.boldSpacing = i1611[5]
  i1610.italicStyle = i1611[6]
  i1610.tabSize = i1611[7]
  i1610.hashCode = i1611[8]
  request.r(i1611[9], i1611[10], 0, i1610, 'material')
  i1610.materialHashCode = i1611[11]
  i1610.m_Version = i1611[12]
  i1610.m_SourceFontFileGUID = i1611[13]
  request.r(i1611[14], i1611[15], 0, i1610, 'm_SourceFontFile_EditorRef')
  request.r(i1611[16], i1611[17], 0, i1610, 'm_SourceFontFile')
  i1610.m_AtlasPopulationMode = i1611[18]
  i1610.m_FaceInfo = request.d('UnityEngine.TextCore.FaceInfo', i1611[19], i1610.m_FaceInfo)
  var i1613 = i1611[20]
  var i1612 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.Glyph')))
  for(var i = 0; i < i1613.length; i += 1) {
    i1612.add(request.d('UnityEngine.TextCore.Glyph', i1613[i + 0]));
  }
  i1610.m_GlyphTable = i1612
  var i1615 = i1611[21]
  var i1614 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Character')))
  for(var i = 0; i < i1615.length; i += 1) {
    i1614.add(request.d('TMPro.TMP_Character', i1615[i + 0]));
  }
  i1610.m_CharacterTable = i1614
  var i1617 = i1611[22]
  var i1616 = []
  for(var i = 0; i < i1617.length; i += 2) {
  request.r(i1617[i + 0], i1617[i + 1], 2, i1616, '')
  }
  i1610.m_AtlasTextures = i1616
  i1610.m_AtlasTextureIndex = i1611[23]
  i1610.m_IsMultiAtlasTexturesEnabled = !!i1611[24]
  i1610.m_ClearDynamicDataOnBuild = !!i1611[25]
  var i1619 = i1611[26]
  var i1618 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.GlyphRect')))
  for(var i = 0; i < i1619.length; i += 1) {
    i1618.add(request.d('UnityEngine.TextCore.GlyphRect', i1619[i + 0]));
  }
  i1610.m_UsedGlyphRects = i1618
  var i1621 = i1611[27]
  var i1620 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.GlyphRect')))
  for(var i = 0; i < i1621.length; i += 1) {
    i1620.add(request.d('UnityEngine.TextCore.GlyphRect', i1621[i + 0]));
  }
  i1610.m_FreeGlyphRects = i1620
  i1610.m_fontInfo = request.d('TMPro.FaceInfo_Legacy', i1611[28], i1610.m_fontInfo)
  i1610.m_AtlasWidth = i1611[29]
  i1610.m_AtlasHeight = i1611[30]
  i1610.m_AtlasPadding = i1611[31]
  i1610.m_AtlasRenderMode = i1611[32]
  var i1623 = i1611[33]
  var i1622 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Glyph')))
  for(var i = 0; i < i1623.length; i += 1) {
    i1622.add(request.d('TMPro.TMP_Glyph', i1623[i + 0]));
  }
  i1610.m_glyphInfoList = i1622
  i1610.m_KerningTable = request.d('TMPro.KerningTable', i1611[34], i1610.m_KerningTable)
  i1610.m_FontFeatureTable = request.d('TMPro.TMP_FontFeatureTable', i1611[35], i1610.m_FontFeatureTable)
  var i1625 = i1611[36]
  var i1624 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_FontAsset')))
  for(var i = 0; i < i1625.length; i += 2) {
  request.r(i1625[i + 0], i1625[i + 1], 1, i1624, '')
  }
  i1610.fallbackFontAssets = i1624
  var i1627 = i1611[37]
  var i1626 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_FontAsset')))
  for(var i = 0; i < i1627.length; i += 2) {
  request.r(i1627[i + 0], i1627[i + 1], 1, i1626, '')
  }
  i1610.m_FallbackFontAssetTable = i1626
  i1610.m_CreationSettings = request.d('TMPro.FontAssetCreationSettings', i1611[38], i1610.m_CreationSettings)
  var i1629 = i1611[39]
  var i1628 = []
  for(var i = 0; i < i1629.length; i += 1) {
    i1628.push( request.d('TMPro.TMP_FontWeightPair', i1629[i + 0]) );
  }
  i1610.m_FontWeightTable = i1628
  var i1631 = i1611[40]
  var i1630 = []
  for(var i = 0; i < i1631.length; i += 1) {
    i1630.push( request.d('TMPro.TMP_FontWeightPair', i1631[i + 0]) );
  }
  i1610.fontWeights = i1630
  return i1610
}

Deserializers["UnityEngine.TextCore.FaceInfo"] = function (request, data, root) {
  var i1632 = root || request.c( 'UnityEngine.TextCore.FaceInfo' )
  var i1633 = data
  i1632.m_FaceIndex = i1633[0]
  i1632.m_FamilyName = i1633[1]
  i1632.m_StyleName = i1633[2]
  i1632.m_PointSize = i1633[3]
  i1632.m_Scale = i1633[4]
  i1632.m_UnitsPerEM = i1633[5]
  i1632.m_LineHeight = i1633[6]
  i1632.m_AscentLine = i1633[7]
  i1632.m_CapLine = i1633[8]
  i1632.m_MeanLine = i1633[9]
  i1632.m_Baseline = i1633[10]
  i1632.m_DescentLine = i1633[11]
  i1632.m_SuperscriptOffset = i1633[12]
  i1632.m_SuperscriptSize = i1633[13]
  i1632.m_SubscriptOffset = i1633[14]
  i1632.m_SubscriptSize = i1633[15]
  i1632.m_UnderlineOffset = i1633[16]
  i1632.m_UnderlineThickness = i1633[17]
  i1632.m_StrikethroughOffset = i1633[18]
  i1632.m_StrikethroughThickness = i1633[19]
  i1632.m_TabWidth = i1633[20]
  return i1632
}

Deserializers["UnityEngine.TextCore.Glyph"] = function (request, data, root) {
  var i1636 = root || request.c( 'UnityEngine.TextCore.Glyph' )
  var i1637 = data
  i1636.m_Index = i1637[0]
  i1636.m_Metrics = request.d('UnityEngine.TextCore.GlyphMetrics', i1637[1], i1636.m_Metrics)
  i1636.m_GlyphRect = request.d('UnityEngine.TextCore.GlyphRect', i1637[2], i1636.m_GlyphRect)
  i1636.m_Scale = i1637[3]
  i1636.m_AtlasIndex = i1637[4]
  i1636.m_ClassDefinitionType = i1637[5]
  return i1636
}

Deserializers["UnityEngine.TextCore.GlyphMetrics"] = function (request, data, root) {
  var i1638 = root || request.c( 'UnityEngine.TextCore.GlyphMetrics' )
  var i1639 = data
  i1638.m_Width = i1639[0]
  i1638.m_Height = i1639[1]
  i1638.m_HorizontalBearingX = i1639[2]
  i1638.m_HorizontalBearingY = i1639[3]
  i1638.m_HorizontalAdvance = i1639[4]
  return i1638
}

Deserializers["UnityEngine.TextCore.GlyphRect"] = function (request, data, root) {
  var i1640 = root || request.c( 'UnityEngine.TextCore.GlyphRect' )
  var i1641 = data
  i1640.m_X = i1641[0]
  i1640.m_Y = i1641[1]
  i1640.m_Width = i1641[2]
  i1640.m_Height = i1641[3]
  return i1640
}

Deserializers["TMPro.TMP_Character"] = function (request, data, root) {
  var i1644 = root || request.c( 'TMPro.TMP_Character' )
  var i1645 = data
  i1644.m_ElementType = i1645[0]
  i1644.m_Unicode = i1645[1]
  i1644.m_GlyphIndex = i1645[2]
  i1644.m_Scale = i1645[3]
  return i1644
}

Deserializers["TMPro.FaceInfo_Legacy"] = function (request, data, root) {
  var i1650 = root || request.c( 'TMPro.FaceInfo_Legacy' )
  var i1651 = data
  i1650.Name = i1651[0]
  i1650.PointSize = i1651[1]
  i1650.Scale = i1651[2]
  i1650.CharacterCount = i1651[3]
  i1650.LineHeight = i1651[4]
  i1650.Baseline = i1651[5]
  i1650.Ascender = i1651[6]
  i1650.CapHeight = i1651[7]
  i1650.Descender = i1651[8]
  i1650.CenterLine = i1651[9]
  i1650.SuperscriptOffset = i1651[10]
  i1650.SubscriptOffset = i1651[11]
  i1650.SubSize = i1651[12]
  i1650.Underline = i1651[13]
  i1650.UnderlineThickness = i1651[14]
  i1650.strikethrough = i1651[15]
  i1650.strikethroughThickness = i1651[16]
  i1650.TabWidth = i1651[17]
  i1650.Padding = i1651[18]
  i1650.AtlasWidth = i1651[19]
  i1650.AtlasHeight = i1651[20]
  return i1650
}

Deserializers["TMPro.TMP_Glyph"] = function (request, data, root) {
  var i1654 = root || request.c( 'TMPro.TMP_Glyph' )
  var i1655 = data
  i1654.id = i1655[0]
  i1654.x = i1655[1]
  i1654.y = i1655[2]
  i1654.width = i1655[3]
  i1654.height = i1655[4]
  i1654.xOffset = i1655[5]
  i1654.yOffset = i1655[6]
  i1654.xAdvance = i1655[7]
  i1654.scale = i1655[8]
  return i1654
}

Deserializers["TMPro.KerningTable"] = function (request, data, root) {
  var i1656 = root || request.c( 'TMPro.KerningTable' )
  var i1657 = data
  var i1659 = i1657[0]
  var i1658 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.KerningPair')))
  for(var i = 0; i < i1659.length; i += 1) {
    i1658.add(request.d('TMPro.KerningPair', i1659[i + 0]));
  }
  i1656.kerningPairs = i1658
  return i1656
}

Deserializers["TMPro.KerningPair"] = function (request, data, root) {
  var i1662 = root || request.c( 'TMPro.KerningPair' )
  var i1663 = data
  i1662.xOffset = i1663[0]
  i1662.m_FirstGlyph = i1663[1]
  i1662.m_FirstGlyphAdjustments = request.d('TMPro.GlyphValueRecord_Legacy', i1663[2], i1662.m_FirstGlyphAdjustments)
  i1662.m_SecondGlyph = i1663[3]
  i1662.m_SecondGlyphAdjustments = request.d('TMPro.GlyphValueRecord_Legacy', i1663[4], i1662.m_SecondGlyphAdjustments)
  i1662.m_IgnoreSpacingAdjustments = !!i1663[5]
  return i1662
}

Deserializers["TMPro.TMP_FontFeatureTable"] = function (request, data, root) {
  var i1664 = root || request.c( 'TMPro.TMP_FontFeatureTable' )
  var i1665 = data
  var i1667 = i1665[0]
  var i1666 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_GlyphPairAdjustmentRecord')))
  for(var i = 0; i < i1667.length; i += 1) {
    i1666.add(request.d('TMPro.TMP_GlyphPairAdjustmentRecord', i1667[i + 0]));
  }
  i1664.m_GlyphPairAdjustmentRecords = i1666
  return i1664
}

Deserializers["TMPro.TMP_GlyphPairAdjustmentRecord"] = function (request, data, root) {
  var i1670 = root || request.c( 'TMPro.TMP_GlyphPairAdjustmentRecord' )
  var i1671 = data
  i1670.m_FirstAdjustmentRecord = request.d('TMPro.TMP_GlyphAdjustmentRecord', i1671[0], i1670.m_FirstAdjustmentRecord)
  i1670.m_SecondAdjustmentRecord = request.d('TMPro.TMP_GlyphAdjustmentRecord', i1671[1], i1670.m_SecondAdjustmentRecord)
  i1670.m_FeatureLookupFlags = i1671[2]
  return i1670
}

Deserializers["TMPro.FontAssetCreationSettings"] = function (request, data, root) {
  var i1674 = root || request.c( 'TMPro.FontAssetCreationSettings' )
  var i1675 = data
  i1674.sourceFontFileName = i1675[0]
  i1674.sourceFontFileGUID = i1675[1]
  i1674.pointSizeSamplingMode = i1675[2]
  i1674.pointSize = i1675[3]
  i1674.padding = i1675[4]
  i1674.packingMode = i1675[5]
  i1674.atlasWidth = i1675[6]
  i1674.atlasHeight = i1675[7]
  i1674.characterSetSelectionMode = i1675[8]
  i1674.characterSequence = i1675[9]
  i1674.referencedFontAssetGUID = i1675[10]
  i1674.referencedTextAssetGUID = i1675[11]
  i1674.fontStyle = i1675[12]
  i1674.fontStyleModifier = i1675[13]
  i1674.renderMode = i1675[14]
  i1674.includeFontFeatures = !!i1675[15]
  return i1674
}

Deserializers["TMPro.TMP_FontWeightPair"] = function (request, data, root) {
  var i1678 = root || request.c( 'TMPro.TMP_FontWeightPair' )
  var i1679 = data
  request.r(i1679[0], i1679[1], 0, i1678, 'regularTypeface')
  request.r(i1679[2], i1679[3], 0, i1678, 'italicTypeface')
  return i1678
}

Deserializers["LevelGamePlayConfigSO"] = function (request, data, root) {
  var i1680 = root || request.c( 'LevelGamePlayConfigSO' )
  var i1681 = data
  var i1683 = i1681[0]
  var i1682 = new (System.Collections.Generic.List$1(Bridge.ns('LevelConfig')))
  for(var i = 0; i < i1683.length; i += 2) {
  request.r(i1683[i + 0], i1683[i + 1], 1, i1682, '')
  }
  i1680.levelConfigDataList = i1682
  return i1680
}

Deserializers["LevelConfig"] = function (request, data, root) {
  var i1686 = root || request.c( 'LevelConfig' )
  var i1687 = data
  request.r(i1687[0], i1687[1], 0, i1686, 'BlocksPaintingConfig')
  request.r(i1687[2], i1687[3], 0, i1686, 'CollectorsConfig')
  var i1689 = i1687[4]
  var i1688 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i1689.length; i += 1) {
    i1688.add(i1689[i + 0]);
  }
  i1686.ColorsUsed = i1688
  return i1686
}

Deserializers["PaintingConfig"] = function (request, data, root) {
  var i1690 = root || request.c( 'PaintingConfig' )
  var i1691 = data
  i1690.PaintingSize = new pc.Vec2( i1691[0], i1691[1] )
  request.r(i1691[2], i1691[3], 0, i1690, 'Sprite')
  var i1693 = i1691[4]
  var i1692 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1693.length; i += 1) {
    i1692.add(request.d('PaintingPixelConfig', i1693[i + 0]));
  }
  i1690.Pixels = i1692
  var i1695 = i1691[5]
  var i1694 = new (System.Collections.Generic.List$1(Bridge.ns('PipeObjectSetup')))
  for(var i = 0; i < i1695.length; i += 1) {
    i1694.add(request.d('PipeObjectSetup', i1695[i + 0]));
  }
  i1690.PipeSetups = i1694
  var i1697 = i1691[6]
  var i1696 = new (System.Collections.Generic.List$1(Bridge.ns('WallObjectSetup')))
  for(var i = 0; i < i1697.length; i += 1) {
    i1696.add(request.d('WallObjectSetup', i1697[i + 0]));
  }
  i1690.WallSetups = i1696
  var i1699 = i1691[7]
  var i1698 = new (System.Collections.Generic.List$1(Bridge.ns('KeyObjectSetup')))
  for(var i = 0; i < i1699.length; i += 1) {
    i1698.add(request.d('KeyObjectSetup', i1699[i + 0]));
  }
  i1690.KeySetups = i1698
  var i1701 = i1691[8]
  var i1700 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1701.length; i += 1) {
    i1700.add(request.d('PaintingPixelConfig', i1701[i + 0]));
  }
  i1690.AdditionPixels = i1700
  var i1703 = i1691[9]
  var i1702 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingConfigBackUp')))
  for(var i = 0; i < i1703.length; i += 1) {
    i1702.add(request.d('PaintingConfigBackUp', i1703[i + 0]));
  }
  i1690.BackUpVariants = i1702
  i1690.CurrentBackUpIndex = i1691[10]
  return i1690
}

Deserializers["PaintingPixelConfig"] = function (request, data, root) {
  var i1706 = root || request.c( 'PaintingPixelConfig' )
  var i1707 = data
  i1706.column = i1707[0]
  i1706.row = i1707[1]
  i1706.color = new pc.Color(i1707[2], i1707[3], i1707[4], i1707[5])
  i1706.colorCode = i1707[6]
  i1706.Hidden = !!i1707[7]
  return i1706
}

Deserializers["PipeObjectSetup"] = function (request, data, root) {
  var i1710 = root || request.c( 'PipeObjectSetup' )
  var i1711 = data
  i1710.Hearts = i1711[0]
  var i1713 = i1711[1]
  var i1712 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1713.length; i += 1) {
    i1712.add(request.d('PaintingPixelConfig', i1713[i + 0]));
  }
  i1710.PixelCovered = i1712
  i1710.ColorCode = i1711[2]
  i1710.Scale = new pc.Vec3( i1711[3], i1711[4], i1711[5] )
  return i1710
}

Deserializers["WallObjectSetup"] = function (request, data, root) {
  var i1716 = root || request.c( 'WallObjectSetup' )
  var i1717 = data
  i1716.Hearts = i1717[0]
  var i1719 = i1717[1]
  var i1718 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1719.length; i += 1) {
    i1718.add(request.d('PaintingPixelConfig', i1719[i + 0]));
  }
  i1716.PixelCovered = i1718
  i1716.ColorCode = i1717[2]
  return i1716
}

Deserializers["KeyObjectSetup"] = function (request, data, root) {
  var i1722 = root || request.c( 'KeyObjectSetup' )
  var i1723 = data
  var i1725 = i1723[0]
  var i1724 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1725.length; i += 1) {
    i1724.add(request.d('PaintingPixelConfig', i1725[i + 0]));
  }
  i1722.PixelCovered = i1724
  i1722.ColorCode = i1723[1]
  return i1722
}

Deserializers["PaintingConfigBackUp"] = function (request, data, root) {
  var i1728 = root || request.c( 'PaintingConfigBackUp' )
  var i1729 = data
  i1728.DateTime = i1729[0]
  i1728._paintingSize = new pc.Vec2( i1729[1], i1729[2] )
  var i1731 = i1729[3]
  var i1730 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1731.length; i += 1) {
    i1730.add(request.d('PaintingPixelConfig', i1731[i + 0]));
  }
  i1728.Pixels = i1730
  var i1733 = i1729[4]
  var i1732 = new (System.Collections.Generic.List$1(Bridge.ns('PipeObjectSetup')))
  for(var i = 0; i < i1733.length; i += 1) {
    i1732.add(request.d('PipeObjectSetup', i1733[i + 0]));
  }
  i1728.PipeSetup = i1732
  var i1735 = i1729[5]
  var i1734 = new (System.Collections.Generic.List$1(Bridge.ns('WallObjectSetup')))
  for(var i = 0; i < i1735.length; i += 1) {
    i1734.add(request.d('WallObjectSetup', i1735[i + 0]));
  }
  i1728.WallSetup = i1734
  var i1737 = i1729[6]
  var i1736 = new (System.Collections.Generic.List$1(Bridge.ns('KeyObjectSetup')))
  for(var i = 0; i < i1737.length; i += 1) {
    i1736.add(request.d('KeyObjectSetup', i1737[i + 0]));
  }
  i1728.KeySetup = i1736
  var i1739 = i1729[7]
  var i1738 = new (System.Collections.Generic.List$1(Bridge.ns('PaintingPixelConfig')))
  for(var i = 0; i < i1739.length; i += 1) {
    i1738.add(request.d('PaintingPixelConfig', i1739[i + 0]));
  }
  i1728.AdditionPixels = i1738
  return i1728
}

Deserializers["LevelColorCollectorsConfig"] = function (request, data, root) {
  var i1740 = root || request.c( 'LevelColorCollectorsConfig' )
  var i1741 = data
  var i1743 = i1741[0]
  var i1742 = new (System.Collections.Generic.List$1(Bridge.ns('ColumnOfCollectorConfig')))
  for(var i = 0; i < i1743.length; i += 1) {
    i1742.add(request.d('ColumnOfCollectorConfig', i1743[i + 0]));
  }
  i1740.CollectorColumns = i1742
  var i1745 = i1741[1]
  var i1744 = new (System.Collections.Generic.List$1(Bridge.ns('LevelColorCollectorsConfigBackUp')))
  for(var i = 0; i < i1745.length; i += 1) {
    i1744.add(request.d('LevelColorCollectorsConfigBackUp', i1745[i + 0]));
  }
  i1740.BackUpVariants = i1744
  i1740.CurrentBackUpIndex = i1741[2]
  return i1740
}

Deserializers["ColumnOfCollectorConfig"] = function (request, data, root) {
  var i1748 = root || request.c( 'ColumnOfCollectorConfig' )
  var i1749 = data
  var i1751 = i1749[0]
  var i1750 = new (System.Collections.Generic.List$1(Bridge.ns('SingleColorCollectorConfig')))
  for(var i = 0; i < i1751.length; i += 1) {
    i1750.add(request.d('SingleColorCollectorConfig', i1751[i + 0]));
  }
  i1748.Collectors = i1750
  var i1753 = i1749[1]
  var i1752 = new (System.Collections.Generic.List$1(Bridge.ns('LockObjectConfig')))
  for(var i = 0; i < i1753.length; i += 1) {
    i1752.add(request.d('LockObjectConfig', i1753[i + 0]));
  }
  i1748.Locks = i1752
  return i1748
}

Deserializers["SingleColorCollectorConfig"] = function (request, data, root) {
  var i1756 = root || request.c( 'SingleColorCollectorConfig' )
  var i1757 = data
  i1756.ID = i1757[0]
  i1756.ColorCode = i1757[1]
  i1756.Bullets = i1757[2]
  i1756.Locked = !!i1757[3]
  i1756.Hidden = !!i1757[4]
  var i1759 = i1757[5]
  var i1758 = new (System.Collections.Generic.List$1(Bridge.ns('System.Int32')))
  for(var i = 0; i < i1759.length; i += 1) {
    i1758.add(i1759[i + 0]);
  }
  i1756.ConnectedCollectorsIDs = i1758
  return i1756
}

Deserializers["LockObjectConfig"] = function (request, data, root) {
  var i1762 = root || request.c( 'LockObjectConfig' )
  var i1763 = data
  i1762.ID = i1763[0]
  i1762.Row = i1763[1]
  return i1762
}

Deserializers["LevelColorCollectorsConfigBackUp"] = function (request, data, root) {
  var i1766 = root || request.c( 'LevelColorCollectorsConfigBackUp' )
  var i1767 = data
  i1766.DateTime = i1767[0]
  var i1769 = i1767[1]
  var i1768 = new (System.Collections.Generic.List$1(Bridge.ns('ColumnOfCollectorConfig')))
  for(var i = 0; i < i1769.length; i += 1) {
    i1768.add(request.d('ColumnOfCollectorConfig', i1769[i + 0]));
  }
  i1766.CollectorColumns = i1768
  return i1766
}

Deserializers["InGameEffectOptions"] = function (request, data, root) {
  var i1770 = root || request.c( 'InGameEffectOptions' )
  var i1771 = data
  i1770.BulletSpeed = i1771[0]
  i1770.BulletScale = i1771[1]
  i1770.ChangeBulletColor = !!i1771[2]
  i1770.ChangeOutlineColor = !!i1771[3]
  i1770.IdleRate = i1771[4]
  i1770.RabbitRandomIdleAnimation = !!i1771[5]
  i1770.RabbitEarAnimation = !!i1771[6]
  i1770.ShakeNeighborBlocks = !!i1771[7]
  i1770.ShakeValue = i1771[8]
  i1770.BlockScaleFactorWhenDestroyed = i1771[9]
  return i1770
}

Deserializers["ColorPalleteData"] = function (request, data, root) {
  var i1772 = root || request.c( 'ColorPalleteData' )
  var i1773 = data
  var i1775 = i1773[0]
  var i1774 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i1775.length; i += 1) {
    i1774.add(i1775[i + 0]);
  }
  i1772.ColorKeys = i1774
  var i1777 = i1773[1]
  var i1776 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Color')))
  for(var i = 0; i < i1777.length; i += 4) {
    i1776.add(new pc.Color(i1777[i + 0], i1777[i + 1], i1777[i + 2], i1777[i + 3]));
  }
  i1772.ColorsValues = i1776
  var i1779 = i1773[2]
  var i1778 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Material')))
  for(var i = 0; i < i1779.length; i += 2) {
  request.r(i1779[i + 0], i1779[i + 1], 1, i1778, '')
  }
  i1772.MatValues = i1778
  return i1772
}

Deserializers["LevelMechanicObjectPrefabs"] = function (request, data, root) {
  var i1784 = root || request.c( 'LevelMechanicObjectPrefabs' )
  var i1785 = data
  request.r(i1785[0], i1785[1], 0, i1784, 'PipeObjectPrefab')
  request.r(i1785[2], i1785[3], 0, i1784, 'PipeHeadPrefab')
  request.r(i1785[4], i1785[5], 0, i1784, 'PipeBodyPrefab')
  request.r(i1785[6], i1785[7], 0, i1784, 'PipeTailPrefab')
  request.r(i1785[8], i1785[9], 0, i1784, 'KeyObjectPrefab')
  request.r(i1785[10], i1785[11], 0, i1784, 'LockPrefab')
  request.r(i1785[12], i1785[13], 0, i1784, 'BigBlockPrefab')
  request.r(i1785[14], i1785[15], 0, i1784, 'DefaultBlockPrefab')
  request.r(i1785[16], i1785[17], 0, i1784, 'GunnerPrefab')
  request.r(i1785[18], i1785[19], 0, i1784, 'ColorPallete')
  return i1784
}

Deserializers["DG.Tweening.Core.DOTweenSettings"] = function (request, data, root) {
  var i1786 = root || request.c( 'DG.Tweening.Core.DOTweenSettings' )
  var i1787 = data
  i1786.useSafeMode = !!i1787[0]
  i1786.safeModeOptions = request.d('DG.Tweening.Core.DOTweenSettings+SafeModeOptions', i1787[1], i1786.safeModeOptions)
  i1786.timeScale = i1787[2]
  i1786.unscaledTimeScale = i1787[3]
  i1786.useSmoothDeltaTime = !!i1787[4]
  i1786.maxSmoothUnscaledTime = i1787[5]
  i1786.rewindCallbackMode = i1787[6]
  i1786.showUnityEditorReport = !!i1787[7]
  i1786.logBehaviour = i1787[8]
  i1786.drawGizmos = !!i1787[9]
  i1786.defaultRecyclable = !!i1787[10]
  i1786.defaultAutoPlay = i1787[11]
  i1786.defaultUpdateType = i1787[12]
  i1786.defaultTimeScaleIndependent = !!i1787[13]
  i1786.defaultEaseType = i1787[14]
  i1786.defaultEaseOvershootOrAmplitude = i1787[15]
  i1786.defaultEasePeriod = i1787[16]
  i1786.defaultAutoKill = !!i1787[17]
  i1786.defaultLoopType = i1787[18]
  i1786.debugMode = !!i1787[19]
  i1786.debugStoreTargetId = !!i1787[20]
  i1786.showPreviewPanel = !!i1787[21]
  i1786.storeSettingsLocation = i1787[22]
  i1786.modules = request.d('DG.Tweening.Core.DOTweenSettings+ModulesSetup', i1787[23], i1786.modules)
  i1786.createASMDEF = !!i1787[24]
  i1786.showPlayingTweens = !!i1787[25]
  i1786.showPausedTweens = !!i1787[26]
  return i1786
}

Deserializers["DG.Tweening.Core.DOTweenSettings+SafeModeOptions"] = function (request, data, root) {
  var i1788 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+SafeModeOptions' )
  var i1789 = data
  i1788.logBehaviour = i1789[0]
  i1788.nestedTweenFailureBehaviour = i1789[1]
  return i1788
}

Deserializers["DG.Tweening.Core.DOTweenSettings+ModulesSetup"] = function (request, data, root) {
  var i1790 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+ModulesSetup' )
  var i1791 = data
  i1790.showPanel = !!i1791[0]
  i1790.audioEnabled = !!i1791[1]
  i1790.physicsEnabled = !!i1791[2]
  i1790.physics2DEnabled = !!i1791[3]
  i1790.spriteEnabled = !!i1791[4]
  i1790.uiEnabled = !!i1791[5]
  i1790.textMeshProEnabled = !!i1791[6]
  i1790.tk2DEnabled = !!i1791[7]
  i1790.deAudioEnabled = !!i1791[8]
  i1790.deUnityExtendedEnabled = !!i1791[9]
  i1790.epoOutlineEnabled = !!i1791[10]
  return i1790
}

Deserializers["TMPro.TMP_Settings"] = function (request, data, root) {
  var i1792 = root || request.c( 'TMPro.TMP_Settings' )
  var i1793 = data
  i1792.m_enableWordWrapping = !!i1793[0]
  i1792.m_enableKerning = !!i1793[1]
  i1792.m_enableExtraPadding = !!i1793[2]
  i1792.m_enableTintAllSprites = !!i1793[3]
  i1792.m_enableParseEscapeCharacters = !!i1793[4]
  i1792.m_EnableRaycastTarget = !!i1793[5]
  i1792.m_GetFontFeaturesAtRuntime = !!i1793[6]
  i1792.m_missingGlyphCharacter = i1793[7]
  i1792.m_warningsDisabled = !!i1793[8]
  request.r(i1793[9], i1793[10], 0, i1792, 'm_defaultFontAsset')
  i1792.m_defaultFontAssetPath = i1793[11]
  i1792.m_defaultFontSize = i1793[12]
  i1792.m_defaultAutoSizeMinRatio = i1793[13]
  i1792.m_defaultAutoSizeMaxRatio = i1793[14]
  i1792.m_defaultTextMeshProTextContainerSize = new pc.Vec2( i1793[15], i1793[16] )
  i1792.m_defaultTextMeshProUITextContainerSize = new pc.Vec2( i1793[17], i1793[18] )
  i1792.m_autoSizeTextContainer = !!i1793[19]
  i1792.m_IsTextObjectScaleStatic = !!i1793[20]
  var i1795 = i1793[21]
  var i1794 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_FontAsset')))
  for(var i = 0; i < i1795.length; i += 2) {
  request.r(i1795[i + 0], i1795[i + 1], 1, i1794, '')
  }
  i1792.m_fallbackFontAssets = i1794
  i1792.m_matchMaterialPreset = !!i1793[22]
  request.r(i1793[23], i1793[24], 0, i1792, 'm_defaultSpriteAsset')
  i1792.m_defaultSpriteAssetPath = i1793[25]
  i1792.m_enableEmojiSupport = !!i1793[26]
  i1792.m_MissingCharacterSpriteUnicode = i1793[27]
  i1792.m_defaultColorGradientPresetsPath = i1793[28]
  request.r(i1793[29], i1793[30], 0, i1792, 'm_defaultStyleSheet')
  i1792.m_StyleSheetsResourcePath = i1793[31]
  request.r(i1793[32], i1793[33], 0, i1792, 'm_leadingCharacters')
  request.r(i1793[34], i1793[35], 0, i1792, 'm_followingCharacters')
  i1792.m_UseModernHangulLineBreakingRules = !!i1793[36]
  return i1792
}

Deserializers["TMPro.TMP_SpriteAsset"] = function (request, data, root) {
  var i1796 = root || request.c( 'TMPro.TMP_SpriteAsset' )
  var i1797 = data
  request.r(i1797[0], i1797[1], 0, i1796, 'spriteSheet')
  var i1799 = i1797[2]
  var i1798 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Sprite')))
  for(var i = 0; i < i1799.length; i += 1) {
    i1798.add(request.d('TMPro.TMP_Sprite', i1799[i + 0]));
  }
  i1796.spriteInfoList = i1798
  var i1801 = i1797[3]
  var i1800 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_SpriteAsset')))
  for(var i = 0; i < i1801.length; i += 2) {
  request.r(i1801[i + 0], i1801[i + 1], 1, i1800, '')
  }
  i1796.fallbackSpriteAssets = i1800
  i1796.hashCode = i1797[4]
  request.r(i1797[5], i1797[6], 0, i1796, 'material')
  i1796.materialHashCode = i1797[7]
  i1796.m_Version = i1797[8]
  i1796.m_FaceInfo = request.d('UnityEngine.TextCore.FaceInfo', i1797[9], i1796.m_FaceInfo)
  var i1803 = i1797[10]
  var i1802 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_SpriteCharacter')))
  for(var i = 0; i < i1803.length; i += 1) {
    i1802.add(request.d('TMPro.TMP_SpriteCharacter', i1803[i + 0]));
  }
  i1796.m_SpriteCharacterTable = i1802
  var i1805 = i1797[11]
  var i1804 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_SpriteGlyph')))
  for(var i = 0; i < i1805.length; i += 1) {
    i1804.add(request.d('TMPro.TMP_SpriteGlyph', i1805[i + 0]));
  }
  i1796.m_SpriteGlyphTable = i1804
  return i1796
}

Deserializers["TMPro.TMP_Sprite"] = function (request, data, root) {
  var i1808 = root || request.c( 'TMPro.TMP_Sprite' )
  var i1809 = data
  i1808.name = i1809[0]
  i1808.hashCode = i1809[1]
  i1808.unicode = i1809[2]
  i1808.pivot = new pc.Vec2( i1809[3], i1809[4] )
  request.r(i1809[5], i1809[6], 0, i1808, 'sprite')
  i1808.id = i1809[7]
  i1808.x = i1809[8]
  i1808.y = i1809[9]
  i1808.width = i1809[10]
  i1808.height = i1809[11]
  i1808.xOffset = i1809[12]
  i1808.yOffset = i1809[13]
  i1808.xAdvance = i1809[14]
  i1808.scale = i1809[15]
  return i1808
}

Deserializers["TMPro.TMP_SpriteCharacter"] = function (request, data, root) {
  var i1814 = root || request.c( 'TMPro.TMP_SpriteCharacter' )
  var i1815 = data
  i1814.m_Name = i1815[0]
  i1814.m_HashCode = i1815[1]
  i1814.m_ElementType = i1815[2]
  i1814.m_Unicode = i1815[3]
  i1814.m_GlyphIndex = i1815[4]
  i1814.m_Scale = i1815[5]
  return i1814
}

Deserializers["TMPro.TMP_SpriteGlyph"] = function (request, data, root) {
  var i1818 = root || request.c( 'TMPro.TMP_SpriteGlyph' )
  var i1819 = data
  request.r(i1819[0], i1819[1], 0, i1818, 'sprite')
  i1818.m_Index = i1819[2]
  i1818.m_Metrics = request.d('UnityEngine.TextCore.GlyphMetrics', i1819[3], i1818.m_Metrics)
  i1818.m_GlyphRect = request.d('UnityEngine.TextCore.GlyphRect', i1819[4], i1818.m_GlyphRect)
  i1818.m_Scale = i1819[5]
  i1818.m_AtlasIndex = i1819[6]
  i1818.m_ClassDefinitionType = i1819[7]
  return i1818
}

Deserializers["TMPro.TMP_StyleSheet"] = function (request, data, root) {
  var i1820 = root || request.c( 'TMPro.TMP_StyleSheet' )
  var i1821 = data
  var i1823 = i1821[0]
  var i1822 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Style')))
  for(var i = 0; i < i1823.length; i += 1) {
    i1822.add(request.d('TMPro.TMP_Style', i1823[i + 0]));
  }
  i1820.m_StyleList = i1822
  return i1820
}

Deserializers["TMPro.TMP_Style"] = function (request, data, root) {
  var i1826 = root || request.c( 'TMPro.TMP_Style' )
  var i1827 = data
  i1826.m_Name = i1827[0]
  i1826.m_HashCode = i1827[1]
  i1826.m_OpeningDefinition = i1827[2]
  i1826.m_ClosingDefinition = i1827[3]
  i1826.m_OpeningTagArray = i1827[4]
  i1826.m_ClosingTagArray = i1827[5]
  i1826.m_OpeningTagUnicodeArray = i1827[6]
  i1826.m_ClosingTagUnicodeArray = i1827[7]
  return i1826
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i1828 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i1829 = data
  var i1831 = i1829[0]
  var i1830 = []
  for(var i = 0; i < i1831.length; i += 1) {
    i1830.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i1831[i + 0]) );
  }
  i1828.files = i1830
  i1828.componentToPrefabIds = i1829[1]
  return i1828
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i1834 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i1835 = data
  i1834.path = i1835[0]
  request.r(i1835[1], i1835[2], 0, i1834, 'unityObject')
  return i1834
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i1836 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i1837 = data
  var i1839 = i1837[0]
  var i1838 = []
  for(var i = 0; i < i1839.length; i += 1) {
    i1838.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i1839[i + 0]) );
  }
  i1836.scriptsExecutionOrder = i1838
  var i1841 = i1837[1]
  var i1840 = []
  for(var i = 0; i < i1841.length; i += 1) {
    i1840.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i1841[i + 0]) );
  }
  i1836.sortingLayers = i1840
  var i1843 = i1837[2]
  var i1842 = []
  for(var i = 0; i < i1843.length; i += 1) {
    i1842.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i1843[i + 0]) );
  }
  i1836.cullingLayers = i1842
  i1836.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i1837[3], i1836.timeSettings)
  i1836.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i1837[4], i1836.physicsSettings)
  i1836.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i1837[5], i1836.physics2DSettings)
  i1836.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i1837[6], i1836.qualitySettings)
  i1836.enableRealtimeShadows = !!i1837[7]
  i1836.enableAutoInstancing = !!i1837[8]
  i1836.enableStaticBatching = !!i1837[9]
  i1836.enableDynamicBatching = !!i1837[10]
  i1836.lightmapEncodingQuality = i1837[11]
  i1836.desiredColorSpace = i1837[12]
  var i1845 = i1837[13]
  var i1844 = []
  for(var i = 0; i < i1845.length; i += 1) {
    i1844.push( i1845[i + 0] );
  }
  i1836.allTags = i1844
  return i1836
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i1848 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i1849 = data
  i1848.name = i1849[0]
  i1848.value = i1849[1]
  return i1848
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i1852 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i1853 = data
  i1852.id = i1853[0]
  i1852.name = i1853[1]
  i1852.value = i1853[2]
  return i1852
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i1856 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i1857 = data
  i1856.id = i1857[0]
  i1856.name = i1857[1]
  return i1856
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i1858 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i1859 = data
  i1858.fixedDeltaTime = i1859[0]
  i1858.maximumDeltaTime = i1859[1]
  i1858.timeScale = i1859[2]
  i1858.maximumParticleTimestep = i1859[3]
  return i1858
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i1860 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i1861 = data
  i1860.gravity = new pc.Vec3( i1861[0], i1861[1], i1861[2] )
  i1860.defaultSolverIterations = i1861[3]
  i1860.bounceThreshold = i1861[4]
  i1860.autoSyncTransforms = !!i1861[5]
  i1860.autoSimulation = !!i1861[6]
  var i1863 = i1861[7]
  var i1862 = []
  for(var i = 0; i < i1863.length; i += 1) {
    i1862.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i1863[i + 0]) );
  }
  i1860.collisionMatrix = i1862
  return i1860
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i1866 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i1867 = data
  i1866.enabled = !!i1867[0]
  i1866.layerId = i1867[1]
  i1866.otherLayerId = i1867[2]
  return i1866
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i1868 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i1869 = data
  request.r(i1869[0], i1869[1], 0, i1868, 'material')
  i1868.gravity = new pc.Vec2( i1869[2], i1869[3] )
  i1868.positionIterations = i1869[4]
  i1868.velocityIterations = i1869[5]
  i1868.velocityThreshold = i1869[6]
  i1868.maxLinearCorrection = i1869[7]
  i1868.maxAngularCorrection = i1869[8]
  i1868.maxTranslationSpeed = i1869[9]
  i1868.maxRotationSpeed = i1869[10]
  i1868.baumgarteScale = i1869[11]
  i1868.baumgarteTOIScale = i1869[12]
  i1868.timeToSleep = i1869[13]
  i1868.linearSleepTolerance = i1869[14]
  i1868.angularSleepTolerance = i1869[15]
  i1868.defaultContactOffset = i1869[16]
  i1868.autoSimulation = !!i1869[17]
  i1868.queriesHitTriggers = !!i1869[18]
  i1868.queriesStartInColliders = !!i1869[19]
  i1868.callbacksOnDisable = !!i1869[20]
  i1868.reuseCollisionCallbacks = !!i1869[21]
  i1868.autoSyncTransforms = !!i1869[22]
  var i1871 = i1869[23]
  var i1870 = []
  for(var i = 0; i < i1871.length; i += 1) {
    i1870.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i1871[i + 0]) );
  }
  i1868.collisionMatrix = i1870
  return i1868
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i1874 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i1875 = data
  i1874.enabled = !!i1875[0]
  i1874.layerId = i1875[1]
  i1874.otherLayerId = i1875[2]
  return i1874
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i1876 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i1877 = data
  var i1879 = i1877[0]
  var i1878 = []
  for(var i = 0; i < i1879.length; i += 1) {
    i1878.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i1879[i + 0]) );
  }
  i1876.qualityLevels = i1878
  var i1881 = i1877[1]
  var i1880 = []
  for(var i = 0; i < i1881.length; i += 1) {
    i1880.push( i1881[i + 0] );
  }
  i1876.names = i1880
  i1876.shadows = i1877[2]
  i1876.anisotropicFiltering = i1877[3]
  i1876.antiAliasing = i1877[4]
  i1876.lodBias = i1877[5]
  i1876.shadowCascades = i1877[6]
  i1876.shadowDistance = i1877[7]
  i1876.shadowmaskMode = i1877[8]
  i1876.shadowProjection = i1877[9]
  i1876.shadowResolution = i1877[10]
  i1876.softParticles = !!i1877[11]
  i1876.softVegetation = !!i1877[12]
  i1876.activeColorSpace = i1877[13]
  i1876.desiredColorSpace = i1877[14]
  i1876.masterTextureLimit = i1877[15]
  i1876.maxQueuedFrames = i1877[16]
  i1876.particleRaycastBudget = i1877[17]
  i1876.pixelLightCount = i1877[18]
  i1876.realtimeReflectionProbes = !!i1877[19]
  i1876.shadowCascade2Split = i1877[20]
  i1876.shadowCascade4Split = new pc.Vec3( i1877[21], i1877[22], i1877[23] )
  i1876.streamingMipmapsActive = !!i1877[24]
  i1876.vSyncCount = i1877[25]
  i1876.asyncUploadBufferSize = i1877[26]
  i1876.asyncUploadTimeSlice = i1877[27]
  i1876.billboardsFaceCameraPosition = !!i1877[28]
  i1876.shadowNearPlaneOffset = i1877[29]
  i1876.streamingMipmapsMemoryBudget = i1877[30]
  i1876.maximumLODLevel = i1877[31]
  i1876.streamingMipmapsAddAllCameras = !!i1877[32]
  i1876.streamingMipmapsMaxLevelReduction = i1877[33]
  i1876.streamingMipmapsRenderersPerFrame = i1877[34]
  i1876.resolutionScalingFixedDPIFactor = i1877[35]
  i1876.streamingMipmapsMaxFileIORequests = i1877[36]
  i1876.currentQualityLevel = i1877[37]
  return i1876
}

Deserializers["Luna.Unity.DTO.UnityEngine.Audio.AudioMixer"] = function (request, data, root) {
  var i1884 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Audio.AudioMixer' )
  var i1885 = data
  var i1887 = i1885[0]
  var i1886 = []
  for(var i = 0; i < i1887.length; i += 1) {
    i1886.push( request.d('Luna.Unity.DTO.UnityEngine.Audio.AudioMixerGroup', i1887[i + 0]) );
  }
  i1884.groups = i1886
  var i1889 = i1885[1]
  var i1888 = []
  for(var i = 0; i < i1889.length; i += 1) {
    i1888.push( request.d('Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot', i1889[i + 0]) );
  }
  i1884.snapshots = i1888
  return i1884
}

Deserializers["Luna.Unity.DTO.UnityEngine.Audio.AudioMixerGroup"] = function (request, data, root) {
  var i1892 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Audio.AudioMixerGroup' )
  var i1893 = data
  i1892.id = i1893[0]
  i1892.childGroupIds = i1893[1]
  i1892.name = i1893[2]
  return i1892
}

Deserializers["Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot"] = function (request, data, root) {
  var i1896 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot' )
  var i1897 = data
  i1896.id = i1897[0]
  var i1899 = i1897[1]
  var i1898 = []
  for(var i = 0; i < i1899.length; i += 1) {
    i1898.push( request.d('Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot+Parameter', i1899[i + 0]) );
  }
  i1896.parameters = i1898
  return i1896
}

Deserializers["Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot+Parameter"] = function (request, data, root) {
  var i1902 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot+Parameter' )
  var i1903 = data
  i1902.name = i1903[0]
  i1902.value = i1903[1]
  return i1902
}

Deserializers["UnityEngine.Events.ArgumentCache"] = function (request, data, root) {
  var i1904 = root || request.c( 'UnityEngine.Events.ArgumentCache' )
  var i1905 = data
  request.r(i1905[0], i1905[1], 0, i1904, 'm_ObjectArgument')
  i1904.m_ObjectArgumentAssemblyTypeName = i1905[2]
  i1904.m_IntArgument = i1905[3]
  i1904.m_FloatArgument = i1905[4]
  i1904.m_StringArgument = i1905[5]
  i1904.m_BoolArgument = !!i1905[6]
  return i1904
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame"] = function (request, data, root) {
  var i1908 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame' )
  var i1909 = data
  i1908.weight = i1909[0]
  i1908.vertices = i1909[1]
  i1908.normals = i1909[2]
  i1908.tangents = i1909[3]
  return i1908
}

Deserializers["TMPro.GlyphValueRecord_Legacy"] = function (request, data, root) {
  var i1910 = root || request.c( 'TMPro.GlyphValueRecord_Legacy' )
  var i1911 = data
  i1910.xPlacement = i1911[0]
  i1910.yPlacement = i1911[1]
  i1910.xAdvance = i1911[2]
  i1910.yAdvance = i1911[3]
  return i1910
}

Deserializers["TMPro.TMP_GlyphAdjustmentRecord"] = function (request, data, root) {
  var i1912 = root || request.c( 'TMPro.TMP_GlyphAdjustmentRecord' )
  var i1913 = data
  i1912.m_GlyphIndex = i1913[0]
  i1912.m_GlyphValueRecord = request.d('TMPro.TMP_GlyphValueRecord', i1913[1], i1912.m_GlyphValueRecord)
  return i1912
}

Deserializers["TMPro.TMP_GlyphValueRecord"] = function (request, data, root) {
  var i1914 = root || request.c( 'TMPro.TMP_GlyphValueRecord' )
  var i1915 = data
  i1914.m_XPlacement = i1915[0]
  i1914.m_YPlacement = i1915[1]
  i1914.m_XAdvance = i1915[2]
  i1914.m_YAdvance = i1915[3]
  return i1914
}

Deserializers.fields = {"Luna.Unity.DTO.UnityEngine.Components.RectTransform":{"pivot":0,"anchorMin":2,"anchorMax":4,"sizeDelta":6,"anchoredPosition3D":8,"rotation":11,"scale":15},"Luna.Unity.DTO.UnityEngine.Components.Canvas":{"planeDistance":0,"referencePixelsPerUnit":1,"isFallbackOverlay":2,"renderMode":3,"renderOrder":4,"sortingLayerName":5,"sortingOrder":6,"scaleFactor":7,"worldCamera":8,"overrideSorting":10,"pixelPerfect":11,"targetDisplay":12,"overridePixelPerfect":13,"enabled":14},"Luna.Unity.DTO.UnityEngine.Components.CanvasGroup":{"m_Alpha":0,"m_Interactable":1,"m_BlocksRaycasts":2,"m_IgnoreParentGroups":3,"enabled":4},"Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer":{"cullTransparentMesh":0},"Luna.Unity.DTO.UnityEngine.Scene.GameObject":{"name":0,"tagId":1,"enabled":2,"isStatic":3,"layer":4},"Luna.Unity.DTO.UnityEngine.Components.Transform":{"position":0,"scale":3,"rotation":6},"Luna.Unity.DTO.UnityEngine.Textures.Texture2D":{"name":0,"width":1,"height":2,"mipmapCount":3,"anisoLevel":4,"filterMode":5,"hdr":6,"format":7,"wrapMode":8,"alphaIsTransparency":9,"alphaSource":10,"graphicsFormat":11,"sRGBTexture":12,"desiredColorSpace":13,"wrapU":14,"wrapV":15},"Luna.Unity.DTO.UnityEngine.Assets.Material":{"name":0,"shader":1,"renderQueue":3,"enableInstancing":4,"floatParameters":5,"colorParameters":6,"vectorParameters":7,"textureParameters":8,"materialFlags":9},"Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag":{"name":0,"enabled":1},"Luna.Unity.DTO.UnityEngine.Components.AudioSource":{"clip":0,"outputAudioMixerGroup":2,"playOnAwake":4,"loop":5,"time":6,"volume":7,"pitch":8,"enabled":9},"Luna.Unity.DTO.UnityEngine.Components.MeshFilter":{"sharedMesh":0},"Luna.Unity.DTO.UnityEngine.Components.MeshRenderer":{"additionalVertexStreams":0,"enabled":2,"sharedMaterial":3,"sharedMaterials":5,"receiveShadows":6,"shadowCastingMode":7,"sortingLayerID":8,"sortingOrder":9,"lightmapIndex":10,"lightmapSceneIndex":11,"lightmapScaleOffset":12,"lightProbeUsage":16,"reflectionProbeUsage":17},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystem":{"main":0,"colorBySpeed":1,"colorOverLifetime":2,"emission":3,"rotationBySpeed":4,"rotationOverLifetime":5,"shape":6,"sizeBySpeed":7,"sizeOverLifetime":8,"textureSheetAnimation":9,"velocityOverLifetime":10,"noise":11,"inheritVelocity":12,"forceOverLifetime":13,"limitVelocityOverLifetime":14,"useAutoRandomSeed":15,"randomSeed":16},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule":{"duration":0,"loop":1,"prewarm":2,"startDelay":3,"startLifetime":4,"startSpeed":5,"startSize3D":6,"startSizeX":7,"startSizeY":8,"startSizeZ":9,"startRotation3D":10,"startRotationX":11,"startRotationY":12,"startRotationZ":13,"startColor":14,"gravityModifier":15,"simulationSpace":16,"customSimulationSpace":17,"simulationSpeed":19,"useUnscaledTime":20,"scalingMode":21,"playOnAwake":22,"maxParticles":23,"emitterVelocityMode":24,"stopAction":25},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve":{"mode":0,"curveMin":1,"curveMax":2,"curveMultiplier":3,"constantMin":4,"constantMax":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient":{"mode":0,"gradientMin":1,"gradientMax":2,"colorMin":3,"colorMax":7},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient":{"mode":0,"colorKeys":1,"alphaKeys":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule":{"enabled":0,"color":1,"range":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey":{"color":0,"time":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey":{"alpha":0,"time":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule":{"enabled":0,"color":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule":{"enabled":0,"rateOverTime":1,"rateOverDistance":2,"bursts":3},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst":{"count":0,"cycleCount":1,"minCount":2,"maxCount":3,"repeatInterval":4,"time":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule":{"enabled":0,"shapeType":1,"randomDirectionAmount":2,"sphericalDirectionAmount":3,"randomPositionAmount":4,"alignToDirection":5,"radius":6,"radiusMode":7,"radiusSpread":8,"radiusSpeed":9,"radiusThickness":10,"angle":11,"length":12,"boxThickness":13,"meshShapeType":16,"mesh":17,"meshRenderer":19,"skinnedMeshRenderer":21,"useMeshMaterialIndex":23,"meshMaterialIndex":24,"useMeshColors":25,"normalOffset":26,"arc":27,"arcMode":28,"arcSpread":29,"arcSpeed":30,"donutRadius":31,"position":32,"rotation":35,"scale":38},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule":{"enabled":0,"mode":1,"animation":2,"numTilesX":3,"numTilesY":4,"useRandomRow":5,"frameOverTime":6,"startFrame":7,"cycleCount":8,"rowIndex":9,"flipU":10,"flipV":11,"spriteCount":12,"sprites":13},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"radial":4,"speedModifier":5,"space":6,"orbitalX":7,"orbitalY":8,"orbitalZ":9,"orbitalOffsetX":10,"orbitalOffsetY":11,"orbitalOffsetZ":12},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule":{"enabled":0,"separateAxes":1,"strengthX":2,"strengthY":3,"strengthZ":4,"frequency":5,"damping":6,"octaveCount":7,"octaveMultiplier":8,"octaveScale":9,"quality":10,"scrollSpeed":11,"scrollSpeedMultiplier":12,"remapEnabled":13,"remapX":14,"remapY":15,"remapZ":16,"positionAmount":17,"rotationAmount":18,"sizeAmount":19},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule":{"enabled":0,"mode":1,"curve":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"space":4,"randomized":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule":{"enabled":0,"limit":1,"limitX":2,"limitY":3,"limitZ":4,"dampen":5,"separateAxes":6,"space":7,"drag":8,"multiplyDragByParticleSize":9,"multiplyDragByParticleVelocity":10},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer":{"mesh":0,"meshCount":2,"activeVertexStreamsCount":3,"alignment":4,"renderMode":5,"sortMode":6,"lengthScale":7,"velocityScale":8,"cameraVelocityScale":9,"normalDirection":10,"sortingFudge":11,"minParticleSize":12,"maxParticleSize":13,"pivot":14,"trailMaterial":17,"applyActiveColorSpace":19,"enabled":20,"sharedMaterial":21,"sharedMaterials":23,"receiveShadows":24,"shadowCastingMode":25,"sortingLayerID":26,"sortingOrder":27,"lightmapIndex":28,"lightmapSceneIndex":29,"lightmapScaleOffset":30,"lightProbeUsage":34,"reflectionProbeUsage":35},"Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer":{"color":0,"sprite":4,"flipX":6,"flipY":7,"drawMode":8,"size":9,"tileMode":11,"adaptiveModeThreshold":12,"maskInteraction":13,"spriteSortPoint":14,"enabled":15,"sharedMaterial":16,"sharedMaterials":18,"receiveShadows":19,"shadowCastingMode":20,"sortingLayerID":21,"sortingOrder":22,"lightmapIndex":23,"lightmapSceneIndex":24,"lightmapScaleOffset":25,"lightProbeUsage":29,"reflectionProbeUsage":30},"Luna.Unity.DTO.UnityEngine.Assets.Mesh":{"name":0,"halfPrecision":1,"useUInt32IndexFormat":2,"vertexCount":3,"aabb":4,"streams":5,"vertices":6,"subMeshes":7,"bindposes":8,"blendShapes":9},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh":{"triangles":0},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape":{"name":0,"frames":1},"Luna.Unity.DTO.UnityEngine.Components.BoxCollider":{"center":0,"size":3,"enabled":6,"isTrigger":7,"material":8},"Luna.Unity.DTO.UnityEngine.Components.TrailRenderer":{"positions":0,"positionCount":1,"time":2,"startWidth":3,"endWidth":4,"widthMultiplier":5,"autodestruct":6,"emitting":7,"numCornerVertices":8,"numCapVertices":9,"minVertexDistance":10,"colorGradient":11,"startColor":12,"endColor":16,"generateLightingData":20,"textureMode":21,"alignment":22,"widthCurve":23,"enabled":24,"sharedMaterial":25,"sharedMaterials":27,"receiveShadows":28,"shadowCastingMode":29,"sortingLayerID":30,"sortingOrder":31,"lightmapIndex":32,"lightmapSceneIndex":33,"lightmapScaleOffset":34,"lightProbeUsage":38,"reflectionProbeUsage":39},"Luna.Unity.DTO.UnityEngine.Components.Camera":{"aspect":0,"orthographic":1,"orthographicSize":2,"backgroundColor":3,"nearClipPlane":7,"farClipPlane":8,"fieldOfView":9,"depth":10,"clearFlags":11,"cullingMask":12,"rect":13,"targetTexture":14,"usePhysicalProperties":16,"focalLength":17,"sensorSize":18,"lensShift":20,"gateFit":22,"commandBufferCount":23,"cameraType":24,"enabled":25},"Luna.Unity.DTO.UnityEngine.Components.Animator":{"animatorController":0,"avatar":2,"updateMode":4,"hasTransformHierarchy":5,"applyRootMotion":6,"humanBones":7,"enabled":8},"Luna.Unity.DTO.UnityEngine.Components.LineRenderer":{"textureMode":0,"alignment":1,"widthCurve":2,"colorGradient":3,"positions":4,"positionCount":5,"widthMultiplier":6,"startWidth":7,"endWidth":8,"numCornerVertices":9,"numCapVertices":10,"useWorldSpace":11,"loop":12,"startColor":13,"endColor":17,"generateLightingData":21,"enabled":22,"sharedMaterial":23,"sharedMaterials":25,"receiveShadows":26,"shadowCastingMode":27,"sortingLayerID":28,"sortingOrder":29,"lightmapIndex":30,"lightmapSceneIndex":31,"lightmapScaleOffset":32,"lightProbeUsage":36,"reflectionProbeUsage":37},"Luna.Unity.DTO.UnityEngine.Textures.Cubemap":{"name":0,"atlasId":1,"mipmapCount":2,"hdr":3,"size":4,"anisoLevel":5,"filterMode":6,"rects":7,"wrapU":8,"wrapV":9},"Luna.Unity.DTO.UnityEngine.Scene.Scene":{"name":0,"index":1,"startup":2},"Luna.Unity.DTO.UnityEngine.Components.Light":{"type":0,"color":1,"cullingMask":5,"intensity":6,"range":7,"spotAngle":8,"shadows":9,"shadowNormalBias":10,"shadowBias":11,"shadowStrength":12,"shadowResolution":13,"lightmapBakeType":14,"renderMode":15,"cookie":16,"cookieSize":18,"enabled":19},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings":{"ambientIntensity":0,"reflectionIntensity":1,"ambientMode":2,"ambientLight":3,"ambientSkyColor":7,"ambientGroundColor":11,"ambientEquatorColor":15,"fogColor":19,"fogEndDistance":23,"fogStartDistance":24,"fogDensity":25,"fog":26,"skybox":27,"fogMode":29,"lightmaps":30,"lightProbes":31,"lightmapsMode":32,"mixedBakeMode":33,"environmentLightingMode":34,"ambientProbe":35,"referenceAmbientProbe":36,"useReferenceAmbientProbe":37,"customReflection":38,"defaultReflection":40,"defaultReflectionMode":42,"defaultReflectionResolution":43,"sunLightObjectId":44,"pixelLightCount":45,"defaultReflectionHDR":46,"hasLightDataAsset":47,"hasManualGenerate":48},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap":{"lightmapColor":0,"lightmapDirection":2},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes":{"bakedProbes":0,"positions":1,"hullRays":2,"tetrahedra":3,"neighbours":4,"matrices":5},"Luna.Unity.DTO.UnityEngine.Assets.Shader":{"ShaderCompilationErrors":0,"name":1,"guid":2,"shaderDefinedKeywords":3,"passes":4,"usePasses":5,"defaultParameterValues":6,"unityFallbackShader":7,"readDepth":9,"isCreatedByShaderGraph":10,"disableBatching":11,"compiled":12},"Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError":{"shaderName":0,"errorMessage":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass":{"id":0,"subShaderIndex":1,"name":2,"passType":3,"grabPassTextureName":4,"usePass":5,"zTest":6,"zWrite":7,"culling":8,"blending":9,"alphaBlending":10,"colorWriteMask":11,"offsetUnits":12,"offsetFactor":13,"stencilRef":14,"stencilReadMask":15,"stencilWriteMask":16,"stencilOp":17,"stencilOpFront":18,"stencilOpBack":19,"tags":20,"passDefinedKeywords":21,"passDefinedKeywordGroups":22,"variants":23,"excludedVariants":24,"hasDepthReader":25},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value":{"val":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending":{"src":0,"dst":1,"op":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp":{"pass":0,"fail":1,"zFail":2,"comp":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup":{"keywords":0,"hasDiscard":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant":{"passId":0,"subShaderIndex":1,"keywords":2,"vertexProgram":3,"fragmentProgram":4,"exportedForWebGl2":5,"readDepth":6},"Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass":{"shader":0,"pass":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue":{"name":0,"type":1,"value":2,"textureValue":6,"shaderPropertyFlag":7},"Luna.Unity.DTO.UnityEngine.Textures.Sprite":{"name":0,"texture":1,"aabb":3,"vertices":4,"triangles":5,"textureRect":6,"packedRect":10,"border":14,"transparency":18,"bounds":19,"pixelsPerUnit":20,"textureWidth":21,"textureHeight":22,"nativeSize":23,"pivot":25,"textureRectOffset":27},"Luna.Unity.DTO.UnityEngine.Assets.AudioClip":{"name":0},"Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip":{"name":0,"wrapMode":1,"isLooping":2,"length":3,"curves":4,"events":5,"halfPrecision":6,"_frameRate":7,"localBounds":8,"hasMuscleCurves":9,"clipMuscleConstant":10,"clipBindingConstant":11},"Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve":{"path":0,"hash":1,"componentType":2,"property":3,"keys":4,"objectReferenceKeys":5},"Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationCurve+ObjectReferenceKey":{"time":0,"value":1},"Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationEvent":{"functionName":0,"floatParameter":1,"intParameter":2,"stringParameter":3,"objectReferenceParameter":4,"time":6},"Luna.Unity.DTO.UnityEngine.Animation.Data.Bounds":{"center":0,"extends":3},"Luna.Unity.DTO.UnityEngine.Animation.Data.AnimationClip+AnimationClipBindingConstant":{"genericBindings":0,"pptrCurveMapping":1},"Luna.Unity.DTO.UnityEngine.Assets.Font":{"name":0,"ascent":1,"originalLineHeight":2,"fontSize":3,"characterInfo":4,"texture":5,"originalFontSize":7},"Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo":{"index":0,"advance":1,"bearing":2,"glyphWidth":3,"glyphHeight":4,"minX":5,"maxX":6,"minY":7,"maxY":8,"uvBottomLeftX":9,"uvBottomLeftY":10,"uvBottomRightX":11,"uvBottomRightY":12,"uvTopLeftX":13,"uvTopLeftY":14,"uvTopRightX":15,"uvTopRightY":16},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorController":{"name":0,"layers":1,"parameters":2,"animationClips":3,"avatarUnsupported":4},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerLayer":{"name":0,"defaultWeight":1,"blendingMode":2,"avatarMask":3,"syncedLayerIndex":4,"syncedLayerAffectsTiming":5,"syncedLayers":6,"stateMachine":7},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateMachine":{"id":0,"name":1,"path":2,"states":3,"machines":4,"entryStateTransitions":5,"exitStateTransitions":6,"anyStateTransitions":7,"defaultStateId":8},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorState":{"id":0,"name":1,"cycleOffset":2,"cycleOffsetParameter":3,"cycleOffsetParameterActive":4,"mirror":5,"mirrorParameter":6,"mirrorParameterActive":7,"motionId":8,"nameHash":9,"fullPathHash":10,"speed":11,"speedParameter":12,"speedParameterActive":13,"tag":14,"tagHash":15,"writeDefaultValues":16,"behaviours":17,"transitions":18},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorStateTransition":{"fullPath":0,"canTransitionToSelf":1,"duration":2,"exitTime":3,"hasExitTime":4,"hasFixedDuration":5,"interruptionSource":6,"offset":7,"orderedInterruption":8,"destinationStateId":9,"isExit":10,"mute":11,"solo":12,"conditions":13},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorCondition":{"mode":0,"parameter":1,"threshold":2},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorTransition":{"destinationStateId":0,"isExit":1,"mute":2,"solo":3,"conditions":4},"Luna.Unity.DTO.UnityEngine.Animation.Mecanim.AnimatorControllerParameter":{"defaultBool":0,"defaultFloat":1,"defaultInt":2,"name":3,"nameHash":4,"type":5},"Luna.Unity.DTO.UnityEngine.Assets.TextAsset":{"name":0,"bytes64":1,"data":2},"Luna.Unity.DTO.UnityEngine.Assets.Resources":{"files":0,"componentToPrefabIds":1},"Luna.Unity.DTO.UnityEngine.Assets.Resources+File":{"path":0,"unityObject":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings":{"scriptsExecutionOrder":0,"sortingLayers":1,"cullingLayers":2,"timeSettings":3,"physicsSettings":4,"physics2DSettings":5,"qualitySettings":6,"enableRealtimeShadows":7,"enableAutoInstancing":8,"enableStaticBatching":9,"enableDynamicBatching":10,"lightmapEncodingQuality":11,"desiredColorSpace":12,"allTags":13},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer":{"id":0,"name":1,"value":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer":{"id":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings":{"fixedDeltaTime":0,"maximumDeltaTime":1,"timeScale":2,"maximumParticleTimestep":3},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings":{"gravity":0,"defaultSolverIterations":3,"bounceThreshold":4,"autoSyncTransforms":5,"autoSimulation":6,"collisionMatrix":7},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings":{"material":0,"gravity":2,"positionIterations":4,"velocityIterations":5,"velocityThreshold":6,"maxLinearCorrection":7,"maxAngularCorrection":8,"maxTranslationSpeed":9,"maxRotationSpeed":10,"baumgarteScale":11,"baumgarteTOIScale":12,"timeToSleep":13,"linearSleepTolerance":14,"angularSleepTolerance":15,"defaultContactOffset":16,"autoSimulation":17,"queriesHitTriggers":18,"queriesStartInColliders":19,"callbacksOnDisable":20,"reuseCollisionCallbacks":21,"autoSyncTransforms":22,"collisionMatrix":23},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.QualitySettings":{"qualityLevels":0,"names":1,"shadows":2,"anisotropicFiltering":3,"antiAliasing":4,"lodBias":5,"shadowCascades":6,"shadowDistance":7,"shadowmaskMode":8,"shadowProjection":9,"shadowResolution":10,"softParticles":11,"softVegetation":12,"activeColorSpace":13,"desiredColorSpace":14,"masterTextureLimit":15,"maxQueuedFrames":16,"particleRaycastBudget":17,"pixelLightCount":18,"realtimeReflectionProbes":19,"shadowCascade2Split":20,"shadowCascade4Split":21,"streamingMipmapsActive":24,"vSyncCount":25,"asyncUploadBufferSize":26,"asyncUploadTimeSlice":27,"billboardsFaceCameraPosition":28,"shadowNearPlaneOffset":29,"streamingMipmapsMemoryBudget":30,"maximumLODLevel":31,"streamingMipmapsAddAllCameras":32,"streamingMipmapsMaxLevelReduction":33,"streamingMipmapsRenderersPerFrame":34,"resolutionScalingFixedDPIFactor":35,"streamingMipmapsMaxFileIORequests":36,"currentQualityLevel":37},"Luna.Unity.DTO.UnityEngine.Audio.AudioMixer":{"groups":0,"snapshots":1},"Luna.Unity.DTO.UnityEngine.Audio.AudioMixerGroup":{"id":0,"childGroupIds":1,"name":2},"Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot":{"id":0,"parameters":1},"Luna.Unity.DTO.UnityEngine.Audio.AudioMixerSnapshot+Parameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame":{"weight":0,"vertices":1,"normals":2,"tangents":3}}

Deserializers.requiredComponents = {"97":[98],"99":[98],"100":[98],"101":[98],"102":[98],"103":[98],"104":[105],"106":[50],"107":[108],"109":[108],"110":[108],"111":[108],"112":[108],"113":[108],"114":[108],"115":[116],"117":[116],"118":[116],"119":[116],"120":[116],"121":[116],"122":[116],"123":[116],"124":[116],"125":[116],"126":[116],"127":[116],"128":[116],"129":[50],"130":[42],"131":[132],"133":[132],"1":[0],"43":[40,42],"75":[80],"76":[40,42,75],"134":[135],"136":[0],"137":[0],"4":[1],"7":[6,0],"138":[0],"3":[1],"139":[0],"140":[0],"141":[0],"142":[0],"143":[0],"144":[0],"145":[0],"146":[0],"147":[0],"148":[6,0],"149":[0],"150":[0],"151":[0],"152":[0],"14":[6,0],"153":[0],"154":[18],"155":[18],"19":[18],"156":[18],"157":[50],"158":[50],"159":[135],"160":[0],"69":[42,0],"21":[0,6],"161":[0],"162":[6,0],"163":[42],"164":[6,0],"165":[0],"166":[135]}

Deserializers.types = ["UnityEngine.RectTransform","UnityEngine.Canvas","UnityEngine.EventSystems.UIBehaviour","UnityEngine.UI.CanvasScaler","UnityEngine.UI.GraphicRaycaster","UnityEngine.CanvasGroup","UnityEngine.CanvasRenderer","UnityEngine.UI.Image","UnityEngine.Sprite","UnityEngine.UI.Button","UnityEngine.MonoBehaviour","SoundUIElement","UnityEngine.AudioClip","PlayNowButtonAnim","UnityEngine.UI.Text","UnityEngine.Font","UiEndGame","UnityEngine.Transform","UnityEngine.EventSystems.EventSystem","UnityEngine.EventSystems.StandaloneInputModule","TutorialLayer","TMPro.TextMeshProUGUI","TMPro.TMP_FontAsset","UnityEngine.Material","UnityEngine.Shader","UnityEngine.Texture2D","CollectorGameManager","CollectorQueueManager","CollectorMoveLimiter","InputManager","GamePlaySound","GameplayManager","LevelGamePlayConfigSO","SoundManager","UnityEditor.Audio.AudioMixerController","UnityEngine.AudioSource","UnityEditor.Audio.AudioMixerGroupController","PadController","AlertFullSlotAnim","Grid3DLayout","UnityEngine.MeshFilter","UnityEngine.Mesh","UnityEngine.MeshRenderer","MeshCombiner","UnityEngine.ParticleSystem","UnityEngine.ParticleSystemRenderer","UnityEngine.SpriteRenderer","LevelConfigSetup","LevelCollectorsSystem","LevelConfig","UnityEngine.Camera","PaintingGridObject","InGameEffectOptions","ColorPalleteData","UnityEngine.GameObject","PaintingPixelComponent","LevelMechanicObjectPrefabs","UnityEngine.BoxCollider","PipeObject","PipePartVisualHandle","AutoTextureScale","KeyObject","IdleRotate","IdleMoveUpDown","UnityEngine.TrailRenderer","LockObject","CollectorController","CollectorAnimation","WallObject","TMPro.TextMeshPro","CullableObject","CachedTransformPathMover","ColorPixelsCollectorObject","CollectorVisualHandler","BulletDisplayHandler","GogoGaga.OptimizedRopesAndCables.Rope","GogoGaga.OptimizedRopesAndCables.RopeMesh","UnityEngine.Animator","EnableRandomRotate","UnityEditor.Animations.AnimatorController","UnityEngine.LineRenderer","CollectorProjectileController","PathTransformBasedCached","PaintingConfig","LevelColorCollectorsConfig","PaintingGridEffects","CollectorColumnController","CollectorProjectilePool","UnityEngine.AudioListener","UnityEngine.Light","UnityEngine.Cubemap","DG.Tweening.Core.DOTweenSettings","TMPro.TMP_Settings","TMPro.TMP_SpriteAsset","TMPro.TMP_StyleSheet","UnityEngine.TextAsset","UnityEditor.Audio.AudioMixerSnapshotController","UnityEngine.AudioLowPassFilter","UnityEngine.AudioBehaviour","UnityEngine.AudioHighPassFilter","UnityEngine.AudioReverbFilter","UnityEngine.AudioDistortionFilter","UnityEngine.AudioEchoFilter","UnityEngine.AudioChorusFilter","UnityEngine.Cloth","UnityEngine.SkinnedMeshRenderer","UnityEngine.FlareLayer","UnityEngine.ConstantForce","UnityEngine.Rigidbody","UnityEngine.Joint","UnityEngine.HingeJoint","UnityEngine.SpringJoint","UnityEngine.FixedJoint","UnityEngine.CharacterJoint","UnityEngine.ConfigurableJoint","UnityEngine.CompositeCollider2D","UnityEngine.Rigidbody2D","UnityEngine.Joint2D","UnityEngine.AnchoredJoint2D","UnityEngine.SpringJoint2D","UnityEngine.DistanceJoint2D","UnityEngine.FrictionJoint2D","UnityEngine.HingeJoint2D","UnityEngine.RelativeJoint2D","UnityEngine.SliderJoint2D","UnityEngine.TargetJoint2D","UnityEngine.FixedJoint2D","UnityEngine.WheelJoint2D","UnityEngine.ConstantForce2D","UnityEngine.StreamingController","UnityEngine.TextMesh","UnityEngine.Tilemaps.TilemapRenderer","UnityEngine.Tilemaps.Tilemap","UnityEngine.Tilemaps.TilemapCollider2D","Unity.VisualScripting.SceneVariables","Unity.VisualScripting.Variables","UnityEngine.UI.Dropdown","UnityEngine.UI.Graphic","UnityEngine.UI.AspectRatioFitter","UnityEngine.UI.ContentSizeFitter","UnityEngine.UI.GridLayoutGroup","UnityEngine.UI.HorizontalLayoutGroup","UnityEngine.UI.HorizontalOrVerticalLayoutGroup","UnityEngine.UI.LayoutElement","UnityEngine.UI.LayoutGroup","UnityEngine.UI.VerticalLayoutGroup","UnityEngine.UI.Mask","UnityEngine.UI.MaskableGraphic","UnityEngine.UI.RawImage","UnityEngine.UI.RectMask2D","UnityEngine.UI.Scrollbar","UnityEngine.UI.ScrollRect","UnityEngine.UI.Slider","UnityEngine.UI.Toggle","UnityEngine.EventSystems.BaseInputModule","UnityEngine.EventSystems.PointerInputModule","UnityEngine.EventSystems.TouchInputModule","UnityEngine.EventSystems.Physics2DRaycaster","UnityEngine.EventSystems.PhysicsRaycaster","Unity.VisualScripting.ScriptMachine","TMPro.TextContainer","TMPro.TMP_Dropdown","TMPro.TMP_SelectionCaret","TMPro.TMP_SubMesh","TMPro.TMP_SubMeshUI","TMPro.TMP_Text","Unity.VisualScripting.StateMachine"]

Deserializers.unityVersion = "2022.3.62f3";

Deserializers.productName = "Cat Blast Blocks";

Deserializers.lunaInitializationTime = "12/10/2025 01:10:38";

Deserializers.lunaDaysRunning = "8.1";

Deserializers.lunaVersion = "6.4.0";

Deserializers.lunaSHA = "6639120529aa36186c6141b5c3fb20246c28bff0";

Deserializers.creativeName = "Flow_Color_Android_Lv41_To_48_Clickbait";

Deserializers.lunaAppID = "35086";

Deserializers.projectId = "b1592133a3dd6b14fa12d21db0d63d5f";

Deserializers.packagesInfo = "com.unity.textmeshpro: 3.0.7\ncom.unity.timeline: 1.7.7\ncom.unity.ugui: 1.0.0";

Deserializers.externalJsLibraries = "";

Deserializers.androidLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.androidLink?window.$environment.packageConfig.androidLink:'Empty';

Deserializers.iosLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.iosLink?window.$environment.packageConfig.iosLink:'Empty';

Deserializers.base64Enabled = "False";

Deserializers.minifyEnabled = "True";

Deserializers.isForceUncompressed = "False";

Deserializers.isAntiAliasingEnabled = "False";

Deserializers.isRuntimeAnalysisEnabledForCode = "False";

Deserializers.runtimeAnalysisExcludedClassesCount = "1693";

Deserializers.runtimeAnalysisExcludedMethodsCount = "4889";

Deserializers.runtimeAnalysisExcludedModules = "physics2d";

Deserializers.isRuntimeAnalysisEnabledForShaders = "False";

Deserializers.isRealtimeShadowsEnabled = "False";

Deserializers.isReferenceAmbientProbeBaked = "False";

Deserializers.isLunaCompilerV2Used = "False";

Deserializers.companyName = "DefaultCompany";

Deserializers.buildPlatform = "StandaloneWindows64";

Deserializers.applicationIdentifier = "com.DefaultCompany.Cat-Blast-Blocks";

Deserializers.disableAntiAliasing = true;

Deserializers.graphicsConstraint = 28;

Deserializers.linearColorSpace = true;

Deserializers.buildID = "4611db42-1a00-431c-a535-093ac8d307d4";

Deserializers.runtimeInitializeOnLoadInfos = [[["Unity","MemoryProfiler","Editor","MemoryProfilerSettings","RuntimeInitialize"],["Unity","MemoryProfiler","Editor","EditorGUICompatibilityHelper","RuntimeInitialize"],["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[["Unity","VisualScripting","RuntimeVSUsageUtility","RuntimeInitializeOnLoadBeforeSceneLoad"]],[["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"]],[["Unity","MemoryProfiler","MetadataInjector","PlayerInitMetadata"]],[]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

