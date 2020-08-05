using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace Panoukos41.Helpers
{
    /// <summary>
    /// Extension methods to use UWP's <see cref="Storyboard"/>.
    /// </summary>
    public static class AnimationExtensions
    {
        /// <summary>
        /// A way to await for a storyboard to finish before continuing execution.
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static async Task PlayAsync(this Storyboard storyboard)
        {
            var tcs = new TaskCompletionSource<object>();
            void lambda(object s, object e) => tcs.TrySetResult(null);

            try
            {
                storyboard.Completed += lambda;
                storyboard.Begin();
                await tcs.Task;
            }
            finally
            {
                storyboard.Completed -= lambda;
            }
        }
    }
}