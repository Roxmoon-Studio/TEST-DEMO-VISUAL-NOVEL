using System.Threading;

namespace Utilities
{
    public static class CancellationTokenSourceUtility
    {
        public static void Recreate(ref CancellationTokenSource cts)
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
            }

            cts = new();
        }
    }
}
