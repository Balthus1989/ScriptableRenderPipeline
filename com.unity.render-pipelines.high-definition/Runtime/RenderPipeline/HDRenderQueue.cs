using System;

namespace UnityEngine.Rendering.HighDefinition
{
    // In HD we don't expose HDRenderQueue instead we create as much value as needed in the enum for our different pass
    // and use inspector to manipulate the value.
    // In the case of transparent we want to use RenderQueue to help with sorting. We define a neutral value for the RenderQueue and priority going from -X to +X
    // going from -X to +X instead of 0 to +X as builtin Unity is better for artists as they can decide late to sort behind or in front of the scene.

    internal static class HDRenderQueue
    {
        const int k_TransparentPriorityQueueRange = 100;

        public enum Priority
        {
            Background = UnityEngine.Rendering.RenderQueue.Background,


            Opaque = UnityEngine.Rendering.RenderQueue.Geometry,
            OpaqueAlphaTest = UnityEngine.Rendering.RenderQueue.AlphaTest,
            // Warning: we must not change Geometry last value to stay compatible with occlusion
            OpaqueLast = UnityEngine.Rendering.RenderQueue.GeometryLast,
            OpaqueRayTracing = UnityEngine.Rendering.RenderQueue.GeometryLast + 1,

            AfterPostprocessOpaque = UnityEngine.Rendering.RenderQueue.GeometryLast + 5,
            AfterPostprocessOpaqueAlphaTest = UnityEngine.Rendering.RenderQueue.GeometryLast + 10,

            // For transparent pass we define a range of 200 value to define the priority
            // Warning: Be sure no range are overlapping
            PreRefractionFirst = 2750 - k_TransparentPriorityQueueRange,
            PreRefraction = 2750,
            PreRefractionLast = 2750 + k_TransparentPriorityQueueRange,
            PreRefractionRayTracing = PreRefractionLast + 10,

            TransparentFirst = UnityEngine.Rendering.RenderQueue.Transparent - k_TransparentPriorityQueueRange,
            Transparent = UnityEngine.Rendering.RenderQueue.Transparent,
            TransparentLast = UnityEngine.Rendering.RenderQueue.Transparent + k_TransparentPriorityQueueRange,
            TransparentRayTracing = TransparentLast + 10,

            LowTransparentFirst = 3400 - k_TransparentPriorityQueueRange,
            LowTransparent = 3400,
            LowTransparentLast = 3400 + k_TransparentPriorityQueueRange,
            LowTransparentRayTracing = LowTransparentLast + 10,

            AfterPostprocessTransparentFirst = 3700 - k_TransparentPriorityQueueRange,
            AfterPostprocessTransparent = 3700,
            AfterPostprocessTransparentLast = 3700 + k_TransparentPriorityQueueRange,

            OldRaytracingTransparent = 3900,

            Overlay = UnityEngine.Rendering.RenderQueue.Overlay
        }

        public enum RenderQueueType
        {
            Background,

            // Opaque
            Opaque,
            AfterPostProcessOpaque,
            OpaqueRayTracing,

            // Transparent
            PreRefraction,
            PreRefractionRayTracing,

            Transparent,
            TransparentRayTracing,

            LowTransparent,
            LowTransparentRayTracing,

            AfterPostprocessTransparent,

            Overlay,

            Unknown
        }

        public static readonly RenderQueueRange k_RenderQueue_OpaqueNoAlphaTest = new RenderQueueRange { lowerBound = (int)Priority.Background, upperBound = (int)Priority.OpaqueAlphaTest - 1 };
        public static readonly RenderQueueRange k_RenderQueue_OpaqueAlphaTest = new RenderQueueRange { lowerBound = (int)Priority.OpaqueAlphaTest, upperBound = (int)Priority.OpaqueLast };
        public static readonly RenderQueueRange k_RenderQueue_OpaqueRayTracing = new RenderQueueRange { lowerBound = (int)Priority.OpaqueRayTracing, upperBound = (int)Priority.OpaqueRayTracing };
        public static readonly RenderQueueRange k_RenderQueue_AllOpaque = new RenderQueueRange { lowerBound = (int)Priority.Background, upperBound = (int)Priority.OpaqueRayTracing };

        public static readonly RenderQueueRange k_RenderQueue_AfterPostProcessOpaque = new RenderQueueRange { lowerBound = (int)Priority.AfterPostprocessOpaque, upperBound = (int)Priority.AfterPostprocessOpaqueAlphaTest };

