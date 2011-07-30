namespace DynamicConfigurationBuilder.MethodStrategies
{
    public interface IMethodStrategy
    {
        bool IsMatch(string methodName);
        void Execute(string methodName, dynamic config, params object[] args);
    }
}