using System;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
    /// <summary>Engine lighting property.</summary>
    [Flags]
    public enum LightingProperty
    {
        None = 0,
        /// <summary>Render only diffuse.</summary>
        DiffuseOnly = 1 << 0,
        /// <summary>Render only specular.</summary>
        SpecularOnly = 1 << 1,
    }

    /// <summary>Output a specific debug mode.</summary>
    public enum DebugFullScreen
    {
        None,
        Depth,
        ScreenSpaceAmbientOcclusion,
        MotionVectors
    }

    /// <summary>Use this request to define how to render an AOV.</summary>
    public unsafe struct AOVRequest
    {
        internal static AOVRequest NewDefault() => new AOVRequest
        {
            m_MaterialProperty = MaterialSharedProperty.None,
            m_LightingProperty = LightingProperty.None,
            m_DebugFullScreen = DebugFullScreen.None,
            m_LightFilterProperty = DebugLightFilterMode.None
        };

        MaterialSharedProperty m_MaterialProperty;
        LightingProperty m_LightingProperty;
        DebugLightFilterMode m_LightFilterProperty;
        DebugFullScreen m_DebugFullScreen;

        AOVRequest* thisPtr
        {
            get
            {
                fixed (AOVRequest* pThis = &this)
                    return pThis;
            }
        }

        /// <summary>Create a new instance by copying values from <paramref name="other"/>.</summary>
        /// <param name="other"></param>
        public AOVRequest(AOVRequest other)
        {
            m_MaterialProperty = other.m_MaterialProperty;
            m_LightingProperty = other.m_LightingProperty;
            m_DebugFullScreen = other.m_DebugFullScreen;
            m_LightFilterProperty = other.m_LightFilterProperty;
        }

        /// <summary>State the property to render. In case of several SetFullscreenOutput chained call, only last will be used.</summary>
        /// <param name="materialProperty">The property to render.</param>
        /// <returns>A ref return to chain calls.</returns>
        public ref AOVRequest SetFullscreenOutput(MaterialSharedProperty materialProperty)
        {
            m_MaterialProperty = materialProperty;
            return ref *thisPtr;
        }

        /// <summary>State the property to render. In case of several SetFullscreenOutput chained call, only last will be used.</summary>
        /// <param name="lightingProperty">The property to render.</param>
        /// <returns>A ref return to chain calls.</returns>
        public ref AOVRequest SetFullscreenOutput(LightingProperty lightingProperty)
        {
            m_LightingProperty = lightingProperty;
            return ref *thisPtr;
        }

        /// <summary>State the property to render. In case of several SetFullscreenOutput chained call, only last will be used.</summary>
        /// <param name="debugFullScreen">The property to render.</param>
        /// <returns>A ref return to chain calls.</returns>
        public ref AOVRequest SetFullscreenOutput(DebugFullScreen debugFullScreen)
        {
            m_DebugFullScreen = debugFullScreen;
            return ref *thisPtr;
        }

        /// <summary>Set the light filter to use.</summary>
        /// <param name="filter">The light filter to use</param>
        /// <returns>A ref return to chain calls.</returns>
        public ref AOVRequest SetLightFilter(DebugLightFilterMode filter)
        {
            m_LightFilterProperty = filter;
            return ref *thisPtr;
        }

        /// <summary>
        /// Populate the debug display settings with the AOV data.
        /// </summary>
        /// <param name="debug">The debug display settings to fill.</param>
        public void FillDebugData(DebugDisplaySettings debug)
        {
            debug.SetDebugViewCommonMaterialProperty(m_MaterialProperty);

            switch (m_LightingProperty)
            {
                case LightingProperty.DiffuseOnly:
                    debug.SetDebugLightingMode(DebugLightingMode.DiffuseLighting);
                    break;
                case LightingProperty.SpecularOnly:
                    debug.SetDebugLightingMode(DebugLightingMode.SpecularLighting);
                    break;
                default:
                {
                    debug.SetDebugLightingMode(DebugLightingMode.None);
                    break;
                }
            }

            debug.SetDebugLightFilterMode(m_LightFilterProperty);

            switch (m_DebugFullScreen)
            {
                case DebugFullScreen.None:
                    debug.SetFullScreenDebugMode(FullScreenDebugMode.None);
                    break;
                case DebugFullScreen.Depth:
                    debug.SetFullScreenDebugMode(FullScreenDebugMode.DepthPyramid);
                    break;
                case DebugFullScreen.ScreenSpaceAmbientOcclusion:
                    debug.SetFullScreenDebugMode(FullScreenDebugMode.SSAO);
                    break;
                case DebugFullScreen.MotionVectors:
                    debug.SetFullScreenDebugMode(FullScreenDebugMode.MotionVectors);
                    break;
                default:
                    throw new ArgumentException("Unknown DebugFullScreen");
            }
        }
    }
}

