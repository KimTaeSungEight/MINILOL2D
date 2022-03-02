using System;

namespace MiniLol.MiniInputSystem
{
    public interface IInputEventDisposable
    {
        IDisposable InputDisposable { get; }
        void InuputSubscribe();
        void InputDispose();
    }
}