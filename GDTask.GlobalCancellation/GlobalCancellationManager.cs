namespace GodotTask.GlobalCancellation;

public static class GlobalCancellationManager {
    private static Tuple<CancellationTokenSource, CancellationToken> globalCancellationTuple = CreateCancellationTuple();

    public static void CancelGlobalCancellationTokenSource() {
        Tuple<CancellationTokenSource, CancellationToken> oldGlobalCancellationTuple = Interlocked.Exchange(ref globalCancellationTuple, CreateCancellationTuple());
        oldGlobalCancellationTuple.Item1.Cancel();
        oldGlobalCancellationTuple.Item1.Dispose();
    }
    public static CancellationToken GetGlobalCancellationToken() {
        return globalCancellationTuple.Item2;
    }

    private static Tuple<CancellationTokenSource, CancellationToken> CreateCancellationTuple() {
        CancellationTokenSource source = new();
        return Tuple.Create(source, source.Token);
    }
}