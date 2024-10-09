using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class CRTEffect : VolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter intensity = new ClampedFloatParameter(0, 0, 1);
    public ClampedFloatParameter blurOffset = new ClampedFloatParameter(0, 0, 0.002f);
    public ClampedFloatParameter scanLineSpeed = new ClampedFloatParameter(0, -0.01f, 0.01f);
    public ClampedIntParameter scanLineCount = new ClampedIntParameter(1, 1, 300);
    public ColorParameter color = new ColorParameter(Color.white, hdr: true, false, true, false);
    public bool IsActive() => intensity.value > 0f;

    public bool IsTileCompatible() => true;
}
