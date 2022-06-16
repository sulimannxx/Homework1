public class TargetDieTransition : Transition
{
    private void Update()
    {
        if (Target == null) NeedToTransit = true;
    }
}