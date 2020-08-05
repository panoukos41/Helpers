using Android.Content;
using Android.OS;
using Android.Views;
using AndroidX.Fragment.App;
using System;

namespace Panoukos41.Helpers.AndroidX.Fragments
{
    /// <summary>
    /// A fragment that exposes lifecycle events as actions
    /// and allows you to set them via code on the fly.
    /// </summary>
    public class FluentFragment : Fragment
    {
        #region Start Actions

        /// <summary>
        /// The provided OnAttach override
        /// </summary>
        public Action<Context> onAttach;

        /// <summary>
        /// The provided OnCreate override
        /// </summary>
        public Action onCreate;

        /// <summary>
        /// The provided OnCreateView override
        /// </summary>
        public Func<LayoutInflater, ViewGroup, Bundle, View> onCreateView;

        /// <summary>
        /// The provided OnActivityCreated override
        /// </summary>
        public Action<Bundle> onActivityCreated;

        /// <summary>
        /// The provided OnStart override
        /// </summary>
        public Action onStart;

        /// <summary>
        /// The provided OnResume override
        /// </summary>
        public Action onResume;

        #endregion

        #region Stop Actions

        /// <summary>
        /// The provided OnPause override
        /// </summary>
        public Action onPause;

        /// <summary>
        /// The provided OnStop override
        /// </summary>
        public Action onStop;

        /// <summary>
        /// The provided OnDestroyView override
        /// </summary>
        public Action onDestroyView;

        /// <summary>
        /// The provided OnDestroy override
        /// </summary>
        public Action onDestroy;

        /// <summary>
        /// The provided OnDetach override
        /// </summary>
        public Action onDetach;

        #endregion

        #region Sets Start Functions

        /// <summary>
        /// Method to se the OnAttach override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnAttach(Action<Context> action)
        {
            onAttach = action;
            return this;
        }

        /// <summary>
        /// Method to se the OnCreate override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnCreate(Action action)
        {
            onCreate = action;
            return this;
        }

        /// <summary>
        /// Method to se the OnCreateView override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnCreateView(Func<LayoutInflater, ViewGroup, Bundle, View> action)
        {
            onCreateView = action;
            return this;
        }

        /// <summary>
        /// Method to se the OnActivityCreated override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnActivityCreated(Action<Bundle> action)
        {
            onActivityCreated = action;
            return this;
        }

        /// <summary>
        /// Method to se the OnStart override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnStart(Action action)
        {
            onStart = action;
            return this;
        }

        /// <summary>
        /// Method to se the OnResume override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnResume(Action action)
        {
            onResume = action;
            return this;
        }

        #endregion

        #region Sets Stop Functions

        /// <summary>
        /// Method to se the OnPause override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnPause(Action action)
        {
            onPause = action;
            return this;
        }

        /// <summary>
        /// Method to se the OnStop override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnStop(Action action)
        {
            onStop = action;
            return this;
        }

        /// <summary>
        /// Method to se the OnDestroyView override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnDestroyView(Action action)
        {
            onDestroyView = action;
            return this;
        }

        /// <summary>
        /// Method to se the OnDestroy override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnDestroy(Action action)
        {
            onDestroy = action;
            return this;
        }

        /// <summary>
        /// Method to se the OnDetach override
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public FluentFragment SetOnDetach(Action action)
        {
            onDetach = action;
            return this;
        }

        #endregion

        #region Start Function Overides

        /// <summary>
        /// OnAttach override, base is called first then <see cref="onAttach"/> is invoked
        /// </summary>
        /// <param name="context"></param>
        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            onAttach?.Invoke(context);
        }

        /// <summary>
        /// OnCreate override, base is called first then <see cref="onCreate"/> is invoked
        /// </summary>
        /// <param name="savedInstanceState"></param>
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            onCreate?.Invoke();
        }

        /// <summary>
        /// OnCreateView override, base is called first then <see cref="onCreateView"/> is invoked
        /// </summary>
        /// <param name="inflater"></param>
        /// <param name="container"></param>
        /// <param name="savedInstanceState"></param>
        /// <returns></returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return onCreateView(inflater, container, savedInstanceState);
        }

        /// <summary>
        /// OnActivityCreated override, base is called first then <see cref="onActivityCreated"/> is invoked
        /// </summary>
        /// <param name="savedInstanceState"></param>
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            onActivityCreated?.Invoke(savedInstanceState);
        }

        /// <summary>
        /// OnStart override, base is called first then <see cref="onStart"/> is invoked
        /// </summary>
        public override void OnStart()
        {
            base.OnStart();
            onStart?.Invoke();
        }

        /// <summary>
        /// OnResume override, base is called first then <see cref="onResume"/> is invoked
        /// </summary>
        public override void OnResume()
        {
            base.OnResume();
            onResume?.Invoke();
        }

        #endregion

        #region Stop Function Overides

        /// <summary>
        /// OnPause override, base is called first then <see cref="onPause"/> is invoked
        /// </summary>
        public override void OnPause()
        {
            base.OnPause();
            onPause?.Invoke();
        }

        /// <summary>
        /// OnStop override, base is called first then <see cref="onStop"/> is invoked
        /// </summary>
        public override void OnStop()
        {
            base.OnStop();
            onStop?.Invoke();
        }

        /// <summary>
        /// OnDestroyView override, base is called first then <see cref="onDestroyView"/> is invoked
        /// </summary>
        public override void OnDestroyView()
        {
            base.OnDestroyView();
            onDestroyView?.Invoke();
        }

        /// <summary>
        /// OnDestroy override, base is called first then <see cref="onDestroy"/> is invoked
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();
            onDestroy?.Invoke();
        }

        /// <summary>
        /// OnDetach override, base is called first then <see cref="onDetach"/> is invoked
        /// </summary>
        public override void OnDetach()
        {
            base.OnDetach();
            onDetach?.Invoke();
        }

        #endregion
    }
}