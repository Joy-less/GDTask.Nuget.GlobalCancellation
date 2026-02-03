namespace GodotTask.GlobalCancellation;

/// <summary>
/// Extension methods for using the global cancellation token.
/// </summary>
public static class GDTaskGlobalCancellationExtensions {
    /// <summary>
    /// Attaches the global <see cref="CancellationToken"/> to the given <see cref="GDTask"/>.
    /// </summary>
    public static GDTask AttachGlobalCancellation(this GDTask task) {
        return task.AttachExternalCancellation(GDTaskGlobalCancellationManager.GetToken());
    }

    /// <summary>
    /// Attaches the global <see cref="CancellationToken"/> to the given <see cref="GDTask{T}"/>.
    /// </summary>
    public static GDTask<T> AttachGlobalCancellation<T>(this GDTask<T> task) {
        return task.AttachExternalCancellation(GDTaskGlobalCancellationManager.GetToken());
    }

    /// <summary>
    /// Attaches the global <see cref="CancellationToken"/> to the given <see cref="GDTask.DeferredAwaitable"/>.
    /// </summary>
    public static GDTask AttachGlobalCancellation(this GDTask.DeferredAwaitable deferredAwaitable) {
        return deferredAwaitable.ToGDTask().AttachGlobalCancellation();
    }

    /// <summary>
    /// Attaches the global <see cref="CancellationToken"/> to the given <see cref="YieldAwaitable"/>.
    /// </summary>
    public static GDTask AttachGlobalCancellation(this YieldAwaitable yieldAwaitable) {
        return yieldAwaitable.ToGDTask().AttachGlobalCancellation();
    }

    /// <summary>
    /// Attaches the global <see cref="CancellationToken"/> to the given <see cref="SwitchToMainThreadAwaitable"/>.
    /// </summary>
    public static GDTask AttachGlobalCancellation(this SwitchToMainThreadAwaitable switchToMainThreadAwaitable) {
        static async GDTask RunAwaitableAsync(SwitchToMainThreadAwaitable switchToMainThreadAwaitable) {
            await switchToMainThreadAwaitable;
        }
        return RunAwaitableAsync(switchToMainThreadAwaitable).AttachGlobalCancellation();
    }

    /// <summary>
    /// Attaches the global <see cref="CancellationToken"/> to the given <see cref="SwitchToThreadPoolAwaitable"/>.
    /// </summary>
    public static GDTask AttachGlobalCancellation(this SwitchToThreadPoolAwaitable switchToThreadPoolAwaitable) {
        static async GDTask RunAwaitableAsync(SwitchToThreadPoolAwaitable switchToThreadPoolAwaitable) {
            await switchToThreadPoolAwaitable;
        }
        return RunAwaitableAsync(switchToThreadPoolAwaitable).AttachGlobalCancellation();
    }

    /// <summary>
    /// Attaches the global <see cref="CancellationToken"/> to the given <see cref="SwitchToSynchronizationContextAwaitable"/>.
    /// </summary>
    public static GDTask AttachGlobalCancellation(this SwitchToSynchronizationContextAwaitable switchToSynchronizationContextAwaitable) {
        static async GDTask RunAwaitableAsync(SwitchToSynchronizationContextAwaitable switchToSynchronizationContextAwaitable) {
            await switchToSynchronizationContextAwaitable;
        }
        return RunAwaitableAsync(switchToSynchronizationContextAwaitable).AttachGlobalCancellation();
    }

    /*/// <summary>
    /// Attaches the global <see cref="CancellationToken"/> to the given <see cref="IGDTaskAsyncEnumerable{T}"/>.
    /// </summary>
    public static IGDTaskAsyncEnumerable<T> AttachGlobalCancellation<T>(this IGDTaskAsyncEnumerable<T> enumerable)
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
    {
        return new AttachGlobalCancellationEnumerable<T>(enumerable);
    }

    /// <summary>
    /// Attaches the global <see cref="CancellationToken"/> to the given <see cref="IGDTaskAsyncEnumerator{T}"/>.
    /// </summary>
    public static IGDTaskAsyncEnumerator<T> AttachGlobalCancellation<T>(this IGDTaskAsyncEnumerator<T> enumerator)
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
    {
        return new AttachGlobalCancellationEnumerable<T>.Enumerator(enumerator, GlobalCancellationManager.GetToken());
    }

    internal sealed class AttachGlobalCancellationEnumerable<T> : IGDTaskAsyncEnumerable<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
    {
        private readonly IGDTaskAsyncEnumerable<T> source;
        private readonly CancellationToken globalCancellationToken;

        public AttachGlobalCancellationEnumerable(IGDTaskAsyncEnumerable<T> source) {
            this.source = source;
            globalCancellationToken = GlobalCancellationManager.GetToken();
        }

        public IGDTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) {
            return new Enumerator(source.GetAsyncEnumerator(cancellationToken), globalCancellationToken);
        }

        public sealed class Enumerator : IGDTaskAsyncEnumerator<T> {
            private readonly IGDTaskAsyncEnumerator<T> source;
            private readonly CancellationToken globalCancellationToken;

            public Enumerator(IGDTaskAsyncEnumerator<T> source, CancellationToken globalCancellationToken) {
                this.source = source;
                this.globalCancellationToken = globalCancellationToken;
            }

            public T Current => source.Current;

            public GDTask<bool> MoveNextAsync() {
                globalCancellationToken.ThrowIfCancellationRequested();

                return source.MoveNextAsync();
            }

            public GDTask DisposeAsync() {
                return source.DisposeAsync();
            }
        }
    }*/
}