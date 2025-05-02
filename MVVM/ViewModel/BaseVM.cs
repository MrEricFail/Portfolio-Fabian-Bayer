using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BaseVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = null;
    protected void InvokeUpdate<T>(ref T source, T value, [CallerMemberName] string propertyName = null)
    {
        if (source.Equals(value)) return;
        source = value;
        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected void InvokeUpdate<T>(Func<T> get, Action<T> set, T value, [CallerMemberName] string propertyName = null)
    {
        if (get.Invoke().Equals(value)) return;
        set.Invoke(value);
        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}