using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IcatuzinhoApp
{
    public class LabelScaleTriggerAction : TriggerAction<Label>
    {
        protected override void Invoke(Label label)
        {
            AnimationLoop(label);
        }

        async void AnimationLoop(Label label)
        {
            await Task.WhenAll(label.FadeTo(0, 1000));
            await Task.Delay(5);
            await Task.WhenAll(label.FadeTo(1, 1000));
        }
    }
}

