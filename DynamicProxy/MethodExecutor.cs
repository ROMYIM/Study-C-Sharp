using System.Reflection.Emit;

namespace DynamicProxy;

public struct MethodExecutor
{
    public AspectDelegate Aspects { get; }
    
    public AspectContext Context { get; }

    public MethodExecutor(AspectDelegate aspects, AspectContext context)
    {
        Aspects = aspects;
        Context = context;
        Context.GenerateInvokeMethod();
    }

    public object Execute(object[] args)
    {
        Context.Parameters = args;
        Aspects(Context);
        return Context.ReturnValue;
    }
}