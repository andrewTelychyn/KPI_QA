using System.Threading;
using System.Threading.Tasks;

namespace LAB6.Helpers
{
    public static class DialogHelper
    {
        public static async Task WaitForDialog(CancellationToken cancellationToken)
        {
            int i = 0;
            do
            {
                await Task.Delay(100);
                i++;
            }
            while (!cancellationToken.IsCancellationRequested && i <= 300);
        }
    }
}
