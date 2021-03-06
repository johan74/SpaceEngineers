using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using VRageMath;

namespace VRageRender
{
    class MyDepthResolve : MyScreenPass
    {
        static PixelShaderId m_ps;

        internal static void Init()
        {
            MyRender11.RegisterSettingsChangedListener(new OnSettingsChangedDelegate(RecreateShadersForSettings));
        }

        internal static void RecreateShadersForSettings()
        {
            m_ps = MyShaders.CreatePs("custom_resolve.hlsl", "resolve_depth");
        }

        internal static void Run(MyBindableResource dst, MyBindableResource src)
        {
            RC.SetPS(m_ps);
            RC.BindDepthRT(dst, DepthStencilAccess.ReadWrite, null);
            RC.BindSRV(0, src);
            DrawFullscreenQuad();
        }
    }
}
