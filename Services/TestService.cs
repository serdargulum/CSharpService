namespace CSharpService.Services;

public class TestService : ITestService
{
    public long HeavyCalculation(int count)
    {
        long sum = 0;
        for (int i = 1; i <= count; i++)
        {
            sum += i;
            sum -= (i / 2);
        }
        return sum;
    }
}