        public static readonly RenderQueueRange k_RenderQueue_PreRefraction = new RenderQueueRange { lowerBound = (int)Priority.PreRefractionFirst, upperBound = (int)Priority.PreRefractionRayTracing };
        public static readonly RenderQueueRange k_RenderQueue_PreRefractionNoRayTracing = new RenderQueueRange { lowerBound = (int)Priority.PreRefractionFirst, upperBound = (int)Priority.PreRefractionLast };
        public static readonly RenderQueueRange k_RenderQueue_PreRefractionRayTracing = new RenderQueueRange { lowerBound = (int)Priority.PreRefractionRayTracing, upperBound = (int)Priority.PreRefractionRayTracing };

        public static readonly RenderQueueRange k_RenderQueue_Transparent = new RenderQueueRange { lowerBound = (int)Priority.TransparentFirst, upperBound = (int)Priority.TransparentRayTracing };
        public static readonly RenderQueueRange k_RenderQueue_TransparentNoRayTracing = new RenderQueueRange { lowerBound = (int)Priority.TransparentFirst, upperBound = (int)Priority.TransparentLast };
        public static readonly RenderQueueRange k_RenderQueue_TransparentRayTracing = new RenderQueueRange { lowerBound = (int)Priority.TransparentRayTracing, upperBound = (int)Priority.TransparentRayTracing };

        public static readonly RenderQueueRange k_RenderQueue_LowTransparent = new RenderQueueRange { lowerBound = (int)Priority.LowTransparentFirst, upperBound = (int)Priority.LowTransparentRayTracing };
        public static readonly RenderQueueRange k_RenderQueue_LowTransparentNoRayTracing = new RenderQueueRange { lowerBound = (int)Priority.LowTransparentFirst, upperBound = (int)Priority.LowTransparentLast };
        public static readonly RenderQueueRange k_RenderQueue_LowTransparentRayTracing = new RenderQueueRange { lowerBound = (int)Priority.LowTransparentRayTracing, upperBound = (int)Priority.LowTransparentRayTracing };

        public static readonly RenderQueueRange k_RenderQueue_AllTransparent = new RenderQueueRange { lowerBound = (int)Priority.PreRefractionFirst, upperBound = (int)Priority.TransparentRayTracing };
        public static readonly RenderQueueRange k_RenderQueue_AllTransparentWithLowRes = new RenderQueueRange { lowerBound = (int)Priority.PreRefractionFirst, upperBound = (int)Priority.LowTransparentRayTracing };

        public static readonly RenderQueueRange k_RenderQueue_AfterPostProcessTransparent = new RenderQueueRange { lowerBound = (int)Priority.AfterPostprocessTransparentFirst, upperBound = (int)Priority.AfterPostprocessTransparentLast };

        public static readonly RenderQueueRange k_RenderQueue_All = new RenderQueueRange { lowerBound = 0, upperBound = 5000 };

        public static bool Contains(this RenderQueueRange range, int value) => range.lowerBound <= value && value <= range.upperBound;

        public static int Clamps(this RenderQueueRange range, int value) => Math.Max(range.lowerBound, Math.Min(value, range.upperBound));

        public static int ClampsTransparentRangePriority(int value) => Math.Max(-k_TransparentPriorityQueueRange, Math.Min(value, k_TransparentPriorityQueueRange));

        public static RenderQueueType GetTypeByRenderQueueValue(int renderQueue)
        {
            if (renderQueue == (int)Priority.Background)
                return RenderQueueType.Background;

            // Opaque queues
            if (k_RenderQueue_AllOpaque.Contains(renderQueue))
                return RenderQueueType.Opaque;
            if (renderQueue == (int)Priority.OpaqueRayTracing)
                return RenderQueueType.OpaqueRayTracing;

            // After post-process queues
            if (k_RenderQueue_AfterPostProcessOpaque.Contains(renderQueue))
                return RenderQueueType.AfterPostProcessOpaque;

            // Pre-refraction queue
            if (k_RenderQueue_PreRefractionNoRayTracing.Contains(renderQueue))
                return RenderQueueType.PreRefraction;
            if (renderQueue == (int)Priority.PreRefractionRayTracing)
                return RenderQueueType.PreRefractionRayTracing;

            // Transparent queues
            if (k_RenderQueue_TransparentNoRayTracing.Contains(renderQueue))
                return RenderQueueType.Transparent;
            if (renderQueue == (int)Priority.TransparentRayTracing || renderQueue == (int) Priority.OldRaytracingTransparent)
                return RenderQueueType.TransparentRayTracing;

            // Low transparent queques
            if (k_RenderQueue_LowTransparentNoRayTracing.Contains(renderQueue))
                return RenderQueueType.LowTransparent;
            if (renderQueue == (int)Priority.LowTransparentRayTracing)
                return RenderQueueType.LowTransparentRayTracing;

            // After post-process transparent
            if (k_RenderQueue_AfterPostProcessTransparent.Contains(renderQueue))
                return RenderQueueType.AfterPostprocessTransparent;

            if (renderQueue == (int)Priority.Overlay)
                return RenderQueueType.Overlay;

            return RenderQueueType.Unknown;
        }

