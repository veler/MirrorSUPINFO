using System;
using GalaSoft.MvvmLight;
using MirrorSUPINFO.SDK.Tools;

namespace MirrorSUPINFO.SDK
{
    public abstract class ModulePageViewModelBase : ViewModelBase, IDisposable
    {
        #region Properties

        public object[] Arguments { get; set; }

        public ITools Tools { get; set; }

        #endregion

        #region Constructors

        public ModulePageViewModelBase(ITools tools, object[] arguments)
        {
            Tools = tools;
            Arguments = arguments;
        }

        #endregion

        #region Methods

        public abstract void VoiceCommandDetectedTask(string commandName, string text, string[] textSplitted);

        public abstract void Dispose();

        #endregion
    }
}
