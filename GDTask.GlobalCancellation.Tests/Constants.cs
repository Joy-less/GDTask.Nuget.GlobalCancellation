namespace GodotTask.GlobalCancellation.Tests;

internal static class Constants {
    internal static async GDTask WaitForTaskReadyAsync() {
        await GDTask.SwitchToMainThread();
        await GDTask.Yield();
    }
}