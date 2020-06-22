using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Beam.Animation
{
    public class AnimationService{
        private IJSRuntime _jsRuntime;

        public static event Action BeamPassTriggered;
        public AnimationService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public ValueTask LoadAnimation(string elementId, int width, int height)
        {
            return _jsRuntime.InvokeVoidAsync("animatedBeam.loadAnimation", elementId, width, height);
        }

        [JSInvokable]
        public static Task BeamPassedBy()
        {
            return Task.Run(() => BeamPassTriggered?.Invoke());

        }
    }
}