        public static int ChangeType(RenderQueueType targetType, int offset = 0, bool alphaTest = false, bool rayTraced = false)
        {
            switch (targetType)
            {
                case RenderQueueType.Background:
                    return (int)Priority.Background;

                case RenderQueueType.Opaque:
                case RenderQueueType.OpaqueRayTracing:
                    return rayTraced ? (int)Priority.OpaqueRayTracing : (alphaTest ? (int)Priority.OpaqueAlphaTest : (int)Priority.Opaque);

                case RenderQueueType.AfterPostProcessOpaque:
                    return alphaTest ? (int)Priority.AfterPostprocessOpaqueAlphaTest : (int)Priority.AfterPostprocessOpaque;

                case RenderQueueType.PreRefraction:
                case RenderQueueType.PreRefractionRayTracing:
                    return rayTraced ? (int)Priority.PreRefractionRayTracing : (int)Priority.PreRefraction + offset;

                case RenderQueueType.Transparent:
                case RenderQueueType.TransparentRayTracing:
                    return rayTraced ? (int)Priority.TransparentRayTracing : (int)Priority.Transparent + offset;

                case RenderQueueType.LowTransparent:
                case RenderQueueType.LowTransparentRayTracing:
                    return rayTraced ? (int)Priority.LowTransparentRayTracing : (int)Priority.LowTransparent + offset;

                case RenderQueueType.AfterPostprocessTransparent:
                    return (int)Priority.AfterPostprocessTransparent + offset;

                case RenderQueueType.Overlay:
                    return (int)Priority.Overlay;
                default:
                    throw new ArgumentException("Unknown RenderQueueType, was " + targetType);
            }
        }

        public static RenderQueueType GetTransparentEquivalent(RenderQueueType type)
        {
            switch(type)
            {
                case RenderQueueType.Opaque:
                    return RenderQueueType.Transparent;
                case RenderQueueType.OpaqueRayTracing:
                    return RenderQueueType.TransparentRayTracing;

                case RenderQueueType.AfterPostProcessOpaque:
                    return RenderQueueType.AfterPostprocessTransparent;

                case RenderQueueType.LowTransparent:
                    return RenderQueueType.LowTransparent;

                case RenderQueueType.PreRefractionRayTracing:
                    return RenderQueueType.PreRefractionRayTracing;

                case RenderQueueType.LowTransparentRayTracing:
                    return RenderQueueType.LowTransparentRayTracing;

                case RenderQueueType.TransparentRayTracing:
                    return RenderQueueType.TransparentRayTracing;

                default:
                    //keep transparent mapped to transparent
                    return type;
                case RenderQueueType.Overlay:
                case RenderQueueType.Background:
                    throw new ArgumentException("Unknow RenderQueueType conversion to transparent equivalent, was " + type);
            }
        }

        public static RenderQueueType GetOpaqueEquivalent(RenderQueueType type)
        {
            switch (type)
            {
                case RenderQueueType.PreRefraction:
                case RenderQueueType.Transparent:
                case RenderQueueType.LowTransparent:
                    return RenderQueueType.Opaque;

                case RenderQueueType.PreRefractionRayTracing:
                case RenderQueueType.TransparentRayTracing:
                case RenderQueueType.LowTransparentRayTracing:
                    return RenderQueueType.OpaqueRayTracing;

                case RenderQueueType.AfterPostprocessTransparent:
                    return RenderQueueType.AfterPostProcessOpaque;

                default:
                    //keep opaque mapped to opaque
                    return type;
                case RenderQueueType.Overlay:
                case RenderQueueType.Background:
                    throw new ArgumentException("Unknow RenderQueueType conversion to opaque equivalent, was " + type);
            }
        }


