namespace GodotTask.GlobalCancellation;

/// <summary>
/// Contains methods to access the global cancellation token used for cancelling <see cref="GDTask"/>s in bulk.
/// </summary>
public static class GDTaskGlobalCancellation {
    private static Tuple<CancellationTokenSource, CancellationToken> globalCancellationTuple = CreateCancellationTuple();

    /// <summary>
    /// Cancels and replaces the global cancellation token.
    /// <br/>
    /// This causes tasks using <see cref="GetToken()"/> or <see cref="GDTaskGlobalCancellationExtensions.AttachGlobalCancellation(GDTask)"/>
    /// to throw an <see cref="OperationCanceledException"/>.
    /// </summary>
    public static void Cancel() {
        Tuple<CancellationTokenSource, CancellationToken> oldGlobalCancellationTuple = Interlocked.Exchange(ref globalCancellationTuple, CreateCancellationTuple());
        oldGlobalCancellationTuple.Item1.Cancel();
        oldGlobalCancellationTuple.Item1.Dispose();
    }
    /// <summary>
    /// Returns the global cancellation token.
    /// </summary>
    public static CancellationToken GetToken() {
        return globalCancellationTuple.Item2;
    }

    private static Tuple<CancellationTokenSource, CancellationToken> CreateCancellationTuple() {
        CancellationTokenSource source = new();
        return Tuple.Create(source, source.Token);
    }
}