        //utility: split opaque/transparent queue
        public enum OpaqueRenderQueue
        {
            Default,
            AfterPostProcessing
        }

        public enum TransparentRenderQueue
        {
            BeforeRefraction,
            Default,
            LowResolution,
            AfterPostProcessing
        }

        public static OpaqueRenderQueue ConvertToOpaqueRenderQueue(RenderQueueType renderQueue)
        {
            switch (renderQueue)
            {
                case RenderQueueType.Opaque:
                case RenderQueueType.OpaqueRayTracing:
                    return OpaqueRenderQueue.Default;
                case RenderQueueType.AfterPostProcessOpaque:
                    return OpaqueRenderQueue.AfterPostProcessing;
                default:
                    throw new ArgumentException("Cannot map to OpaqueRenderQueue, was " + renderQueue);
            }
        }

        public static RenderQueueType ConvertFromOpaqueRenderQueue(OpaqueRenderQueue opaqueRenderQueue, bool rayTraced = false)
        {
            switch (opaqueRenderQueue)
            {
                case OpaqueRenderQueue.Default:
                    return rayTraced ? RenderQueueType.OpaqueRayTracing : RenderQueueType.Opaque;
                case OpaqueRenderQueue.AfterPostProcessing:
                    return RenderQueueType.AfterPostProcessOpaque;
                default:
                    throw new ArgumentException("Unknown OpaqueRenderQueue, was " + opaqueRenderQueue);
            }
        }

        public static TransparentRenderQueue ConvertToTransparentRenderQueue(RenderQueueType renderQueue)
        {
            switch (renderQueue)
            {
                case RenderQueueType.PreRefraction:
                case RenderQueueType.PreRefractionRayTracing:
                    return TransparentRenderQueue.BeforeRefraction;
                case RenderQueueType.Transparent:
                case RenderQueueType.TransparentRayTracing:
                    return TransparentRenderQueue.Default;
                case RenderQueueType.LowTransparent:
                case RenderQueueType.LowTransparentRayTracing:
                    return TransparentRenderQueue.LowResolution;
                case RenderQueueType.AfterPostprocessTransparent:
                    return TransparentRenderQueue.AfterPostProcessing;
                default:
                    throw new ArgumentException("Cannot map to TransparentRenderQueue, was " + renderQueue);
            }
        }

        public static RenderQueueType ConvertFromTransparentRenderQueue(TransparentRenderQueue transparentRenderqueue, bool rayTraced = false)
        {
            switch (transparentRenderqueue)
            {
                case TransparentRenderQueue.BeforeRefraction:
                    return rayTraced ? RenderQueueType.PreRefractionRayTracing : RenderQueueType.PreRefraction;
                case TransparentRenderQueue.Default:
                    return rayTraced ? RenderQueueType.TransparentRayTracing : RenderQueueType.Transparent;
                case TransparentRenderQueue.LowResolution:
                    return rayTraced ? RenderQueueType.LowTransparentRayTracing : RenderQueueType.LowTransparent;
                case TransparentRenderQueue.AfterPostProcessing:
                    return RenderQueueType.AfterPostprocessTransparent;
                default:
                    throw new ArgumentException("Unknown TransparentRenderQueue, was " + transparentRenderqueue);
            }
        }

        public static string GetShaderTagValue(int index)
        {
            // Special case for transparent (as we have transparent range from PreRefractionFirst to AfterPostprocessTransparentLast
            // that start before RenderQueue.Transparent value
            if (HDRenderQueue.k_RenderQueue_AllTransparent.Contains(index)
                || HDRenderQueue.k_RenderQueue_AfterPostProcessTransparent.Contains(index)
                || HDRenderQueue.k_RenderQueue_LowTransparent.Contains(index))
            {
                int v = (index - (int)RenderQueue.Transparent);
                return "Transparent" + ((v < 0) ? "" : "+") + v;
            }
            else if (index >= (int)RenderQueue.Overlay)
                return "Overlay+" + (index - (int)RenderQueue.Overlay);
            else if (index >= (int)RenderQueue.AlphaTest)
                return "AlphaTest+" + (index - (int)RenderQueue.AlphaTest);
            else if (index >= (int)RenderQueue.Geometry)
                return "Geometry+" + (index - (int)RenderQueue.Geometry);
            else
            {
                int v = (index - (int)RenderQueue.Background);
                return "Background" + ((v < 0) ? "" : "+") + v;
            }
        }
    }
}
